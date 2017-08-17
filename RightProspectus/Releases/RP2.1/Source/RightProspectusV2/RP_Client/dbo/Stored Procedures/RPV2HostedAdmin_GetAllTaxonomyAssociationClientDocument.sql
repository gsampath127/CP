CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTaxonomyAssociationClientDocument]
AS
BEGIN

	SELECT  distinct	  
		TaxonomyAssociation.[Level] as Level,
		TaxonomyAssociation.TaxonomyId AS TaxonomyId,
		NameOverride,
		TaxonomyAssociationClientDocument.ClientDocumentId AS ClientDocumentId,
	    ClientDocumentType.ClientDocumentTypeId as ClientDocumentTypeId,
		ClientDocumentType.Name as ClientDocumentTypeName,
		ClientDocument.Name as ClientDocumentName,
		ClientDocument.[FileName] as ClientDocumentFileName,
		TaxonomyAssociationClientDocument.UtcModifiedDate as UtcLastModified,
		TaxonomyAssociationClientDocument.ModifiedBy
	FROM TaxonomyAssociationClientDocument
	INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TaxonomyAssociationClientDocument.TaxonomyAssociationId
	INNER JOIN ClientDocument ON ClientDocument.ClientDocumentId = TaxonomyAssociationClientDocument.ClientDocumentId
	INNER JOIN ClientDocumentType ON ClientDocumentType.ClientDocumentTypeId = ClientDocument.ClientDocumentTypeId

END 
GO



