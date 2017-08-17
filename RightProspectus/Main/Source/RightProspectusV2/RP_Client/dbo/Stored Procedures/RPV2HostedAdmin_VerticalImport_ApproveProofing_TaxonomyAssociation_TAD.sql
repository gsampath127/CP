CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAD]
@SiteId Int
AS
BEGIN
	
	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
	
	INSERT INTO @TaxonomyAssociationIdsForSite
	SELECT DISTINCT TaxonomyAssociationId, MarketId, isproofing FROM TaxonomyAssociation WHERE SiteID = @SiteId_X
	
	--DELETE Records from TaxonomyAssociation
	DECLARE @TAIdsToBeDeleted TABLE(TaxonomyAssociationId INT)
	INSERT INTO @TAIdsToBeDeleted
		SELECT prod.TaxonomyAssociationId
		FROM TaxonomyAssociation prod
		INNER JOIN @TaxonomyAssociationIdsForSite TAFSProd On prod.TaxonomyAssociationId = TAFSProd.TaxonomyAssociationId AND TAFSProd.IsProofing = 0
		WHERE TAFSProd.MarketId NOT IN (SELECT MarketId FROM @TaxonomyAssociationIdsForSite TAFSProofing where TAFSProofing.isproofing = 1)		


	--DELETE Records from DocumentTypeAssociation and then delete from TaxonomyAssociation
	DELETE FROM DocumentTypeAssociation
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

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
	SELECT proofing.[Level],proofing.TaxonomyId,proofing.SiteId,proofing.ParentTaxonomyAssociationId,proofing.NameOverride,proofing.DescriptionOverride,
			proofing.CssClass,proofing.MarketId,proofing.UtcModifiedDate,proofing.ModifiedBy,0, proofing.[Order], proofing.TabbedPageNameOverride
	FROM TaxonomyAssociation proofing
	INNER JOIN @TaxonomyAssociationIdsForSite TAFSProofing On proofing.TaxonomyAssociationId = TAFSProofing.TaxonomyAssociationId AND TAFSProofing.IsProofing = 1
	WHERE proofing.MarketId NOT IN (SELECT MarketId FROM @TaxonomyAssociationIdsForSite TAFSProd where TAFSProd.isproofing = 0)
		
END
Go