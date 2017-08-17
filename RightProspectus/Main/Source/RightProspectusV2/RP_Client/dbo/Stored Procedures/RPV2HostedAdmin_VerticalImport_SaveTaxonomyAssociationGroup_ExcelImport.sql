CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroup_ExcelImport]
(
	@Added TT_VerticalImport_TaxonomyAssociationGroup READONLY
)
AS
BEGIN
	BEGIN TRY
    BEGIN TRANSACTION;
		

	MERGE TaxonomyAssociationGroup AS T
	USING @Added AS S
	ON (S.TaxonomyAssociationGroupId = T.TaxonomyAssociationGroupId ) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT( Name, [Description],SiteId,CssClass,
		UtcModifiedDate,ModifiedBy) 
			 VALUES(					
					S.Name,
					S.[Description],
					S.SiteId,							  
					S.CssClass,       
					GETUTCDATE(),
					S.ModifiedBy
			)
	WHEN MATCHED 
		THEN UPDATE 
			SET 			
			[Description]=s.[Description],
			CssClass=S.CssClass,
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
Go