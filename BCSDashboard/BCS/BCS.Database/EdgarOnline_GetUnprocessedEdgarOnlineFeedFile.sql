CREATE PROCEDURE [dbo].[EdgarOnline_GetUnprocessedEdgarOnlineFeedFile]
AS
BEGIN

	SELECT [FileName] FROM EdgarOnlineFeedFileHistory WHERE IsProcessed = 0 Order By EdgarOnlineFeedFileHistoryID
	
End
GO