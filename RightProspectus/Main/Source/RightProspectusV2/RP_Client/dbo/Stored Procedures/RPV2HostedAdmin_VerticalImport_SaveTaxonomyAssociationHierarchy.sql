CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationHierarchy]
(
@Added TT_VerticalImport_TaxonomyAssociationHierachy READONLY,
@Updated TT_VerticalImport_TaxonomyAssociationHierachy READONLY,
@Deleted TT_VerticalImport_TaxonomyAssociationHierachy READONLY
)
AS
BEGIN
	BEGIN TRY
    BEGIN TRANSACTION;
		-- Delete the records
		DELETE  TAHD
		FROM TaxonomyAssociationHierachy TAHD
		INNER JOIN @Deleted d on d.ParentTaxonomyAssociationId = TAHD.ParentTaxonomyAssociationId
												  And d.ChildTaxonomyAssociationId = TAHD.ChildTaxonomyAssociationId
       -- Insert the records
		INSERT INTO TaxonomyAssociationHierachy(ParentTaxonomyAssociationId, ChildTaxonomyAssociationId, RelationshipType,[Order],
               UtcModifiedDate, ModifiedBy)
		SELECT
				a.ParentTaxonomyAssociationId,
				a.ChildTaxonomyAssociationId,
				1,
				a.[Order],            
				GETUTCDATE(),
				a.ModifiedBy
		FROM @Added a
 
		-- Update the records
		UPDATE TAHD
		SET TAHD.[Order]=UTAHD.[Order],
			   TAHD.UtcModifiedDate = GETUTCDATE(),
			   TAHD.ModifiedBy=UTAHD.ModifiedBy
		FROM TaxonomyAssociationHierachy TAHD
		INNER JOIN @Updated UTAHD ON UTAHD.ParentTaxonomyAssociationId = TAHD.ParentTaxonomyAssociationId
												  And UTAHD.ChildTaxonomyAssociationId = TAHD.ChildTaxonomyAssociationId

	COMMIT TRANSACTION;

	SELECT 'Success' AS [Status]
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

		SELECT 'Fail' as [Status]
 
    
	END CATCH
END
Go
