CREATE PROCEDURE [dbo].[RPV2HostedSites_CUSIPMergerLiqudationReportData]
@TT_TaxonomyIdMarketId [dbo].[TT_TaxonomyIdMarketId] READONLY
AS
BEGIN
	SELECT 
		tt.marketId,
		CASE WHEN ProsTicker.CUSIP IS NULL THEN 'Liquidated' ELSE 'Merged' END As 'Status',	
		tt.clientName
	FROM @TT_TaxonomyIdMarketId tt
	LEFT JOIN ProsTicker ON tt.marketId = ProsTicker.CUSIP
	WHERE ProsTicker.CUSIP IS NULL OR 
	tt.taxonomyId <> ProsTicker.ProspectusID 
END
GO