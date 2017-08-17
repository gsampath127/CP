CREATE TYPE [dbo].[TT_TaxonomyIdMarketId] AS TABLE(
	[clientName] [nvarchar](50) NULL,
	[marketId] [nvarchar](50) NULL,
	[taxonomyId] [int] NULL,
	[level] [int] NULL
)
GO