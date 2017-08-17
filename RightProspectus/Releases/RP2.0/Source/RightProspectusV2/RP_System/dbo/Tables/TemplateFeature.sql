CREATE TABLE [dbo].[TemplateFeature] (
    [TemplateId]  INT            NOT NULL,
    [Key]         NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateFeature] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [Key] ASC),
    CONSTRAINT [fk1_TemplateFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
);

