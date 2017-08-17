CREATE TABLE [dbo].[DocType] (
    [DocTypeId]      VARCHAR (4)    NOT NULL,
    [DocTypeDesc]    VARCHAR (255)  NULL,
    [DocPriority]    INT            NULL,
    [DocumentTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100) NULL,
    CONSTRAINT [PK_DocType] PRIMARY KEY CLUSTERED ([DocTypeId] ASC)
);

