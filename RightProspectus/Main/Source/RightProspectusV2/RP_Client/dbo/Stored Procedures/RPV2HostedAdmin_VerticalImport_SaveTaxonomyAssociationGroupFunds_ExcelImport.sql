CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport]
(
	@Added TT_TaxonomyAssociationGroupTaxonomyAssociation READONLY
)
AS
BEGIN
	BEGIN TRY
   BEGIN TRANSACTION;
	MERGE TaxonomyAssociationGroupTaxonomyAssociation AS T
	USING @Added AS S
	ON ((S.TaxonomyAssociationGroupId = T.TaxonomyAssociationGroupId and S.TaxonomyAssociationId = T.TaxonomyAssociationId )) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT( TaxonomyAssociationGroupId, TaxonomyAssociationId,[Order],
		UtcModifiedDate,ModifiedBy) 
			 VALUES(					
					S.TaxonomyAssociationGroupId,
					S.TaxonomyAssociationId,
					S.[Order],					
					GETUTCDATE(),
					S.ModifiedBy
			)
	WHEN MATCHED 
		THEN UPDATE 
			SET 
			T.[Order]=S.[Order],
			T.UtcModifiedDate = GETUTCDATE(),
			T.ModifiedBy = S.ModifiedBy;      
	COMMIT TRANSACTION;
	SELECT 'Success' AS [Status]
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION;
		SELECT 'Fail' as [Status]   
	END CATCH
END