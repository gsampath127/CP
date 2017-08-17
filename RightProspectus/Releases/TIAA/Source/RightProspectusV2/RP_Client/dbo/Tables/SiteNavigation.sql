CREATE TABLE [dbo].[SiteNavigation] (
    [SiteNavigationId] INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]           INT           NOT NULL,
    [NavigationKey]    VARCHAR (200) NOT NULL,
    [PageId]           INT           NULL,
	[CurrentVersion]   INT			 NOT NULL,
    [LanguageCulture]  VARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME      CONSTRAINT [DF_SiteNavigation_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       INT           NULL,
    CONSTRAINT [PK_SiteNavigation] PRIMARY KEY CLUSTERED ([SiteNavigationId] ASC),
    CONSTRAINT [FK_SiteNavigation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId])
);

