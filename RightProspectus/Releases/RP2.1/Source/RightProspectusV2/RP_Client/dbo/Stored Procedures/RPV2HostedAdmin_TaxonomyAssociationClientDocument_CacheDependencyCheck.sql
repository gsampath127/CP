CREATE PROCEDURE [dbo].[RPV2HostedAdmin_TaxonomyAssociationClientDocument_CacheDependencyCheck]
AS
BEGIN

	SELECT TaxonomyAssociationId, COUNT_BIG(*) AS Total
	FROM   dbo.TaxonomyAssociation
	GROUP BY TaxonomyAssociationId;

	SELECT ClientDocumentTypeId, COUNT_BIG(*) AS Total
	FROM   dbo.ClientDocumentType
	GROUP BY ClientDocumentTypeId;

	SELECT ClientDocumentId, COUNT_BIG(*) AS Total
	FROM   dbo.ClientDocument
	GROUP BY ClientDocumentId;

	SELECT TaxonomyAssociationId, ClientDocumentId,  COUNT_BIG(*) AS Total
	FROM   dbo.TaxonomyAssociationClientDocument
	GROUP BY TaxonomyAssociationId, ClientDocumentId;

END
GO

