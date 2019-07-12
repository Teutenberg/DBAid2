<#
    PsDBAid2.psm1 is a helper module for scripted activities. This module will contain functions to help simplify scripting and increase productivity. 
#>
Import-Module SqlServer
[Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

<#
    .SYNOPSIS
        Connect to a SQL Server Database Engine and return the server object.

    .PARAMETER ServerName
        String containing the host name of the SQL Server to connect to.

    .PARAMETER InstanceName
        String containing the SQL Server Database Engine instance to connect to.

    .PARAMETER Credential
        PSCredential object with the credentials to use to impersonate a user when connecting.
        If this is not provided then the current user will be used to connect to the SQL Server Database Engine instance.

    .PARAMETER LoginType
        Specifies which type of logon credential should be used. The valid types
        are Integrated, WindowsUser, or SqlLogin. If WindowsUser or SqlLogin are
        specified then the parameter SetupCredential needs to be specified as well.
        If set to 'Integrated' then the credentials that the resource current are
        run with will be used.
        If set to 'WindowsUser' then the it will impersonate using the Windows
        login specified in the parameter SetupCredential.
        If set to 'WindowsUser' then the it will impersonate using the native SQL
        login specified in the parameter SetupCredential.
        Default value is 'Integrated'.

    .PARAMETER StatementTimeout
        Set the query StatementTimeout in seconds. Default 600 seconds (10mins).
#>
function Connect-SqlServer
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ServerName = $env:COMPUTERNAME,

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $InstanceName = 'MSSQLSERVER',

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter()]
        [ValidateSet('Integrated', 'WindowsUser', 'SqlLogin')]
        [System.String]
        $LoginType = 'Integrated',

        [Parameter()]
        [ValidateNotNull()]
        [System.Int32]
        $StatementTimeout = 600
    )

    if ($InstanceName -eq 'MSSQLSERVER')
    {
        $databaseEngineInstance = $ServerName
    }
    else
    {
        $databaseEngineInstance = '{0}\{1}' -f $ServerName, $InstanceName
    }

    $sqlServerObject  = New-Object -TypeName 'Microsoft.SqlServer.Management.Smo.Server'
    $sqlConnectionContext = $sqlServerObject.ConnectionContext

    $sqlConnectionContext.ServerInstance = $databaseEngineInstance
    $sqlConnectionContext.StatementTimeout = $StatementTimeout
    $sqlConnectionContext.ApplicationName = 'SqlServerDsc'

    if ($LoginType -eq 'Integrated')
    {
        <#
            This is only used for verbose messaging and not for the connection
            string since this is using Integrated Security=true (SSPI).
        #>
        $connectUserName = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name
    }
    else
    {
        if ($Credential)
        {
            $connectUsername = $Credential.UserName

            if ($LoginType -eq 'SqlLogin')
            {
                $sqlConnectionContext.LoginSecure = $false
                $sqlConnectionContext.Login = $connectUsername
                $sqlConnectionContext.SecurePassword = $Credential.Password
            }

            if ($LoginType -eq 'WindowsUser')
            {
                $sqlConnectionContext.ConnectAsUser = $true
                $sqlConnectionContext.ConnectAsUserName = $connectUsername
                $sqlConnectionContext.ConnectAsUserPassword = $Credential.GetNetworkCredential().Password
            }
        }
        else
        {
            Write-Error -Message "Credential required for login type $LoginType" -Category InvalidArgument
        }
    }

    Write-Verbose -Message "Username: $connectUsername; LoginType $LoginType" -Verbose

    try
    {
        $sqlConnectionContext.Connect()

        if ($sqlServerObject.Status -match '^Online$')
        {
            Write-Verbose -Message "Instance: $databaseEngineInstance" -Verbose
            return $sqlServerObject
        }
        else
        {
            throw
        }
    }
    catch
    {
        Write-Error -Message "Failed to connect to instance $databaseEngineInstance" -Category InvalidOperation
    }
    finally
    {
        <#
            Connect will ensure we actually can connect, but we need to disconnect
            from the session so we don't have anything hanging. If we need run a
            method on the returned $sqlServerObject it will automatically open a
            new session and then close, therefore we don't need to keep this
            session open.
        #>
        $sqlConnectionContext.Disconnect()
    }
}


<#
    .SYNOPSIS
        Script logins from a collection of SQL Servers and executes on all SQL Servers to ensure logins match. 

    .PARAMETER SqlServers
        String array containing the list of SQL Servers to merge logins.

    .PARAMETER Credential
        PSCredential object with the credentials to use to impersonate a user when connecting.
        If this is not provided then the current user will be used to connect to the SQL Server Database Engine instance.

    .PARAMETER ExcludeLogins
        COllection of logins that should not be merged. 

#>
function Merge-SqlLogins
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ServerName = $env:COMPUTERNAME,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter()]
        [ValidateSet('Integrated', 'WindowsUser', 'SqlLogin')]
        [System.String]
        $LoginType = 'Integrated',

        [Parameter()]
        [System.String[]]
        $ExcludeLogins
    )

    [string[]]$LoginScripts = @()

    foreach ($SqlInstance in $SqlServers) {
        $Server = Connect-SqlServer -ServerName $SqlInstance -Credential $Credential -LoginType $LoginType

        $Options = New-Object 'Microsoft.SqlServer.Management.Smo.ScriptingOptions'
	    $Options.LoginSid = $true
	    $Options.Permissions = $true
        $Options.IncludeIfNotExists = $true
        
        Write-Output "Scripting Logins from [$SqlInstance]..."
	    $LoginScripts += $Server.Logins.Where({ $_.LoginType -eq 'WindowsUser' -and $_.Name -notlike "NT *" -and $_.Name -notin $ExcludeLogins }).Script($Options)
    }

    foreach ($SqlInstance in $SqlServers) {
        $Server = Connect-SqlServer -ServerName $SqlInstance -Credential $Credential -LoginType $LoginType

        Write-Output "Syncing Logins to [$SqlInstance]..."
        $Server.Databases['master'].ExecuteNonQuery($LoginScripts)
    }
}
