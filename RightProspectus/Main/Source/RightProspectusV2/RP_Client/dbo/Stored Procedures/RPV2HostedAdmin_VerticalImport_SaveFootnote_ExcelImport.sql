CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveFootnote_ExcelImport]
@Added [TT_VerticalImport_Footnote] READONLY
AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION;

	MERGE Footnote AS T
	USING @Added AS S
	ON (S.TaxonomyAssociationId = T.TaxonomyAssociationId ) 
	WHEN NOT MATCHED BY TARGET
		THEN INSERT(TaxonomyAssociationId,TaxonomyAssociationGroupId,LanguageCulture,[Text],[Order],
				 UtcModifiedDate,ModifiedBy)
				VALUES( S.TaxonomyAssociationId,
				        S.TaxonomyAssociationGroupId,
				        S.LanguageCulture, 
				        S.[Text], 
				        S.[Order],
					    GETUTCDATE(),
					    S.ModifiedBy 
					    )
		
	WHEN MATCHED 
		THEN UPDATE 
			SET [Order] = S.[Order],
				[Text] = S.[Text],
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
