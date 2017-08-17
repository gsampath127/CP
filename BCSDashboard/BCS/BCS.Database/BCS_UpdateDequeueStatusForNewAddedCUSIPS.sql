Alter Procedure BCS_UpdateDequeueStatusForNewAddedCUSIPS
as
Begin
Declare @UpdateDequeueTable Table
 (
	ID int identity(1,1),
	BCSURLDownloadQueueID int,
	ProsId int,
	ProsDocID int,
	ProsDocTypeID varchar(3),
	ProcessedDate datetime,
	ShouldUpdate bit
	 
 )
 INSERT INTO @UpdateDequeueTable
 SELECT BCSURLDownloadQueueID,BCSURLDownloadQueue.ProsID
		,BCSURLDownloadQueue.ProsDocID,Prosdocs.ProsDocTypeId
		,BCSURLDownloadQueue.ProcessedDate
		,1
 FROM BCSURLDownloadQueue
	 INNER JOIN Edgar on BCSURLDownloadQueue.EdgarID = Edgar.EdgarID
	 INNER JOIN ProsDocs on BCSURLDownloadQueue.ProsDocID = ProsDocs.ProsDocID
 WHERE  IsDequeued in (0,1) and IsDownloaded=0
   AND BCSURLDownloadQueue.ProsID not in (SELECT ProsID FROM BCSDocUpdate)
   AND BCSURLDownloadQueue.ProsID in 
    (
	  SELECT Prospectusid from ProsTicker 
      INNER JOIN Prospectus on ProsTicker.ProspectusID = Prospectus.ProsID
      INNER JOIN Company on Prospectus.CompanyID = Company.CompanyID
      AND ISNULL(Cusip,'') != ''
    )
      AND  ProcessedDate < GETDATE()
 ORDER BY BCSURLDownloadQueue.ProsID,
		Prosdocs.ProsDocTypeId Desc,
		Edgar.DocumentDate Desc,      
		Edgar.DateFiled Desc
		
			
	DECLARE @RowCount int
   
    Set @RowCount = 1
    
    DECLARE @CurrentProsID int
    
    DECLARE @CurrentDequeueID int
    
        Declare @QueueIDRowCount int
	
	SELECT @QueueIDRowCount = COUNT(*) from @UpdateDequeueTable
        
    WHILE @RowCount <= @QueueIDRowCount
	BEGIN
	
		SELECT @CurrentDequeueID = ID,@CurrentProsID = ProsId from @UpdateDequeueTable 		
		 WHERE ID = @RowCount
	
		UPDATE @UpdateDequeueTable
		  SET ShouldUpdate=0
		  WHERE ProsId = @CurrentProsID
		  AND ID > @RowCount 
		  AND ShouldUpdate=1
		  
		SET @RowCount = @RowCount + 1
	End
	
	Update BCSURLDownloadQueue
	 SET IsDequeued=0
	 WHERE BCSURLDownloadQueueID in
	   (
		SELECT BCSURLDownloadQueueID FROM @UpdateDequeueTable
	     WHERE ShouldUpdate=1
	    )
End