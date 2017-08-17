 CREATE PROCEDURE [dbo].[RPV2HostedAdmin_CUDHistory_CacheDependencyCheck]

AS

BEGIN

   	SELECT	CHD.CUDHistoryId, COUNT_BIG(*) AS Total

	FROM [CUDHistory] CH INNER JOIN [CUDHistoryData] CHD on
      
	 CH.CUDHistoryID=CHD.CUDHistoryID

	 GROUP BY CHD.CUDHistoryId,CHD.ColumnName

END