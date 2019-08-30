
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

    .PARAMETER StatementTimeout
        Set the query StatementTimeout in seconds. Default 600 seconds (10mins).
#>
function Install-SqlServer
{
    [CmdletBinding()]
    param
    (
        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String]
        $SqlSetupPath,

        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String]
        $InstanceName,
        
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ProductKey,

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $Collation = 'Latin1_General_CI_AS',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathInstanceData = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathUserData = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathUserLog = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathTempData = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathTempLog = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $PathBackupData = 'C:\Program Files\Microsoft SQL Server',

        [Parameter()]
        [ValidateNotNull()]
        [System.String[]]
        $SysAdminAccounts = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name,

        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $TcpPortsqlEngine = '1433',

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential
    )

    # If path is root, add extra \ - e.g. D:\\
    $PathInstanceData = ($PathInstanceData.Trim('\')).ForEach({ if ($_.EndsWith(':')) { "$_\\" } else { $_ }})[0]

    # Get the setup.exe major product version
    $SQLMajorVersion = (Get-Item -Path "$SqlSetupPath\setup.exe").VersionInfo.ProductVersion.Split('.')[0]

    # Create SqlSetup parameter hastable
    $SqlSetupParams = @{
        SourcePath                 = $SqlSetupPath
        ProductKey                 = $ProductKey
        InstanceName               = $InstanceName
        Features                   = 'SQLEngine,Conn'
        SQLCollation               = $Collation
        SQLSysAdminAccounts        = $SysAdminAccounts
        InstallSharedDir           = 'C:\Program Files\Microsoft SQL Server'
        InstallSharedWOWDir        = 'C:\Program Files (x86)\Microsoft SQL Server'
        InstanceDir                = 'C:\Program Files\Microsoft SQL Server'
        InstallSQLDataDir          = $PathInstanceData
        SQLUserDBDir               = (Join-Path $PathUserData "MSSQL$SQLMajorVersion.$InstanceName\MSSQL\DATA")
        SQLUserDBLogDir            = (Join-Path $PathUserLog  "MSSQL$SQLMajorVersion.$InstanceName\MSSQL\LOG")
        SQLTempDBDir               = (Join-Path $PathTempData "MSSQL$SQLMajorVersion.$InstanceName\MSSQL\TEMPDB")
        SQLTempDBLogDir            = (Join-Path $PathTempLog "MSSQL$SQLMajorVersion.$InstanceName\MSSQL\TEMPDB")
        SQLBackupDir               = (Join-Path $PathBackupData  "MSSQL$SQLMajorVersion.$InstanceName\MSSQL\BACKUP")
        UpdateEnabled              = 'True'
        UpdateSource               = 'MU'
        ForceReboot                = $false
        PsDscRunAsCredential       = $Credential
    }

    # Create SqlServerNetwork parameter hastable
    $SqlServerNetworkParams = @{
        InstanceName         = $InstanceName
        ProtocolName         = 'Tcp'
        IsEnabled            = $true
        TCPDynamicPort       = $false
        TCPPort              = $TcpPortsqlEngine
        RestartService       = $true
        PsDscRunAsCredential = $Credential
    }

    # Set network parameters for firewall rules
    $FwRuleNameTcp = "SQL Server DBEngine - $InstanceName (TCP-In)"
    $FwRuleNameUdp = "SQL Server Browser (UDP-In)"
    $FwTcpPorts = $TcpPortsqlEngine, '1434'
    $FwUdpPorts = '1434'

    if ($SQLMajorVersion) {
        Write-Output "Initiating SqlSetup..."

        if (Invoke-DscResource -ModuleName 'SqlServerDsc' -Name 'SqlSetup' -Property $SqlSetupParams -Method Test) {
            Write-Verbose "Instance with name [$InstanceName] is already installed. Skipping installation..."
        } else {
            Write-Verbose "Installing instance [$InstanceName]"
            Invoke-DscResource -ModuleName 'SqlServerDsc' -Name 'SqlSetup' -Property $SqlSetupParams -Method Set -Verbose
        }

        Write-Output "Configuring SqlServerNetwork..."
        
        if (Invoke-DscResource -ModuleName SqlServerDsc -Name SqlServerNetwork -Property $SqlServerNetworkParams -Method Test) {
            Write-Verbose 'Instance TCP port already configured...'
        } 
        else {
            Write-Verbose "Configuring instance TCP port."
            Invoke-DscResource -ModuleName SqlServerDsc -Name SqlServerNetwork -Property $SqlServerNetworkParams -Method Set -Verbose
        }

        Write-Output "Configuring Firewall Rules..."

        if (Get-NetFirewallRule | Where-Object { $_.DisplayName -ilike $FwRuleNameTcp }) {
            Write-Verbose "Firewall rule for '$FwRuleNameTcp' already exists..."
        }
        else {
            New-NetFirewallRule -DisplayName $FwRuleNameTcp -Direction Inbound -Profile Domain -Action Allow -Protocol TCP -LocalPort $FwTcpPorts -RemoteAddress Any
            Write-Verbose "Firewall rule for '$FwRuleNameTcp' on port '$FwTcpPorts' created successfully."
        }
        
        if (Get-NetFirewallRule | Where-Object { $_.DisplayName -ilike $FwRuleNameUdp }) {
            Write-Verbose "Firewall rule for '$FwRuleNameUdp' already exists..."
        }
        else {
            New-NetFirewallRule -DisplayName $FwRuleNameUdp -Direction Inbound -Profile Domain -Action Allow -Protocol UDP -LocalPort $FwUdpPorts -RemoteAddress Any
            Write-Verbose "Firewall rule for '$FwRuleNameUdp' on port '$FwUdpPorts' created successfully."
        }
    } else {
        Write-Error "'$SqlSetupPath\setup.exe' not found! Please check that SqlSetupPath is correct."
    }
}


<#
    .SYNOPSIS
        Deploy the DBAid2 solution to a SQL instance. 

    .PARAMETER SqlServer
        String containing the SQL Server to connect to.

    .PARAMETER Credential
        PSCredential object to impersonate when connecting. 
        When username contains a domain, windows Auth is used, otherwise SQL auth is used. 
        If $null then integrated security is used.

    .PARAMETER ReleaseZipFile
        Path to the downloaded release zip file.
#>
function Install-Dbaid2
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
        [ValidateNotNullorEmpty()]
        [System.String]
        $ReleaseZipFile
    )

    $Smo = Connect-Sql -SqlServer $SqlServer -Credential $Credential
    $Dac = New-Object -TypeName 'Microsoft.SqlServer.Management.Dac'

    $DacPath = "\database\bin\Release\_dbaid.dacpac"

    Write-Output "Deploying DBAid Solution to $SqlServer..."

    Invoke-Command -ComputerName localhost -Credential $Credential -ScriptBlock {
        $pinfo = New-Object System.Diagnostics.ProcessStartInfo
        $pinfo.CreateNoWindow = $true
        $pinfo.UseShellExecute = $false
        $pinfo.RedirectStandardError = $true
        $pinfo.RedirectStandardOutput = $true
        $pinfo.FileName = 'C:\Program Files\Microsoft SQL Server\150\DAC\bin\SqlPackage.exe'
        $pinfo.Arguments = "/sf:$Using:DacPath /a:Publish /tsn:$Using:SqlServer /TargetDatabaseName:_dbaid /p:RegisterDataTierApplication=True /p:BlockWhenDriftDetected=False /p:DropObjectsNotInSource=False"
        $p = New-Object System.Diagnostics.Process
        $p.StartInfo = $pinfo
        $p.Start() | Out-Null
        # To avoid deadlocks, always read the output stream first and then wait. 
        $stdout = $p.StandardOutput.ReadToEnd()
        $stderr = $p.StandardError.ReadToEnd()
        # Timeout 300000 = 5mins
        $p.WaitForExit(300000)
        Write-Output "stdout: $stdout"
        Write-Output "stderr: $stderr"
        Write-Output "exit code: $($p.ExitCode)"
        
        if ($p.ExitCode -ne 0){
            Write-Error "Failed to deploy package..."
        }
    }
}
