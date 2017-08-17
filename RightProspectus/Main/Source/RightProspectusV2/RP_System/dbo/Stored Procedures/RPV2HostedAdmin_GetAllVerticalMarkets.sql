CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllVerticalMarkets]
AS
BEGIN
	SELECT  VerticalMarkets.VerticalMarketId,
		VerticalMarkets.MarketName,
		VerticalMarkets.[MarketDescription],
		VerticalMarkets.UtcModifiedDate as UtcLastModified,
		VerticalMarkets.ModifiedBy as ModifiedBy	   
	FROM VerticalMarkets
	ORDER BY VerticalMarketId
END	
