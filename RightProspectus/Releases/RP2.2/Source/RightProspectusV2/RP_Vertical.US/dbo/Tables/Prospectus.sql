CREATE TABLE [dbo].[Prospectus] (
    [ProsID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProsName]        NVARCHAR (255) NULL,
    [LevelID]         INT            NULL,
    [Company]         NVARCHAR (255) NULL,
    [URL]             NVARCHAR (255) NULL,
    [ProsDate]        DATETIME       NULL,
    [RevisedProsDate] DATETIME       NULL,
    [SAISURL]         NVARCHAR (255) NULL,
    [SuppURL]         NVARCHAR (255) NULL,
    [SAISURL2]        NVARCHAR (255) NULL,
    [SuppURL2]        NVARCHAR (255) NULL,
    [SAISURL3]        NVARCHAR (255) NULL,
    [SuppURL3]        NVARCHAR (255) NULL,
    [SAISURL4]        NVARCHAR (255) NULL,
    [SuppURL4]        NVARCHAR (255) NULL,
    [SAISURL5]        NVARCHAR (255) NULL,
    [SuppURL5]        NVARCHAR (255) NULL,
    [AltURL]          NVARCHAR (255) NULL,
    [UseAltURL]       CHAR (2)       NULL,
    [CompanyID]       INT            NULL,
    [Online]          CHAR (1)       NULL,
    [ARDate]          DATETIME       NULL,
    [RevisedARDate]   DATETIME       NULL,
    [SARDate]         DATETIME       NULL,
    [RevisedSARDate]  DATETIME       NULL,
    [FEY]             VARCHAR (5)    NULL,
    [PHDate]          DATETIME       NULL,
    [RevisedPHDate]   DATETIME       NULL,
    [PVRDate]         DATETIME       NULL,
    [RevisedPVRDate]  DATETIME       NULL,
    [SDate]           DATETIME       NULL,
    [RevisedSDate]    DATETIME       NULL,
    [SSDate]          DATETIME       NULL,
    [SPDate]          DATETIME       NULL,
    [RevisedSPDate]   DATETIME       NULL,
    [SPSDate]         DATETIME       NULL,
    [PSDate]          DATETIME       NULL,
    [RevisedPSDate]   DATETIME       NULL,
    [FSDate]          DATETIME       NULL,
    [RevisedFSDate]   DATETIME       NULL,
    [COMDate]         DATETIME       NULL,
    [RevisedCOMDate]  DATETIME       NULL,
    [GWFDate]         DATETIME       NULL,
    [RevisedGWFDate]  DATETIME       NULL,
    CONSTRAINT [PK_Prospectus] PRIMARY KEY CLUSTERED ([ProsID] ASC)
);


GO
CREATE TRIGGER ProspectusUpdate
       ON dbo.Prospectus
       FOR UPDATE
    AS
	DECLARE @TDate DATETIME
	SET @TDate = GETDATE()
	IF UPDATE(CompanyID)
	BEGIN
			DECLARE @TempCompanyID int
			DECLARE @TempCompanyIDDelet nvarchar(255)
			SELECT @TempCompanyID = CompanyID FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempCompanyIDDelet = CompanyID
			   FROM Deleted
			IF(@TempCompanyIDDelet <> @TempCompanyID)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'CompanyID' 
					from Deleted
			END
	END
   	IF UPDATE(ProsName)
	BEGIN
			DECLARE @TempProsName nvarchar(255)
			DECLARE @TempProsNameDelet nvarchar(255)
			SELECT @TempProsName = ProsName FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempProsNameDelet = ProsName
			   FROM Deleted
			IF(@TempProsNameDelet <> @TempProsName)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'ProsName' 
					from Deleted
			END
	END
	IF UPDATE(ProsDate)
	BEGIN
			DECLARE @TempProsDate datetime
			DECLARE @TempProsDateDelet datetime
			SELECT @TempProsDate = ProsDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempProsDateDelet = ProsDate
			   FROM Deleted
			IF(@TempProsDateDelet <> @TempProsDate)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'ProsDate' 
					from Deleted
			END

	END
	IF UPDATE(ARDate)
	BEGIN
			DECLARE @TempARDate datetime
			DECLARE @TempARDateDelet datetime
			SELECT @TempARDate = ARDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempARDateDelet = ARDate
			   FROM Deleted
			IF(@TempARDateDelet <> @TempARDate)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'ARDate' 
					from Deleted
			END

	END
    IF UPDATE(FSDate)
	BEGIN
			DECLARE @TempFSDate datetime
			DECLARE @TempFSDateDelet datetime
			SELECT @TempFSDate = FSDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempFSDateDelet = FSDate
			   FROM Deleted
			IF(@TempFSDateDelet <> isnull(@TempFSDate,getdate() ))
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FSDate, RevisedFSDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FSDate, RevisedFSDate ,FEY, PHDate, RevisedPHDate, @TDate , 'FSDate' 
					from Deleted
			END

	END
    IF UPDATE(RevisedFSDate)
	BEGIN
			DECLARE @TempRevisedFSDate datetime
			DECLARE @TempRevisedFSDateDelet datetime
			SELECT @TempRevisedFSDate = RevisedFSDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempRevisedFSDateDelet = RevisedFSDate
			   FROM Deleted
			IF(@TempRevisedFSDateDelet <> isnull(@TempRevisedFSDate,getdate() ))
			BEGIN
					insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FSDate, RevisedFSDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FSDate, RevisedFSDate ,FEY, PHDate, RevisedPHDate, @TDate , 'RevisedFSDate' 
					from Deleted
			END
	END
	IF UPDATE(SARDate)
	BEGIN
			DECLARE @TempSARDate datetime
			DECLARE @TempSARDateDelet datetime
			SELECT @TempSARDate = SARDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempSARDateDelet = SARDate
			   FROM Deleted
			IF(@TempSARDateDelet <> @TempSARDate)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'SARDate' 
					from Deleted
			END
	END
	IF UPDATE(FEY)
	BEGIN
			DECLARE @TempFEY varchar(5)
			DECLARE @TempFEYDelet varchar(5)
			SELECT @TempFEY = FEY FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempFEYDelet = FEY
			   FROM Deleted
			IF(@TempFEYDelet <> @TempFEY)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'FEY' 
					from Deleted
			END
	END
	IF UPDATE(PHDate)
	BEGIN
		DECLARE @TempPHDate datetime
			DECLARE @TempPHDateDelet datetime
			SELECT @TempSARDate = PHDate FROM Prospectus WHERE ProsID in (SELECT ProsID
			   FROM Deleted)
			SELECT @TempPHDateDelet = PHDate
			   FROM Deleted
			IF(@TempPHDateDelet <> @TempPHDate)
			BEGIN
				insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, UpdateDate, UpdatedField)
					select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, @TDate , 'PHDate' 
					from Deleted
			END
	END
GO
CREATE TRIGGER ProspectusDeletion
       ON dbo.Prospectus
       FOR DELETE
    AS
       IF NOT EXISTS
          (
           SELECT ProsID FROM ProspectusHistory WHERE ProsID in (SELECT ProsID
           FROM Deleted)
          )
          BEGIN

		insert into ProspectusHistory(ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3, SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate, FSDate, RevisedFSDate, DeletionDate) 
			select ProsID, ProsName, LevelID, Company, URL, ProsDate, RevisedProsDate, SAISURL, SuppURL, SAISURL2, SuppURL2, SAISURL3, SuppURL3,SAISURL4, SuppURL4, SAISURL5, SuppURL5, AltURL, UseAltURL, CompanyID, Online, ARDate, RevisedARDate, SARDate, RevisedSARDate, FEY, PHDate, RevisedPHDate,  FSDate, RevisedFSDate, Getdate() 
			from Deleted 
			  
          END