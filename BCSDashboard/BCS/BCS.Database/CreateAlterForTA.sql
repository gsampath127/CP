
ALTER TABLE [dbo].[BCSDocUpdateGIMSlink] ADD IsAPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateGIMSlink_IsAPC]  DEFAULT ((0)),
											 APCReceivedDate DateTime NULL,
											 IsOPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateGIMSlink_IsOPC]  DEFAULT ((0)),
											 OPCReceivedDate DateTime NULL
GO


Alter table BCSClientConfig
 add SendIPDocUpdate bit default(0) not null,
   IsCUSIPWatchListProvided  bit default(0) not null,
   CUSIPWatchlistPickupFTPLocation nvarchar(500) null,
   CUSIPWatchlistPickupFTPUserName nvarchar(50) null,
   CUSIPWatchlistPickupFTPPassword nvarchar(50) null,
   CUSIPWatchListArchiveDropPath NVARCHAR (500) NULL,
   IncludeRemovedWatchListCUSIPInIPDocUpdate bit default(0) not null,
   IPDeliveryMethod nvarchar(200) NULL,
   IPFileNamePrefix nvarchar(100) NULL
   
   
CREATE TABLE [dbo].[BCSTransamericaWatchListCUSIPs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSIP] [nvarchar](50) NULL,
	[FundName] [nvarchar](500) NULL,
	[CIK] [nvarchar](50) NULL,
	[SeriesID] [nvarchar](50) NULL,
	[ClassContractID] [nvarchar](50) NULL,
	[TickerSymbol] [nvarchar](10) NULL,
	[Class] [nvarchar](100) NULL,
 CONSTRAINT [PK_BCSTransamericaWatchListCUSIPs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSTransamericaWatchListCUSIPsHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NOT NULL,
	[CUSIP] [nvarchar](50) NULL,
	[FundName] [nvarchar](500) NULL,
	[CIK] [nvarchar](50) NULL,
	[SeriesID] [nvarchar](50) NULL,
	[ClassContractID] [nvarchar](50) NULL,
	[TickerSymbol] [nvarchar](10) NULL,
	[Class] [nvarchar](100) NULL,
	[InsertionDate] [datetime] NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_BCSTransamericaWatchListCUSIPsHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSDocUpdateSupplements](
	[BCSDocUpdateSupplementsID] [int] IDENTITY(1,1) NOT NULL,
	[EdgarID] [int] NOT NULL,
	[Acc#] [nvarchar](250) NOT NULL,
	[PDFName] [nvarchar](55) NULL,
	[DocumentType] [nvarchar](4) NOT NULL,
	[FormType] [nvarchar](200) NOT NULL,
	[CUSIP] [nvarchar](10) NOT NULL,
	[TickerID] [int] NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[DocumentDate] [datetime] NOT NULL,
	[ProsID] [int] NOT NULL,
	[FundName] [nvarchar](255) NOT NULL,
	[ProsDocID] [int] NULL,
	[IsFiled] [bit] NOT NULL,
	[FiledDate] [datetime] NOT NULL,
	[FilingStatusAddedDate] [datetime] NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[ProcessedDate] [datetime] NULL,
	[PageCount] [int] NULL,
	[PageSizeHeight] [decimal](5, 2) NULL,
	[PageSizeWidth] [decimal](5, 2) NULL,
	[BCSDocUpdateSupplementsSlinkID] [int] NULL,
	[IsRemoved] [bit] NOT NULL CONSTRAINT [DF_BCSDocUpdateSupplements_IsRemoved]  DEFAULT ((0))
 CONSTRAINT [PK_BCSDocUpdateSupplements] PRIMARY KEY CLUSTERED 
(
	[BCSDocUpdateSupplementsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BCSDocUpdateSupplements] ADD  CONSTRAINT [DF_BCSDocUpdateSupplements_IsFiled]  DEFAULT ((0)) FOR [IsFiled]
GO

ALTER TABLE [dbo].[BCSDocUpdateSupplements] ADD  CONSTRAINT [DF_BCSDocUpdateSupplements_IsProcessed]  DEFAULT ((0)) FOR [IsProcessed]
GO


CREATE TABLE [dbo].[BCSDocUpdateSupplementsSlink](
	[BCSDocUpdateSupplementsSlinkID] [int] IDENTITY(1,1) NOT NULL,
	[ZipFileName] [nvarchar](255) NOT NULL,
	[IsExported] [bit] NOT NULL,
	[ExportedDate] [datetime] NULL,
	[IsAPF] [bit] NOT NULL,
	[APFReceivedDate] [datetime] NULL,
	[IsOPF] [bit] NOT NULL,
	[OPFReceivedDate] [datetime] NULL,
	IsAPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateSupplementsSlink_IsAPC]  DEFAULT ((0)),
	APCReceivedDate DateTime NULL,
	IsOPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateSupplementsSlink_IsOPC]  DEFAULT ((0)),
	OPCReceivedDate DateTime NULL,
 CONSTRAINT [PK_BCSDocUpdateSupplementsSlink] PRIMARY KEY CLUSTERED 
(
	[BCSDocUpdateSupplementsSlinkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BCSDocUpdateSupplementsSlink] ADD  CONSTRAINT [DF_BCSDocUpdateSupplementsSlink_IsExported]  DEFAULT ((0)) FOR [IsExported]
GO

ALTER TABLE [dbo].[BCSDocUpdateSupplementsSlink] ADD  CONSTRAINT [DF_BCSDocUpdateSupplementsSlink_IsAPF]  DEFAULT ((0)) FOR [IsAPF]
GO

ALTER TABLE [dbo].[BCSDocUpdateSupplementsSlink] ADD  CONSTRAINT [DF_BCSDocUpdateSupplementsSlink_IsOPF]  DEFAULT ((0)) FOR [IsOPF]
GO


/*AR SAR Tables - Start */

CREATE TABLE [dbo].[BCSDocUpdateARSAR](
	[BCSDocUpdateARSARID] [int] IDENTITY(1,1) NOT NULL,
	[EdgarID] [int] NOT NULL,
	[Acc#] [nvarchar](250) NOT NULL,
	[PDFName] [nvarchar](55) NULL,
	[DocumentType] [nvarchar](4) NOT NULL,
	[FormType] [nvarchar](200) NOT NULL,
	[CUSIP] [nvarchar](10) NOT NULL,
	[TickerID] [int] NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[DocumentDate] [datetime] NOT NULL,
	[ProsID] [int] NOT NULL,
	[FundName] [nvarchar](255) NOT NULL,
	[ProsDocID] [int] NULL,
	[IsFiled] [bit] NOT NULL,
	[FiledDate] [datetime] NOT NULL,
	[FilingStatusAddedDate] [datetime] NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[ProcessedDate] [datetime] NULL,
	[PageCount] [int] NULL,
	[PageSizeHeight] [decimal](5, 2) NULL,
	[PageSizeWidth] [decimal](5, 2) NULL,
	[BCSDocUpdateARSARSlinkID] [int] NULL,
 CONSTRAINT [PK_BCSDocUpdateARSAR] PRIMARY KEY CLUSTERED 
(
	[BCSDocUpdateARSARID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BCSDocUpdateARSAR] ADD  CONSTRAINT [DF_BCSDocUpdateARSAR_IsFiled]  DEFAULT ((0)) FOR [IsFiled]
GO

ALTER TABLE [dbo].[BCSDocUpdateARSAR] ADD  CONSTRAINT [DF_BCSDocUpdateARSAR_IsProcessed]  DEFAULT ((0)) FOR [IsProcessed]
GO


CREATE TABLE [dbo].[BCSDocUpdateARSARSlink](
	[BCSDocUpdateARSARSlinkID] [int] IDENTITY(1,1) NOT NULL,
	[ZipFileName] [nvarchar](255) NOT NULL,
	[IsExported] [bit] NOT NULL,
	[ExportedDate] [datetime] NULL,
	[IsAPF] [bit] NOT NULL,
	[APFReceivedDate] [datetime] NULL,
	[IsOPF] [bit] NOT NULL,
	[OPFReceivedDate] [datetime] NULL,
	IsAPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateARSARSlink_IsAPC]  DEFAULT ((0)),
	APCReceivedDate DateTime NULL,
	IsOPC BIT NOT NULL CONSTRAINT [DF_BCSDocUpdateARSARSlink_IsOPC]  DEFAULT ((0)),
	OPCReceivedDate DateTime NULL,
 CONSTRAINT [PK_BCSDocUpdateARSARSlink] PRIMARY KEY CLUSTERED 
(
	[BCSDocUpdateARSARSlinkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BCSDocUpdateARSARSlink] ADD  CONSTRAINT [DF_BCSDocUpdateARSARSlink_IsExported]  DEFAULT ((0)) FOR [IsExported]
GO

ALTER TABLE [dbo].[BCSDocUpdateARSARSlink] ADD  CONSTRAINT [DF_BCSDocUpdateARSARSlink_IsAPF]  DEFAULT ((0)) FOR [IsAPF]
GO

ALTER TABLE [dbo].[BCSDocUpdateARSARSlink] ADD  CONSTRAINT [DF_BCSDocUpdateARSARSlink_IsOPF]  DEFAULT ((0)) FOR [IsOPF]
GO

/*AR SAR Tables - END */

CREATE TABLE [dbo].[BCSTransamericaFTP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateReceived] [datetime] NULL,
	[fileName] [nvarchar](max) NULL,
	[isProcessed] [int] NULL,
	[RTFileDateStamp] [datetime] NULL,
 CONSTRAINT [PK_BCSTransamericaFTP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[BCSTransamerica_FTP_RT1](
	[clmn1] [nvarchar](50) NULL,
	[clmn2] [nvarchar](50) NULL,
	[clmn3] [nvarchar](50) NULL,
	[clmn4] [nvarchar](50) NULL,
	[clmn5] [nchar](9) NULL,
	[clmn6] [nvarchar](max) NULL,
	[clmn7] [nvarchar](50) NULL,
	[clmn8] [nvarchar](50) NULL,
	[clmn9] [nvarchar](50) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[BCSAllianceBernsteinFTP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateReceived] [datetime] NULL,
	[fileName] [nvarchar](max) NULL,
	[isProcessed] [int] NULL,
	[RTFileDateStamp] [datetime] NULL,
 CONSTRAINT [PK_BCSAllianceBernsteinFTP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[BCSAllianceBernstein_FTP_RT1](
	[clmn1] [nvarchar](50) NULL,
	[clmn2] [nvarchar](50) NULL,
	[clmn3] [nvarchar](50) NULL,
	[clmn4] [nvarchar](50) NULL,
	[clmn5] [nchar](9) NULL,
	[clmn6] [nvarchar](max) NULL,
	[clmn7] [nvarchar](50) NULL,
	[clmn8] [nvarchar](50) NULL,
	[clmn9] [nvarchar](50) NULL
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[BCSAllianceBernsteinWatchListCUSIPs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSIP] [nvarchar](50) NULL,
	[FundName] [nvarchar](500) NULL,
	[CIK] [nvarchar](50) NULL,
	[SeriesID] [nvarchar](50) NULL,
	[ClassContractID] [nvarchar](50) NULL,
	[TickerSymbol] [nvarchar](10) NULL,
	[Class] [nvarchar](100) NULL,
 CONSTRAINT [PK_BCSAllianceBernsteinWatchListCUSIPs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSAllianceBernsteinWatchListCUSIPsHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NOT NULL,
	[CUSIP] [nvarchar](50) NULL,
	[FundName] [nvarchar](500) NULL,
	[CIK] [nvarchar](50) NULL,
	[SeriesID] [nvarchar](50) NULL,
	[ClassContractID] [nvarchar](50) NULL,
	[TickerSymbol] [nvarchar](10) NULL,
	[Class] [nvarchar](100) NULL,
	[InsertionDate] [datetime] NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_BCSAllianceBernsteinWatchListCUSIPsHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[BCSAllianceBernsteinWatchListCUSIPsForFLPurpose](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSIP] [nvarchar](50) NULL,
	[FundName] [nvarchar](500) NULL,
	[CIK] [nvarchar](50) NULL,
	[SeriesID] [nvarchar](50) NULL,
	[ClassContractID] [nvarchar](50) NULL,
	[TickerSymbol] [nvarchar](10) NULL,
 CONSTRAINT [PK_BCSAllianceBernsteinWatchListCUSIPsForFLPurpose] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


Create TRIGGER [dbo].[BCSAllianceBernsteinWatchListCUSIPsDeletion]
       ON [dbo].[BCSAllianceBernsteinWatchListCUSIPs]
       FOR DELETE
    AS
       
				insert into BCSAllianceBernsteinWatchListCUSIPsHistory (ID,  CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, DeletionDate)
					select  id, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class,  Getdate() 
				from 
					Deleted


				INSERT INTO BCSAllianceBernsteinWatchListCUSIPsForFLPurpose(CUSIP, CIK, SeriesID, ClassContractID, TickerSymbol)

					SELECT  Deleted.CUSIP, CIK, SeriesID, ClassContractID, TickerSymbol
					FROM Deleted
					INNER JOIN BCSDocUpdateSupplements ON BCSDocUpdateSupplements.CUSIP = Deleted.CUSIP
					WHERE BCSDocUpdateSupplements.IsProcessed = 0 AND BCSDocUpdateSupplements.IsRemoved = 0

					UNION

					SELECT  Deleted.CUSIP, CIK, SeriesID, ClassContractID, TickerSymbol
					FROM Deleted
					INNER JOIN BCSDocUpdateARSAR ON BCSDocUpdateARSAR.CUSIP = Deleted.CUSIP
					WHERE BCSDocUpdateARSAR.IsProcessed = 0
			  
				



GO



    Create TRIGGER [dbo].[BCSAllianceBernsteinWatchListCUSIPsInsert]
       ON [dbo].[BCSAllianceBernsteinWatchListCUSIPs]
       FOR INSERT
    AS
		
		insert into BCSAllianceBernsteinWatchListCUSIPsHistory   (ID, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, InsertionDate)
		SELECT ID, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, getdate() FROM INSERTED



GO



 Create TRIGGER [dbo].[BCSTransamericaWatchListCUSIPsDeletion]
       ON [dbo].[BCSTransamericaWatchListCUSIPs]
       FOR DELETE
    AS
       
				insert into BCSTransamericaWatchListCUSIPsHistory (ID,  CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, DeletionDate)
					select  id, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class,  Getdate() 
				from 
					Deleted
			  
          



GO



    Create TRIGGER [dbo].[BCSTransamericaWatchListCUSIPsInsert]
       ON [dbo].[BCSTransamericaWatchListCUSIPs]
       FOR INSERT
    AS
		
		insert into BCSTransamericaWatchListCUSIPsHistory   (ID, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, InsertionDate)
		SELECT ID, CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class, getdate() FROM INSERTED



GO


Create View BCSWatchListCUSIPView 
AS
SELECT CUSIP FROM BCSTransamericaWatchListCUSIPs 
UNION
SELECT CUSIP FROM BCSAllianceBernsteinWatchListCUSIPs
GO

Create View BCSFLWatchListCUSIPView 
AS
SELECT CUSIP FROM BCSAllianceBernsteinWatchListCUSIPsForFLPurpose
GO



CREATE TABLE [dbo].[BCSAllianceBernsteinDailyIPDetails](
	[BCSAllianceBernsteinDailyIPDetailsID] [int] IDENTITY(1,1) NOT NULL,	
	[IPDocUpdateFileName] [nvarchar](200) NULL,
	[HeaderDate] [nvarchar](200) NULL,
	[CUSIP] [nvarchar](20) NULL,
	[FundName] [nvarchar](500) NULL,
	[PDFName] [nvarchar](110) NULL,
	[DocumentType] [nvarchar](8) NULL,
	[EffectiveDate] [DateTime] NULL,
	[DocumentDate] [DateTime] NULL,
	[RRDInternalDocumentID] [INT] NULL,
	[SECAcc#] [nvarchar](500) NULL,
	[SECDateFiled] [datetime] NULL,
	[SECFormType] [nvarchar](200) NULL,
	[EdgarID] [INT] NULL,
	[PageCount] [INT] NULL,
	[PageSizeHeight] [decimal] NULL,
	[PageSizeWidth] [decimal] NULL,
	[RPProcessStep] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BCSAllianceBernsteinDailyIPDetails] PRIMARY KEY CLUSTERED 
(
	[BCSAllianceBernsteinDailyIPDetailsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSTransamericaDailyIPDetails](
	[BCSTransamericaDailyIPDetailsID] [int] IDENTITY(1,1) NOT NULL,	
	[IPDocUpdateFileName] [nvarchar](200) NULL,
	[HeaderDate] [nvarchar](200) NULL,
	[CUSIP] [nvarchar](20) NULL,
	[FundName] [nvarchar](500) NULL,
	[PDFName] [nvarchar](110) NULL,
	[DocumentType] [nvarchar](8) NULL,
	[EffectiveDate] [DateTime] NULL,
	[DocumentDate] [DateTime] NULL,
	[RRDInternalDocumentID] [INT] NULL,
	[SECAcc#] [nvarchar](500) NULL,
	[SECDateFiled] [datetime] NULL,
	[SECFormType] [nvarchar](200) NULL,
	[EdgarID] [INT] NULL,
	[PageCount] [INT] NULL,
	[PageSizeHeight] [decimal] NULL,
	[PageSizeWidth] [decimal] NULL,
	[RPProcessStep] [nvarchar](10) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BCSTransamericaDailyIPDetails] PRIMARY KEY CLUSTERED 
(
	[BCSTransamericaDailyIPDetailsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[BCSDocUpdateSupplements] ADD IsNotDuplicate bit NOT NULL CONSTRAINT [DF_BCSDocUpdateSupplements_IsNotDuplicate]  DEFAULT ((0))
GO


ALTER TABLE [dbo].[BCSDocUpdateARSAR] ADD IsNotDuplicate bit NOT NULL CONSTRAINT [DF_BCSDocUpdateARSAR_IsNotDuplicate]  DEFAULT ((0))
GO

ALTER TABLE [dbo].[BCSClientConfig] ADD ShowClientInDashboard bit NOT NULL CONSTRAINT [DF_BCSClientConfig_ShowClientInDashboard]  DEFAULT ((0))
GO

ALTER TABLE [dbo].[BCSDocUpdateARSAR] ADD IsRemoved bit NOT NULL CONSTRAINT [DF_BCSDocUpdateARSAR_IsRemoved]  DEFAULT ((0))
GO


CREATE TABLE [dbo].[BCSClientIPFileConfig](
	[BCSClientIPFileConfigID] [int] IDENTITY(1,1) NOT NULL,
	[ClientPrefix] [NVARCHAR](100) NOT NULL,	
	[SendTimeFrom] [NVARCHAR](500) NOT NULL,
	[SendTimeTo] [NVARCHAR](500) NOT NULL,	
	[LastFileSentDate] [DATETIME] NULL,
 CONSTRAINT [PK_BCSClientIPFileConfigID] PRIMARY KEY CLUSTERED 
(
	[BCSClientIPFileConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE BCSClientIPFileConfig 
	ADD NeedIPConfirmationFile BIT NOT NULL CONSTRAINT [DF_BCSClientIPFileConfig_NeedIPConfirmationFile]  DEFAULT ((0)),
		IPConfirmationFileDropFTPPath nvarchar(500) NULL,
		IPConfirmationFileDropFTPUserName nvarchar(50) NULL,
	    IPConfirmationFileDropFTPPassword nvarchar (50) NULL
