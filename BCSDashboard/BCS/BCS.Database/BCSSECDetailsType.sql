USE [db1029]
GO

/****** Object:  UserDefinedTableType [dbo].[FundCompliDeliveryProsDetailType]    Script Date: 10/17/2014 17:14:43 ******/
CREATE TYPE [dbo].[SECDetailsType] AS TABLE(
	[Acc#] [nvarchar](255) NOT NULL,
	[EdgarID] [int] NOT NULL,
	[DateFiled] [datetime] NOT NULL,	
	[DocumentDate] [datetime] NOT NULL,
	[DocumentType] [nvarchar](3) NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[FormType] [nvarchar](50) NOT NULL
)
GO


