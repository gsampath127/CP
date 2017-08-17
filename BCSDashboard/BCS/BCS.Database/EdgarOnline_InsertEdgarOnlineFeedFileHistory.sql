CREATE PROCEDURE [dbo].[EdgarOnline_InsertEdgarOnlineFeedFileHistory]
@FileName nvarchar(200)
AS
BEGIN

	INSERT INTO EdgarOnlineFeedFileHistory(DateReceived, FileName) VALUES(GETDATE(), @FileName)	
	
End
GO