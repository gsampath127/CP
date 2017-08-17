CREATE PROCEDURE [dbo].[EdgarOnline_SetEdgarOnlineFeedFileProcessed]
@FileName nvarchar(200)
AS
BEGIN

	Update EdgarOnlineFeedFileHistory
	SET IsProcessed = 1
	WHERE IsProcessed = 0 and FileName = @FileName
	
End
GO