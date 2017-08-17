CREATE PROCEDURE [dbo].[BCS_AllianceBernsteinInsertWatchListHistory]
@FileName nvarchar(1000)
AS
BEGIN

	DECLARE @dateReceived DATETIME = GETDATE()

	INSERT INTO BCSAllianceBernsteinFTP(dateReceived, fileName, isProcessed) VALUES(@dateReceived, @FileName, 0)

	UPDATE BCSClientWatchlistFileConfig
	SET LastFileReceivedDate = @dateReceived, MissedFileSLAEmailSent = NULL
	WHERE ClientPrefix = 'AB'
	
End
GO