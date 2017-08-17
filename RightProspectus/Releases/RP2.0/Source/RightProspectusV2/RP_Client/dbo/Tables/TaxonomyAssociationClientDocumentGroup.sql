CREATE TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] (
    [TaxonomyAssociationId] INT      NOT NULL,
    [ClientDocumentGroupId] INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME CONSTRAINT [DF_TaxonomyAssociationClientDocumentGroup_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationClientDocumentGroup] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC, [ClientDocumentGroupId] ASC),
    CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_ClientDocumentGroup] FOREIGN KEY ([ClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId]),
    CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

