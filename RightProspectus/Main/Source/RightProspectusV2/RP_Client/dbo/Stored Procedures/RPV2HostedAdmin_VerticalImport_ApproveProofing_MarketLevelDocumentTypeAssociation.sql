CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_MarketLevelDocumentTypeAssociation]
@SiteId Int
AS
BEGIN

	
	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
	DECLARE @DefaultPageID int
	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId_X
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		
		SELECT DISTINCT TaxonomyAssociationId, MarketId, isproofing           
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteId_X

		UNION
   
		SELECT CTA.TaxonomyAssociationId, CTA.MarketId, CTA.isproofing  
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
			SELECT TaxonomyAssociationID FROM TaxonomyAssociation WHERE SiteId = @SiteId_X
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL		
		
	END
	ELSE IF @DefaultPageID = 4 --TAD case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		SELECT DISTINCT TaxonomyAssociationId, MarketId, isproofing FROM TaxonomyAssociation WHERE SiteID = @SiteId_X
	END
	ELSE IF @DefaultPageID = 7 --TAD case
	BEGIN

		;WITH TopLevelTaxonomyAssociationGroup 
		AS (SELECT 
				@SiteId_X AS SiteId,
				TaxonomyAssociationGroupId                
        FROM   TaxonomyAssociationGroup 
        WHERE  SiteID = @SiteId_X 

        UNION ALL 
        SELECT 				
				NULL AS SiteId,
				TAG.TaxonomyAssociationGroupId               
        FROM   TopLevelTaxonomyAssociationGroup TLTAG
        INNER JOIN TaxonomyAssociationGroup TAG ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)

		INSERT INTO @TaxonomyAssociationIdsForSite
			SELECT DISTINCT TaxonomyAssociationId, MarketId, IsProofing FROM TopLevelTaxonomyAssociationGroup
			LEFT JOIN (
				SELECT TaxonomyAssociationGroupId, TaxonomyAssociation.TaxonomyAssociationId, MarketId, IsProofing
				FROM TaxonomyAssociationGroupTaxonomyAssociation 
				INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TaxonomyAssociationGroupTaxonomyAssociation.TaxonomyAssociationId
			)TAGTA ON TopLevelTaxonomyAssociationGroup.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
			WHERE TaxonomyAssociationId IS NOT NULL
	END

	--------------------------------------Document Type Association At Taxonomy Level
	
	--Deleting Records at Taxonomy level
	
	DELETE prodDTA
	FROM DocumentTypeAssociation prodDTA
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodDTA.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0		
	LEFT JOIN 
	(
		SELECT DT.*, TAFSProofing.MarketId 'TaxonomyMarketId'
		FROM DocumentTypeAssociation DT
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On DT.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

	) proofingDTA on proofingDTA.DocumentTypeId = prodDTA.DocumentTypeId AND proofingDTA.TaxonomyMarketId = TAFSProd.MarketId
	WHERE proofingDTA.DocumentTypeId IS NULL
	
		
	-- Update Records at Taxonomy level
	DECLARE @MLDTAsToBeUpdated TABLE(DocumentTypeAssociationId INT)
	INSERT INTO @MLDTAsToBeUpdated
		SELECT prodDTA.DocumentTypeAssociationId
		FROM DocumentTypeAssociation prodDTA
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodDTA.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
		INNER JOIN 
		(
			SELECT DT.*, TAFSProofing.MarketId 'TaxonomyMarketId'
			FROM DocumentTypeAssociation DT
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On DT.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

		) proofing on proofing.DocumentTypeId = prodDTA.DocumentTypeId AND proofing.TaxonomyMarketId = TAFSProd.MarketId


	UPDATE prodDTA
	SET prodDTA.[Order] = proofing.[Order],		
		prodDTA.HeaderText = proofing.HeaderText,
		prodDTA.LinkText = proofing.LinkText,
		prodDTA.DescriptionOverride = proofing.DescriptionOverride,
		prodDTA.CssClass = proofing.CssClass,
		prodDTA.UtcModifiedDate = GETUTCDATE(),
		prodDTA.ModifiedBy=proofing.ModifiedBy
	FROM DocumentTypeAssociation prodDTA
	INNER JOIN @MLDTAsToBeUpdated MLDTA ON MLDTA.DocumentTypeAssociationId = prodDTA.DocumentTypeAssociationId
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodDTA.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
	INNER JOIN 
	(
		SELECT DT.*, TAFSProofing.MarketId 'TaxonomyMarketId'
		FROM DocumentTypeAssociation DT
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On DT.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

	) proofing on proofing.DocumentTypeId = prodDTA.DocumentTypeId AND proofing.TaxonomyMarketId = TAFSProd.MarketId

	--Insert Records at Taxonomy level
	INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,TaxonomyAssociationId,[Order],HeaderText,LinkText,DescriptionOverride,
			CssClass,MarketId,UtcModifiedDate,ModifiedBy,IsProofing)
	SELECT proofingDTA.DocumentTypeId,proofingDTA.SiteId,TAFSProd.TaxonomyAssociationId,proofingDTA.[Order],proofingDTA.HeaderText,
		   proofingDTA.LinkText,proofingDTA.DescriptionOverride,
		   proofingDTA.CssClass,proofingDTA.MarketId,proofingDTA.UtcModifiedDate,proofingDTA.ModifiedBy,0
	FROM DocumentTypeAssociation proofingDTA
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofingDTA.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProd.IsProofing = 0		
	LEFT JOIN 
	(
		SELECT DT.*, TAFSProd.MarketId 'TaxonomyMarketId'
		FROM DocumentTypeAssociation DT
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On DT.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

	)prodDTA on proofingDTA.DocumentTypeId = prodDTA.DocumentTypeId	AND prodDTA.TaxonomyMarketId = TAFSProofing.MarketId												
	WHERE prodDTA.DocumentTypeId IS NULL

END
Go
