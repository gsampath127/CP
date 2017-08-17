-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy
AS
BEGIN
 SELECT [ParentTaxonomyAssociationId]
      ,[ChildTaxonomyAssociationId]
      ,[RelationshipType]
      ,[Order]
      ,[UtcModifiedDate] AS UtcLastModified
      ,[ModifiedBy]
  FROM [dbo].[TaxonomyAssociationHierachy]
END