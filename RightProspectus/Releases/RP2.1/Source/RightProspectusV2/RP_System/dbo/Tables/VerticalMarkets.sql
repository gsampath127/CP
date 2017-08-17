CREATE TABLE [dbo].[VerticalMarkets] (
    [VerticalMarketId]     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MarketName]           NVARCHAR (200) NOT NULL,
    [ConnectionStringName] VARCHAR (200)  NOT NULL,
    [DatabaseName]     VARCHAR (200)  NOT NULL,
    [MarketDescription]    NVARCHAR (400) NULL,
    [UtcModifiedDate]      DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]           INT            NOT NULL,
    CONSTRAINT [PK_VerticalMarkets] PRIMARY KEY CLUSTERED ([VerticalMarketId] ASC)
);

