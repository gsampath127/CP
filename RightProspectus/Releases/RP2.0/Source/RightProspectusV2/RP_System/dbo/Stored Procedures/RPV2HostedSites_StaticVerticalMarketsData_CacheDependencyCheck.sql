CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticVerticalMarketsData_CacheDependencyCheck]
AS
BEGIN
  
	SELECT	VerticalMarketId, COUNT_BIG(*) AS Total
	FROM	dbo.VerticalMarkets
	GROUP BY VerticalMarketId;

END