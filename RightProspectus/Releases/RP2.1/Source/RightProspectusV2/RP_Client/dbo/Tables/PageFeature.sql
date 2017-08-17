CREATE TABLE [dbo].[PageFeature] (
    [SiteId]          INT           NOT NULL,
    [PageId]          INT           NOT NULL,
    [Key]             VARCHAR (200) NOT NULL,
    [FeatureMode]     INT           CONSTRAINT [DF_PageFeature_FeatureMode] DEFAULT ((0)) NOT NULL,
    [UtcModifiedDate] DATETIME      CONSTRAINT [DF_PageFeature_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT           NOT NULL,
    CONSTRAINT [PK_PageFeature] PRIMARY KEY CLUSTERED ([SiteId] ASC, [PageId] ASC, [Key] ASC),
    CONSTRAINT [FK_PageFeature_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId])
);

