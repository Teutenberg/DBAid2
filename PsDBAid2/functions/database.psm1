<#
    .SYNOPSIS
        Returns a colletion of smo user objects for a collection of databases.

    .PARAMETER SqlServer
        String containing the source SQL Server where the logins and permissions are to be copied from.

    .PARAMETER Databases
        String array containing the list of databases to return users objects.

    .PARAMETER Credential
        PSCredential object with the credentials to use to impersonate a user when connecting.
        If this is not provided then the current user will be used to connect to the SQL Server Database Engine instance.

    .PARAMETER Filter
        String filter returns only users like pattern. Default value = "*"

    .PARAMETER Include
        String array of users that will be included. 

    .PARAMETER Exclude
        String array of users that will be excluded. 
#>
function Restore-Database
{
    [CmdletBinding()]
    param
    (
        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String]
        $SqlServer,

        [Parameter(Mandatory=$true)]
        [ValidateNotNull()]
        [System.String]
        $Database,

        [Parameter()]
        [switch]
        $KeepExistingUsers,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential    
    )
    
    $Server = Connect-Sql -SqlServer $SqlServer -Credential $Credential

    $Users = $Server.Databases[$Database].Users

    #Do restore here...

    foreach ($user in $Users) {
        $Server.Databases[$Database].Users.Add($user)
    }
}


<#
    .SYNOPSIS
        Enable log shipping on a database

    .PARAMETER PrimarySqlServer
        String - Primary SQL Server e.g host\instance.

    .PARAMETER SecondarySqlServer
        String - Secondary SQL Server e.g host\instance.

    .PARAMETER PrimaryDatabase
        String - primary database name.

    .PARAMETER SecondaryDatabase
        String - secondary database name.

    .PARAMETER PrimaryDir
        string - Absolute path on primary host where backups are to be written to e.g. "C:\path\folder".
        Defaults to backup directory\logshipping-primary\PrimaryDatabase

    .PARAMETER SecondaryDir
        string - Absolute path on secondary host where backups are to be copied to e.g. "C:\path\folder".
        Defaults to backup directory\logshipping-secondary\SecondaryDatabase
    
    .PARAMETER MatchDir
        switch - Matches the primary database path directory for restore, otherwise uses the default paths. 

    .PARAMETER SysAdminGroup
        string - Optional AD group to grant SMB share access to.

    .PARAMETER Credential
        PSCredential object with the credentials to use to impersonate a user when connecting.
        If this is not provided then the current user will be used to connect to the SQL Server Database Engine instance.
#>
function Enable-LogShipping
{
    [CmdletBinding()]
    Param(
        [parameter(Mandatory=$true, HelpMessage='(Mandatory) Primary Server - "HOST\INSTANCE"')]
        [ValidateNotNullOrEmpty()]
        [string]
        $PrimarySqlServer,
        
        [parameter(Mandatory=$true, HelpMessage='(Mandatory) Secondary Server - "HOST\INSTANCE"')]
        [ValidateNotNullOrEmpty()]
        [string]
        $SecondarySqlServer,
    
        [parameter(Mandatory=$true, HelpMessage='(Mandatory) Primary database - "DBNAME"')]
        [ValidateNotNullOrEmpty()]
        [string]
        $PrimaryDatabase,
    
        [parameter(Mandatory=$false, HelpMessage='(Optional) Secondary database - Default = PrimaryDatabase')]
        [ValidateNotNullOrEmpty()]
        [string]
        $SecondaryDatabase = $PrimaryDatabase,
         
        [parameter(Mandatory=$false, HelpMessage='(Optional) Primary directory path, D:\path - Default= Backup Path')]
        [ValidateNotNullOrEmpty()]
        [string]
        $PrimaryDir,
    
        [parameter(Mandatory=$false, HelpMessage='(Optional) Secondary directory path, D:\path - Default= Backup Path')]
        [ValidateNotNullOrEmpty()]
        [string]
        $SecondaryDir,
    
        [parameter(Mandatory=$false, HelpMessage='(Switch) Matches the primary database paths.')]
        [ValidateNotNullOrEmpty()]
        [switch]
        $MatchDir,
    
        [parameter(Mandatory=$false, HelpMessage='(Optional) AD group to grant SMB share access to.')]
        [ValidateNotNullOrEmpty()]
        [string]
        $SysAdminGroup,

        [Parameter()]
        [ValidateNotNull()]
        [System.Management.Automation.PSCredential]
        $Credential
    )

    $ErrorActionPreference = "Stop"

    #region: <# AUTO PARAMETERS #>
    $pConnectArgs = @{ SqlServer = $PrimarySqlServer }
    $sConnectArgs = @{ SqlServer = $SecondarySqlServer }

    if ($Credential) {
        $pConnectArgs.Add('Credential', $Credential)
        $sConnectArgs.Add('Credential', $Credential)
    }

    $pSQLServerObject = Connect-Sql @pConnectArgs
    $sSQLServerObject = Connect-Sql @sConnectArgs

    if (!$PrimaryDir) {
        $PrimaryDir = Join-Path $pSQLServerObject.BackupDirectory "logshipping-primary\$PrimaryDatabase"
    }
    if (!$SecondaryDir) {
        $SecondaryDir = Join-Path $sSQLServerObject.BackupDirectory "logshipping-secondary\$SecondaryDatabase"
    }
    
    $pServerInstance = $pSQLServerObject.Name
    $pComputerName = $pSQLServerObject.NetName
    $pInstanceId = $pSQLServerObject.ServiceInstanceId
    [array[]]$pDatabaseFiles = $pSQLServerObject.databases[$PrimaryDatabase].FileGroups.Files | Select-Object Name, FileName, @{Name='Type';E={'D'}}
    $pDatabaseFiles += $pSQLServerObject.databases[$PrimaryDatabase].LogFiles | Select-Object Name, FileName, @{Name='Type';E={'L'}}
    $pLogShipDir = $PrimaryDir
    $pShareName = Split-Path $pLogShipDir -Leaf
    $pSharePath = '\\' + (Join-Path $pComputerName $pShareName) 
    $pServiceAccountSql = $pSQLServerObject.ServiceAccount
    $pServiceAccountAgt = $pSQLServerObject.JobServer.ServiceAccount
    
    $sServerInstance = $sSQLServerObject.Name
    $sComputerName = $sSQLServerObject.NetName
    $sInstanceId = $sSQLServerObject.ServiceInstanceId
    $sLogShipDir = $SecondaryDir
    $sShareName = Split-Path $sLogShipDir -Leaf
    $sSharePath = '\\' + (Join-Path $sComputerName $sShareName)
    $sServiceAccountSql = $sSQLServerObject.ServiceAccount
    $sServiceAccountAgt = $sSQLServerObject.JobServer.ServiceAccount
    $sDefaultDataDir = $sSQLServerObject.DefaultFile
    $sDefaultLogDir = $sSQLServerObject.DefaultLog
    $sMoveStr = ''
    
    $pSmbAccess = @($SysAdminGroup, $pServiceAccountSql, $pServiceAccountAgt, $sServiceAccountSql, $sServiceAccountAgt) | Where-Object { $_.Length -gt 0 } | Select-Object -Unique
    $sSmbAccess = @($SysAdminGroup, $sServiceAccountSql, $sServiceAccountAgt) | Where-Object { $_.Length -gt 0 } | Select-Object -Unique

    $pSqlShareArgs = @{
        ComputerName  = $pComputerName
        Directory     = $pLogShipDir 
        ShareName     = $pShareName 
        ShareAccounts = $pSmbAccess
    }

    $sSqlShareArgs = @{
        ComputerName  = $sComputerName
        Directory     = $sLogShipDir 
        ShareName     = $sShareName 
        ShareAccounts = $sSmbAccess
    }

    if ($Credential) {
        $pSqlShareArgs.Add('Credential', $Credential)
        $sSqlShareArgs.Add('Credential', $Credential)
    }

    if ($MatchDir) {
        $sMoveStr = ($pDatabaseFiles.ForEach({", MOVE '" + $_.Name + "' TO '" + $_.FileName.Replace($pInstanceId,$sInstanceId).Replace($PrimaryDatabase,$SecondaryDatabase) + "'"})) -join ''
    } else {
        $sMoveStr = ($pDatabaseFiles.ForEach({", MOVE '" + $_.Name + "' TO '" + (&{if ($_.Type -eq 'L') {$sDefaultLogDir} else {$sDefaultDataDir}}) + (Split-Path $_.FileName -Leaf).Replace($PrimaryDatabase,$SecondaryDatabase) + "'"})) -join ''
    }
    
    $pAlterRecovery = "ALTER DATABASE [$PrimaryDatabase] SET RECOVERY FULL WITH NO_WAIT"
    $pBackupDatabase = "BACKUP DATABASE [$PrimaryDatabase] TO DISK = N'$pSharePath\$PrimaryDatabase.bak' WITH NOINIT"
    $sRestoreBackup = "RESTORE DATABASE [$SecondaryDatabase] FROM DISK = N'$pSharePath\$PrimaryDatabase.bak' WITH REPLACE, NORECOVERY$sMoveStr"
    
    $pSetupLogShipping = "DECLARE @BackupJobId UNIQUEIDENTIFIER, @RetCode INT;
    EXEC @RetCode = master.dbo.sp_add_log_shipping_primary_database @database = N'$PrimaryDatabase',@backup_directory = N'$pLogShipDir',@backup_share = N'$pSharePath'
        ,@backup_job_name = N'LSBackup_$PrimaryDatabase',@backup_retention_period = 4320,@backup_compression = 2,@backup_threshold = 60,@threshold_alert_enabled = 1
        ,@history_retention_period = 5760,@backup_job_id = @BackupJobId OUTPUT,@overwrite = 1; 
    IF (@@ERROR = 0 AND @RetCode = 0)
    BEGIN 
        DECLARE @BackUpScheduleID INT;
        EXEC msdb.dbo.sp_add_schedule @schedule_name =N'LSBackupSchedule_$PrimaryDatabase',@enabled = 1
            ,@freq_type = 4,@freq_interval = 1,@freq_subday_type = 4 ,@freq_subday_interval = 15 ,@freq_recurrence_factor = 0,@schedule_id = @BackUpScheduleID OUTPUT; 
        EXEC msdb.dbo.sp_attach_schedule @job_id = @BackupJobId, @schedule_id = @BackUpScheduleID;  
        EXEC msdb.dbo.sp_update_job @job_id = @BackupJobId, @enabled = 1;
        EXEC master.dbo.sp_add_log_shipping_primary_secondary @primary_database = N'$PrimaryDatabase',@secondary_server = N'$sServerInstance',@secondary_database = N'$SecondaryDatabase',@overwrite = 1;
    END"
    
    $sSetupLogShipping = "DECLARE @CopyJobId UNIQUEIDENTIFIER, @RestoreJobId UNIQUEIDENTIFIER, @RetCode INT;
    EXEC @RetCode = master.dbo.sp_add_log_shipping_secondary_primary @primary_server = N'$pServerInstance', @primary_database = N'$PrimaryDatabase' 
        ,@backup_source_directory = N'$pSharePath ',@backup_destination_directory = N'$sSharePath' 
        ,@copy_job_name = N'LSCopy_$pServerInstance`_$PrimaryDatabase',@restore_job_name = N'LSRestore_$pServerInstance`_$PrimaryDatabase' 
        ,@file_retention_period = 4320, @overwrite = 1, @copy_job_id = @CopyJobId OUTPUT, @restore_job_id = @RestoreJobId OUTPUT;
    IF (@@ERROR = 0 AND @RetCode = 0) 
    BEGIN 
        DECLARE @CopyJobScheduleID INT, @RestoreJobScheduleID INT;
        EXEC msdb.dbo.sp_add_schedule @schedule_name =N'DefaultCopyJobSchedule_$pServerInstance`_$PrimaryDatabase' 
            ,@enabled = 1,@freq_type = 4,@freq_interval = 1,@freq_subday_type = 4,@freq_subday_interval = 15,@freq_recurrence_factor = 0,@schedule_id = @CopyJobScheduleID OUTPUT;
        EXEC msdb.dbo.sp_attach_schedule @job_id = @CopyJobId, @schedule_id = @CopyJobScheduleID;
        EXEC msdb.dbo.sp_add_schedule @schedule_name =N'DefaultRestoreJobSchedule_$pServerInstance`_$PrimaryDatabase' 
            ,@enabled = 1,@freq_type = 4,@freq_interval = 1,@freq_subday_type = 4,@freq_subday_interval = 15,@freq_recurrence_factor = 0,@schedule_id = @RestoreJobScheduleID OUTPUT;
        EXEC msdb.dbo.sp_attach_schedule @job_id = @RestoreJobId, @schedule_id = @RestoreJobScheduleID;
        EXEC master.dbo.sp_add_log_shipping_secondary_database @secondary_database = N'$SecondaryDatabase',@primary_server = N'$pServerInstance',@primary_database = N'$PrimaryDatabase' 
            ,@restore_delay = 0,@restore_mode = 0,@disconnect_users = 0,@restore_threshold = 45,@threshold_alert_enabled = 1,@history_retention_period = 5760,@overwrite = 1;
        EXEC msdb.dbo.sp_update_job @job_id = @CopyJobId, @enabled = 1;
        EXEC msdb.dbo.sp_update_job @job_id = @RestoreJobId, @enabled = 1;
    END"
    
    #endregion
    
    #region: SETUP LogShipping
    Set-SqlShare @pSqlShareArgs
    Set-SqlShare @sSqlShareArgs
    
    Invoke-Sqlcmd -ServerInstance $pServerInstance -Database 'master' -Query $pAlterRecovery -OutputSqlErrors $true
    Invoke-Sqlcmd -ServerInstance $pServerInstance -Database 'master' -Query $pBackupDatabase -OutputSqlErrors $true
    Invoke-Sqlcmd -ServerInstance $sServerInstance -Database 'master' -Query $sRestoreBackup -OutputSqlErrors $true
    Invoke-Sqlcmd -ServerInstance $pServerInstance -Database 'master' -Query $pSetupLogShipping -OutputSqlErrors $true
    Invoke-Sqlcmd -ServerInstance $sServerInstance -Database 'master' -Query $sSetupLogShipping -OutputSqlErrors $true
    #endregion
}