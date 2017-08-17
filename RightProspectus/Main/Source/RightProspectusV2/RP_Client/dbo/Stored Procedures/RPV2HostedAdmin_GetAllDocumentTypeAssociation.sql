-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllDocumentTypeAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllDocumentTypeAssociation
AS
BEGIN
 SELECT DocumentTypeAssociationId
      ,DocumentTypeId
      ,SiteId
      ,TaxonomyAssociationId
      ,[Order]
      ,HeaderText
      ,LinkText
      ,DescriptionOverride
      ,CssClass
      ,MarketId
      ,UtcModifiedDate AS UtcLastModified
      ,ModifiedBy
	  ,IsProofing
  FROM dbo.DocumentTypeAssociation

END