CREATE TABLE [dbo].[PageText] (
    [PageTextId]      INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]          INT           NOT NULL,
    [PageId]          INT           NOT NULL,
    [ResourceKey]     VARCHAR (200) NOT NULL,
    [CurrentVersion]  INT           CONSTRAINT [DF_PageText_CurrentVersion] DEFAULT ((0)) NOT NULL,
    [LanguageCulture] VARCHAR (50)  NULL,
    [UtcModifiedDate] DATETIME      CONSTRAINT [DF_PageText_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_PageText] PRIMARY KEY CLUSTERED ([PageTextId] ASC),
    CONSTRAINT [FK_PageText_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [IX_PageText] UNIQUE NONCLUSTERED ([SiteId] ASC, [PageId] ASC, [ResourceKey] ASC, [CurrentVersion] ASC)
);

