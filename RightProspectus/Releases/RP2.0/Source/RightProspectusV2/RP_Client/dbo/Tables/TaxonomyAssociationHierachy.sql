CREATE TABLE [dbo].[TaxonomyAssociationHierachy] (
    [ParentTaxonomyAssociationId] INT      NOT NULL,
    [ChildTaxonomyAssociationId]  INT      NOT NULL,
    [RelationshipType]            INT      NOT NULL,
    [Order]                       INT      NULL,
    [UtcModifiedDate]             DATETIME CONSTRAINT [DF_TaxonomyAssociationHierachy_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                  INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationHierachy] PRIMARY KEY CLUSTERED ([ParentTaxonomyAssociationId] ASC, [ChildTaxonomyAssociationId] ASC, [RelationshipType] ASC),
    CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]),
    CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation1] FOREIGN KEY ([ChildTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

