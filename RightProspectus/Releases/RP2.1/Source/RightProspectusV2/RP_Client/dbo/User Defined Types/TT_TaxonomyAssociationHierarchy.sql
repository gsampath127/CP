CREATE TYPE [dbo].[TT_TaxonomyAssociationHierarchy] AS TABLE
(
	parenttaxonomyassociationsystemid int,
	parentlevel int,
	parenttaxonomymarketId nvarchar(50),
	relationshipType int,
	childimportid nvarchar(60),
	[deleteparent] bit,
	[deletechild] bit
)
