CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationClientDocument]				 
@TaxonomyId int,
@ClientDocumentId int,
@deletedBy int                				 			
AS 
BEGIN 

	DELETE TaxonomyAssociationClientDocument
	WHERE TaxonomyAssociationId IN (
				SELECT TaxonomyAssociationId FROM TaxonomyAssociation WHERE TaxonomyId = @TaxonomyId) 
	AND ClientDocumentId = @ClientDocumentId
		    
   
	UPDATE	CUDHistory				 
	SET	UserId = @deletedBy
	WHERE	TableName = N'TaxonomyAssociationClientDocument'
	AND	[Key] IN (
				SELECT TaxonomyAssociationId FROM TaxonomyAssociation WHERE TaxonomyId = @TaxonomyId)
	AND [SecondKey] = @ClientDocumentId
				
	AND [CUDType] = 'D' 

END
GO