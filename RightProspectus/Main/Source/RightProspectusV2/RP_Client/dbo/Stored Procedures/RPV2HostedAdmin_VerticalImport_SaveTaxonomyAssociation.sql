CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociation]
@Added TT_VerticalImport_TaxonomyAssociation READONLY,
@Updated TT_VerticalImport_TaxonomyAssociation READONLY,
@Deleted TT_VerticalImport_TaxonomyAssociation READONLY
AS
BEGIN

BEGIN TRY
    BEGIN TRANSACTION;
	-------------------------------------------------Start: Delete records from TaxonomyAssociation-------------------------------------------------
	--1. DELETE records from TaxonomyAssociationHierarchy table
	DELETE TAH
	FROM TaxonomyAssociationHierachy TAH
	INNER JOIN @Deleted d on d.TaxonomyAssociationId = TAH.ChildTaxonomyAssociationId OR
							 d.TaxonomyAssociationId = TAH.ParentTaxonomyAssociationId	

	DELETE FROM TaxonomyAssociationGroupTaxonomyAssociation
	Where TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @Deleted)

	DELETE FROM DocumentTypeAssociation
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @Deleted)

	DELETE FROM Footnote
	WHERE TaxonomyAssociationId IN(SELECT TaxonomyAssociationId from @Deleted)

	DELETE FROM TaxonomyAssociation
	WHERE TaxonomyAssociation.TaxonomyAssociationId IN (SELECT d.TaxonomyAssociationId FROM @Deleted d)

	-------------------------------------------------END: Delete records from TaxonomyAssociation-------------------------------------------------
	 -- Insert the records
	INSERT INTO TaxonomyAssociation(Level, TaxonomyId, SiteId, ParentTaxonomyAssociationId, NameOverride, DescriptionOverride,
			CssClass, MarketId, UtcModifiedDate, ModifiedBy, IsProofing,[Order],[TabbedPageNameOverride])
	SELECT 
			1,
			a.TaxonomyId,
			a.SiteId,
			a.ParentTaxonomyAssociationId,
			a.NameOverride,
			a.DescriptionOverride,
			a.CssClass,
			a.MarketId,
			GETUTCDATE(),
			a.ModifiedBy,
			1,
			a.[Order],
			a.[TabbedPageNameOverride]
	 FROM @Added a

	-- Update the records
	UPDATE TaxonomyAssociation
	SET NameOverride=UTA.NameOverride,
		DescriptionOverride=UTA.DescriptionOverride,
		CssClass=UTA.CssClass,
		SiteId=UTA.SiteId,
		UtcModifiedDate=GETUTCDATE(),
		ModifiedBy=UTA.ModifiedBy,
		[Order] =UTA.[Order]  ,
		[TabbedPageNameOverride]=UTA.[TabbedPageNameOverride]
	FROM TaxonomyAssociation 
	INNER JOIN @Updated UTA ON TaxonomyAssociation.TaxonomyAssociationId = UTA.TaxonomyAssociationId 


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


	COMMIT TRANSACTION;
	SELECT 'Success' AS [Status]
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION;

		SELECT 'Fail' as [Status]    
	END CATCH
END 
