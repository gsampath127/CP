CREATE TABLE [dbo].[ClientDocumentData] (
    [ClientDocumentId] INT             NOT NULL,
    [Data]             VARBINARY (MAX) NULL,
    [HasData]          AS              (case when [Data] IS NULL then (0) else (1) end) PERSISTED NOT NULL,
    [DataLength]       AS              (len([Data])) PERSISTED,   
    [DataHash]		   AS (CONVERT([varbinary](20),case [Data] when NULL then NULL else [dbo].[fnHashBytesNVARCHARMAX]('SHA1',[Data]) end,0)) PERSISTED,
    [UtcModifiedDate]  DATETIME        CONSTRAINT [DF_ClientDocumentData_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       INT             NULL,
    CONSTRAINT [PK_ClientDocumentData] PRIMARY KEY CLUSTERED ([ClientDocumentId] ASC),
    CONSTRAINT [FK_ClientDocumentData_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId])
);

