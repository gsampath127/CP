-- =============================================
-- Author:		Noel Dsouza
-- Create date: 2nd-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTaxonomy
AS
BEGIN
  SELECT DISTINCT 		
		[LEVEL],		
		TaxonomyId,
		NameOverride AS TaxonomyName
	FROM TaxonomyAssociation
END