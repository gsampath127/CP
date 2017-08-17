CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationHierarchy_ExcelImport]
(
	@Added TT_VerticalImport_TaxonomyAssociationHierachy READONLY
)
AS
BEGIN
	BEGIN TRY
    BEGIN TRANSACTION;
		

	MERGE TaxonomyAssociationHierachy AS T
	USING @Added AS S
	ON (S.ParentTaxonomyAssociationId = T.ParentTaxonomyAssociationId AND S.ChildTaxonomyAssociationId = T.ChildTaxonomyAssociationId) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(ParentTaxonomyAssociationId, ChildTaxonomyAssociationId, RelationshipType,[Order],
               UtcModifiedDate, ModifiedBy) 
			 VALUES(
					S.ParentTaxonomyAssociationId,
					S.ChildTaxonomyAssociationId,
					S.RelationshipType,
					S.[Order],            
					GETUTCDATE(),
					S.ModifiedBy
			)
	WHEN MATCHED 
		THEN UPDATE 
			SET [Order] = S.[Order],
			UtcModifiedDate = GETUTCDATE(),
			ModifiedBy = S.ModifiedBy;
       
	COMMIT TRANSACTION;

	SELECT 'Success' AS [Status]
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

		SELECT 'Fail' as [Status]
 
    
	END CATCH
END
GO