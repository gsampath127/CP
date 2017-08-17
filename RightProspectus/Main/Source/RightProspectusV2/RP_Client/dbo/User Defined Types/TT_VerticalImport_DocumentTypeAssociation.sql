
/****** Object:  UserDefinedTableType [dbo].[TT_VerticalImport_DocumentTypeAssociation]    Script Date: 1/20/2017 3:15:21 PM ******/
CREATE TYPE [dbo].[TT_VerticalImport_DocumentTypeAssociation] AS TABLE(
	[DocumentTypeAssociationId] [int] NULL,
	[DocumentTypeId] [int] NULL,
	[SiteId] [int] NULL,
	[TaxonomyAssociationId] [int] NULL,
	[Order] [int] NULL,
	[HeaderText] [nvarchar](100) NULL,
	[LinkText] [nvarchar](100) NULL,
	[DescriptionOverride] [nvarchar](400) NULL,
	[CssClass] [nvarchar](100) NULL,
	[MarketId] [nvarchar](50) NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL
)
GO

