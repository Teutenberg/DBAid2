/*
Copyright (C) 2015 Datacom
GNU GENERAL PUBLIC LICENSE
Version 3, 29 June 2007
*/

CREATE PROCEDURE [checkmk].[inventory_database]
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	/* Inventory check_database state_desc */
	MERGE INTO [checkmk].[config_database] [target]
	USING sys.databases [source]
	ON [target].[name] = [source].[name] COLLATE DATABASE_DEFAULT
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([name]) VALUES ([source].[name])
	WHEN MATCHED THEN
		UPDATE SET [target].[backup_check_tran_hour] = CASE WHEN [source].[recovery_model_desc] = 'SIMPLE' THEN NULL ELSE 1 END
			,[target].[inventory_date] = GETDATE()
	WHEN NOT MATCHED BY SOURCE AND [target].[inventory_date] < DATEADD(DAY, -7, GETDATE()) THEN
		DELETE;

	/* Change defaults for tempdb - e.g. we don't want backup or integrity check alerts as it gets neither done on it */
	UPDATE [checkmk].[config_database]
	SET backup_check_enabled = 0, 
		integrity_check_enabled = 0, 
		logshipping_check_enabled = 0, 
		mirroring_check_enabled = 0
	WHERE [name] = N'tempdb';
END

