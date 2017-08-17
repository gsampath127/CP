CREATE TABLE [dbo].[TaxonomyAssociationClientDocument] (
    [TaxonomyAssociationId] INT      NOT NULL,
    [ClientDocumentId]      INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME CONSTRAINT [DF_TaxonomyAssociationClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationClientDocument] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC, [ClientDocumentId] ASC),
    CONSTRAINT [FK_TaxonomyAssociationClientDocument_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId]),
    CONSTRAINT [FK_TaxonomyAssociationClientDocument_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

