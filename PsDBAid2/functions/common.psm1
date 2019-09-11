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


<#
    .SYNOPSIS
        Split the SqlServer name into parts, e.g. host, instance, port. 

    .PARAMETER SqlServer
        String containing the SQL Server to connect to.
#>
function Split-SqlServerName
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $SqlServer
    )

    $SqlHost = $SqlServer
    $SqlInstance = 'MSSQLSERVER'
    $SqlPort = $null
    
    if ($SqlServer -match '\\') {
        $SqlHost = $SqlServer.Split('\')[0]
        $SqlInstance = $SqlServer.Split('\')[1].Split(',')[0]
        $SqlPort = $SqlServer.Split(',')[1]
    }
    
    if ($SqlServer -match ',' -and $SqlServer -notmatch '\\') {
        $SqlHost = $SqlServer.Split(',')[0]
        $SqlInstance = ''
        $SqlPort = $SqlServer.Split(',')[1]
    }

    $SqlServerParts = @{
        Host = $SqlHost
        Instance = $SqlInstance
        Port = $SqlPort
    }

    return $SqlServerParts
}


<#
    .SYNOPSIS
        Creates a new SMB share. If a share already exists, this will remove and recreate based on the parameters passed to this function. 

    .PARAMETER ComputerName
        String computer to create share on. 
    
    .PARAMETER Directory
        String directory on computer to share. 

    .PARAMETER ShareName
        String name of the SMB share.

    .PARAMETER ShareAccounts
        String[] list of accounts to be granted FullControl permissions to share. 

    .PARAMETER Credential
        PSCredential object to impersonate when connecting. 
        When username contains a domain, windows Auth is used, otherwise SQL auth is used. 
        If $null then integrated security is used.
#>
function Set-SqlShare
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ComputerName,

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $Directory,

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ShareName,

        [Parameter()]
        [ValidateNotNull()]
        [System.String[]]
        $ShareAccounts,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential
    )

    $ScriptBlock = {
        Param($dir, $smbname, $accounts)

        if (!(Test-Path $dir)) { 
            New-Item $dir -type directory | Out-Null
            Write-Verbose -Message "Created directory '$dir'."
        }

        $acl = Get-Acl $dir

        foreach ($obj in $accounts) {
            $rule = New-Object system.security.accesscontrol.filesystemaccessrule($obj,"FullControl","ContainerInherit,ObjectInherit","None","Allow")
            $acl.SetAccessRule($rule) 
            Write-Verbose -Message "Granted FullControl to '$obj' on '$dir'."
        }

        Set-Acl $dir $acl
        Get-SmbShare | Where-Object { $_.Name -eq $smbname } | Remove-SmbShare -Force
        New-SmbShare -Name $smbname -Path $dir -FullAccess $accounts | Out-Null
        Write-Verbose -Message "Created share '$smbname'."
    }

    $Parameters = @{
        ComputerName = $ComputerName
        ScriptBlock = $ScriptBlock
        ArgumentList = $Directory, $ShareName, $ShareAccounts
    }

    if ($Credential) {
        $Parameters.Add('Credential', $Credential)
    }

    Invoke-Command @Parameters
}