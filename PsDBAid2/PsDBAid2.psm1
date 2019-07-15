<#
    PsDBAid2.psm1 is a helper module for scripted activities. This module will contain functions to help simplify scripting and increase productivity. 
#>
Import-Module SqlServer

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
        Compares logins between SQL Servers and adds missing logins and server roles and server object permissions. This is a useful function to keep Availability Group cluster logins synced.

    .PARAMETER SqlServers
        String array containing the list of SQL Servers to merge logins and permissions.

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

    .PARAMETER ExcludeLogins
        Collection of logins that should not be merged. 

#>
function Merge-SqlLogins
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $SqlServers,

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

    foreach ($SqlInstance in $SqlServers) {
        $LocalServerName = $SqlInstance.Split('\')[0]
        $LocalInstanceName = $SqlInstance.Split('\')[1]
        $RemoteServers = $SqlServers.Where({$_ -ne $SqlInstance})
        
        $LocalServer = Connect-SqlServer -ServerName $LocalServerName -InstanceName $LocalInstanceName -Credential $Credential -LoginType $LoginType 
        $LocalLogins = $LocalServer.Logins.Where({ $_.Sid -ne 1 -and $_.Name -notlike "##*" -and $_.Name -notin $ExcludeLogins })

        foreach ($RemoteInstance in $RemoteServers) {
            $RemoteServerName = $RemoteInstance.Split('\')[0]
            $RemoteInstanceName = $RemoteInstance.Split('\')[1]
            $RemoteServer = Connect-SqlServer -ServerName $RemoteServerName -InstanceName $RemoteInstanceName -Credential $Credential -LoginType $LoginType
        
            foreach ($LocalLogin in $LocalLogins) {
                $LocalLogin.Refresh()
                $RemoteLogin = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Login -ArgumentList $RemoteServer, $LocalLogin.Name
                $RemoteLogin.Refresh()
    
                if (-not $RemoteLogin.CreateDate) {
                    $RemoteLogin.LoginType = $LocalLogin.LoginType
    
                    if ($LocalLogin.LoginType -eq "WindowsUser") {
                        $RemoteLogin.Create("")
                    }
                    elseif ($LocalLogin.LoginType -eq "SqlLogin") {
                        $SqlLoginHash = $LocalServer.Databases['master'].ExecuteWithResults("SELECT [Hash]=CONVERT(NVARCHAR(512), LOGINPROPERTY(N'$($LocalLogin.Name)','PASSWORDHASH'), 1)").Tables.Hash
                        $RemoteLogin.Create($SqlLoginHash, [Microsoft.SqlServer.Management.Smo.LoginCreateOptions]::IsHashed)
                    }
                }
        
                foreach ($ServerPermission in $LocalServer.enumServerPermissions($LocalLogin.Name).PermissionType) {
                    $RemoteServer.Grant($ServerPermission, $RemoteLogin.Name)
                }
    
                foreach ($ServerRole in $LocalLogin.ListMembers()) {
                    $RemoteLogin.AddToRole($ServerRole)
                }
            }
        }
    }
}
