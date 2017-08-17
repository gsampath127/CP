CREATE TABLE [dbo].[ClientDocumentGroupClientDocument] (
    [ClientDocumentGroupId] INT      NOT NULL,
    [ClientDocumentId]      INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME CONSTRAINT [DF_ClientDocumentGroupClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_ClientDocumentGroupClientDocument] PRIMARY KEY CLUSTERED ([ClientDocumentGroupId] ASC, [ClientDocumentId] ASC),
    CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId]),
    CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocumentGroup] FOREIGN KEY ([ClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId])
);

