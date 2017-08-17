CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_DocumentTypeAssociation]
@SiteId Int
AS
BEGIN
	
	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

	--Deleting Records at site level
	DELETE FROM DocumentTypeAssociation
	Where DocumentTypeAssociationId IN(

		SELECT prod.DocumentTypeAssociationId
		FROM DocumentTypeAssociation prod
		LEFT JOIN DocumentTypeAssociation proofing on proofing.DocumentTypeId = prod.DocumentTypeId And proofing.SiteId = prod.SiteId and proofing.IsProofing = 1
		WHERE prod.SiteId = @SiteId_X
		AND prod.IsProofing = 0
		AND proofing.DocumentTypeId IS NULL
	)

	-- Update Records at site level
	UPDATE prod
	SET prod.[Order] = proofing.[Order],
		prod.HeaderText = proofing.HeaderText,
		prod.LinkText = proofing.LinkText,
		prod.DescriptionOverride = proofing.DescriptionOverride,
		prod.CssClass = proofing.CssClass,
		prod.UtcModifiedDate = GETUTCDATE(),
		prod.ModifiedBy=proofing.ModifiedBy
	FROM DocumentTypeAssociation prod
	INNER JOIN DocumentTypeAssociation proofing on proofing.DocumentTypeId = prod.DocumentTypeId And proofing.SiteId = prod.SiteId and proofing.IsProofing = 1
	WHERE prod.SiteId = @SiteId_X AND prod.IsProofing = 0
			
	-- Insert Records at site level
	INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,TaxonomyAssociationId,[Order],HeaderText,LinkText,DescriptionOverride,
				CssClass,MarketId,UtcModifiedDate,ModifiedBy,IsProofing)
	SELECT proofing.DocumentTypeId,proofing.SiteId,proofing.TaxonomyAssociationId,proofing.[Order],proofing.HeaderText,proofing.LinkText,proofing.DescriptionOverride,
			proofing.CssClass,proofing.MarketId,proofing.UtcModifiedDate,proofing.ModifiedBy,0
	FROM DocumentTypeAssociation proofing
	LEFT JOIN DocumentTypeAssociation prod on proofing.DocumentTypeId = prod.DocumentTypeId And proofing.SiteId = prod.SiteId and prod.IsProofing = 0
	WHERE proofing.SiteId = @SiteId_X
	AND proofing.IsProofing = 1
	AND prod.DocumentTypeId IS NULL

	
	--------DocumentTypeExternalID Merge
	
	MERGE DocumentTypeExternalID AS T
	USING (
		SELECT * FROM(
			SELECT Row_Number() Over(Partition by DocumentTypeId Order by DocumentTypeAssociationid) 'RowNum', * FROM DocumentTypeAssociation
		)t where t.RowNum = 1
	) AS S
	ON (S.DocumentTypeId = T.DocumentTypeId) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(DocumentTypeId, ExternalId, IsPrimary, UtcModifiedDate, ModifiedBy) 
			 VALUES(
						S.DocumentTypeId, S.MarketId, 0, GETUTCDATE(), 1
				   )
	WHEN NOT MATCHED BY Source 
		THEN DELETE;

END
GO