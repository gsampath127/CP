CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_SaveFootnote]
@Added [TT_VerticalImport_Footnote] READONLY,
@Updated [TT_VerticalImport_Footnote] READONLY,
@Deleted [TT_VerticalImport_Footnote] READONLY
AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION;
	-- Delete the records
	DELETE FROM Footnote
	WHERE Footnote.FootnoteId
	IN (SELECT d.FootnoteId FROM @Deleted d)

	 -- Insert the records
	INSERT INTO Footnote(TaxonomyAssociationId,TaxonomyAssociationGroupId,LanguageCulture,[Text],[Order],
				 UtcModifiedDate,ModifiedBy)
		SELECT a.TaxonomyAssociationId, a.TaxonomyAssociationGroupId, a.LanguageCulture, a.[Text], a.[Order],
			  GETUTCDATE(), a.ModifiedBy
		FROM @Added a

	-- Update the records
	UPDATE fn
		SET [Order] = u.[Order],
		LanguageCulture = u.LanguageCulture,
		[Text] = u.[Text],
		UtcModifiedDate = GETUTCDATE(),
		ModifiedBy = u.ModifiedBy
	FROM Footnote fn
	INNER JOIN @Updated u ON fn.FootnoteId = u.FootnoteId 

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