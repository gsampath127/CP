CREATE TABLE [dbo].[RequestMaterialEmailHistory](
	[RequestMaterialEmailHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[RecipEmail] [nvarchar](150) NULL,
	[RequestDateUtc] [datetime] NOT NULL CONSTRAINT DF_RequestMaterialEmailHistory_RequestDateUtc DEFAULT (GETUTCDATE()),
	[UniqueID] [uniqueIdentifier] NOT NULL,	
	[RequestBatchId] [uniqueIdentifier] NULL,
	[FClickDateUtc] [datetime] NULL,
	[RequestUri] [int] NULL,
	[UserAgentId] [int] NULL,
	[IPAddress] [varchar](15) NULL,
	[ReferrerUri] [int] null,
	[Sent] [bit] NULL,	
	CONSTRAINT [PK_RequestMaterialEmailHistory] PRIMARY KEY CLUSTERED 
	(
		[RequestMaterialEmailHistoryId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
