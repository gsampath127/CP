CREATE TABLE [dbo].[Footnote] (
    [FootnoteId]                 INT            IDENTITY (1, 1) NOT NULL,
    [TaxonomyAssociationId]      INT            NULL,
    [TaxonomyAssociationGroupId] INT            NULL,
    [LanguageCulture]            VARCHAR (50)   NULL,
    [Text]                       NVARCHAR (MAX) NULL,
    [Order]                      INT            CONSTRAINT [DF_Footnote_Order] DEFAULT (0) NOT NULL,
    [UtcModifiedDate]            DATETIME       CONSTRAINT [DF_Footnote_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                 INT            NULL,
    CONSTRAINT [PK_Footnote] PRIMARY KEY CLUSTERED ([FootnoteId] ASC),
    CONSTRAINT [FK_Footnote_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]),
    CONSTRAINT [FK_Footnote_TaxonomyAssociationGroup] FOREIGN KEY ([TaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId])
);

