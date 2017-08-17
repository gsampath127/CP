CREATE PROCEDURE [dbo].[RPV2HostedSites_VerifyTaxonomyByMarketId]
	@RPV2HostedSitesVerifyTaxonomyByMarketIdDataType [dbo].[RPV2HostedSitesVerifyTaxonomyByMarketIdDataType] READONLY
AS
BEGIN
	SELECT 
		ProsTicker.CUSIP, ProsTicker.SeriesID,
		CASE WHEN ClientTable.IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName
	FROM Prospectus  
		INNER JOIN ProsTicker ON ProsTicker.ProspectusID = Prospectus.ProsID
		INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID
		INNER JOIN @RPV2HostedSitesVerifyTaxonomyByMarketIdDataType ClientTable ON ProsTicker.CUSIP = ClientTable.MarketId
END