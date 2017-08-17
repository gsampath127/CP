USE [db1029]
GO


Alter TABLE [dbo].[ProsDocs]
	Add [PageCount] [int] NULL,
	[PageSizeHeight] [decimal](5, 2) NULL,
	[PageSizeWidth] [decimal](5, 2) NULL
	
Alter TABLE [dbo].[BCSDocUpdate]
	Add [PageCount] [int] NULL,
	[PageSizeHeight] [decimal](5, 2) NULL,
	[PageSizeWidth] [decimal](5, 2) NULL	

CREATE TABLE [dbo].[BCSTRPFLT](
	[BCSTRPFLTID] [int] IDENTITY(1,1) NOT NULL,
	[FUNDCODE] [nvarchar](7) NULL,
	[FUNDNAME] [nvarchar](40) NULL,
	[FUNDTYPE] [nvarchar](7) NULL,
	[FUNDTELEACCESSCODE] [nvarchar](3) NULL,
	[FUNDCUSIPNUMBER] [nvarchar](8) NULL,
	[FUNDCHKHEADINGCODE] [nvarchar](2) NULL,
	[FUNDGROUPNUMBER] [nvarchar](11) NULL,	
	[FUNDPROSPECTUSINSERT] [nvarchar](1) NULL,
	[FUNDPROSPECTUSINSERT2] [nvarchar](1) NULL,
	[FUNDTICKERSYMBOL] [nvarchar](5) NULL,
	[FUNDDocName] [nvarchar](5) NULL,	
	[DateFLTRecordHasChanged] [datetime] NULL,
	[DatePDFReceivedonFTP] [datetime] NULL
 CONSTRAINT [PK_BCSFLT] PRIMARY KEY CLUSTERED 
(
	[BCSTRPFLTID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[BCSTRPFLTHistory](
	[BCSTRPFLTHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[BCSTRPFLTID] [int] NULL,
	[FUNDCODE] [nvarchar](7) NULL,
	[FUNDNAME] [nvarchar](40) NULL,
	[FUNDTYPE] [nvarchar](7) NULL,
	[FUNDTELEACCESSCODE] [nvarchar](3) NULL,
	[FUNDCUSIPNUMBER] [nvarchar](8) NULL,
	[FUNDCHKHEADINGCODE] [nvarchar](2) NULL,
	[FUNDGROUPNUMBER] [nvarchar](11) NULL,	
	[FUNDPROSPECTUSINSERT] [nvarchar](1) NULL,
	[FUNDPROSPECTUSINSERT2] [nvarchar](1) NULL,
	[FUNDTICKERSYMBOL] [nvarchar](5) NULL,
	[FUNDDocName] [nvarchar](5) NULL,	
	[DateFLTRecordHasChanged] [datetime] NULL,
	[DatePDFReceivedonFTP] [datetime] NULL,
	[AuditDate]  [datetime] NOT NULL,
	[AuditAction] [nvarchar](250) NOT NULL
 CONSTRAINT [PK_BCSFLTHistory] PRIMARY KEY CLUSTERED 
(
	[BCSTRPFLTHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


GO



CREATE TYPE [dbo].[BCSFLTTableType] AS TABLE(	
	[FUNDCODE] [nvarchar](7) NULL,
	[FUNDNAME] [nvarchar](40) NULL,
	[FUNDTYPE] [nvarchar](7) NULL,
	[FUNDTELEACCESSCODE] [nvarchar](3) NULL,
	[FUNDCUSIPNUMBER] [nvarchar](8) NULL,
	[FUNDCHKHEADINGCODE] [nvarchar](2) NULL,
	[FUNDGROUPNUMBER] [nvarchar](11) NULL,
	[FUNDPROSPECTUSINSERT] [nvarchar](1) NULL,
	[FUNDPROSPECTUSINSERT2] [nvarchar](1) NULL,
	[FUNDTICKERSYMBOL] [nvarchar](5) NULL,
	[FUNDDocName] [nvarchar](5) NULL
)
GO


--Triggers

CREATE TRIGGER [dbo].[BCSTRPFLTInsert]
       ON [dbo].[BCSTRPFLT]
       FOR INSERT
AS
Begin
  INSERT INTO BCSTRPFLTHistory(
		[BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,[AuditDate]
      ,[AuditAction])
 SELECT [BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,GETDATE()
	  ,'Inserted'
  FROM inserted	
		
End
GO

CREATE TRIGGER [dbo].[BCSTRPFLTUpdate]
       ON [dbo].[BCSTRPFLT]
       FOR UPDATE
AS
Begin
  INSERT INTO BCSTRPFLTHistory(
		[BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,[AuditDate]
      ,[AuditAction])
 SELECT [BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,GETDATE()
	  ,'Updated'
  FROM deleted	
		
End
GO

CREATE TRIGGER [dbo].[BCSTRPFLTDelete]
       ON [dbo].[BCSTRPFLT]
       FOR DELETE
AS
Begin
  INSERT INTO BCSTRPFLTHistory
		([BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,[AuditDate]
      ,[AuditAction])
 SELECT [BCSTRPFLTID]
		,[FUNDCODE] 
      ,[FUNDNAME]
      ,[FUNDTYPE]
      ,[FUNDTELEACCESSCODE]
      ,[FUNDCUSIPNUMBER]
      ,[FUNDCHKHEADINGCODE]
      ,[FUNDGROUPNUMBER]
      ,[FUNDPROSPECTUSINSERT]
      ,[FUNDPROSPECTUSINSERT2]
      ,[FUNDTICKERSYMBOL]
      ,[FUNDDocName]
      ,[DateFLTRecordHasChanged]
      ,[DatePDFReceivedonFTP]
      ,GETDATE()
	  ,'Deleted'
  FROM deleted	
		
End
GO


USE [db1029]
GO



CREATE TABLE [dbo].[BCSTRPFLTFileInfo](
	[BCSTRPFLTFileInfoID] [int] IDENTITY(1,1) NOT NULL,	
	[FileName] [nvarchar](40) NULL,	
	[DateReceived] [datetime] NULL,
 CONSTRAINT [PK_BCSTRPFLTFileInfo] PRIMARY KEY CLUSTERED 
(
	[BCSTRPFLTFileInfoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO







