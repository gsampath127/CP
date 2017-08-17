CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociationClientDocument]
@TaxonomyId INT,
@ClientDocumentId INT,
@ModifiedBy INT
AS
BEGIN

	INSERT INTO TaxonomyAssociationClientDocument(
					TaxonomyAssociationId,
					ClientDocumentId,
					UtcModifiedDate,
					ModifiedBy)
		SELECT  TaxonomyAssociationId,
				@ClientDocumentId,
				GETUTCDATE(),
				@ModifiedBy
		FROM TaxonomyAssociation 
		WHERE TaxonomyId = @TaxonomyId
END
GO

