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