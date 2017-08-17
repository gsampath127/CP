CREATE TABLE [dbo].[TaxonomyAssociationMetaData] (
    [TaxonomyAssociationID] INT            NOT NULL,
    [Key]                   VARCHAR (20)   NOT NULL,
    [DataType]              INT            NOT NULL,
    [Order]                 INT            NULL,
    [IntegerValue]          INT            NULL,
    [BooleanValue]          BIT            NULL,
    [DateTimeValue]         DATETIME       NULL,
    [StringValue]           NVARCHAR (MAX) NULL,
    [UtcModifiedDate]       DATETIME       CONSTRAINT [DF_TaxonomyAssociationMetaData_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            INT            NULL,
    CONSTRAINT [PK_TaxonomyAssociationMetaData] PRIMARY KEY CLUSTERED ([TaxonomyAssociationID] ASC, [Key] ASC),
    CONSTRAINT [FK_TaxonomyAssociationMetaData_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationID]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId])
);

