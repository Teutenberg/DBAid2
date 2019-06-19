/*
Copyright (C) 2015 Datacom
GNU GENERAL PUBLIC LICENSE
Version 3, 29 June 2007
*/

CREATE PROCEDURE [system].[generate_secret] (
	@length INT = 20,
	@secret VARCHAR(100) OUTPUT
)
WITH ENCRYPTION 
AS
BEGIN
	WHILE 1=1
	BEGIN
		DECLARE @char CHAR;
		SELECT @char = CHAR(CAST(ROUND((122-48)*RAND()+48,0) AS INT)) OPTION(RECOMPILE);
		SELECT @secret = CONCAT(@secret, @char);

		IF (LEN(@secret) >= @length)
			BREAK;
		ELSE
			CONTINUE;
	END

	SELECT [secret]=@secret;
END
GO