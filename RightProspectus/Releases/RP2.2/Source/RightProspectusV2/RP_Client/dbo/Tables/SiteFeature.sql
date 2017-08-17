CREATE TABLE [dbo].[SiteFeature] (
    [SiteId]          INT           NOT NULL,
    [Key]             VARCHAR (200) NOT NULL,
    [FeatureMode]     INT           CONSTRAINT [DF_SiteFeature_FeatureMode] DEFAULT ((0)) NOT NULL,
    [UtcModifiedDate] DATETIME      CONSTRAINT [DF_SiteFeature_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_SiteFeature] PRIMARY KEY CLUSTERED ([SiteId] ASC, [Key] ASC),
    CONSTRAINT [FK_SiteFeature_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId])
);

