Create Procedure BCS_GetBCSSynchronizerRecordsToArchive
as
Begin
  SELECT 
	BCSURLDownloadQueueID
  FROM BCSURLDownloadQueue
  WHERE IsDownloaded =1 and ProcessedDate <= GETDATE()-5 
  
End