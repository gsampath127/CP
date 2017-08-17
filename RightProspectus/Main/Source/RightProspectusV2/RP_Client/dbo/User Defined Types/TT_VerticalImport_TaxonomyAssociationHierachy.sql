CREATE TYPE [dbo].[TT_VerticalImport_TaxonomyAssociationHierachy] AS TABLE(
	[ParentTaxonomyAssociationId] [int] NOT NULL,
	[ChildTaxonomyAssociationId] [int] NOT NULL,
	[RelationshipType] [int] NOT NULL,
	[Order] [int] NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL
)
GO