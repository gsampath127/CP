CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveDocumentTypeAssociation]
@Added [TT_VerticalImport_DocumentTypeAssociation] READONLY,
@Updated [TT_VerticalImport_DocumentTypeAssociation] READONLY,
@Deleted [TT_VerticalImport_DocumentTypeAssociation] READONLY
AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION;
	-- Delete the records
	DELETE FROM DocumentTypeAssociation
	WHERE DocumentTypeAssociation.DocumentTypeAssociationId
	IN (SELECT d.DocumentTypeAssociationId FROM @Deleted d)

	 -- Insert the records
	INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,TaxonomyAssociationId,[Order],HeaderText,
				LinkText, DescriptionOverride, CssClass, MarketId, UtcModifiedDate,ModifiedBy, IsProofing)
		SELECT a.DocumentTypeId, a.SiteId, a.TaxonomyAssociationId, a.[Order], a.HeaderText,
				ISNULL(a.LinkText, a.HeaderText), ISNULL(a.DescriptionOverride,''), a.CssClass, a.MarketId, GETUTCDATE(), a.ModifiedBy, 1
		FROM @Added a

	-- Update the records
	UPDATE dt
		SET [Order] = u.[Order],
		HeaderText = u.HeaderText,
		LinkText = ISNULL(u.LinkText, u.HeaderText),
		DescriptionOverride = ISNULL(u.DescriptionOverride,''),
		CssClass = u.CssClass,
		UtcModifiedDate = GETUTCDATE(),
		ModifiedBy = u.ModifiedBy
	FROM DocumentTypeAssociation dt
	INNER JOIN @Updated u ON dt.DocumentTypeAssociationId = u.DocumentTypeAssociationId AND dt.IsProofing=1


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


