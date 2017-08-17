CREATE TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] (
    [TaxonomyAssociationGroupId] INT      NOT NULL,
    [TaxonomyAssociationId]      INT      NOT NULL,
    [Order]                      INT      NULL,
    [UtcModifiedDate]            DATETIME CONSTRAINT [DF_TaxonomyAssociationGroupTaxonomyAssociation_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                 INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationGroupTaxonomyAssociation] PRIMARY KEY CLUSTERED ([TaxonomyAssociationGroupId] ASC, [TaxonomyAssociationId] ASC),
    CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]),
    CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociationGroup] FOREIGN KEY ([TaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId])
);

