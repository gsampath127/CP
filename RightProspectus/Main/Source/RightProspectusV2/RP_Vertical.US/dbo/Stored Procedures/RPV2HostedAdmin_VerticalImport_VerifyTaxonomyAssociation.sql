CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_VerticalImport_VerifyTaxonomyAssociation]
@ClientMarketIds [TT_TaxonomyMarketId] READONLY
AS
BEGIN

	SELECT DISTINCT Prospectus.ProsID 'TaxonomyId', ProsTicker.CUSIP 'MarketId'
	FROM ProsTicker 
	INNER JOIN @ClientMarketIds ClientMarketIds ON ProsTicker.CUSIP = ClientMarketIds.MarketId
	INNER JOIN Prospectus ON Prospectus.ProsId = ProsTicker.ProspectusId

END 
Go