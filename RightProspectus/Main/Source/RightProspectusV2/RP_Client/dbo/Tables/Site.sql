CREATE TABLE [dbo].[Site] (
    [SiteId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [TemplateId]      INT            NOT NULL,
    [DefaultPageId]   INT            NOT NULL,
    [ParentSiteId]    INT            NULL,
    [Description]     NVARCHAR (400) NULL,
    [UtcModifiedDate] DATETIME       CONSTRAINT [DF_Site_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT            NULL,
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([SiteId] ASC),
    CONSTRAINT [FK_Site_Site] FOREIGN KEY ([ParentSiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [IX_Site] UNIQUE NONCLUSTERED ([Name] ASC)
);

