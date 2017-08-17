Create Procedure BCS_SaveBCSSynchronizerArchive
@BCSURLDownloadQueueID int
as
Begin
    DELETE FROM BCSURLDownloadQueue 	 
		 OUTPUT deleted.BCSURLDownloadQueueID,
				deleted.EdgarID, 
				deleted.ProsID, 
				deleted.ProsDocID,
				deleted.URLToDownload,	
				deleted.IsDequeued,
				deleted.IsDownloaded,
				deleted.IsErrored,			
				deleted.ProcessedDate INTO  BCSURLDownloadQueueArchive   			
	 WHERE BCSURLDownloadQueueID = @BCSURLDownloadQueueID  
End