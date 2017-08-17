CREATE TABLE [dbo].[SiteText] (
    [SiteTextId]      INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]          INT           NOT NULL,
    [ResourceKey]     VARCHAR (200) NOT NULL,
    [CurrentVersion]  INT           CONSTRAINT [DF_SiteText_CurrentVersion] DEFAULT ((0)) NOT NULL,
    [LanguageCulture] VARCHAR (50)  NULL,
    [UtcModifiedDate] DATETIME      CONSTRAINT [DF_SiteText_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_SiteText] PRIMARY KEY CLUSTERED ([SiteTextId] ASC),
    CONSTRAINT [FK_SiteText_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [IX_SiteText] UNIQUE NONCLUSTERED ([SiteId] ASC, [ResourceKey] ASC, [LanguageCulture] ASC)
);

