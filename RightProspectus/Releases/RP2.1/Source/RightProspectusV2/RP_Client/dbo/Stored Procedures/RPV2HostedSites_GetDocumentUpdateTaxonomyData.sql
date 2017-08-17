CREATE PROCEDURE [dbo].[RPV2HostedSites_GetDocumentUpdateTaxonomyData]
AS
BEGIN

	SELECT DISTINCT MarketId, CASE WHEN ISNULL(NameOverride, '') != '' THEN CAST (1 AS BIT) ELSE  CAST (0 AS BIT) END 'IsNameOverrideProvided'
	FROM TaxonomyAssociation

END
GO