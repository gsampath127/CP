CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociationClientDocument]
@TaxonomyAssociationId INT,
@ClientDocumentId INT,
@ModifiedBy INT
AS
BEGIN

	INSERT INTO TaxonomyAssociationClientDocument(  

			TaxonomyAssociationId,  

			ClientDocumentId,  

			UtcModifiedDate,        

			ModifiedBy)  

		VALUES

			(@TaxonomyAssociationId,        

			@ClientDocumentId,  

			GETUTCDATE(),     

			@ModifiedBy)  
END

