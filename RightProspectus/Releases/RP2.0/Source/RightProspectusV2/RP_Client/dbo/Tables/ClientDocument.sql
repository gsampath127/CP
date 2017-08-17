CREATE TABLE [dbo].[ClientDocument] (
    [ClientDocumentId]     INT             IDENTITY (1, 1) NOT NULL,
    [ClientDocumentTypeId] INT             NOT NULL,
    [FileName]             NVARCHAR (260)  NOT NULL,
    [MimeType]             NVARCHAR (127)  NOT NULL,
    [IsPrivate]            BIT             NOT NULL,
    [ContentUri]           NVARCHAR (2083) NULL,
    [Name]                 NVARCHAR (100)  NULL,
    [Description]          NVARCHAR (400)  NULL,
    [UtcModifiedDate]      DATETIME        CONSTRAINT [DF_ClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]           INT             NULL,
    CONSTRAINT [PK_ClientDocument] PRIMARY KEY CLUSTERED ([ClientDocumentId] ASC),
    CONSTRAINT [FK_ClientDocument_ClientDocumentType] FOREIGN KEY ([ClientDocumentTypeId]) REFERENCES [dbo].[ClientDocumentType] ([ClientDocumentTypeId])
);

