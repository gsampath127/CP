Create Procedure BCS_UpdateURLToDownloadStatus
@BCSURLDownloadQueueID int,
@IsErrored bit=0,
@IsDownloaded bit=0
as
Begin
  IF @IsErrored = 1
  Begin
     UPDATE BCSURLDownloadQueue
      SET IsErrored=1
      WHERE BCSURLDownloadQueueID = @BCSURLDownloadQueueID     
  End
  Else
   If @IsDownloaded = 1
   Begin
      UPDATE BCSURLDownloadQueue
      SET IsDownloaded=1
      WHERE BCSURLDownloadQueueID = @BCSURLDownloadQueueID     
   End
End