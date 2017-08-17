
CREATE TYPE [dbo].[TT_VerticalImport_TaxonomyAssociation] AS TABLE(
	[TaxonomyAssociationId] [int] NULL,
	[Level] [int] NULL,
	[TaxonomyId] [int] NULL,
	[SiteId] [int] NULL,
	[ParentTaxonomyAssociationId] [int] NULL,
	[NameOverride] [nvarchar](400) NULL,
	[DescriptionOverride] [nvarchar](400) NULL,
	[CssClass] [nvarchar](100) NULL,
	[MarketId] [nvarchar](50) NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL
)
GO

