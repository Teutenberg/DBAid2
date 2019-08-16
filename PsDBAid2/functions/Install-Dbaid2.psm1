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
