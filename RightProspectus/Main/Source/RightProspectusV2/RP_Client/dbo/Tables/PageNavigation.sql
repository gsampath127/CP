CREATE TABLE [dbo].[PageNavigation] (
    [PageNavigationId] INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]           INT           NOT NULL,
    [PageId]           INT           NOT NULL,
    [NavigationKey]    VARCHAR (200) NOT NULL,
	[CurrentVersion]   INT NOT NULL,
    [LanguageCulture]  VARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME      CONSTRAINT [DF_PageNavigation_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       INT           NULL,
    CONSTRAINT [PK_PageNavigation] PRIMARY KEY CLUSTERED ([PageNavigationId] ASC),
    CONSTRAINT [FK_PageNavigation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [IX_PageNavigation] UNIQUE NONCLUSTERED ([SiteId] ASC, [PageId] ASC, [NavigationKey] ASC, [LanguageCulture] ASC)
);

