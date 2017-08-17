CREATE TABLE [dbo].[TemplateText] (
    [TemplateId]  INT            NOT NULL,
    [ResourceKey] VARCHAR (200)  NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [IsHtml]      BIT            NULL,
    [DefaultText] NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateText] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [ResourceKey] ASC),
    CONSTRAINT [fk_TemplateText] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
);

