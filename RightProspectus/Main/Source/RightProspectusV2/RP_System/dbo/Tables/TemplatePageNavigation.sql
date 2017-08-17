CREATE TABLE [dbo].[TemplatePageNavigation] (
    [TemplateId]           INT            NOT NULL,
    [PageId]               INT            NOT NULL,
    [NavigationKey]        VARCHAR (200)  NOT NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [XslTransform]         XML            NULL,
    [DefaultNavigationXml] XML            NULL,
    [Description]          NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageNavigation] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [NavigationKey] ASC),
    CONSTRAINT [fk_TemplatePageNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]),
    CONSTRAINT [fk_TemplatePageNavigation2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
);

