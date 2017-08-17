CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAGD]
@SiteId Int
AS
BEGIN

	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.
	----------------------------------------------------Start: TaxonomyAssociation-------------------------------------------------------------------------------

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
		
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
	
	
	 
	-- Update Records
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
	SELECT proofing.[Level],proofing.TaxonomyId,proofing.SiteId,proofing.ParentTaxonomyAssociationId,proofing.NameOverride,proofing.DescriptionOverride,
			proofing.CssClass,proofing.MarketId,proofing.UtcModifiedDate,proofing.ModifiedBy,0, proofing.[Order], proofing.TabbedPageNameOverride
	FROM TaxonomyAssociation proofing
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofing.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	LEFT JOIN @TaxonomyAssociationIdsForSite TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProd.IsProofing = 0	
	LEFT JOIN TaxonomyAssociation ON TaxonomyAssociation.MarketId = proofing.MarketId AND TaxonomyAssociation.SiteId IS NULL AND TaxonomyAssociation.IsProofing = 0 -- Added this to avoid duplicate records
	WHERE TAFSProd.MarketId IS NULL And TaxonomyAssociation.TaxonomyAssociationId IS NULL

	--INSERT Records into TaxonomyAssociation
	INSERT INTO TaxonomyAssociation([Level],TaxonomyId,SiteId,ParentTaxonomyAssociationId,NameOverride,DescriptionOverride,
				CssClass,MarketId,UtcModifiedDate,ModifiedBy,IsProofing, [Order], TabbedPageNameOverride)
	SELECT proofing.[Level],proofing.TaxonomyId,proofing.SiteId,proofing.ParentTaxonomyAssociationId,proofing.NameOverride,proofing.DescriptionOverride,
			proofing.CssClass,proofing.MarketId,proofing.UtcModifiedDate,proofing.ModifiedBy,0, proofing.[Order], proofing.TabbedPageNameOverride
	FROM TaxonomyAssociation proofing
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofing.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	LEFT JOIN @TaxonomyAssociationIdsForSite TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProd.IsProofing = 0	
	LEFT JOIN TaxonomyAssociation ON TaxonomyAssociation.MarketId = proofing.MarketId AND TaxonomyAssociation.SiteId IS NULL AND TaxonomyAssociation.IsProofing = 0 -- Added this to avoid duplicate records
	WHERE TAFSProd.MarketId IS NULL And TaxonomyAssociation.TaxonomyAssociationId IS NULL

	----------------------------------------------------END: TaxonomyAssociation-------------------------------------------------------------------------------

	----------------------------------------------------Start: TaxonomyAssociationGroup-------------------------------------------------------------------------------
	


	DECLARE @TaxonomyAssociationGroupIdForSite TABLE(TaxonomyAssociationGroupId int, UniqueGroupId uniqueidentifier, isproofing bit)
		
	;WITH TopLevelTaxonomyAssociationGroup 
    AS (SELECT 
				@SiteId_X AS SiteId,
				TaxonomyAssociationGroupId, 
                ParentTaxonomyAssociationGroupId,
				ParentTaxonomyAssociationId,
				IsProofing,
				UniqueGroupId
        FROM   TaxonomyAssociationGroup 
        WHERE  SiteID = @SiteId_X 

        UNION ALL 
        SELECT 				
				NULL AS SiteId,
				TAG.TaxonomyAssociationGroupId, 
                TAG.ParentTaxonomyAssociationGroupId,
				TAG.ParentTaxonomyAssociationId,
				TAG.IsProofing,
				TAG.UniqueGroupId
        FROM   TopLevelTaxonomyAssociationGroup TLTAG
        INNER JOIN TaxonomyAssociationGroup TAG ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)

		INSERT INTO @TaxonomyAssociationGroupIdForSite
			SELECT DISTINCT TaxonomyAssociationGroupId, UniqueGroupId, IsProofing FROM TopLevelTaxonomyAssociationGroup
	
	

	--DELETE Records from TaxonomyAssociation
	DECLARE @TAGIdsToBeDeleted TABLE(TaxonomyAssociationGroupId INT)
	INSERT INTO @TAGIdsToBeDeleted
		SELECT prod.TaxonomyAssociationGroupId
		FROM TaxonomyAssociationGroup prod
		INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd On prod.TaxonomyAssociationGroupId = TAGFSProd.TaxonomyAssociationGroupId AND TAGFSProd.IsProofing = 0
		LEFT JOIN @TaxonomyAssociationGroupIdForSite TAGFSProofing on TAGFSProd.UniqueGroupId = TAGFSProofing.UniqueGroupId and TAGFSProofing.isproofing = 1
		WHERE TAGFSProofing.UniqueGroupId IS NULL


	--DELETE Records from TaxonomyAssociationGroupTaxonomyAssociation and then delete from TaxonomyAssociationGroup
	DELETE FROM TaxonomyAssociationGroupTaxonomyAssociation
	WHERE TaxonomyAssociationGroupId IN(SELECT TaxonomyAssociationGroupId from @TAGIdsToBeDeleted)

	DELETE FROM Footnote
	WHERE TaxonomyAssociationGroupId IN(SELECT TaxonomyAssociationGroupId from @TAGIdsToBeDeleted)

	DELETE FROM TaxonomyAssociationGroup
	Where TaxonomyAssociationGroupId IN(SELECT TaxonomyAssociationGroupId from @TAGIdsToBeDeleted)

	
	 
	-- Update Records
	UPDATE TAG
	SET TAG.Name = proofingTAG.Name,
		TAG.[Description] = proofingTAG.[Description],
		TAG.SiteId = proofingTAG.SiteId,
		TAG.CssClass = proofingTAG.CssClass,
		TAG.UtcModifiedDate = GETUTCDATE(),
		TAG.ModifiedBy=proofingTAG.ModifiedBy,
		TAG.[Order] = proofingTAG.[Order]		
	FROM TaxonomyAssociationGroup TAG
	INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd On TAG.TaxonomyAssociationGroupId = TAGFSProd.TaxonomyAssociationGroupId AND TAGFSProd.IsProofing = 0	
	INNER JOIN (
	
				SELECT proofing.* 
				FROM TaxonomyAssociationGroup proofing
				INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProf On proofing.TaxonomyAssociationGroupId = TAGFSProf.TaxonomyAssociationGroupId 
																		AND TAGFSProf.IsProofing = 1

	)proofingTAG on proofingTAG.UniqueGroupId = TAGFSProd.UniqueGroupId



	--INSERT Records into TaxonomyAssociationGroup
	INSERT INTO TaxonomyAssociationGroup(Name,[Description],SiteId,ParentTaxonomyAssociationId,CssClass,
				UtcModifiedDate,ModifiedBy,[Order],IsProofing,UniqueGroupId)
	SELECT proofing.Name,proofing.[Description],proofing.SiteId,proofing.ParentTaxonomyAssociationId,proofing.CssClass,
			proofing.UtcModifiedDate,proofing.ModifiedBy,proofing.[Order], 0, proofing.UniqueGroupId
	FROM TaxonomyAssociationGroup proofing
	INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProofing On proofing.TaxonomyAssociationGroupId = TAGFSProofing.TaxonomyAssociationGroupId AND TAGFSProofing.IsProofing = 1	
	LEFT JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd on TAGFSProofing.UniqueGroupId = TAGFSProd.UniqueGroupId and TAGFSProd.isproofing = 0
	WHERE TAGFSProd.UniqueGroupId IS NULL


	--INSERT Records into TaxonomyAssociationGroup
	INSERT INTO TaxonomyAssociationGroup(Name,[Description],SiteId,ParentTaxonomyAssociationId,CssClass,
				UtcModifiedDate,ModifiedBy,[Order],IsProofing,UniqueGroupId)
	SELECT proofing.Name,proofing.[Description],proofing.SiteId,proofing.ParentTaxonomyAssociationId,proofing.CssClass,
			proofing.UtcModifiedDate,proofing.ModifiedBy,proofing.[Order], 0, proofing.UniqueGroupId
	FROM TaxonomyAssociationGroup proofing
	INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProofing On proofing.TaxonomyAssociationGroupId = TAGFSProofing.TaxonomyAssociationGroupId AND TAGFSProofing.IsProofing = 1	
	LEFT JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd on TAGFSProofing.UniqueGroupId = TAGFSProd.UniqueGroupId and TAGFSProd.isproofing = 0
	WHERE TAGFSProd.UniqueGroupId IS NULL

	----------------------------------------------------------	

	
	DECLARE @TAGMapping table(ProofingTAGId int, ProdGTAId int)
	INSERT INTO @TAGMapping
	SELECT  DISTINCT proofingTAG.TaxonomyAssociationGroupId, prodTAG.TaxonomyAssociationGroupId
	FROM TaxonomyAssociationGroup proofingTAG	
	INNER JOIN @TaxonomyAssociationGroupIdForSite TAFSProf On proofingTAG.TaxonomyAssociationGroupId = TAFSProf.TaxonomyAssociationGroupId 
																		AND TAFSProf.IsProofing = 1
	INNER JOIN (
		
				SELECT prod.* 
				FROM TaxonomyAssociationGroup prod where prod.IsProofing = 0

	)prodTAG On  prodTAG.UniqueGroupId = proofingTAG.UniqueGroupId


	UPDATE TAG
	SET TAG.ParentTaxonomyAssociationGroupId = mapping.ProdGTAId
	FROM TaxonomyAssociationGroup TAG	
	INNER JOIN (
	
				SELECT proofing.* 
				FROM TaxonomyAssociationGroup proofing
				INNER JOIN @TaxonomyAssociationGroupIdForSite TAFSProf On proofing.TaxonomyAssociationGroupId = TAFSProf.TaxonomyAssociationGroupId 
																		AND TAFSProf.IsProofing = 1

	)proofingTAG on proofingTAG.UniqueGroupId = TAG.UniqueGroupId AND TAG.IsProofing = 0
	LEFT JOIN @TAGMapping mapping ON mapping.ProofingTAGId = proofingTAG.ParentTaxonomyAssociationGroupId --Need Left join because ParentTaxonomyAssociationGroupId can be NULL

	--Update @TaxonomyAssociationGroupIdForSite data
	DELETE @TaxonomyAssociationGroupIdForSite

	;WITH TopLevelTaxonomyAssociationGroup 
    AS (SELECT 
				@SiteId_X AS SiteId,
				TaxonomyAssociationGroupId, 
                ParentTaxonomyAssociationGroupId,
				ParentTaxonomyAssociationId,
				IsProofing,
				UniqueGroupId
        FROM   TaxonomyAssociationGroup 
        WHERE  SiteID = @SiteId_X 

        UNION ALL 
        SELECT 				
				NULL AS SiteId,
				TAG.TaxonomyAssociationGroupId, 
                TAG.ParentTaxonomyAssociationGroupId,
				TAG.ParentTaxonomyAssociationId,
				TAG.IsProofing,
				TAG.UniqueGroupId
        FROM   TopLevelTaxonomyAssociationGroup TLTAG
        INNER JOIN TaxonomyAssociationGroup TAG ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)

		INSERT INTO @TaxonomyAssociationGroupIdForSite
			SELECT DISTINCT TaxonomyAssociationGroupId, UniqueGroupId, IsProofing FROM TopLevelTaxonomyAssociationGroup
	----------------------------------------------------------
	----------------------------------------------------End: TaxonomyAssociationGroup-------------------------------------------------------------------------------

	----------------------------------------------------Start: TaxonomyAssociationGroupTaxonomyAssociation-------------------------------------------------------------------------------
	
	
	DECLARE @TAMapping table(ProofingTAId int, ProdTAId int)

	INSERT INTO @TAMapping
	SELECT  DISTINCT TAFSProofing.TaxonomyAssociationId, TAFSProd.TaxonomyAssociationId
	FROM @TaxonomyAssociationIdsForSite TAFSProofing	
	INNER JOIN 
	(		
		SELECT prod.* 
		FROM TaxonomyAssociation prod				
		where SiteId IS NULL AND IsProofing = 0
	)TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProofing.IsProofing = 1

	
	DELETE TAGTAProd		
	FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTAProd
	INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd ON TAGFSProd.TaxonomyAssociationGroupId = TAGTAProd.TaxonomyAssociationGroupId
																AND TAGFSProd.isproofing = 0
	INNER JOIN TaxonomyAssociation TAProd ON TAProd.TaxonomyAssociationId = TAGTAProd.TaxonomyAssociationId	
	LEFT JOIN(
		SELECT TAGTA.*, UniqueGroupId, MarketId
		FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTA
		INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProf ON TAGFSProf.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
															AND  TAGFSProf.isproofing = 1		
		INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TAGTA.TaxonomyAssociationId
	)TAGTAProofing ON TAGTAProofing.UniqueGroupId = TAGFSProd.UniqueGroupId AND TAGTAProofing.MarketId = TAProd.MarketId
	WHERE TAGTAProofing.UniqueGroupId IS NULL
		
	------------------------------------UPDATE	TaxonomyAssociationGroupTaxonomyAssociation
	DECLARE @TAGTAsToBeUpdated TABLE(TaxonomyAssociationGroupId INT, TaxonomyAssociationId INT, NewOrder INT)

	INSERT INTO @TAGTAsToBeUpdated
		SELECT ProdTAGTA.TaxonomyAssociationGroupId, ProdTAGTA.TaxonomyAssociationId, proofingTAGTA.[Order]
		FROM TaxonomyAssociationGroupTaxonomyAssociation ProdTAGTA
		INNER JOIN @TaxonomyAssociationGroupIdForSite prodTAG ON prodTAG.TaxonomyAssociationGroupId = ProdTAGTA.TaxonomyAssociationGroupId
																		AND  prodTAG.isproofing = 0
		INNER JOIN TaxonomyAssociation prodTA ON prodTA.TaxonomyAssociationId = ProdTAGTA.TaxonomyAssociationId
		INNER JOIN(
		
			SELECT TaxonomyAssociationGroupTaxonomyAssociation.*, UniqueGroupId, MarketId 
			FROM TaxonomyAssociationGroupTaxonomyAssociation		
			INNER JOIN @TaxonomyAssociationGroupIdForSite profTAG ON profTAG.TaxonomyAssociationGroupId = TaxonomyAssociationGroupTaxonomyAssociation.TaxonomyAssociationGroupId
																	AND  profTAG.isproofing = 1
			INNER JOIN @TaxonomyAssociationIdsForSite profTA ON profTA.TaxonomyAssociationId = TaxonomyAssociationGroupTaxonomyAssociation.TaxonomyAssociationId
																	AND  profTA.isproofing = 1
	
		)ProofingTAGTA ON ProofingTAGTA.UniqueGroupId = prodTAG.UniqueGroupId AND ProofingTAGTA.MarketId = prodTA.MarketId
		WHERE ProdTAGTA.[Order] <> ProofingTAGTA.[Order]

	UPDATE ProdTAGTA
	SET ProdTAGTA.[Order] = ProofingTAGTA.NewOrder
	FROM TaxonomyAssociationGroupTaxonomyAssociation ProdTAGTA
	INNER JOIN @TAGTAsToBeUpdated ProofingTAGTA ON ProofingTAGTA.TaxonomyAssociationGroupId = ProdTAGTA.TaxonomyAssociationGroupId AND
												   ProofingTAGTA.TaxonomyAssociationId = ProdTAGTA.TaxonomyAssociationId


	
	INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order], UtcModifiedDate, ModifiedBy)
		SELECT TAGMapping.ProdGTAId, TAMapping.ProdTAId, TAGTAProofing.[Order], GETUTCDATE(), TAGTAProofing.ModifiedBy
		FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTAProofing
		INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProofing ON TAGFSProofing.TaxonomyAssociationGroupId = TAGTAProofing.TaxonomyAssociationGroupId
																	AND TAGFSProofing.isproofing = 1
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProf ON TAFSProf.TaxonomyAssociationId = TAGTAProofing.TaxonomyAssociationId AND TAFSProf.isproofing = 1
		INNER JOIN @TAMapping TAMapping ON TAMapping.ProofingTAId = TAGTAProofing.TaxonomyAssociationId
		INNER JOIN @TAGMapping TAGMapping ON TAGMapping.ProofingTAGId = TAGTAProofing.TaxonomyAssociationGroupId
		LEFT JOIN(
			SELECT TAGTA.*, UniqueGroupId, MarketId
			FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTA
			INNER JOIN @TaxonomyAssociationGroupIdForSite TAGFSProd ON TAGFSProd.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
																AND  TAGFSProd.isproofing = 0
			INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TAGTA.TaxonomyAssociationId
		)TAGTAProd ON TAGFSProofing.UniqueGroupId = TAGTAProd.UniqueGroupId AND TAFSProf.MarketId = TAGTAProd.MarketId
		WHERE TAGTAProd.UniqueGroupId IS NULL
	----------------------------------------------------Start: TaxonomyAssociationGroupTaxonomyAssociation-------------------------------------------------------------------------------

	
END