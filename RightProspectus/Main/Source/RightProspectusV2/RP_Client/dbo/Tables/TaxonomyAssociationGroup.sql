CREATE TABLE [dbo].[TaxonomyAssociationGroup] (
    [TaxonomyAssociationGroupId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]                             NVARCHAR (100) NOT NULL,
    [Description]                      NVARCHAR (400) NULL,
    [SiteId]                           INT            NULL,
    [ParentTaxonomyAssociationId]      INT            NULL,
    [ParentTaxonomyAssociationGroupId] INT            NULL,
    [CssClass]                         VARCHAR (50)   NULL,
    [UtcModifiedDate]                  DATETIME       CONSTRAINT [DF_TaxonomyAssociationGroup_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                       INT            NULL,
	[Order]							   INT            NULL,
	[IsProofing]					   BIT			  NOT NULL CONSTRAINT [DF_TaxonomyAssociationGroup_IsProofing]  DEFAULT ((1)),
	[UniqueGroupId]					   uniqueidentifier  NOT NULL CONSTRAINT [DF_TaxonomyAssociationGroup_UniqueGroupId]  DEFAULT ((newID())),
    CONSTRAINT [PK_TaxonomyAssociationGroup] PRIMARY KEY CLUSTERED ([TaxonomyAssociationGroupId] ASC),
    CONSTRAINT [FK_TaxonomyAssociationGroup_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]),
    CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociationGroup] FOREIGN KEY ([ParentTaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId])
);

