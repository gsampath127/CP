CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationGroups]   --  exec RPV2HostedSites_GetTaxonomyAssociationGroups 6
	@TAGID INT=NULL,
	@SiteName NVARCHAR(100)=NULL
AS
BEGIN
DECLARE @SiteID INT	
	IF @SiteName IS NULL
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END
	SELECT 
		TaxonomyAssociationGroupId,
		Name,
		[Description],
		CssClass,
		ParentTaxonomyAssociationId
	FROM TaxonomyAssociationGroup 
	WHERE SiteId = @SiteID

	--Get All Taxonomies

	SELECT DISTINCT
		TAG.TaxonomyAssociationGroupId,	
		TAG.Name,
		TA.TaxonomyAssociationId,   
		TA.TaxonomyID,
		TA.NameOverride,
		TA.DescriptionOverride,
		TA.NameOverride AS TaxonomyNameOverRide,
		TA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		TA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,         
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) as DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder		
	FROM [dbo].[TaxonomyAssociationGroup] TAG
	    INNER JOIN [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] TAGTA ON TAG.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId 
																				AND (@TAGID IS NULL OR @TAGID = TAG.TaxonomyAssociationGroupId)
		INNER JOIN [dbo].[TaxonomyAssociation] TA ON TAGTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
		
	WHERE TA.TaxonomyAssociationId IS NOT NULL
	
	ORDER BY 1,3,14
END
GO

