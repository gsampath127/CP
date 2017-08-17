-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
-- RPV2HostedAdmin_GetAllTaxonomyLevelExternalId
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTaxonomyLevelExternalId]
AS
BEGIN
	SELECT DISTINCT 
		   TLE.[Level],
		   TLE.TaxonomyId,
		   TA.NameOverride AS TaxonomyName,
		   TLE.ExternalId,
		   TLE.IsPrimary,
		   TLE.SendDocumentUpdate,
		   TLE.[UtcModifiedDate] as UtcLastModified,
		   TLE.[ModifiedBy]
	  FROM [TaxonomyLevelExternalId] TLE
	   INNER JOIN [TaxonomyAssociation]	TA ON TLE.[Level] = TA.[Level]
			AND TLE.TaxonomyId = TA.TaxonomyId
	  ORDER BY TLE.TaxonomyId
END
 