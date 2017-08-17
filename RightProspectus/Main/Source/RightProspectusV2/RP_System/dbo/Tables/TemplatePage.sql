CREATE TABLE [dbo].[TemplatePage] (
    [TemplateId] INT NOT NULL,
    [PageId]     INT NOT NULL,
    CONSTRAINT [PK_TemplatePage] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC),
    CONSTRAINT [fk1_TemplatePage] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]),
    CONSTRAINT [fk2_TemplatePage] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
);

