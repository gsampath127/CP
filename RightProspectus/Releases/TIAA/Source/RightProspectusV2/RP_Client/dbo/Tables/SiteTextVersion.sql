CREATE TABLE [dbo].[SiteTextVersion] (
    [SiteTextId]    INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [Text]          NVARCHAR (MAX) NULL,
    [UtcCreateDate] DATETIME       CONSTRAINT [DF_SiteTextVersion_UtcCreateDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]     INT            NULL,
    CONSTRAINT [PK_SiteTextVersion] PRIMARY KEY CLUSTERED ([SiteTextId] ASC, [Version] ASC),
    CONSTRAINT [FK_SiteTextVersion_SiteText] FOREIGN KEY ([SiteTextId]) REFERENCES [dbo].[SiteText] ([SiteTextId])
);

