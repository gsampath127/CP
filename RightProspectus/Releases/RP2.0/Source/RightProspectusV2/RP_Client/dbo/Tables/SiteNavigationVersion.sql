CREATE TABLE [dbo].[SiteNavigationVersion] (
    [SiteNavigationId] INT      NOT NULL,
    [Version]          INT      NOT NULL,
    [NavigationXml]    XML      NOT NULL,
    [UtcCreateDate]    DATETIME CONSTRAINT [DF_SiteNavigationVersion_UtcCreateDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]        INT      NULL,
    CONSTRAINT [PK_SiteNavigationVersion] PRIMARY KEY CLUSTERED ([SiteNavigationId] ASC, [Version] ASC),
    CONSTRAINT [FK_SiteNavigationVersion_SiteNavigation] FOREIGN KEY ([SiteNavigationId]) REFERENCES [dbo].[SiteNavigation] ([SiteNavigationId])
);

