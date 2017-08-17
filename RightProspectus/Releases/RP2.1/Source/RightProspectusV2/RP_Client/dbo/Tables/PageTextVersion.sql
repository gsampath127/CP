CREATE TABLE [dbo].[PageTextVersion] (
    [PageTextId]    INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [Text]          NVARCHAR (MAX) NULL,
    [UtcCreateDate] DATETIME       CONSTRAINT [DF_PageTextVersion_UtcCreateDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]     INT            NULL,
    CONSTRAINT [PK_PageTextVersion] PRIMARY KEY CLUSTERED ([PageTextId] ASC, [Version] ASC),
    CONSTRAINT [FK_PageTextVersion_PageText] FOREIGN KEY ([PageTextId]) REFERENCES [dbo].[PageText] ([PageTextId])
);

