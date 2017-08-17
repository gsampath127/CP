CREATE PROCEDURE [dbo].[BCS_TransamericaInsertWatchListHistory]
@FileName nvarchar(1000)
AS
BEGIN

	DECLARE @dateReceived DATETIME = GETDATE()

	INSERT INTO BCSTransamericaFTP(dateReceived, fileName, isProcessed) VALUES(@dateReceived, @FileName, 0)

	UPDATE BCSClientWatchlistFileConfig
	SET LastFileReceivedDate = @dateReceived, MissedFileSLAEmailSent = NULL
	WHERE ClientPrefix = 'AEG'
	
End
GO