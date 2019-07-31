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