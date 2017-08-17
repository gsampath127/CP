CREATE TABLE [dbo].[TemplatePageFeature] (
    [TemplateId]  INT            NOT NULL,
    [PageId]      INT            NOT NULL,
    [Key]         NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageFeature] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [Key] ASC),
    CONSTRAINT [fk1_TemplatePageFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]),
    CONSTRAINT [fk2_TemplatePageFeature] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
);

