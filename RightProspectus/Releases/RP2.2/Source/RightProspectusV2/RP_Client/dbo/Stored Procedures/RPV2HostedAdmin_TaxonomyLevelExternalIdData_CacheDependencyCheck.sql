-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015
-- RPV2HostedAdmin_TaxonomyLevelExternalIdData_CacheDependencyCheck

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_TaxonomyLevelExternalIdData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	[Level],TaxonomyId,ExternalId, COUNT_BIG(*) AS Total
	FROM	dbo.TaxonomyLevelExternalId
	GROUP BY [Level],TaxonomyId,ExternalId;
END