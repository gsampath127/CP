CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAL]
@SiteId Int
AS
BEGIN

	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
	
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


	
	--DELETE Records from TaxonomyAssociation Only - Parent. Child will be at customer level and will be deleted only if no proofing market id exists
	--Its is handled in RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation SP

	DECLARE @TAIdsToBeDeleted TABLE(TaxonomyAssociationId INT)
	INSERT INTO @TAIdsToBeDeleted
		SELECT prod.TaxonomyAssociationId
		FROM TaxonomyAssociation prod
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prod.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
		WHERE TAFSProd.MarketId NOT IN (SELECT MarketId FROM @TaxonomyAssociationIdsForSite TAFSProofing where TAFSProofing.isproofing = 1)	 
		and prod.SiteId = @SiteId_X

	--DELETE Records from DocumentTypeAssociation and then delete from TaxonomyAssociation
	DELETE FROM DocumentTypeAssociation
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	DELETE FROM TaxonomyAssociationHierachy
	WHERE ParentTaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)
	
	DELETE FROM Footnote
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	DELETE FROM TaxonomyAssociation
	Where TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	
	
	 
	-- Update Records at site level
	DECLARE @TAIdsToBeUpdated TABLE(TaxonomyAssociationId INT)
	INSERT INTO @TAIdsToBeUpdated
		SELECT prodTA.TaxonomyAssociationId 	
		FROM TaxonomyAssociation prodTA
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodTA.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
		INNER JOIN (
	
					SELECT proofing.* 
					FROM TaxonomyAssociation proofing
					INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofing.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

		)proofingTA on proofingTA.MarketId = prodTA.MarketId
		WHERE
		prodTA.NameOverride <> proofingTA.NameOverride
		OR prodTA.DescriptionOverride <> proofingTA.DescriptionOverride
		OR prodTA.CssClass <> proofingTA.CssClass
		OR prodTA.TabbedPageNameOverride <> proofingTA.TabbedPageNameOverride
		OR prodTA.[Order] <> proofingTA.[Order]



	UPDATE prodTA
	SET 
		prodTA.NameOverride = proofingTA.NameOverride,
		prodTA.DescriptionOverride = proofingTA.DescriptionOverride,
		prodTA.CssClass = proofingTA.CssClass,
		prodTA.UtcModifiedDate = GETUTCDATE(),
		prodTA.ModifiedBy=proofingTA.ModifiedBy,
		prodTA.[Order] = proofingTA.[Order],
		prodTA.TabbedPageNameOverride = proofingTA.TabbedPageNameOverride
	FROM TaxonomyAssociation prodTA
	INNER JOIN @TAIdsToBeUpdated TAFSProd On prodTA.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId
	INNER JOIN (
	
				SELECT proofing.* 
				FROM TaxonomyAssociation proofing
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofing.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

	)proofingTA on proofingTA.MarketId = prodTA.MarketId


	--INSERT Records into TaxonomyAssociation
	INSERT INTO TaxonomyAssociation([Level],TaxonomyId,SiteId,ParentTaxonomyAssociationId,NameOverride,DescriptionOverride,
				CssClass,MarketId,UtcModifiedDate,ModifiedBy,IsProofing, [Order], TabbedPageNameOverride)
	SELECT proofingTA.[Level],proofingTA.TaxonomyId,proofingTA.SiteId,proofingTA.ParentTaxonomyAssociationId,proofingTA.NameOverride,proofingTA.DescriptionOverride,
			proofingTA.CssClass,proofingTA.MarketId,proofingTA.UtcModifiedDate,proofingTA.ModifiedBy,0, proofingTA.[Order], proofingTA.TabbedPageNameOverride
	FROM TaxonomyAssociation proofingTA
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofingTA.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	LEFT JOIN @TaxonomyAssociationIdsForSite TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProd.IsProofing = 0
	LEFT JOIN TaxonomyAssociation ON TaxonomyAssociation.MarketId = proofingTA.MarketId AND TaxonomyAssociation.SiteId IS NULL AND TaxonomyAssociation.IsProofing = 0 -- Added this to avoid duplicate records
	WHERE TAFSProd.MarketId IS NULL And TaxonomyAssociation.TaxonomyAssociationId IS NULL


	-----------------------------------------INSERT/Delete INTO TaxonomyAssociationHierachy ---------------------------------------
	DECLARE @TAMapping table(ProofingTAId int, ProdTAId int)

	INSERT INTO @TAMapping
	SELECT  DISTINCT proofingTA.TaxonomyAssociationId, prodTA.TaxonomyAssociationId
	FROM TaxonomyAssociation proofingTA
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofingTA.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	INNER JOIN (
		
				SELECT prod.* 
				FROM TaxonomyAssociation prod				
				where ISNULL(SiteId , @SiteId_X) = @SiteId_X AND IsProofing = 0
	)prodTA On  prodTA.MarketId = proofingTA.MarketId


	
	

	DELETE ProdTAHD		
	FROM TaxonomyAssociationHierachy ProdTAHD
	INNER JOIN(

			SELECT parent.TaxonomyAssociationId, parent.MarketId 
			FROM TaxonomyAssociation parent		
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On parent.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

	)prodParentTA ON prodParentTA.TaxonomyAssociationId = ProdTAHD.ParentTaxonomyAssociationId
	INNER JOIN(

			SELECT child.TaxonomyAssociationId, child.MarketId 
			FROM TaxonomyAssociation child		
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On child.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

	)prodChildTA ON prodChildTA.TaxonomyAssociationId = ProdTAHD.ChildTaxonomyAssociationId	
	LEFT JOIN(
			
			SELECT profParentTA.MarketId 'profParentTAMarketId', profChildTA.MarketId 'profChildTAMarketId'
			FROM TaxonomyAssociationHierachy TAHD
			INNER JOIN(

				SELECT parent.TaxonomyAssociationId, parent.MarketId 
				FROM TaxonomyAssociation parent		
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProf On parent.TaxonomyAssociationId = TAFSProf.TaxonomyAssociationId AND TAFSProf.IsProofing = 1

			)profParentTA ON profParentTA.TaxonomyAssociationId = TAHD.ParentTaxonomyAssociationId
			INNER JOIN(

				SELECT child.TaxonomyAssociationId, child.MarketId 
				FROM TaxonomyAssociation child		
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProf On child.TaxonomyAssociationId = TAFSProf.TaxonomyAssociationId AND TAFSProf.IsProofing = 1

			)profChildTA ON profChildTA.TaxonomyAssociationId = TAHD.ChildTaxonomyAssociationId


	)proofingTAHD ON proofingTAHD.profParentTAMarketId = prodParentTA.MarketId AND proofingTAHD.profChildTAMarketId = prodChildTA.MarketId
	WHERE proofingTAHD.profChildTAMarketId IS NULL


	-----------------------------------------Update TaxonomyAssociationHierachy ---------------------------------------
	DECLARE @TAHDsToBeUpdated TABLE(ParentTaxonomyAssociationId INT, ChildTaxonomyAssociationId INT, NewOrder INT)

	INSERT INTO @TAHDsToBeUpdated
		SELECT ProdTAH.ParentTaxonomyAssociationId, ProdTAH.ChildTaxonomyAssociationId, proofingTAHD.[Order]
		FROM TaxonomyAssociationHierachy ProdTAH
		INNER JOIN(

				SELECT parent.TaxonomyAssociationId, parent.MarketId 
				FROM TaxonomyAssociation parent		
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On parent.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

		)prodParentTA ON prodParentTA.TaxonomyAssociationId = ProdTAH.ParentTaxonomyAssociationId
		INNER JOIN(

			SELECT child.TaxonomyAssociationId, child.MarketId 
			FROM TaxonomyAssociation child		
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On child.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

		)prodChildTA ON prodChildTA.TaxonomyAssociationId = ProdTAH.ChildTaxonomyAssociationId
		INNER JOIN(

				SELECT profParentTA.MarketId 'profParentTAMarketId', profChildTA.MarketId 'profChildTAMarketId', TAHD.[Order], TAHD.RelationshipType
				FROM TaxonomyAssociationHierachy TAHD
				INNER JOIN(

					SELECT parent.TaxonomyAssociationId, parent.MarketId 
					FROM TaxonomyAssociation parent		
					INNER JOIN @TaxonomyAssociationIdsForSite TAFSProf On parent.TaxonomyAssociationId = TAFSProf.TaxonomyAssociationId AND TAFSProf.IsProofing = 1

				)profParentTA ON profParentTA.TaxonomyAssociationId = TAHD.ParentTaxonomyAssociationId
				INNER JOIN(

					SELECT child.TaxonomyAssociationId, child.MarketId 
					FROM TaxonomyAssociation child		
					INNER JOIN @TaxonomyAssociationIdsForSite TAFSProf On child.TaxonomyAssociationId = TAFSProf.TaxonomyAssociationId AND TAFSProf.IsProofing = 1

				)profChildTA ON profChildTA.TaxonomyAssociationId = TAHD.ChildTaxonomyAssociationId
	
		)proofingTAHD ON proofingTAHD.profParentTAMarketId = prodParentTA.MarketId AND proofingTAHD.profChildTAMarketId = prodChildTA.MarketId
		WHERE ProdTAH.[Order] <> proofingTAHD.[Order]


	UPDATE prodTAHD
	SET prodTAHD.[Order] = proofTAHD.NewOrder
	FROM TaxonomyAssociationHierachy prodTAHD
	INNER JOIN @TAHDsToBeUpdated proofTAHD ON proofTAHD.ParentTaxonomyAssociationId = prodTAHD.ParentTaxonomyAssociationId AND
											  proofTAHD.ChildTaxonomyAssociationId = prodTAHD.ChildTaxonomyAssociationId



	INSERT INTO TaxonomyAssociationHierachy(ParentTaxonomyAssociationId, ChildTaxonomyAssociationId, RelationshipType, [Order], UtcModifiedDate, ModifiedBy)
		SELECT TAMappingParent.ProdTAId, TAMappingChild.ProdTAId, proofingTAHD.RelationshipType, proofingTAHD.[Order], proofingTAHD.UtcModifiedDate, proofingTAHD.ModifiedBy
		FROM TaxonomyAssociationHierachy proofingTAHD	
		INNER JOIN(

			SELECT parent.TaxonomyAssociationId, parent.MarketId 
			FROM TaxonomyAssociation parent		
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On parent.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

		)proofingParentTA ON proofingParentTA.TaxonomyAssociationId = proofingTAHD.ParentTaxonomyAssociationId
		INNER JOIN(

			SELECT child.TaxonomyAssociationId, child.MarketId 
			FROM TaxonomyAssociation child		
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On child.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1

		)proofingChildTA ON proofingChildTA.TaxonomyAssociationId = proofingTAHD.ChildTaxonomyAssociationId
		INNER JOIN @TAMapping TAMappingParent ON proofingTAHD.ParentTaxonomyAssociationId = TAMappingParent.ProofingTAId
		INNER JOIN @TAMapping TAMappingChild ON proofingTAHD.ChildTaxonomyAssociationId = TAMappingChild.ProofingTAId
		LEFT JOIN(

			SELECT prodParentTA.MarketId 'prodParentTAMarketId', prodChildTA.MarketId 'prodChildTAMarketId'
			FROM TaxonomyAssociationHierachy TAHD
			INNER JOIN(

				SELECT parent.TaxonomyAssociationId, parent.MarketId 
				FROM TaxonomyAssociation parent		
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On parent.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

			)prodParentTA ON prodParentTA.TaxonomyAssociationId = TAHD.ParentTaxonomyAssociationId
			INNER JOIN(

				SELECT child.TaxonomyAssociationId, child.MarketId 
				FROM TaxonomyAssociation child		
				INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On child.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

			)prodChildTA ON prodChildTA.TaxonomyAssociationId = TAHD.ChildTaxonomyAssociationId
	
		)prodTAHD ON prodTAHD.prodParentTAMarketId = proofingParentTA.MarketId AND prodTAHD.prodChildTAMarketId = proofingChildTA.MarketId
		WHERE prodTAHD.prodChildTAMarketId IS NULL
	------------------------------------------------------------------------------------------------------------

END
GO