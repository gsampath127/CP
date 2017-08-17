
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationHierarchy]
	@InternalPTAID INT=null,
	@ExternalID NVARCHAR(100)=null,
	@SiteName nvarchar(100)=null,
	@IsProofing BIT
AS
BEGIN
	DECLARE @SiteID int
	
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END

	Declare @InternalPTAIDToQueryOn INT
	
	IF @InternalPTAID is not null
		BEGIN
		  SET @InternalPTAIDToQueryOn= @InternalPTAID
		END
	ELSE IF @ExternalID IS NOT NULL
		BEGIN
			SELECT TOP 1 @InternalPTAIDToQueryOn = TAH.ParentTaxonomyAssociationId
			FROM TaxonomyAssociationHierachy TAH 
			  INNER JOIN TaxonomyAssociation TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId AND TA.IsProofing = @IsProofing
			  INNER JOIN TaxonomyLevelExternalId TLE on TA.TaxonomyId = TLE.TaxonomyId AND 	TA.[Level] = TLE.[Level]		  
			WHERE TLE.ExternalId = @ExternalID AND TA.SiteId = @SiteID
		END
	ELSE
		BEGIN
			SELECT TOP 1 @InternalPTAIDToQueryOn = TAH.ParentTaxonomyAssociationId
			FROM TaxonomyAssociationHierachy TAH 
			  INNER JOIN TaxonomyAssociation TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId AND TA.IsProofing = @IsProofing
			  INNER JOIN TaxonomyLevelExternalId TLE on TA.TaxonomyId = TLE.TaxonomyId AND 	TA.[Level] = TLE.[Level]		  
			WHERE TA.SiteId = @SiteID
		END

	SELECT DISTINCT
		PTA.TaxonomyAssociationId,   
		PTA.TaxonomyID,
		PTA.NameOverride,
		PTA.DescriptionOverride,
		1 AS IsParent,		
		PTA.NameOverride AS TaxonomyNameOverRide,
		PTA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		PTA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,         
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) as DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder,
		isnull(PTA.[Order],0) TaxonomyOrder
	FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] PTA ON TAH.ParentTaxonomyAssociationId = PTA.TaxonomyAssociationID AND TAH.ParentTaxonomyAssociationID = @InternalPTAIDToQueryOn AND PTA.IsProofing = @IsProofing 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.IsProofing = @IsProofing 
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = PTA.TaxonomyAssociationID And TALevelDTA.IsProofing = @IsProofing
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON PTA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON PTA.TaxonomyId  = TALE.TaxonomyId AND PTA.[Level] = TALE.[Level]
		
	WHERE PTA.TaxonomyAssociationId IS NOT NULL
	
	UNION
	
	SELECT DISTINCT
		CTA.TaxonomyAssociationID,
		CTA.TaxonomyID,
		CTA.NameOverride,
		CTA.DescriptionOverride,
		0 AS IsParent,
		CTA.NameOverride AS TaxonomyNameOverRide,
		CTA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		CTA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,            
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeNameLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder,
		isnull(TAH.[Order],0) TaxonomyOrder
	FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID AND TAH.ParentTaxonomyAssociationID = @InternalPTAIDToQueryOn AND CTA.IsProofing = @IsProofing 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.IsProofing = @IsProofing
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = CTA.TaxonomyAssociationID And TALevelDTA.IsProofing = @IsProofing
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON CTA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON CTA.TaxonomyId  = TALE.TaxonomyId AND CTA.[Level] = TALE.[Level]
	WHERE CTA.TaxonomyAssociationId IS NOT NULL
	ORDER BY 5,1,13        
        
END        
