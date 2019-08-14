<#
    .SYNOPSIS
        Connect to a computer and returns the SQL Service information. 

    .PARAMETER ComputerNames
        String array containing the Computer where you want to get SQL Services from.

    .PARAMETER Credential
        PSCredential object to impersonate when connecting. 

    .PARAMETER StatementTimeout
        Set the query StatementTimeout in seconds. Default 600 seconds (10mins).
#>
function Get-SqlService
{
    [CmdletBinding()]
    param
    (
        [Parameter()]
        [ValidateNotNull()]
        [System.String]
        $ComputerNames = $env:COMPUTERNAME,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential
    )

    $NsRoot = "root\Microsoft\SqlServer"
    $SqlServices = @()

foreach ($Computer in $ComputerNames) {
    $CimSess = New-CimSession -ComputerName $Computer -Credential $Credential -Authentication Negotiate
    $NsLeaf = Get-CimInstance -CimSession $CimSess -Namespace $NsRoot -Class __NAMESPACE -Filter "Name LIKE 'ComputerManagement__'" | Sort-Object -Property Name -Descending | Select-Object -First 1 -ExpandProperty Name
    $CimObject = Get-CimInstance -CimSession $CimSess -Namespace "$NsRoot\$NsLeaf" -Class SqlService

    foreach ($Object in $CimObject) {
        $SqlServiceType = $(
            Switch ($Object.SqlServiceType) {
                1  {'MSSQLSERVER'}
                2  {'SQLSERVERAGENT'}
                3  {'MSFTESQL'} 
                4  {'MsDtsServer'}
                5  {'MSSQLServerOLAPService'}
                6  {'ReportServer'}
                7  {'SQLBrowser'}
                8  {'NsService'}
                9  {'MSSQLFDLauncher'}
                10 {'SQLPBENGINE'}
                11 {'SQLPBDMS'}
                12 {'MSSQLLaunchpad'}
        })

        $ServiceState = $(
            Switch ($Object.State) {
                1 {'Stopped'}
                2 {'Start Pending'}
                3 {'Stop Pending'}
                4 {'Running'}
                5 {'Continue Pending'}
                6 {'Pause Pending'}
                7 {'Paused'}
        })

        $ServiceStartMode = $(
            switch ($Agent.StartMode) {
                0 {'Boot'}
                1 {'System'}
                2 {'Automatic'}
                3 {'Manual'}
                4 {'Disabled'}
        })

        $SqlInstanceName = $(
            Switch ($SqlServiceType) {
                'MSSQLSERVER'    { $Object.BinaryPath.Substring($Object.BinaryPath.IndexOf('-s')+2).Trim() }
                'SQLSERVERAGENT' { $Object.BinaryPath.Substring($Object.BinaryPath.IndexOf('-i')+2).Trim() }
                default          { '' }
        })

        $TcpPort = $(
            Switch ($SqlServiceType) {
                'MSSQLSERVER'   { (Get-CimInstance -CimSession $CimSess -Namespace "$NsRoot\$NsLeaf" -Class ServerNetworkProtocolProperty -Filter "InstanceName = '$InstanceName' AND IPAddressName = 'IPAll' AND PropertyStrVal <> ''").PropertyStrVal }
                default         { '' }
        })

        $ServiceProperties = @{
            SqlServiceType = $SqlServiceType
            ComputerName   = $Object.HostName
            InstanceName   = $SqlInstanceName
            ServiceAccount = $Object.StartName
            State          = $ServiceState
            StartMode      = $ServiceStartMode
            BinaryPath     = $Object.BinaryPath
            TcpPort        = $TcpPort
        }

        $SqlServices += New-Object psobject -Property $ServiceProperties
    }

    Remove-CimSession -CimSession $CimSess
}

    return $SqlServices 
}
