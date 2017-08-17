CREATE TABLE [dbo].[ClientSettings] (
    [ClientId]        INT      NOT NULL,
    [DefaultSiteId]   INT      NOT NULL,
    [UtcModifiedDate] DATETIME CONSTRAINT [DF_ClientSettings_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT      NULL
);

