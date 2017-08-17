CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation]
@SiteId Int
AS
BEGIN
	
	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

	DECLARE @DefaultPageID int
	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId_X
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		EXEC RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAL @SiteId_X
	END
	ELSE IF @DefaultPageID = 4 --TAD case
	BEGIN
		EXEC RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAD @SiteId_X
	END
	ELSE IF @DefaultPageID = 7 --TAGD case
	BEGIN
		EXEC RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation_TAGD @SiteId_X
	END


	--Delete TaxonomyAssociation at client level
	DECLARE @TAIdsToBeDeleted TABLE(TaxonomyAssociationId INT)
	INSERT INTO @TAIdsToBeDeleted
		SELECT ProdTA.TaxonomyAssociationId
		FROM TaxonomyAssociation ProdTA
		LEFT JOIN
		(
			SELECT TaxonomyAssociation.MarketId
			FROM TaxonomyAssociation 
			WHERE IsProofing = 1 and SiteId IS NULL 
		
		)proofingTA ON proofingTA.MarketId = ProdTA.MarketId
		WHERE ProdTA.SiteId IS NULL AND proofingTA.MarketId IS NULL

	DELETE TAH
	FROM TaxonomyAssociationHierachy TAH
	INNER JOIN @TAIdsToBeDeleted d on d.TaxonomyAssociationId = TAH.ChildTaxonomyAssociationId OR
							 d.TaxonomyAssociationId = TAH.ParentTaxonomyAssociationId	

	DELETE FROM TaxonomyAssociationGroupTaxonomyAssociation
	Where TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	DELETE FROM DocumentTypeAssociation
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	DELETE FROM Footnote
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @TAIdsToBeDeleted)

	DELETE FROM TaxonomyAssociation
	WHERE TaxonomyAssociation.TaxonomyAssociationId IN (SELECT TaxonomyAssociationId FROM @TAIdsToBeDeleted)

	--------Taxonomy Level ExternalId Merge
	
	MERGE TaxonomyLevelExternalId AS T
	USING (
		SELECT * FROM(
			SELECT Row_Number() Over(Partition by TaxonomyID, Level, MarketID Order by TaxonomyAssociationId) 'RowNum', * FROM TaxonomyAssociation
		)t where t.RowNum = 1
	) AS S
	ON (S.TaxonomyID = T.TaxonomyID AND S.Level = T.Level AND S.MarketID = T.ExternalId) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT([Level],TaxonomyId,
				ExternalId,UtcModifiedDate,ModifiedBy) 
			 VALUES(
					S.Level, S.TaxonomyId,
						S.MarketId, GETUTCDATE(), 1
					)
	WHEN NOT MATCHED BY Source 
		THEN DELETE;

END