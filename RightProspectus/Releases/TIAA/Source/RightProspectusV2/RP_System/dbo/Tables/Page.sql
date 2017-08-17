CREATE TABLE [dbo].[Page] (
    [PageId]      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([PageId] ASC)
);

