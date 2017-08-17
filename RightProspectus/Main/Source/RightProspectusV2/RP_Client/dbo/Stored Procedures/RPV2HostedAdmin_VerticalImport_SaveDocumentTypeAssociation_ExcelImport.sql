CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveDocumentTypeAssociation_ExcelImport]
@SiteId int,
@Added [TT_VerticalImport_DocumentTypeAssociation] READONLY
AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION;

	MERGE DocumentTypeAssociation AS T
	USING @Added AS S
	ON (S.MarketId = T.MarketId AND ISNULL(T.SiteId, '') = ISNULL(@SiteId, '') AND ISNULL(S.TaxonomyAssociationId, '') = ISNULL(T.TaxonomyAssociationId, '') AND T.IsProofing = 1) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(DocumentTypeId,SiteId,TaxonomyAssociationId,[Order],HeaderText,
				LinkText, DescriptionOverride, CssClass, MarketId, UtcModifiedDate,ModifiedBy, IsProofing) 
			 VALUES(
					S.DocumentTypeId,					
					@SiteId,
					S.TaxonomyAssociationId,
					S.[Order],
					S.HeaderText,
					ISNULL(S.LinkText, S.HeaderText),
					ISNULL(S.DescriptionOverride, ''),
					S.CssClass,
					S.MarketId,
					GETUTCDATE(),
					S.ModifiedBy,
					1
			)
	WHEN MATCHED 
		THEN UPDATE 
			SET [Order] = S.[Order],
				HeaderText = S.HeaderText,
				LinkText = ISNULL(S.LinkText, S.HeaderText),
				DescriptionOverride = ISNULL(S.DescriptionOverride, ''),
				CssClass = S.CssClass,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = S.ModifiedBy;
	

	--------DocumentTypeExternalID Merge
	
	MERGE DocumentTypeExternalID AS T
	USING (
		SELECT * FROM(
			SELECT Row_Number() Over(Partition by DocumentTypeId Order by DocumentTypeAssociationid) 'RowNum', * FROM DocumentTypeAssociation
		)t where t.RowNum = 1
	) AS S
	ON (S.DocumentTypeId = T.DocumentTypeId) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(DocumentTypeId,ExternalId, IsPrimary, UtcModifiedDate, ModifiedBy) 
			 VALUES(
						S.DocumentTypeId, S.MarketId, 0, GETUTCDATE(), 1
				   )
	WHEN NOT MATCHED BY Source 
		THEN DELETE;

	COMMIT TRANSACTION;

	SELECT 'Success' AS [Status]
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

		SELECT 'Fail' as [Status]   

	END CATCH
END