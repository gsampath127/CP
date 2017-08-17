CREATE TABLE [dbo].[DocumentTypeAssociation] (
    [DocumentTypeAssociationId] INT            IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]            INT            NOT NULL,
    [SiteId]                    INT            NULL,
    [TaxonomyAssociationId]     INT            NULL,
    [Order]                     INT            NULL,
    [HeaderText]                NVARCHAR (100) NULL,
    [LinkText]                  NVARCHAR (100) NULL,
    [DescriptionOverride]       NVARCHAR (400) NULL,
    [CssClass]                  VARCHAR (50)   NULL,
	[MarketId]                  NVARCHAR (50)   NULL,
    [UtcModifiedDate]           DATETIME       CONSTRAINT [DF_DocumentTypeAssociation_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                INT            NULL,
    [IsProofing] BIT NOT NULL, 
    CONSTRAINT [PK_DocumentTypeAssociation] PRIMARY KEY CLUSTERED ([DocumentTypeAssociationId] ASC),
    CONSTRAINT [FK_DocumentTypeAssociation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [FK_DocumentTypeAssociation_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

