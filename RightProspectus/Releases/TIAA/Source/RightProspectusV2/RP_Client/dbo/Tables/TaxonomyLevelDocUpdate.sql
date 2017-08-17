CREATE TABLE [dbo].[TaxonomyLevelDocUpdate]
(
    [MarketId]				NVARCHAR(100)	NOT NULL,
	[DocumentTypeID]		INT				NOT NULL,
	[TaxonomyName]			NVARCHAR (1000)	NOT NULL,    
    [DocumentDate]			DATETIME		NULL,
	[DocumentUpdatedDate]	DATETIME		NULL,
	CONSTRAINT [PK_TaxonomyLevelDocUpdate] PRIMARY KEY CLUSTERED ([MarketId] ASC, [DocumentTypeID] ASC)   
);