/*



*/

CREATE PROCEDURE [health].[get_block_process_history]
WITH ENCRYPTION
AS
BEGIN
	DECLARE @xml XML;

	SELECT TOP(1) @xml = CAST([target_data] as XML)
	FROM sys.dm_xe_session_targets [t]
		INNER JOIN sys.dm_xe_sessions [s]
			ON  [t].[event_session_address] = [s].[address]
	WHERE [t].[target_name] = 'ring_buffer'
		AND [s].[name] = 'blocking';

	SELECT [xml_blocked_report] = [n].[data].[query]('.')
	FROM @xml.nodes('//RingBufferTarget/event') AS [n]([data])
	WHERE [n].[data].[value]('@name','VARCHAR(4000)') = 'blocked_process_report'
END
