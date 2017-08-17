CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroup]
(
@Added [TT_VerticalImport_TaxonomyAssociationGroup] READONLY,
@Updated [TT_VerticalImport_TaxonomyAssociationGroup] READONLY,
@Deleted [TT_VerticalImport_TaxonomyAssociationGroup] READONLY
)
AS
BEGIN

 BEGIN TRY
   BEGIN TRANSACTION 
	--Delete 
	-----------------------------------------------------------------------------------------------------------------------
	DELETE TAGTA
	FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTA
	INNER JOIN @Deleted d ON TAGTA.TaxonomyAssociationGroupId = d.TaxonomyAssociationGroupId

	DELETE TAG
	FROM TaxonomyAssociationGroup TAG
	INNER JOIN @Deleted d ON TAG.TaxonomyAssociationGroupId = d.TaxonomyAssociationGroupId
	-----------------------------------------------------------------------------------------------------------------------

	--Insert
	INSERT INTO TaxonomyAssociationGroup(Name,ParentTaxonomyAssociationGroupId,[Description],SiteId,CssClass,UtcModifiedDate,ModifiedBy, [Order])
	SELECT a.Name,a.ParentTaxonomyAssociationGroupId,a.[Description],a.SiteId,a.CssClass,GETUTCDATE(),a.ModifiedBy, a.[Order] from @Added a

	--Update
	UPDATE tag
	SET 
		Name=u.Name,
		[Description]=u.[Description],
		[ParentTaxonomyAssociationGroupId]=u.[ParentTaxonomyAssociationGroupId],
		CssClass=u.CssClass,
		UtcModifiedDate = GETUTCDATE(),
		ModifiedBy = u.ModifiedBy,
		[Order] = u.[Order],
		SiteId = u.SiteId
	FROM TaxonomyAssociationGroup tag  
	INNER JOIN @Updated u ON tag.TaxonomyAssociationGroupId = u.TaxonomyAssociationGroupId
   
COMMIT TRANSACTION;

SELECT 'Success' AS [Status]

END TRY
BEGIN CATCH

	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

	SELECT 'Fail' as [Status]

END CATCH
END

