CREATE TYPE [dbo].[TT_VerticalImport_Footnote] AS TABLE(
	[FootnoteId] [int] NOT NULL,
	[TaxonomyAssociationId] [int] NULL,
	[TaxonomyAssociationGroupId] [int] NULL,
	[LanguageCulture] [varchar](50) NULL,
	[Text] [nvarchar](1000) NULL,
	[Order] [int] NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL
)
GO