-- =============================================
-- Author:		Noel Dsouza
-- Create date: 23rd-Sep-2015
-- RPV2HostedAdmin_GetTaxonomyNameForTaxonomyIDs
-- =============================================

CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetTaxonomyNameForTaxonomyIDs
@TaxonomyIds TT_Taxonomy readonly
AS
BEGIN
  Declare @CurrentLevel int
  SELECT TOP 1 @CurrentLevel=Level FROM @TaxonomyIds
  IF @CurrentLevel = 1 
  BEGIN
	  SELECT DISTINCT TaxonomyIds.TaxonomyID,
		 COMPANY.CompanyName + ' ' + Prospectus.ProsName AS TaxonomyName,
		 TaxonomyIds.[Level]
	  FROM Prospectus 
	  INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID
	  INNER JOIN @TaxonomyIds TaxonomyIds ON TaxonomyIds.TaxonomyID = Prospectus.ProsID
  END
END

