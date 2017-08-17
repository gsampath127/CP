--Created By : Noel Dsouza
--Created Date : 10/16/2015
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetTaxonomyIdsForMarketIDs]
	@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY	
AS
BEGIN
  SELECT TaxonomyMarketIDs.[level],
		TaxonomyMarketIDs.marketId,
		CASE TaxonomyMarketIDs.[level]
			When 0 Then Company.CompanyID
			When 1 Then Prospectus.ProsID 
		END AS TaxonomyID
   FROM @TaxonomyMarketIDs TaxonomyMarketIDs
   INNER JOIN Prosticker ON TaxonomyMarketIDs.marketId = ProsTicker.CUSIP
   INNER JOIN Prospectus on Prosticker.ProspectusID = Prospectus.ProsID
   INNER JOIN Company on Prospectus.CompanyID = Company.CompanyID  
END