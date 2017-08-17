CREATE TABLE [dbo].[TaxonomyAssociation] (
    [TaxonomyAssociationId]       INT            IDENTITY (1, 1) NOT NULL,
    [Level]                       INT            NOT NULL,
    [TaxonomyId]                  INT            NOT NULL,
    [SiteId]                      INT            NULL,
    [ParentTaxonomyAssociationId] INT            NULL,
    [NameOverride]                NVARCHAR (200) NULL,
    [DescriptionOverride]         NVARCHAR (400) NULL,
    [CssClass]                    VARCHAR (50)   NULL,
	[MarketId]                    NVARCHAR (50)   NULL,
    [UtcModifiedDate]             DATETIME       CONSTRAINT [DF_TaxonomyAssociation_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                  INT            NULL,
	[TabbedPageNameOverride]      NVARCHAR(200)	 NULL,
    [IsProofing]				  BIT			 NOT NULL CONSTRAINT [DF_TaxonomyAssociation_IsProofing]  DEFAULT ((1)),
    [Order]						  INT			 NULL, 
    CONSTRAINT [PK_TaxonomyAssociation] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC),
    CONSTRAINT [FK_TaxonomyAssociation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [FK_TaxonomyAssociation_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

