Set-Location $PSScriptRoot
Import-Module .\configg.psm1

[string[]]$SQLServers = "SETTSQLD05\INST01,4309"
[string]$OutputPath = $PSScriptRoot

foreach ($SQLServer in $SQLServers) {
    $FileName = $SQLServer.Replace('\','@') + "-" + (Get-Date -Format "yyyyMMddHHmmss") + ".md"
    $FilePath = Join-Path $OutputPath $FileName

    Get-ConfigReport $SQLServer | Out-File -FilePath $FilePath -Force
}