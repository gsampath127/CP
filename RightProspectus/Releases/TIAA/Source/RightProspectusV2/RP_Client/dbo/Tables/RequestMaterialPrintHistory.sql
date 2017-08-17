CREATE TABLE [dbo].[RequestMaterialPrintHistory](
	[RequestMaterialPrintHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,	
	[ClientCompanyName] [nvarchar](100) NULL,	
	[ClientFirstName] [nvarchar](100) NULL,
	[ClientMiddleName] [nvarchar](100) NULL,
	[ClientLastName] [nvarchar](100) NULL,
	[ClientName] [nvarchar](200) NULL,
	[Address1] [nvarchar](200) NULL,
	[Address2] [nvarchar](200) NULL,
	[Address3] [nvarchar](200) NULL,
	[City] [nvarchar](100) NULL,
	[StateOrProvince] [nvarchar](100) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Country] [nvarchar](200) NULL,
	[RequestDateUtc] [datetime] NOT NULL CONSTRAINT DF_RequestMaterialPrintHistory_RequestDateUtc DEFAULT (GETUTCDATE()),
	[UniqueID] [uniqueIdentifier] NOT NULL,
	[RequestBatchId] [uniqueIdentifier] NULL,
	[RequestUri] [int] NULL,
	[UserAgentId] [int] NULL,
	[IPAddress] [varchar](15) NULL,
	[ReferrerUri] [int] null,	
 CONSTRAINT [PK_RequestMaterialPrintHistory] PRIMARY KEY CLUSTERED 
(
	[RequestMaterialPrintHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
