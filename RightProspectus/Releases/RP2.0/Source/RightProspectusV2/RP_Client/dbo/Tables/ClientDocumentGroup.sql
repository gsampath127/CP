CREATE TABLE [dbo].[ClientDocumentGroup] (
    [ClientDocumentGroupId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]                        NVARCHAR (100) NOT NULL,
    [Description]                 NVARCHAR (400) NULL,
    [ParentClientDocumentGroupId] INT            NULL,
    [CssClass]                    VARCHAR (50)   NULL,
    [UtcModifiedDate]             DATETIME       CONSTRAINT [DF_ClientDocumentGroup_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                  INT            NULL,
    CONSTRAINT [PK_ClientDocumentGroup] PRIMARY KEY CLUSTERED ([ClientDocumentGroupId] ASC),
    CONSTRAINT [FK_ClientDocumentGroup_ClientDocumentGroup] FOREIGN KEY ([ParentClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId])
);

