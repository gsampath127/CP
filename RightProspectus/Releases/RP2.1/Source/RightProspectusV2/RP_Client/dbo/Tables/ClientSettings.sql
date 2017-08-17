CREATE TABLE [dbo].[ClientSettings] (
    [ClientId]        INT      NOT NULL CONSTRAINT ClientID_Unique_Constraint UNIQUE(ClientId),
    [DefaultSiteId]   INT      NOT NULL,
    [UtcModifiedDate] DATETIME CONSTRAINT [DF_ClientSettings_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT      NULL,
    CONSTRAINT [FK_ClientSettings_Site] FOREIGN KEY ([DefaultSiteId]) REFERENCES [dbo].[Site] ([SiteId])
);

