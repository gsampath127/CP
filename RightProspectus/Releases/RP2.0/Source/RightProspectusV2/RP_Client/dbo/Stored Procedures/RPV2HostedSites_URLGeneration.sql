CREATE PROCEDURE [dbo].[RPV2HostedSites_URLGeneration]
@MarketID NVARCHAR(100) = NULL,
@SearchSiteName NVARCHAR(100) = NULL
AS  
BEGIN

	DECLARE @SiteDetails TABLE(RowNum int, SiteId int, Name nvarchar(200), DefaultPageId int)	
	DECLARE @URLDetails TABLE(SiteName nvarchar(100),FundName nvarchar(200), TaxonomyId int, Level int,TAExternalID NVARCHAR(100),
								DTExternalID NVARCHAR(100), DocumentType NVARCHAR(100), DocumentTypeOrder int, XBRLFeatureMode int)

	DECLARE @SiteCount int, @RowCount int = 1, @DefaultPageID int, @SiteID int, @SiteName NVARCHAR(100), @XBRLFeatureMode int = 0
	
	-- FETCH Site Details based on Search creteria
	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]
	WHERE @SearchSiteName IS NULL OR Name = @SearchSiteName
	
	--Get the total site counts
	SELECT @SiteCount = COUNT(*) from @SiteDetails
	
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN
	
		SELECT @SiteID = SiteID, @SiteName = Name, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount

		SELECT @XBRLFeatureMode = FeatureMode FROm SiteFeature where [Key] = 'XBRL' and SiteId = @SiteID

		IF @DefaultPageID = 1 --TAL case
		BEGIN

		
			--logic for TAL
		INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		SELECT DISTINCT
	    @SiteName  AS SiteName,
		PTA.NameOverride AS FundName,
		PTA.TaxonomyId, PTA.Level,
		TALE.ExternalId,
		
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
	    CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,		
		@XBRLFeatureMode
      	FROM [dbo].[TaxonomyAssociationHierachy] TAH

		INNER JOIN [dbo].[TaxonomyAssociation] PTA ON TAH.ParentTaxonomyAssociationId = PTA.TaxonomyAssociationID 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID 
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = PTA.TaxonomyAssociationID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON PTA.TaxonomyId  = TALE.TaxonomyId AND PTA.[Level] = TALE.[Level]

	    WHERE (PTA.TaxonomyAssociationId IS NOT NULL)
	    And(PTA.MarketId=@MarketID or @MarketID is NULL)
			    
    UNION
        SELECT DISTINCT
	    @SiteName AS SiteName,
	    CTA.NameOverride,
		CTA.TaxonomyId, CTA.Level,
		TALE.ExternalId,
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
		
	  	CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
	    @XBRLFeatureMode
    	FROM [dbo].[TaxonomyAssociationHierachy] TAH

		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = CTA.TaxonomyAssociationID		
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
    	LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON CTA.TaxonomyId  = TALE.TaxonomyId AND CTA.[Level] = TALE.[Level]

	    WHERE CTA.TaxonomyAssociationId IS NOT NULL
	    And(CTA.MarketId=@MarketID or @MarketID is NULL)	    

    END
		
		
		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN

			--Logic for TAD

		INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		
		SELECT DISTINCT 
		@SiteName  AS SiteName,
		TA.NameOverride as FundName,	
		TA.TaxonomyId, TA.Level,
		TALE.ExternalID,
		
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		@XBRLFeatureMode		
		FROM [dbo].[TaxonomyAssociation] TA 
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
		WHERE TA.TaxonomyAssociationId IS NOT NULL AND TA.SiteId = @SiteID
		And(TA.MarketId=@MarketID or @MarketID is NULL)


	End
		  
		SET @RowCount = @RowCount + 1
	End


	SELECT * FROM @URLDetails t
	ORDER BY t.SiteName, t.FundName, t.TAExternalID, t.DocumentTypeOrder

END
Go
