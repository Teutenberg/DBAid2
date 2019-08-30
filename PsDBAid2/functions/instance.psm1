<#
    .SYNOPSIS
        Set the minimum and maximum memory configuration for a SQL Server instance. 

    .PARAMETER SqlServer
        String containing the SQL Server to connect to.

    .PARAMETER Credential
        PSCredential object for PsDscRunAsCredential.

    .PARAMETER InstanceName
        String containing the SQL Server Database Engine instance to connect to.

    .PARAMETER StatementTimeout
        Set the query StatementTimeout in seconds. Default 600 seconds (10mins).
#>
function Set-SqlMemory
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
        $MinMemory = 0,

        [Parameter()]
        [ValidateNotNull()]
        [System.Int32]
        $MaxMemory,

        [Parameter()]
        [ValidateNotNull()]
        [Switch]
        $DynamicAlloc
    )

    $SqlServerParts = Split-SqlServerName -SqlServer $SqlServer
 
    $SqlServerMemoryParams = @{
        Ensure               = 'Present'
        ServerName           = $SqlServerParts.Host
        InstanceName         = $SqlServerParts.Instance
        MinMemory            = $MinMemory
    }

    if ($DynamicAlloc -or -not $MaxMemory) {
        Write-Verbose -Message "Setting DynamicAlloc to true."
        $SqlServerMemoryParams.Add("DynamicAlloc",$true)
    }
    else {
        $SqlServerMemoryParams.Add("DynamicAlloc",$false)
        $SqlServerMemoryParams.Add("MaxMemory",$MaxMemory)
    }

    if ($Credential) { 
        Write-Verbose -Message "Setting PsDscRunAsCredential to [$($Credential.UserName)]."
        $SqlServerMemoryParams.Add("PsDscRunAsCredential",$Credential)
    }

    if (Invoke-DscResource -ModuleName SqlServerDsc -Name SqlServerMemory -Property $SqlServerMemoryParams -Method Test) {
        Write-Output "Skipping - SqlServerMemory already configured to desired state on SQL instance [$SqlServer]."
    } 
    else {
        Write-Output 'Configuring SqlServerMemory to desired state on SQL instance [$SqlServer]...'
        Invoke-DscResource -ModuleName SqlServerDsc -Name SqlServerMemory -Property $SqlServerMemoryParams -Method Set -Verbose
    }
}


<#
    .SYNOPSIS
        Compares logins between SQL Servers and adds missing logins and server roles and server object permissions. This is a useful function to keep Availability Group cluster logins synced.

    .PARAMETER SourceSqlServer
        String containing the source SQL Server where the logins and permissions are to be copied from.

    .PARAMETER DestinationSqlServers
        String array containing the list of destination SQL Servers where the logins and permissions are to be copied to.

    .PARAMETER Credential
        PSCredential object with the credentials to use to impersonate a user when connecting.
        If this is not provided then the current user will be used to connect to the SQL Server Database Engine instance.

    .PARAMETER Filter
        String filter returns only logins like pattern. Default value = "*"

    .PARAMETER Include
        String array of logins that will be included. 

    .PARAMETER Exclude
        String array of logins that will be excluded. 
#>
function Copy-Login
{
    [CmdletBinding()]
    param
    (
        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String]
        $SourceSqlServer,

        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String[]]
        $DestinationSqlServers,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter()]
        [System.String]
        $Filter = "*",

        [Parameter()]
        [System.String[]]
        $Include,

        [Parameter()]
        [System.String[]]
        $Exclude
    )
    
    $SourceServer = Connect-Sql -SqlServer $SourceSqlServer -Credential $Credential
    $SourceLogins = $SourceServer.Logins.Where({ $_.Sid -ne 1 -and $_.Name -notlike "##*" -and $_.Name -notin $Exclude -and ($_.Name -ilike $Filter -or $_Name -iin $Include) })

    Write-Output -Message "Syncing logins between source and destination..."
    Write-Output -Message "Connected to source server: $SourceSqlServer"
    Write-Verbose -Message "Source logins to be copied:"
    $SourceLogins | ForEach-Object { Write-Verbose -Message "`t$($_.Name)" }

    foreach ($DestSqlServer in $DestinationSqlServers) {
        $DestServer = Connect-Sql -SqlServer $DestSqlServer -Credential $Credential
        
        Write-Output -Message "Connected to destination server: $DestSqlServer"
        
        foreach ($SourceLogin in $SourceLogins) {
            $SourceLogin.Refresh()
            $DestLogin = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Login -ArgumentList $DestServer, $SourceLogin.Name
            $DestLogin.Refresh()

            if (-not $DestLogin.CreateDate) {
                $DestLogin.LoginType = $SourceLogin.LoginType

                if ($SourceLogin.LoginType -eq "WindowsUser") {
                    $DestLogin.Create("")
                    Write-Verbose -Message "Created Windows login: $($DestLogin.Name)"
                }
                elseif ($SourceLogin.LoginType -eq "SqlLogin") {
                    $SqlLoginHash = $SourceServer.Databases['master'].ExecuteWithResults("SELECT [Hash]=CONVERT(NVARCHAR(512), LOGINPROPERTY(N'$($SourceLogin.Name)','PASSWORDHASH'), 1)").Tables.Hash
                    $DestLogin.Create($SqlLoginHash, [Microsoft.SqlServer.Management.Smo.LoginCreateOptions]::IsHashed)
                    Write-Verbose -Message "Created SQL login: $($DestLogin.Name)"
                }
            }
            else {
                Write-Verbose -Message "User already exists: $($DestLogin.Name)"
            }
    
            foreach ($ServerPermission in $SourceServer.enumServerPermissions($SourceLogin.Name).PermissionType) {
                $DestServer.Grant($ServerPermission, $DestLogin.Name)
                Write-Verbose -Message "Granted server permission [$ServerPermission] to login [$($DestLogin.Name)]"
            }

            foreach ($ServerRole in $SourceLogin.ListMembers()) {
                $DestLogin.AddToRole($ServerRole)
                Write-Verbose -Message "Granted server role [$ServerRole] to login [$($DestLogin.Name)]"
            }
        }
    }

    Write-Output -Message "Syncing logins completed..."
}
