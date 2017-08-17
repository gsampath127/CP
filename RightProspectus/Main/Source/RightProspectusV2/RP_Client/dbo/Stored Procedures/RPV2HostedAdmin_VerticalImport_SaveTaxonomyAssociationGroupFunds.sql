CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroupFunds]
@Added [TT_TaxonomyAssociationGroupTaxonomyAssociation] READONLY,
@Updated [TT_TaxonomyAssociationGroupTaxonomyAssociation] READONLY,
@Deleted [TT_TaxonomyAssociationGroupTaxonomyAssociation] READONLY


AS 
BEGIN
 BEGIN TRY
  BEGIN TRANSACTION
    DELETE TAGD FROM TaxonomyAssociationGroupTaxonomyAssociation TAGD
    INNER JOIN @Deleted d on d.TaxonomyAssociationGroupId=TAGD.TaxonomyAssociationGroupId AND d.TaxonomyAssociationId = TAGD.TaxonomyAssociationId

	INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],[UtcModifiedDate],[ModifiedBy])
	SELECT a.[TaxonomyAssociationGroupId],
	       a.[TaxonomyAssociationId],
		   a.[Order],
		    GETUTCDATE(),
		   a.ModifiedBy
			FROM @Added a

		UPDATE TAGD
		SET TAGD.[Order]=UTAGD.[Order],
			   TAGD.UtcModifiedDate = GETUTCDATE(),
			   TAGD.ModifiedBy=UTAGD.ModifiedBy
		FROM TaxonomyAssociationGroupTaxonomyAssociation TAGD
		INNER JOIN @Updated UTAGD ON UTAGD.TaxonomyAssociationId = TAGD.TaxonomyAssociationId
												  And UTAGD.TaxonomyAssociationGroupId = TAGD.TaxonomyAssociationGroupId

 COMMIT TRANSACTION;

SELECT 'Success' AS [Status]
END TRY

BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;

    SELECT 'Fail' as [Status]
 
    
END CATCH
END