CREATE PROCEDURE [dbo].[RPV2HostedSites_GetRequestMaterialPrintRequestData]
@ReportFromDate DateTime,
@ReportToDate DateTime
AS
BEGIN
	SELECT  DISTINCT UniqueID,
		RMPH.RequestMaterialPrintHistoryID,
		RMPH.RequestDateUtc,
		RMPH.ClientCompanyName,
		RMPH.ClientFirstName,
		RMPH.ClientLastName, 
		RMPH.Address1,
		RMPH.Address2,
		RMPH.City,
		RMPH.StateOrProvince,
		RMPH.PostalCode,
		RMPPD.Quantity,
		TA.TaxonomyAssociationID,
		TA.TaxonomyID,
		TA.NameOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,
		RMSKU.SKUName
	FROM RequestMaterialPrintHistory RMPH
	INNER JOIN RequestMaterialPrintProsDetail RMPPD on RMPH.RequestMaterialPrintHistoryID = RMPPD.RequestMaterialPrintHistoryID
	INNER JOIN [dbo].[TaxonomyAssociation] TA ON RMPPD.TaxonomyAssociationId = TA.TaxonomyAssociationId
	LEFT JOIN [dbo].RequestMaterialSKUDetail RMSKU on TA.TaxonomyAssociationId = RMSKU.TaxonomyAssociationId AND RMPPD.DocumentTypeId = RMSKU.DocumentTypeId
	RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.DocumentTypeId = RMPPD.DocumentTypeId AND SiteLevelDTA.SiteId IS NOT NULL
	LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
	WHERE TA.TaxonomyAssociationId IS NOT NULL AND (RequestDateUtc BETWEEN  @ReportFromDate AND @ReportToDate)
	ORDER BY RMPH.RequestMaterialPrintHistoryID      

END
GO