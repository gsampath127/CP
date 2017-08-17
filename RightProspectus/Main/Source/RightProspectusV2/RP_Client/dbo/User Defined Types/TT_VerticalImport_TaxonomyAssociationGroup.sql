CREATE TYPE [dbo].[TT_VerticalImport_TaxonomyAssociationGroup] AS TABLE(
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CssClass] [varchar](50) NULL,
	[TaxonomyAssociationGroupId] [int] NULL,
	[SiteId] [int] NULL,
	[ParentTaxonomyAssociationId] [int] NULL,
	[ParentTaxonomyAssociationGroupId] [int] NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[Order] [int] NULL
)
GO