CREATE TABLE [dbo].[ClientDns] (
    [ClientDnsId]     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClientId]        INT           NOT NULL,
	[SiteId]          INT           NOT NULL,
    [Dns]             VARCHAR (255) NOT NULL,
    [UtcModifiedDate] DATETIME      DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT           NOT NULL,
    CONSTRAINT [PK_ClientDns] PRIMARY KEY CLUSTERED ([ClientDnsId] ASC),
    CONSTRAINT [fk_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId])
);

