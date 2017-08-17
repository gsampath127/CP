CREATE TABLE [dbo].[ProsDocs] (
    [ProsDocId]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProsId]                  INT            NULL,
    [ProsDocTypeId]           VARCHAR (3)    NULL,
    [ProsDocOrder]            INT            NULL,
    [ProsDocURL]              VARCHAR (500)  NULL,
    [ProsDocAltURL]           VARCHAR (500)  NULL,
    [ProsDocUseAltURL]        BIT            NULL,
    [ProsDocLevel]            INT            NULL,
    [Removed]                 BIT            CONSTRAINT [DF_ProsDocs_Removed] DEFAULT ((0)) NOT NULL,
    [ProsDocPDFInitial]       INT            CONSTRAINT [DF_ProsDocs_ProsDocPDFInitial] DEFAULT ((1)) NULL,
    [ProsDocUsePDF]           BIT            CONSTRAINT [DF_ProsDocs_ProsDocUsePDF] DEFAULT ((0)) NULL,
    [ProsDocBackUpURL]        VARCHAR (500)  NULL,
    [ProsDocBackUpURLArchive] VARCHAR (500)  NULL,
    [isBackUpURLSynchronized] TINYINT        NULL,
    [ClientID]                INT            NULL,
    [CustomizedTypeID]        INT            NULL,
    [ProsDocFundSiteURL]      VARCHAR (500)  NULL,
    [PageCount]               INT            NULL,
    [PageSizeHeight]          DECIMAL (5, 2) NULL,
    [PageSizeWidth]           DECIMAL (5, 2) NULL,
    CONSTRAINT [PK_ProsDocs] PRIMARY KEY CLUSTERED ([ProsDocId] ASC)
);


GO
CREATE TRIGGER [dbo].[ProsDocsUpdate]
       ON [dbo].[ProsDocs]
       FOR UPDATE
    AS
	DECLARE @TDate DATETIME
	SET @TDate = GETDATE()
   	IF UPDATE(ProsDocBackUpURL) 
	BEGIN
		IF NOT EXISTS(SELECT ProsDocBackUpURL FROM Deleted WHERE ProsDocBackUpURL IS NULL)
		BEGIN
			DECLARE @TempProsDocBackUpURL varchar(500)
			DECLARE @TempProsDocBackUpURLDelet varchar(500)
			SELECT @TempProsDocBackUpURL = ProsDocBackUpURL FROM ProsDocs WHERE ProsDocID in (SELECT ProsDocID
			   FROM Deleted)
			SELECT @TempProsDocBackUpURLDelet = ProsDocBackUpURL
			   FROM Deleted
			IF(@TempProsDocBackUpURLDelet <> @TempProsDocBackUpURL)
			BEGIN
				 insert into ProsDocsHistory (ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocBackUpURL, ProsDocLevel, UpdateDate, UpdatedField)
						select ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocBackUpURLArchive, ProsDocLevel, @TDate , 'ProsDocBackUpURL' 
							from Deleted 
 			END
		END
		--RETURN

	END

	IF UPDATE(ProsDocURL) 
	BEGIN
		DECLARE @TempProsDocURL varchar(500)
		DECLARE @TempProsDocURLDelet varchar(500)
		SELECT @TempProsDocURL = ProsDocURL FROM ProsDocs WHERE ProsDocID in (SELECT ProsDocID
           FROM Deleted)
		SELECT @TempProsDocURLDelet = ProsDocURL
           FROM Deleted
		IF(@TempProsDocURLDelet <> @TempProsDocURL)
		BEGIN
		 insert into ProsDocsHistory (ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL,ProsDocBackUpURL, ProsDocLevel, UpdateDate, UpdatedField)
				select ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocBackUpURL, ProsDocLevel, @TDate , 'ProsDocURL' 
					from Deleted 

				update prosdocs set isBackUpURLSynchronized = 0, ProsDocBackUpURL = NULL, ProsDocBackUpURLArchive= NULL  where ProsDocId in (select ProsDocId from Deleted)
 		END
		--RETURN

	END



	IF UPDATE(prosdocUseAlturl) 
	BEGIN
		DECLARE @TempProsdocUseAlturl bit
		DECLARE @TempProsdocUseAlturlDelet bit
		SELECT @TempProsdocUseAlturl = prosdocUseAlturl FROM ProsDocs WHERE ProsDocID in (SELECT ProsDocID
           FROM Deleted)
		SELECT @TempProsdocUseAlturlDelet = prosdocUseAlturl
           FROM Deleted
		IF(@TempProsdocUseAlturlDelet <> @TempProsdocUseAlturl)
		BEGIN
				update prosdocs set isBackUpURLSynchronized = 0, ProsDocBackUpURL = NULL, ProsDocBackUpURLArchive= NULL  where ProsDocId in (select ProsDocId from Deleted)
 		END
	END




	IF UPDATE(ProsDocAltURL)
	BEGIN
		DECLARE @TempProsDocAltURL varchar(500)
		DECLARE @TempProsDocAltURLDelet varchar(500)
		SELECT @TempProsDocAltURL = ProsDocAltURL FROM ProsDocs WHERE ProsDocID in (SELECT ProsDocID
           FROM Deleted)
		SELECT @TempProsDocAltURLDelet = ProsDocAltURL
           FROM Deleted
		IF(@TempProsDocAltURLDelet <> @TempProsDocAltURL)
		BEGIN
 			 insert into ProsDocsHistory (ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocBackUpURL, ProsDocLevel, UpdateDate, UpdatedField)
					select ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL,ProsDocBackUpURL, ProsDocLevel, @TDate , 'ProsDocAltURL' 
						from Deleted 
		END
		--RETURN

	END

	IF UPDATE(ProsDocLevel)
	BEGIN
		DECLARE @TempProsDocLevel int
		DECLARE @TempProsDocLevelDelet int
		SELECT @TempProsDocLevel = ProsDocLevel FROM ProsDocs WHERE ProsDocID in (SELECT ProsDocID
           FROM Deleted)
		SELECT @TempProsDocLevelDelet = ProsDocLevel
           FROM Deleted
		IF(@TempProsDocLevelDelet <> @TempProsDocLevel)
		BEGIN
 			 insert into ProsDocsHistory (ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocBackUpURL, ProsDocLevel, UpdateDate, UpdatedField)
					select ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL,ProsDocBackUpURL, ProsDocLevel, @TDate , 'ProsDocLevel' 
						from Deleted 
		END
		--RETURN

	END
GO
  CREATE TRIGGER [dbo].[ProsDocsDeletion]
       ON [dbo].[ProsDocs]
       FOR DELETE
    AS
       IF NOT EXISTS
          (
           SELECT ProsDocId FROM ProsDocsHistory WHERE ProsDocId in (SELECT ProsDocId
           FROM Deleted) and DeletionDate is not null
          )
          BEGIN
			   insert into ProsDocsHistory (ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocLevel, DeletionDate)
				select ProsDocId, ProsId, ProsDocTypeId, ProsDocOrder, ProsDocURL, ProsDocAltURL, ProsDocUseAltURL, ProsDocLevel, Getdate() 
					from Deleted 
          END