CREATE TABLE [dbo].[TemplateNavigation] (
    [TemplateId]           INT            NOT NULL,
    [NavigationKey]        VARCHAR (200)  NOT NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [XslTransform]         XML            NULL,
    [DefaultNavigationXml] XML            NULL,
    [Description]          NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateNavigation] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [NavigationKey] ASC),
    CONSTRAINT [fk_TemplateNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
);

