USE [db1029]
GO

CREATE TABLE [dbo].[FirstDollarDocType](
	[DocTypeId] [varchar](4) NOT NULL,
	[DocTypeDesc] [varchar](255) NULL,
	[DocPriority] [int] NULL,
 CONSTRAINT [PK_FirstDollarDocType] PRIMARY KEY CLUSTERED 
(
	[DocTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [db1029].[dbo].[FirstDollarDocType]([DocTypeId] ,[DocTypeDesc] ,[DocPriority])
     VALUES ('SP' ,'Summary Prospectus' ,1)
GO

INSERT INTO [db1029].[dbo].[FirstDollarDocType]([DocTypeId] ,[DocTypeDesc] ,[DocPriority])
     VALUES ('RSP' ,'Revised Summary Prospectus' ,2)
GO

INSERT INTO [db1029].[dbo].[FirstDollarDocType]([DocTypeId] ,[DocTypeDesc] ,[DocPriority])
     VALUES ('SPS' ,'Summary Prospectus Supplement' ,3)
GO


USE [db1029]
GO

CREATE TABLE [dbo].[BCSExceptionLog](
	[ExceptionID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,	
	[ExceptionMessage] [varchar](max) NULL,
	[ApplicationName] [varchar](250) NULL,
	[AUDIT_TS] [datetime] NULL,
 CONSTRAINT [PK_BCSExceptionLog] PRIMARY KEY CLUSTERED 
(
	[ExceptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TYPE [dbo].[TT_SLinkDocumentId] AS TABLE(
	[DocumentId] [nvarchar](50) NULL
)
GO