CREATE TABLE [dbo].[PageNavigationVersion] (
    [PageNavigationId] INT      NOT NULL,
    [Version]          INT      NOT NULL,
    [NavigationXml]    XML      NOT NULL,
    [UtcCreateDate]    DATETIME CONSTRAINT [DF_PageNavigationVersion_UtcCreateDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]        INT      NULL,
    CONSTRAINT [PK_PageNavigationVersion] PRIMARY KEY CLUSTERED ([PageNavigationId] ASC, [Version] ASC),
    CONSTRAINT [FK_PageNavigationVersion_PageNavigation] FOREIGN KEY ([PageNavigationId]) REFERENCES [dbo].[PageNavigation] ([PageNavigationId])
);

