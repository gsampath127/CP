CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_Footnote]
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
	ELSE IF @DefaultPageID = 7 --TAGD case
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

	--------------------------------------Delete Footnotes
	
	--Deleting Records at Taxonomy level
	DELETE prodFN
	FROM Footnote prodFN
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodFN.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0		
	LEFT JOIN (

			SELECT TAFSProofing.MarketId 
			FROM Footnote FN
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On FN.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
		
	)proofingFN On  TAFSProd.MarketId = proofingFN.MarketId		
	WHERE proofingFN.MarketId IS NULL

		
	--------------------------------------Update Footnotes
	DECLARE @FNIdsToBeUpdated TABLE(FootnoteId INT, NewText nvarchar(max), NewOrder INT, ModifiedBy INT)
	INSERT INTO @FNIdsToBeUpdated
		SELECT prodFN.FootnoteId, proofingFN.Text, proofingFN.[Order], proofingFN.ModifiedBy
		From Footnote prodFN
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prodFN.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
		INNER JOIN (

					SELECT FN.*, TAFSProofing.MarketId 
					FROM Footnote FN
					INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On FN.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
		
		)proofingFN On  TAFSProd.MarketId = proofingFN.MarketId
		WHERE
		prodFN.[Text] <> proofingFN.[Text] 
		OR prodFN.[Order] <> proofingFN.[Order] 


	UPDATE prodFN
	SET prodFN.Text = FNTBUProd.NewText,
		prodFN.[Order] = FNTBUProd.NewOrder,
		prodFN.UtcModifiedDate = GETUTCDATE(),
		prodFN.ModifiedBy = FNTBUProd.ModifiedBy
	From Footnote prodFN
	INNER JOIN @FNIdsToBeUpdated FNTBUProd On FNTBUProd.FootnoteId = prodFN.FootnoteId	


	--------------------------------------Insert Footnotes

	--Insert Records at Taxonomy level
	INSERT INTO Footnote(TaxonomyAssociationId, TaxonomyAssociationGroupId, LanguageCulture, Text, [Order], UtcModifiedDate, ModifiedBy)
	SELECT TAFSProd.TaxonomyAssociationId, proofingFN.TaxonomyAssociationGroupId, proofingFN.LanguageCulture, proofingFN.Text, proofingFN.[Order], proofingFN.UtcModifiedDate, proofingFN.ModifiedBy
	FROM Footnote proofingFN 
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofingFN.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On TAFSProofing.MarketId = TAFSProd.MarketId AND TAFSProd.IsProofing = 0	
	LEFT JOIN 
	(
		SELECT TAFSProd.MarketId 
			FROM Footnote FN
			INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On FN.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0

	)prodFN on prodFN.MarketId = TAFSProofing.MarketId										
	WHERE prodFN.MarketId IS NULL

END