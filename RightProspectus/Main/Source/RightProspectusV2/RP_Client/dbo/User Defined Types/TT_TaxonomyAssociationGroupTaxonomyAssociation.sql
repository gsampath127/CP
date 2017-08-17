CREATE TYPE [dbo].[TT_TaxonomyAssociationGroupTaxonomyAssociation] AS TABLE(
	[TaxonomyAssociationGroupId] [int] NOT NULL,
	[TaxonomyAssociationId] [int] NOT NULL,
	[Order] [int] NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL
)
GO

