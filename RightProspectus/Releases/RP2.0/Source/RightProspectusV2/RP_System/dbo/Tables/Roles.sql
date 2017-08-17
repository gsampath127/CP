CREATE TABLE [dbo].[Roles] (
    [RoleId]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [UtcModifiedDate] DATETIME       DEFAULT (getutcdate()) NULL,
    [ModifiedBy]      INT            NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

