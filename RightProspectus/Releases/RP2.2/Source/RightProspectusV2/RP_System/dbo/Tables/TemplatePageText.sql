CREATE TABLE [dbo].[TemplatePageText] (
    [TemplateId]  INT            NOT NULL,
    [PageId]      INT            NOT NULL,
    [ResourceKey] VARCHAR (200)  NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [IsHtml]      BIT            NULL,
    [DefaultText] NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageText] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [ResourceKey] ASC),
    CONSTRAINT [fk_TemplatePageText1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]),
    CONSTRAINT [fk_TemplatePageText2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
);

