
CREATE PROCEDURE [dbo].RPV2HostedSites_GetTaxonomyAssociationDocuments
@SiteName nvarchar(100)=null,
@IsProofing BIT
as
Begin
	DECLARE @SiteID int
	
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END

	
	SELECT DISTINCT 
		TA.TaxonomyAssociationID,
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
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		isnull(Footnote.[Order],0) FootnoteOrder,
		isnull(TA.[Order],0) TaxonomyOrder
	FROM [dbo].[TaxonomyAssociation] TA 
		--LEFT OUTER JOIN [dbo].[TaxonomyAssociationHierachy] TAH on TA.TaxonomyAssociationId = TAH.ParentTaxonomyAssociationId
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.IsProofing = @IsProofing
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID AND TALevelDTA.IsProofing = @IsProofing
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
	WHERE TA.TaxonomyAssociationId IS NOT NULL 
	--and TAH.ParentTaxonomyAssociationId is null 
	and TA.SiteId = @SiteID
	And TA.IsProofing = @IsProofing
	ORDER BY 1,12        

END