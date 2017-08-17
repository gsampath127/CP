-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomyAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllTaxonomyAssociation
AS
BEGIN
 SELECT TaxonomyAssociationId
      ,[Level]
      ,TaxonomyId
      ,SiteId
      ,ParentTaxonomyAssociationId
      ,NameOverride
      ,DescriptionOverride
      ,CssClass
      ,MarketId
      ,UtcModifiedDate AS UtcLastModified
      ,ModifiedBy
	  ,IsProofing
	  ,[Order]
	  ,TabbedPageNameOverride
  FROM dbo.TaxonomyAssociation
END