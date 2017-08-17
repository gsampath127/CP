CREATE TABLE [dbo].[DocumentTypeExternalId] (
    [DocumentTypeId]  INT            NOT NULL,
    [ExternalId]      NVARCHAR (100) NOT NULL,
	[IsPrimary]		  BIT CONSTRAINT [DF_DocumentTypeExternalId_IsPrimary] DEFAULT ((0)) NOT NULL,
    [UtcModifiedDate] DATETIME       CONSTRAINT [DF_DocumentTypeExternalId_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT            NULL,
    CONSTRAINT [PK_DocumentTypeExternalId] PRIMARY KEY CLUSTERED ([DocumentTypeId] ASC, [ExternalId] ASC),
    CONSTRAINT [IX_DocumentTypeExternalId] UNIQUE NONCLUSTERED ([ExternalId] ASC)
);

