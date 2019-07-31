<#
    .SYNOPSIS
        Connect to a SQL Server Database Engine and return the server object.

    .PARAMETER SqlServer
        String containing the SQL Server to connect to.

    .PARAMETER Credential
        PSCredential object to impersonate when connecting. 
        When username contains a domain, windows Auth is used, otherwise SQL auth is used. 
        If $null then integrated security is used.

    .PARAMETER StatementTimeout
        Set the query StatementTimeout in seconds. Default 600 seconds (10mins).
#>
function Connect-Sql
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $SqlServer = $env:COMPUTERNAME,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter()]
        [ValidateNotNull()]
        [System.Int32]
        $StatementTimeout = 600
    )

    $sqlServerObject = New-Object -TypeName 'Microsoft.SqlServer.Management.Smo.Server'
    $sqlServerObject.ConnectionContext.ServerInstance = $SqlServer
    $sqlServerObject.ConnectionContext.StatementTimeout = $StatementTimeout
    $sqlServerObject.ConnectionContext.ApplicationName = 'PsDBAid2'

    if ($Credential) {
        $connectUsername = $Credential.GetNetworkCredential().UserName

        if ($Credential.GetNetworkCredential().Domain) {
            # Connecting with impersonated Windows account
            Write-Verbose -Message "Connecting with Windows authentication credential supplied."
            
            $sqlServerObject.ConnectionContext.LoginSecure = $true
            $sqlServerObject.ConnectionContext.ConnectAsUser = $true
            $sqlServerObject.ConnectionContext.ConnectAsUserName = $connectUsername
            $sqlServerObject.ConnectionContext.ConnectAsUserPassword = $Credential.GetNetworkCredential().Password
        }
        else {
            # Connecting with SQL login
            Write-Verbose -Message "Connecting with SQL authentication credential supplied."

            $sqlServerObject.ConnectionContext.LoginSecure = $false
            $sqlServerObject.ConnectionContext.Login = $connectUsername
            $sqlServerObject.ConnectionContext.SecurePassword = $Credential.Password
        }
    }
    else {
        # Connecting with integrated security
        Write-Verbose -Message "Connecting with Integrated security."
        $connectUserName = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name
    }

    Write-Verbose -Message "UserName: $connectUsername"

    try
    {
        $sqlServerObject.ConnectionContext.Connect()

        if ($sqlServerObject.Status -match '^Online$')
        {
            Write-Verbose -Message "Connected to SQL instance: $SqlServer"
            return $sqlServerObject
        }
        else
        {
            throw
        }
    }
    catch
    {
        Write-Error -Message "Failed to connect to SQL instance $SqlServer" -Category InvalidOperation
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
        $sqlServerObject.ConnectionContext.Disconnect()
    }
}
