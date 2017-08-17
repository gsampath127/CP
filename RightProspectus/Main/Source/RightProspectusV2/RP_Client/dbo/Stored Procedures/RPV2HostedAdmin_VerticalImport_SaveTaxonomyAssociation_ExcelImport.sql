CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociation_ExcelImport]
@SiteId int,
@Added TT_VerticalImport_TaxonomyAssociation READONLY
AS
BEGIN

BEGIN TRY
    BEGIN TRANSACTION;


	MERGE TaxonomyAssociation AS T
	USING @Added AS S
	ON (S.MarketId = T.MarketId AND ISNULL(T.SiteId, '') = ISNULL(@SiteId, '') AND T.IsProofing = 1) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(Level, TaxonomyId, SiteId, ParentTaxonomyAssociationId, NameOverride, DescriptionOverride,
				CssClass, MarketId, UtcModifiedDate, ModifiedBy, IsProofing, [Order],[TabbedPageNameOverride]) 
			 VALUES(
					1,
					S.TaxonomyId,
					@SiteId,
					S.ParentTaxonomyAssociationId,
					S.NameOverride,
					S.DescriptionOverride,
					S.CssClass,
					S.MarketId,
					GETUTCDATE(),
					S.ModifiedBy,
					1,
					S.[Order],
					S.[TabbedPageNameOverride]
			)
	WHEN MATCHED 
		THEN UPDATE 
			SET NameOverride = S.NameOverride,
			DescriptionOverride = S.DescriptionOverride,
			CssClass = S.CssClass,
			[Order] = S.[Order],
			[TabbedPageNameOverride]=S.[TabbedPageNameOverride],
			UtcModifiedDate = GETUTCDATE(),
			ModifiedBy = S.ModifiedBy;


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