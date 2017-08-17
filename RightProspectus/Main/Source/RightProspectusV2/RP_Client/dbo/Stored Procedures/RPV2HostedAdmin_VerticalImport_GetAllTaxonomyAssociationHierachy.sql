CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_GetAllTaxonomyAssociationHierachy]
@SiteId int
AS
BEGIN
SELECT 
	TaxonomyAssociationHierachy.ParentTaxonomyAssociationId,
	TaxonomyAssociationHierachy.ChildTaxonomyAssociationId,
	Child.TaxonomyId as 'ChildTaxonomyId',
	Child.MarketId as 'ChildMarketId',
	Child.NameOverride as 'ChildNameOverride',
	Parent.TaxonomyId as 'ParentTaxonomyId',
	Parent.MarketId as 'ParentMarketId',
	Parent.NameOverride as 'ParentNameOverride',
	TaxonomyAssociationHierachy.RelationshipType,
	TaxonomyAssociationHierachy.[Order],
	TaxonomyAssociationHierachy.UtcModifiedDate,
	TaxonomyAssociationHierachy.ModifiedBy
FROM TaxonomyAssociation Child
	INNER  JOIN TaxonomyAssociationHierachy
	ON Child.TaxonomyAssociationId = TaxonomyAssociationHierachy.ChildTaxonomyAssociationId
	INNER JOIN TaxonomyAssociation Parent
	ON Parent.TaxonomyAssociationId = TaxonomyAssociationHierachy.ParentTaxonomyAssociationId
where Parent.SiteId=@SiteId

END
