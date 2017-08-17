Print N'Dropping table [EdgarOnlineFeed]...'
Go

DROP TABLE [dbo].[EdgarOnlineFeed]
Go

Print N'Creating table [EdgarOnlineFeed]...'
Go

CREATE TABLE [dbo].[EdgarOnlineFeed](
    [EdgarOnlineFeedId] [int] IDENTITY(1,1) NOT NULL,
	[ECUSIP] [nvarchar](10) NULL,
	[EFundName] [nvarchar](500) NULL,
	[Eticker] [nvarchar](50) NULL,
	[ECompanyName] [nvarchar](500) NULL,
	[ECIK] [nvarchar](50) NULL,
	[ESeriesID] [nvarchar](50) NULL,
	[EClassContractID] [nvarchar](50) NULL,
	[Euniverseabbrev] [nvarchar](50) NULL,
	CONSTRAINT [PK_EdgarOnlineFeed] PRIMARY KEY CLUSTERED 
	(
		[EdgarOnlineFeedId] ASC
	)
) ON [PRIMARY]

GO

Print N'Creating table EdgarOnlineFeedHistory...'
Go


CREATE TABLE [dbo].[EdgarOnlineFeedHistory](
    [EdgarOnlineFeedHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[EdgarOnlineFeedId] [int] NOT NULL,
	[ECUSIP] [nvarchar](10) NULL,
	[EFundName] [nvarchar](500) NULL,
	[Eticker] [nvarchar](50) NULL,
	[ECompanyName] [nvarchar](500) NULL,
	[ECIK] [nvarchar](50) NULL,
	[ESeriesID] [nvarchar](50) NULL,
	[EClassContractID] [nvarchar](50) NULL,
	[Euniverseabbrev] [nvarchar](50) NULL,
	InsertionDate DateTime NULL,
	DeletionDate DateTime NULL,
	UpdateDate DateTime NULL,
    CONSTRAINT [PK_EdgarOnlineFeedHistory] PRIMARY KEY CLUSTERED 
	(
		[EdgarOnlineFeedHistoryId] ASC
	)
) ON [PRIMARY]

GO

Print N'Creating table EdgarOnlineFeed_FTP...'
Go

CREATE TABLE [dbo].[EdgarOnlineFeed_FTP](
	[ECUSIP] [nvarchar](10) NULL,
	[EFundName] [nvarchar](500) NULL,
	[Eticker] [nvarchar](50) NULL,
	[ECompanyName] [nvarchar](500) NULL,
	[ECIK] [nvarchar](50) NULL,
	[ESeriesID] [nvarchar](50) NULL,
	[EClassContractID] [nvarchar](50) NULL,
	[Euniverseabbrev] [nvarchar](50) NULL
) ON [PRIMARY]

GO
Print N'Creating Trigger EdgarOnlineFeedTrigger...'
Go

CREATE TRIGGER [dbo].[EdgarOnlineFeedTrigger]
   ON  [dbo].[EdgarOnlineFeed]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN

	DECLARE @InsertionDate DateTime = NULL, @DeletionDate DateTime = NULL, @UpdateDate DateTime = NULL
	DECLARE @action as char(1);

	SET @action = 'I'; -- Set Action to Insert by default.

	IF EXISTS(SELECT * FROM DELETED)
    BEGIN
        SET @action = 
            CASE
                WHEN EXISTS(SELECT * FROM INSERTED) THEN 'U' -- Set Action to Updated.
                ELSE 'D' -- Set Action to Deleted.       
            END
    END

	IF @action = 'I'
	BEGIN
		SET @InsertionDate = Getdate()
	END
	ELSE IF @action = 'U'
	BEGIN
		SET @DeletionDate = Getdate()
	END IF @action = 'D'
	BEGIN
		SET @UpdateDate = Getdate()
	END


	IF @action = 'I'
	BEGIN
		INSERT INTO [dbo].[EdgarOnlineFeedHistory]
           ([EdgarOnlineFeedId]
           ,[ECUSIP]
           ,[EFundName]
           ,[Eticker]
           ,[ECompanyName]
           ,[ECIK]
           ,[ESeriesID]
           ,[EClassContractID]
           ,[Euniverseabbrev]
           ,[InsertionDate]
           ,[DeletionDate]
           ,[UpdateDate])     
		SELECT[EdgarOnlineFeedId]
           ,[ECUSIP]
           ,[EFundName]
           ,[Eticker]
           ,[ECompanyName]
           ,[ECIK]
           ,[ESeriesID]
           ,[EClassContractID]
           ,[Euniverseabbrev]
           ,@InsertionDate
           ,@DeletionDate
           ,@UpdateDate
		FROM inserted
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[EdgarOnlineFeedHistory]
           ([EdgarOnlineFeedId]
           ,[ECUSIP]
           ,[EFundName]
           ,[Eticker]
           ,[ECompanyName]
           ,[ECIK]
           ,[ESeriesID]
           ,[EClassContractID]
           ,[Euniverseabbrev]
           ,[InsertionDate]
           ,[DeletionDate]
           ,[UpdateDate])     
		SELECT[EdgarOnlineFeedId]
           ,[ECUSIP]
           ,[EFundName]
           ,[Eticker]
           ,[ECompanyName]
           ,[ECIK]
           ,[ESeriesID]
           ,[EClassContractID]
           ,[Euniverseabbrev]
           ,@InsertionDate
           ,@DeletionDate
           ,@UpdateDate
		FROM deleted
	END
END
GO

Print N'Creating table EdgarOnlineFeedFileHistory...'
Go
CREATE TABLE [dbo].[EdgarOnlineFeedFileHistory](
	[EdgarOnlineFeedFileHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[DateReceived] [datetime] NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[IsProcessed] [bit] NOT NULL CONSTRAINT [DF_EdgarOnlineFeedFileHistory_IsProcessed]  DEFAULT ((0)),
	CONSTRAINT [PK_EdgarOnlineFeedFileHistory] PRIMARY KEY CLUSTERED 
	(
		[EdgarOnlineFeedFileHistoryId] ASC
	)
) ON [PRIMARY]
GO

Print N'Altering Trigger ProsTickerUpdate...'
Go

ALTER TRIGGER [dbo].[ProsTickerUpdate]
       ON [dbo].[ProsTicker]
       FOR UPDATE
    AS
	DECLARE @TDate DATETIME
	SET @TDate = GETDATE()

   	IF UPDATE(TickerSymbol) 
	BEGIN
		DECLARE @TempTickerSymbol NVARCHAR(10)
		DECLARE @TempTickerSymbolDelet NVARCHAR(10)
		SELECT @TempTickerSymbol = TickerSymbol FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempTickerSymbolDelet = TickerSymbol
           FROM Deleted
		IF(@TempTickerSymbolDelet <> @TempTickerSymbol)
		BEGIN
			insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)
						select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'TickerSymbol' 
					from 
						Deleted
		END
	END
	IF UPDATE(Class) 
	BEGIN
		DECLARE @TempClass varchar(100)
		DECLARE @TempClassDelet varchar(100)
		SELECT @TempClass = Class FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempClassDelet = Class
           FROM Deleted
		IF(@TempClassDelet <> @TempClass)
		BEGIN
			insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)
						select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'Class' 
					from 
						Deleted
		END
	END
	IF  UPDATE(CUSIP)
	BEGIN
		DECLARE @TempCUSIP varchar(10)
		DECLARE @TempCUSIPDelet varchar(10)
		SELECT @TempCUSIP = CUSIP FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempCUSIPDelet = CUSIP
           FROM Deleted
		IF(@TempCUSIPDelet <> @TempCUSIP)
		BEGIN
			insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)
						select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'CUSIP' 
					from 
						Deleted		
			IF (@TempCUSIPDelet<>'' and @TempCUSIP<>'')
			BEGIN
				update troweprice set cusip_d = @TempCUSIP, cusip = @TempCUSIP where cusip_d = 	@TempCUSIPDelet
				update axavit set cusip = @TempCUSIP where cusip = 	@TempCUSIPDelet
				update securitybenefits set cusip = @TempCUSIP where cusip = @TempCUSIPDelet
			END


		END
	END
	IF UPDATE(LIPPER)
	BEGIN
		DECLARE @TempLIPPER varchar(50)
		DECLARE @TempLIPPERDelet varchar(50)
		SELECT @TempLIPPER = LIPPER FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempLIPPERDelet = LIPPER
           FROM Deleted
		IF(RTrim(Ltrim(@TempLIPPERDelet)) <> RTrim(Ltrim(@TempLIPPER)))
		BEGIN
			insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)
						select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'LIPPER' 
					from 
						Deleted
		END
	END
	IF UPDATE(SecurityTypeID)
	BEGIN
		DECLARE @TempSecurityTypeID int
		DECLARE @TempSecurityTypeIDDelet int
		SELECT @TempSecurityTypeID = SecurityTypeID FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempSecurityTypeIDDelet = SecurityTypeID
           FROM Deleted
		IF(ISNULL(@TempSecurityTypeIDDelet, 0) <> @TempSecurityTypeID)
		BEGIN
			insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, SecurityTypeID, UpdateDate, UpdatedField)
						select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, SecurityTypeID, @TDate, 'SecurityTypeID' 
					from 
						Deleted
		END
	END
	IF UPDATE(CIK) OR UPDATE(ClassContractID)
	BEGIN
		DECLARE @TempCIK varchar(50)
		DECLARE @TempCIKDelet varchar(50)
		DECLARE @TempClassContractID varchar(50)
		DECLARE @TempClassContractIDDelet varchar(50)
		DECLARE @TempSeriesID varchar(50)
		DECLARE @TempSeriesIDDelet varchar(50)
		DECLARE @CUSIP varchar(10)
		
		SELECT @CUSIP = CUSIP FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempCIK = CIK FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempCIKDelet = CIK
           FROM Deleted
		SELECT @TempClassContractID = ClassContractID FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempClassContractIDDelet = ClassContractID
           FROM Deleted
		SELECT @TempSeriesID = SeriesID FROM ProsTicker WHERE TickerID in (SELECT TickerID
           FROM Deleted)
		SELECT @TempSeriesIDDelet = SeriesID
           FROM Deleted

		IF NOT EXISTS(SELECT CIK FROM Deleted WHERE CIK IS NULL) AND NOT EXISTS(SELECT CIK FROM Deleted WHERE CIK='')
		BEGIN
			IF EXISTS(SELECT CUSIP_D FROM TROWEPRICE WHERE CUSIP_D IN(SELECT CUSIP FROM PROSTICKER WHERE CIK=(SELECT CIK FROM DELETED)))
			BEGIN
				update troweprice set CIK=(select top 1 cik from prosticker where cusip=troweprice.cusip_d),
				ClassContractID=(select top 1 ClassContractID from prosticker where cusip=troweprice.cusip_d)
			END

			IF EXISTS(SELECT CUSIP FROM axavit WHERE CUSIP IN(SELECT CUSIP FROM PROSTICKER WHERE CIK=(SELECT CIK FROM DELETED)))
			BEGIN
				update axavit set CIK=(select top 1 cik from prosticker where cusip=axavit.cusip),
				SeriesID=(select top 1 SeriesID from prosticker where cusip=axavit.cusip),
				ClassContractID=(select top 1 ClassContractID from prosticker where cusip=axavit.cusip)
				where (axavit.CUSIP IN ('46600H604', '46600H505', '46600H604', '46600H703', '46600H786', '46600H794', '46600H851', '46600H885'))
			END
			

		update securitybenefits set CIK = @TempCIK, ClassContractID=@TempClassContractID, SeriesID =@TempSeriesID where cusip = @CUSIP 
		END
		
	END
	Go





Print N'Creating Table HolidayType...'
Go
CREATE TABLE [dbo].[HolidayType](
	[HolidayTypeID] [int] IDENTITY(1,1) NOT NULL,
	[HolidayTypeName] [nvarchar] (100) NOT NULL,
	[HolidayTypeDescription] [nvarchar] (200) NULL,
 CONSTRAINT [PK_HolidayType] PRIMARY KEY CLUSTERED 
(
	[HolidayTypeID] ASC
)
) ON [PRIMARY]

GO


INSERT INTO HolidayType(HolidayTypeName, HolidayTypeDescription)
	values('Stock', 'Stock Holidays')
Go

Print N'Creating Table [Holidays]...'
Go
CREATE TABLE [dbo].[Holidays](
	[HolidayID] [int] IDENTITY(1,1) NOT NULL,
	[HolidayTypeId] [int] NOT NULL,
	[Date] [DateTime] NOT NULL,
	CONSTRAINT FK_Holidays_HolidayTypeId FOREIGN KEY (HolidayTypeId) REFERENCES HolidayType(HolidayTypeId),
 CONSTRAINT [PK_Holidays] PRIMARY KEY CLUSTERED 
(
	[HolidayID] ASC
)
) ON [PRIMARY]

GO


INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2016-11-24')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2016-12-26')
Go


INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-01-02')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-01-16')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-02-20')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-04-14')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-05-29')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-07-04')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-09-04')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-11-23')
Go
INSERT INTO Holidays(HolidayTypeId, Date) values(1, '2017-12-25')
Go



ALTER table BCSClientConfig ADD HolidayTypeId INT NULL 
	Constraint fk_BCSClientConfig_HolidayType Foreign Key (HolidayTypeId) References HolidayType(HolidayTypeId)

GO



update BCSClientConfig 
set HolidayTypeId = 1
WHERE ClientName = 'AllianceBernstein'
GO

Print N'Update Completed...'
Go