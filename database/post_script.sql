/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.					
--------------------------------------------------------------------------------------
*/

USE [_dbaid];
GO

DECLARE @collector_secret VARCHAR(20);
EXEC [system].[generate_secret] @length=20, @secret=@collector_secret OUT

/* Insert static variables */
MERGE INTO [system].[configuration] AS [Target] 
USING (SELECT N'INSTANCE_GUID', CAST(NEWID() AS SQL_VARIANT)
	UNION SELECT N'SANITISE_COLLECTOR_DATA',1
	UNION SELECT N'COLLECTOR_SECRET', @collector_secret
) AS [Source] ([key],[value])  
ON [Target].[key] = [Source].[key] 
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([key],[value]) 
	VALUES ([Source].[key],[Source].[value]);
GO

/* execute inventory */
EXEC [checkmk].[inventory_database];
GO
EXEC [checkmk].[inventory_agentjob];
GO
EXEC [checkmk].[inventory_alwayson];
GO

/* Insert collector procedures with NULL last_execution datetime */
MERGE INTO [collector].[last_execution] AS [Target]
USING (
	SELECT [object_name]=[name]
		,[last_execution]=NULL 
	FROM sys.objects 
	WHERE [type] = 'P' 
		AND SCHEMA_NAME([schema_id]) = N'collector'
) AS [Source]([object_name],[last_execution])
ON [Target].[object_name] = [Source].[object_name]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([object_name],[last_execution]) VALUES ([Source].[object_name],[Source].[last_execution]);

/* Create job categories */
IF NOT EXISTS (SELECT 1 FROM msdb.dbo.syscategories WHERE [name] = N'_dbaid_ag_primary_only')
  EXEC msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'_dbaid_ag_primary_only';
IF NOT EXISTS (SELECT 1 FROM msdb.dbo.syscategories WHERE [name] = N'_dbaid_ag_secondary_only')  
  EXEC msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'_dbaid_ag_secondary_only';
IF NOT EXISTS (SELECT 1 FROM msdb.dbo.syscategories WHERE [name] = N'_dbaid_ag_job_maintenance')
  EXEC msdb.dbo.sp_add_category @class = N'JOB', @type = N'LOCAL', @name = N'_dbaid_ag_job_maintenance';
GO

/* Create Maintenance Jobs */
USE [msdb]
GO

DECLARE @DetectedOS NVARCHAR(7);

/* sys.dm_os_host_info is relatively new (SQL 2017+ despite what BOL says; not from 2008). If it's there, query it (result being 'Linux' or 'Windows'). If not there, it's Windows. */
IF EXISTS (SELECT 1 FROM sys.system_objects WHERE [name] = N'dm_os_host_info' AND [schema_id] = SCHEMA_ID(N'sys'))
  SELECT @DetectedOS = [host_platform] FROM sys.dm_os_host_info;
ELSE 
  SELECT @DetectedOS = N'Windows';

DECLARE @jobId BINARY(16)
	,@JobTokenServer CHAR(22)
	,@JobTokenLogDir NVARCHAR(260)
	,@JobTokenDateTime CHAR(49)
	,@cmd NVARCHAR(4000)
	,@out NVARCHAR(260)
	,@owner sysname
	,@schid INT
	,@timestamp NVARCHAR(13);

SELECT @JobTokenServer = N'$' + N'(ESCAPE_DQUOTE(SRVR))'
	,@JobTokenDateTime = N'$' + N'(ESCAPE_DQUOTE(STEPID))_' + N'$' + N'(ESCAPE_DQUOTE(STRTDT))_' + N'$' + N'(ESCAPE_DQUOTE(STRTTM))'
	,@owner = (SELECT [name] FROM sys.server_principals WHERE [sid] = 0x01)
	,@timestamp = CONVERT(VARCHAR(8), GETDATE(), 112) + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR(2)) + CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR(2)) + CAST(DATEPART(SECOND, GETDATE()) AS VARCHAR(2));

/* Linux filesystems use forward slash for navigating folders, not backslash. */
IF @DetectedOS = N'Windows'
  	SELECT @JobTokenLogDir = LEFT(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260)),LEN(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260))) - CHARINDEX('\',REVERSE(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260))))) + N'\';
ELSE
	SELECT @JobTokenLogDir = LEFT(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260)),LEN(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260))) - CHARINDEX('/',REVERSE(CAST(SERVERPROPERTY('ErrorLogFileName') AS NVARCHAR(260))))) + N'/';

IF ((SELECT LOWER(CAST(SERVERPROPERTY('Edition') AS NVARCHAR(128)))) LIKE '%express%')
	PRINT 'Express Edition Detected. No SQL Agent.';
ELSE
BEGIN
	IF NOT EXISTS (SELECT [name] FROM [msdb].[dbo].[syscategories] WHERE [name] = '_dbaid_maintenance')
		EXEC msdb.dbo.sp_add_category
			@class=N'JOB',
			@type=N'LOCAL',
			@name=N'_dbaid_maintenance';

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_delete_system_history')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_delete_system_history', @owner_login_name=@owner,
			@enabled=0, @category_name=N'_dbaid_maintenance', @description=N'Executes [system].[delete_system_history] to cleanup job, backup, cmdlog history in [_dbaid] and msdb database.', 
			@job_id = @jobId OUTPUT;

		SET @out = @JobTokenLogDir + N'_dbaid_maintenance_history_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DeleteSystemHistory', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=3, @on_fail_action=2, 
			@subsystem=N'TSQL', @command=N'EXEC [system].[delete_system_history] @job_olderthan_day=92,@backup_olderthan_day=92,@dbmail_olderthan_day=92,@maintplan_olderthan_day=92;', 
			@database_name=N'_dbaid',
			@output_file_name=@out,
			@flags=2;

		/* Set step to quit with success on success if on Linux - no second job step (yet) */
		IF @DetectedOS = N'Linux'
		  EXEC msdb.dbo.sp_update_jobstep @job_id = @jobId, @step_id = 1, @on_success_action = 1;

		/* Not valid for Linux. Need bash equivalent. */
		IF @DetectedOS = N'Windows'
		BEGIN
			SET @cmd = N'cmd /q /c "For /F "tokens=1 delims=" %v In (''ForFiles /P "' + @JobTokenLogDir + N'" /m "_dbaid_*.log" /d -30 2^>^&1'') do if EXIST "' + @JobTokenLogDir + N'"%v echo del "' + @JobTokenLogDir + N'"%v& del "' + @JobTokenLogDir + N'"\%v"'; 
				
			EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'DeleteLogFiles', 
				@step_id=2, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, @subsystem=N'CmdExec', 
				@command=@cmd,
				@output_file_name=@out,
				@flags=2;
		END

		EXEC msdb.dbo.sp_update_job @job_id=@jobId, @start_step_id=1;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_delete_system_history')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_delete_system_history';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_delete_system_history',
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @active_start_time=50000;

		EXEC msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_backup_user_diff')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_backup_user_diff', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;


		/* No support for @CleanupTime parameter on Linux. */
		IF @DetectedOS = N'Linux'
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''DIFF'', @CheckSum=''Y''';
		END
		ELSE
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''DIFF'', @CheckSum=''Y'', @CleanupTime=72';
		END
			
		SET @out = @JobTokenLogDir + N'_dbaid_backup_user_diff_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_backup_user_diff', 
				@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
				@command=@cmd, 
				@subsystem = N'TSQL',
				@output_file_name=@out,
				@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_diff')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_diff';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_backup_user_diff',
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @active_start_time=190000;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_backup_user_full')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_backup_user_full', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;


		/* No support for @CleanupTime parameter on Linux. */
		IF @DetectedOS = N'Linux'
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''FULL'', @CheckSum=''Y''';
		END
		ELSE
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''FULL'', @CheckSum=''Y'', @CleanupTime=72';
		END

		
		SET @out = @JobTokenLogDir + N'_dbaid_backup_user_full_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_backup_user_full', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_full')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_full';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_backup_user_full',
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @active_start_time=200000;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_backup_user_tran')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_backup_user_tran', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;

		/* No support for @CleanupTime parameter on Linux. */
		IF @DetectedOS = N'Linux'
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''LOG'', @CheckSum=''Y''';
		END
		ELSE
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''USER_DATABASES'', @BackupType=''LOG'', @CheckSum=''Y'', @CleanupTime=72';
		END
		
		SET @out = @JobTokenLogDir + N'_dbaid_backup_user_tran_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_backup_user_tran', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_tran')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_user_tran';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_backup_user_tran',
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=4, @freq_subday_interval=15, @active_start_time=0;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_backup_system_full')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_backup_system_full', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;

		/* No support for @CleanupTime parameter on Linux. */
		IF @DetectedOS = N'Linux'
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''SYSTEM_DATABASES'', @BackupType=''FULL'', @CheckSum=''Y''';
		END
		ELSE
		BEGIN
			SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseBackup] @Databases=''SYSTEM_DATABASES'', @BackupType=''FULL'', @CheckSum=''Y'', @CleanupTime=72';
		END

		SET @out = @JobTokenLogDir + N'_dbaid_backup_system_full_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_backup_system_full', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2,
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_system_full')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_backup_system_full';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_backup_system_full',  
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @active_start_time=180000;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_index_optimise_user')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_index_optimise_user', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [_dbaid].[dbo].[IndexOptimize] @Databases=''USER_DATABASES'', @UpdateStatistics=''ALL'', @OnlyModifiedStatistics=''Y'', @StatisticsResample=''Y'', @MSShippedObjects=''Y'', @LockTimeout=600, @LogToTable=''Y''';

		SET @out = @JobTokenLogDir + N'_dbaid_index_optimise_user_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_index_optimise_user', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2,
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_index_optimise_user')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_index_optimise_user';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_index_optimise_user',  
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @freq_recurrence_factor=0, @active_start_time=0;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_index_optimise_system')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_index_optimise_system', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [_dbaid].[dbo].[IndexOptimize] @Databases=''SYSTEM_DATABASES'', @UpdateStatistics=''ALL'', @OnlyModifiedStatistics=''Y'', @StatisticsResample=''Y'', @MSShippedObjects=''Y'', @LockTimeout=600, @LogToTable=''Y''';

		SET @out = @JobTokenLogDir + N'_dbaid_index_optimise_system_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_index_optimise_system', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_index_optimise_system')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_index_optimise_system';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_index_optimise_system',  
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=1, @freq_recurrence_factor=0, @active_start_time=0;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_integrity_check_user')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_integrity_check_user', @owner_login_name=@owner,
			@enabled=0, 
			@category_name=N'_dbaid_maintenance', 
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseIntegrityCheck] @Databases=''USER_DATABASES'', @CheckCommands=''CHECKDB'', @LockTimeout=600, @LogToTable=''Y''';

		SET @out = @JobTokenLogDir + N'_dbaid_integrity_check_user_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_integrity_check_user', 
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@command=@cmd, 
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_integrity_check_user')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_integrity_check_user';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_integrity_check_user',  
				@enabled=1, @freq_type=8, @freq_interval=1, @freq_subday_type=1, @freq_recurrence_factor=1, @active_start_time=40000;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_integrity_check_system')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_integrity_check_system', @owner_login_name=@owner,
			@enabled=0,
			@category_name=N'_dbaid_maintenance',
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [_dbaid].[dbo].[DatabaseIntegrityCheck] @Databases=''SYSTEM_DATABASES'', @CheckCommands=''CHECKDB'', @LockTimeout=600, @LogToTable=''Y''';

		SET @out = @JobTokenLogDir + N'_dbaid_integrity_check_system_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_integrity_check_system',
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@command=@cmd,
			@subsystem = N'TSQL',
			@output_file_name=@out,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_integrity_check_system')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_integrity_check_system';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_integrity_check_system',
				@enabled=1, @freq_type=8, @freq_interval=1, @freq_subday_type=1, @freq_recurrence_factor=1, @active_start_time=40000;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_set_ag_agent_job_state')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_set_ag_agent_job_state', @owner_login_name=@owner,
			@enabled=0,
			@category_name=N'_dbaid_ag_job_maintenance',
      		@description = N'Called from "_dbaid_set_ag_agent_job_state" alert. The alert and job are DISABLED by default and should remain disabled if manual failover is configured as if this server is restarted, the alert detects a failover event and enables/disables the jobs. However, failover doesn''t actually occur, and the alert doesn''t detect the primary coming back online to enable/disable the jobs. Both the alert and this job need to be enabled for jobs to be updated after failover.',
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [_dbaid].[system].[set_ag_agent_job_state] @ag_name = N''<Availability Group Name>'', @wait_seconds = 30;';

		SET @out = @JobTokenLogDir + N'_dbaid_integrity_check_system_' + @JobTokenDateTime + N'.log';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_set_ag_agent_job_state',
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, @subsystem=N'TSQL',
			@command=@cmd,
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_set_ag_agent_job_state')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_set_ag_agent_job_state';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId, @schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_set_ag_agent_job_state',
				@enabled=1, @freq_type=64, @freq_interval=0, @freq_subday_type=0, @freq_recurrence_factor=0, @active_start_time=0;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	SET @jobId = NULL;

	IF NOT EXISTS (SELECT [job_id] FROM [msdb].[dbo].[sysjobs_view] WHERE [name] = N'_dbaid_checkmk_writelog')
	BEGIN
	BEGIN TRANSACTION
		EXEC msdb.dbo.sp_add_job @job_name=N'_dbaid_checkmk_writelog', @owner_login_name=@owner,
			@enabled=0,
			@category_name=N'_dbaid_maintenance',
			@job_id = @jobId OUTPUT;

		SET @cmd = N'EXEC [checkmk].[inventory_agentjob]
EXEC [checkmk].[inventory_alwayson]
EXEC [checkmk].[inventory_database]
GO

EXEC [checkmk].[chart_capacity_fg] @writelog = 1
EXEC [checkmk].[check_agentjob] @writelog = 1
EXEC [checkmk].[check_alwayson] @writelog = 1
EXEC [checkmk].[check_backup] @writelog = 1
EXEC [checkmk].[check_database] @writelog = 1
EXEC [checkmk].[check_integrity] @writelog = 1
EXEC [checkmk].[check_logshipping] @writelog = 1
EXEC [checkmk].[check_mirroring] @writelog = 1
GO';

		EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'_dbaid_checkmk_writelog',
			@step_id=1, @cmdexec_success_code=0, @on_success_action=1, @on_fail_action=2, 
			@database_name=N'_dbaid',
			@command=@cmd,
			@subsystem = N'TSQL',
			@flags=2;

		IF EXISTS (SELECT TOP(1) [schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_checkmk_writelog')
		BEGIN
			SET @schid = NULL;
			SELECT TOP(1) @schid=[schedule_id] FROM msdb.dbo.sysschedules WHERE [name] = N'_dbaid_checkmk_writelog';
			EXEC msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schid
		END
		ELSE
			EXEC msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'_dbaid_checkmk_writelog',
				@enabled=1, @freq_type=4, @freq_interval=1, @freq_subday_type=4, @freq_subday_interval=15, @active_start_time=0;

		EXEC msdb.dbo.sp_add_jobserver @job_id=@jobId, @server_name = N'(local)';
	COMMIT TRANSACTION
	END

	

	/* Create SQL Agent alert */
	DECLARE @alertname NVARCHAR(128);
	SELECT @alertname = [name] FROM msdb.dbo.sysalerts WHERE [message_id] = 1480;

	IF (@alertname IS NOT NULL)
	BEGIN
		IF (SELECT [job_id] FROM msdb.dbo.sysalerts WHERE [name]=@alertname) = '00000000-0000-0000-0000-000000000000'
			EXEC msdb.dbo.sp_update_alert @name=@alertname, @job_name=N'_dbaid_set_ag_agent_job_state'
		ELSE PRINT N'WARNING: Cannot configure Agent alert for "_dbaid_set_ag_agent_job_state", as message_id 1480 is already configured.'
	END
	ELSE IF NOT EXISTS (SELECT 1 FROM msdb.dbo.sysalerts WHERE [name] = N'_dbaid_set_ag_agent_job_state')
		EXEC msdb.dbo.sp_add_alert @name = N'_dbaid_set_ag_agent_job_state', @message_id = 1480, @severity = 0, @enabled = 0, @delay_between_responses = 0, @include_event_description_in = 1, @job_name = N'_dbaid_set_ag_agent_job_state';
END
GO


