CREATE TABLE [dbo].[TaxonomyLevelExternalId] (
    [Level]           INT            NOT NULL,
    [TaxonomyId]      INT            NOT NULL,
    [ExternalId]      NVARCHAR (100) NOT NULL,
	[IsPrimary]		  BIT		CONSTRAINT [DF_TaxonomyLevelExternalId_IsPrimary] DEFAULT ((0)) NOT NULL,
    [UtcModifiedDate] DATETIME       CONSTRAINT [DF_TaxonomyLevelExternalId_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT            NULL,
	[SendDocumentUpdate] BIT	CONSTRAINT [DF_TaxonomyLevelExternalId_SendDocumentUpdate] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TaxonomyLevelExternalId] PRIMARY KEY CLUSTERED ([Level] ASC, [TaxonomyId] ASC, [ExternalId] ASC),
    CONSTRAINT [IX_TaxonomyLevelExternalId] UNIQUE NONCLUSTERED ([ExternalId] ASC)
);

