
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[ClientName] [nvarchar](200) NOT NULL CONSTRAINT ClientName_Unique_Constraint UNIQUE(ClientName),
	[ConnectionStringName] [varchar](200) NOT NULL,
	[DatabaseName] [varchar](200) NOT NULL,
	[VerticalMarketId] [int] NOT NULL,
	[ClientDescription] [nvarchar](400) NULL,
	[UtcModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [dbo].[Clients] ADD  DEFAULT (getutcdate()) FOR [UtcModifiedDate]
GO


GO

ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [fk1_Clients] FOREIGN KEY([VerticalMarketId])
REFERENCES [dbo].[VerticalMarkets] ([VerticalMarketId])
GO

ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [fk1_Clients]
GO


