USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_TransamericaGetIPDocUpdateDetails]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_TransamericaGetIPDocUpdateDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_TransamericaGetIPDocUpdateDetails]
GO  
  
CREATE Procedure [dbo].[BCS_TransamericaGetIPDocUpdateDetails]
@CurrentDate DateTime,
@YesterdaysDate DateTime,
@IPDocUpdateFileName nvarchar(200),
@HeaderDate nvarchar(200)
as  
Begin

	DECLARE @IncludeRemovedWatchListCUSIPInIPDocUpdate bit;
	SELECT @IncludeRemovedWatchListCUSIPInIPDocUpdate = IncludeRemovedWatchListCUSIPInIPDocUpdate
	FROM BCSClientConfig 
	WHERE ClientPrefix = 'AEG'

	DECLARE @TEMPCUSIPToBeIncluded Table(CUSIP nvarchar(20), DeletionDate DateTime NULL, WatchListCUSIP bit NOT NULL)
	DECLARE @TEMPStateChangeCUSIPNotInWatchList Table(CUSIP nvarchar(20))

	--1) Take All CUSIP FROM WatchList
	INSERT INTO @TEMPCUSIPToBeIncluded(CUSIP, DeletionDate, WatchListCUSIP)
	SELECT CUSIP, NULL, 1 FROM BCSTransamericaWatchListCUSIPs

	IF @IncludeRemovedWatchListCUSIPInIPDocUpdate = 1
	BEGIN
		--2) Take State Changed CUSIp that are not in WatchList
		INSERT INTO @TEMPStateChangeCUSIPNotInWatchList

		
			SELECT BCSDocUpdateSupplements.CUSIP FROM BCSDocUpdateSupplements
			LEFT JOIN BCSTransamericaWatchListCUSIPs ON BCSTransamericaWatchListCUSIPs.CUSIP = BCSDocUpdateSupplements.CUSIP
			LEFT OUTER JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID
			WHERE BCSTransamericaWatchListCUSIPs.CUSIP IS NULL AND BCSDocUpdateSupplements.IsProcessed = 1 AND BCSDocUpdateSupplements.IsRemoved = 0 AND
			((ISNULL(BCSDocUpdateSupplementsSlink.IsAPC,0) != 0 AND BCSDocUpdateSupplementsSlink.APCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateSupplementsSlink.IsOPC,0) != 0 AND BCSDocUpdateSupplementsSlink.OPCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateSupplementsSlink.IsAPF,0) != 0 AND BCSDocUpdateSupplementsSlink.APFReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateSupplementsSlink.IsOPF,0) != 0 AND BCSDocUpdateSupplementsSlink.OPFReceivedDate Between @YesterdaysDate AND @CurrentDate)	OR
			 (ISNULL(BCSDocUpdateSupplementsSlink.IsExported,0) != 0 AND BCSDocUpdateSupplementsSlink.ExportedDate Between @YesterdaysDate AND @CurrentDate) 
			)

			UNION

			SELECT BCSDocUpdateARSAR.CUSIP FROM BCSDocUpdateARSAR
			LEFT JOIN BCSTransamericaWatchListCUSIPs ON BCSTransamericaWatchListCUSIPs.CUSIP = BCSDocUpdateARSAR.CUSIP
			LEFT OUTER JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
			WHERE BCSTransamericaWatchListCUSIPs.CUSIP IS NULL AND BCSDocUpdateARSAR.IsProcessed = 1 AND 
			(
			 (ISNULL(BCSDocUpdateARSARSlink.IsAPC,0) != 0 AND BCSDocUpdateARSARSlink.APCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateARSARSlink.IsOPC,0) != 0 AND BCSDocUpdateARSARSlink.OPCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateARSARSlink.IsAPF,0) != 0 AND BCSDocUpdateARSARSlink.APFReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
			 (ISNULL(BCSDocUpdateARSARSlink.IsOPF,0) != 0 AND BCSDocUpdateARSARSlink.OPFReceivedDate Between @YesterdaysDate AND @CurrentDate)	OR
			 (ISNULL(BCSDocUpdateARSARSlink.IsExported,0) != 0 AND BCSDocUpdateARSARSlink.ExportedDate Between @YesterdaysDate AND @CurrentDate)
			)


		--3) Consider deleted CUSIP from watchlist for which state is changed today
		INSERT INTO @TEMPCUSIPToBeIncluded(CUSIP, DeletionDate, WatchListCUSIP)
		SELECT TEMPStateChangeCUSIPNotInWatchList.CUSIP, DeletedCUSIPs.DeletionDate, 0 
		FROM @TEMPStateChangeCUSIPNotInWatchList TEMPStateChangeCUSIPNotInWatchList
		INNER JOIN 
		(
			SELECT * FROM (
			SELECT CUSIP, DeletionDate, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
			FROM BCSTransamericaWatchListCUSIPsHistory) t

			WHERE rownum = 1 and DeletionDate IS NOT NULL
		)DeletedCUSIPs On DeletedCUSIPs.CUSIP = TEMPStateChangeCUSIPNotInWatchList.CUSIP

	END

	--3) Consider @TEMPCUSIPToBeIncluded table ,  take all records if WatchListCUSIP = 1 
	--   else check BCSDocUpdate.IsProcessed =1 AND BCSDocUpdate.DocumentDate <= TEMPCUSIPToBeIncluded.DeletionDate 
	

	DECLARE @IPDetailstable Table(CUSIP nvarchar(20) NULL,
								FundName nvarchar(500) NULL,
								PDFName nvarchar(110) NULL,
								DocumentType nvarchar(8) NULL,
								EffectiveDate DateTime NULL,
								DocumentDate DateTime NULL,
								RRDInternalDocumentID INT NULL,
								SECAcc# nvarchar(500) NULL,
								SECDateFiled datetime NULL,
								SECFormType nvarchar(200) NULL,
								EdgarID INT NULL,
								PageCount INT NULL,
								PageSizeHeight decimal NULL,
								PageSizeWidth decimal NULL,
								RPProcessStep nvarchar(10) NULL)

    -- Insert details into table vaiable so that it can be used for storing details for report purpose
	INSERT INTO @IPDetailstable(CUSIP, FundName, PDFName, DocumentType,	EffectiveDate, DocumentDate, RRDInternalDocumentID,
								 SECAcc#, SECDateFiled, SECFormType, EdgarID, PageCount, PageSizeHeight, PageSizeWidth, RPProcessStep)

	SELECT distinct BCSDocUpdateSupplements.CUSIP, BCSDocUpdateSupplements.FundName, PDFName, BCSDocUpdateSupplements.DocumentType,  
			BCSDocUpdateSupplements.EffectiveDate, BCSDocUpdateSupplements.DocumentDate, ProsDocID as RRDInternalDocumentID,			
			BCSDocUpdateSupplements.Acc# as SECAcc#, BCSDocUpdateSupplements.FiledDate as SECDateFiled,			
			BCSDocUpdateSupplements.FormType as SECFormType, BCSDocUpdateSupplements.EdgarID,			
			BCSDocUpdateSupplements.[PageCount], BCSDocUpdateSupplements.PageSizeHeight, BCSDocUpdateSupplements.PageSizeWidth,
			CASE WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
				 WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
				 WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN 'AP'
				 WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
				 WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
				 WHEN BCSDocUpdateSupplements.IsProcessed = 0 THEN 'FL'
			END AS 'RPProcessStep' 
	FROM BCSDocUpdateSupplements  
	INNER JOIN @TEMPCUSIPToBeIncluded TEMPCUSIPToBeIncluded ON 
					BCSDocUpdateSupplements.CUSIP = TEMPCUSIPToBeIncluded.CUSIP 
					AND (TEMPCUSIPToBeIncluded.WatchListCUSIP = 1 OR BCSDocUpdateSupplements.DocumentDate <= TEMPCUSIPToBeIncluded.DeletionDate)
	INNER JOIN ProsTicker on Prosticker.CUSIP = BCSDocUpdateSupplements.CUSIP  	
	INNER JOIN Prospectus on BCSDocUpdateSupplements.ProsID = Prospectus.ProsID	
	LEFT OUTER JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID
	WHERE BCSDocUpdateSupplements.IsRemoved = 0 AND
	((ISNULL(BCSDocUpdateSupplementsSlink.IsAPC,0) != 0 AND BCSDocUpdateSupplementsSlink.APCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateSupplementsSlink.IsOPC,0) != 0 AND BCSDocUpdateSupplementsSlink.OPCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateSupplementsSlink.IsAPF,0) != 0 AND BCSDocUpdateSupplementsSlink.APFReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateSupplementsSlink.IsOPF,0) != 0 AND BCSDocUpdateSupplementsSlink.OPFReceivedDate Between @YesterdaysDate AND @CurrentDate)	OR
	 (ISNULL(BCSDocUpdateSupplementsSlink.IsExported,0) != 0 AND BCSDocUpdateSupplementsSlink.ExportedDate Between @YesterdaysDate AND @CurrentDate) OR	 
	 (ISNULL(BCSDocUpdateSupplements.IsFiled,0) != 0 AND BCSDocUpdateSupplements.FilingStatusAddedDate Between @YesterdaysDate AND @CurrentDate)	 	 
	)
	
	UNION	
	
	SELECT distinct BCSDocUpdateARSAR.CUSIP, BCSDocUpdateARSAR.FundName, PDFName, BCSDocUpdateARSAR.DocumentType,  
			BCSDocUpdateARSAR.EffectiveDate, BCSDocUpdateARSAR.DocumentDate, ProsDocID as RRDInternalDocumentID,			
			BCSDocUpdateARSAR.Acc# as SECAcc#, BCSDocUpdateARSAR.FiledDate as SECDateFiled,			
			BCSDocUpdateARSAR.FormType as SECFormType, BCSDocUpdateARSAR.EdgarID,			
			BCSDocUpdateARSAR.[PageCount], BCSDocUpdateARSAR.PageSizeHeight, BCSDocUpdateARSAR.PageSizeWidth,
			CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
				 WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
				 WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
				 WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
				 WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
				 WHEN BCSDocUpdateARSAR.IsProcessed = 0 THEN 'FL'
			END AS 'RPProcessStep'
	FROM BCSDocUpdateARSAR
	INNER JOIN @TEMPCUSIPToBeIncluded TEMPCUSIPToBeIncluded ON 
					BCSDocUpdateARSAR.CUSIP = TEMPCUSIPToBeIncluded.CUSIP 
					AND (TEMPCUSIPToBeIncluded.WatchListCUSIP = 1 OR BCSDocUpdateARSAR.DocumentDate <= TEMPCUSIPToBeIncluded.DeletionDate)
	INNER JOIN ProsTicker on Prosticker.CUSIP = BCSDocUpdateARSAR.CUSIP  	
	INNER JOIN Prospectus on BCSDocUpdateARSAR.ProsID = Prospectus.ProsID	
	LEFT OUTER JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
	WHERE 
	((ISNULL(BCSDocUpdateARSARSlink.IsAPC,0) != 0 AND BCSDocUpdateARSARSlink.APCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateARSARSlink.IsOPC,0) != 0 AND BCSDocUpdateARSARSlink.OPCReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateARSARSlink.IsAPF,0) != 0 AND BCSDocUpdateARSARSlink.APFReceivedDate Between @YesterdaysDate AND @CurrentDate) OR
	 (ISNULL(BCSDocUpdateARSARSlink.IsOPF,0) != 0 AND BCSDocUpdateARSARSlink.OPFReceivedDate Between @YesterdaysDate AND @CurrentDate)	OR
	 (ISNULL(BCSDocUpdateARSARSlink.IsExported,0) != 0 AND BCSDocUpdateARSARSlink.ExportedDate Between @YesterdaysDate AND @CurrentDate) OR	 
	 (ISNULL(BCSDocUpdateARSAR.IsFiled,0) != 0 AND BCSDocUpdateARSAR.FilingStatusAddedDate Between @YesterdaysDate AND @CurrentDate)	 
	)
	
	ORDER BY CUSIP


	-- Insert details for IP report
	INSERT INTO BCSTransamericaDailyIPDetails(IPDocUpdateFileName, HeaderDate, CUSIP, FundName, PDFName, DocumentType,	EffectiveDate, DocumentDate, RRDInternalDocumentID,
								 SECAcc#, SECDateFiled, SECFormType, EdgarID, PageCount, PageSizeHeight, PageSizeWidth, RPProcessStep, CreatedDate)
				SELECT @IPDocUpdateFileName, @HeaderDate, CUSIP, FundName, PDFName, DocumentType,	EffectiveDate, DocumentDate, RRDInternalDocumentID,
				       SECAcc#, SECDateFiled, SECFormType, EdgarID, PageCount, PageSizeHeight, PageSizeWidth, RPProcessStep, GetDate()
				FROM @IPDetailstable
				WHERE DocumentType NOT IN ('QR', 'RQR')

	-- Finally fetch all details for file creation
	SELECT CUSIP, FundName, PDFName, DocumentType,	EffectiveDate, DocumentDate, RRDInternalDocumentID, 
	SECAcc#, SECDateFiled, SECFormType, EdgarID, PageCount, PageSizeHeight, PageSizeWidth, RPProcessStep
	FROM @IPDetailstable
	WHERE DocumentType NOT IN ('QR', 'RQR')
	
	
 End
 Go