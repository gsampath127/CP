CREATE TABLE [dbo].[ClientDocumentType] (
    [ClientDocumentTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (400) NULL,
    [UtcModifiedDate]      DATETIME       CONSTRAINT [DF_ClientDocumentType_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]           INT            NULL,
    CONSTRAINT [PK_ClientDocumentType] PRIMARY KEY CLUSTERED ([ClientDocumentTypeId] ASC)
);

