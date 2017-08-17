Create Procedure BCS_GIMRemoveExistingPDFZipRecords
@BCSDocUpdateID int
as
Begin
  DELETE BCSDocUpdateGIMSlink
   WHERE DocUpdateID = @BCSDocUpdateID
  
End