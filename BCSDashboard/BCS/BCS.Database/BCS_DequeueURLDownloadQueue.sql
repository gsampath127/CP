ALTER Procedure [dbo].[BCS_DequeueURLDownloadQueue]
as
Begin

	SET NOCOUNT ON

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	Declare @ProcessedDate datetime
	
	set @ProcessedDate = DATEADD(mi,-5,getdate())
	
	Declare @BatchSize int
	set @BatchSize = 5


	Declare @ArchiveBatch Table
	(
		BCSURLDownloadQueueID int,
		EdgarId int,
		ProsID int,
		ProsDocID int,
		URLToDownload nvarchar(500),
		ProcessedDate datetime
	)
  
	UPDATE top(@BatchSize) BCSURLDownloadQueue WITH (UPDLOCK, READPAST)
	 SET IsDequeued = 1
		 OUTPUT inserted.BCSURLDownloadQueueID,
				inserted.EdgarID, 
				inserted.ProsID, 
				inserted.ProsDocID,
				inserted.URLToDownload,				
				inserted.ProcessedDate INTO  @ArchiveBatch   			
	 WHERE IsDequeued=0 and ProcessedDate <= @ProcessedDate
	 
	 IF exists(SELECT top 1 BCSURLDownloadQueueID FROM @ArchiveBatch)	 
		 SELECT ArchiveBatch.BCSURLDownloadQueueID,ArchiveBatch.EdgarID,ArchiveBatch.ProsID,Prosticker.CUSIP,TickerID,
				ArchiveBatch.ProsDocID,URLToDownload,ProcessedDate,
				Company.CompanyName + '-' + Prospectus.ProsName as FundName,
				ProsDocTypeId,Edgar.DocumentType,
				CASE WHEN BCSWatchListCUSIPView.CUSIP is null THEN 0 ELSE 1 END AS IsWatchListCUSIP,
				CASE WHEN BCSFLWatchListCUSIPView.CUSIP is null THEN 0 ELSE 1 END AS IsFLWatchListCUSIP			
		  FROM @ArchiveBatch ArchiveBatch 	  
		  INNER JOIN Prospectus on Prospectus.ProsID = ArchiveBatch.ProsID
		  INNER JOIN Company on Prospectus.CompanyID = Company.CompanyID
		 INNER JOIN ProsTicker on ArchiveBatch.ProsID = ProsTicker.ProspectusID
		 INNER JOIN ProsDocs on ArchiveBatch.ProsDocID = ProsDocs.ProsDocID
		 INNER JOIN Edgar on Edgar.EdgarID = ArchiveBatch.EdgarID
		 LEFT OUTER JOIN BCSWatchListCUSIPView on Prosticker.CUSIP = BCSWatchListCUSIPView.CUSIP
		 LEFT OUTER JOIN BCSFLWatchListCUSIPView on Prosticker.CUSIP = BCSFLWatchListCUSIPView.CUSIP
		 WHERE ISNULL(Prosticker.CUSIP,'') != '' 
		 ORDER BY ArchiveBatch.ProsID,ArchiveBatch.URLToDownload
	 ELSE
	     SELECT -1 BCSURLDownloadQueueID,-1 as EdgarID,-1 as ProsID,'' as CUSIP,-1 as TickerID,
				-1 as ProsDocID,'' as URLToDownload,'01/01/1980' as ProcessedDate,
				'' as FundName,'' as ProsDocTypeId,'' as DocumentType,0 AS IsWatchListCUSIP,
				0 AS IsFLWatchListCUSIP
	     WHERE 1 <> 1
	 
	 
End	 