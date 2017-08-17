
Alter Procedure BCS_GIMGetSlinkFileListPendingForUpload
as
Begin

  SELECT DISTINCT ZIPFileName 
  FROM BCSDocUpdateGIMSlink
  INNER JOIN BCSDocUpdate on BCSDocUpdateGIMSlink.DocUpdateID = BCSDocUpdate.BCSDocUpdateId
  INNER JOIN ProsTicker on BCSDocUpdate.CUSIP = ProsTicker.CUSIP
  INNER JOIN Prospectus on BCSDocUpdate.ProsID = Prospectus.ProsID
  WHERE IsRemoved =0 AND IsExported=0

  UNION

  SELECT DISTINCT ZIPFileName 
  FROM BCSDocUpdateSupplementsSlink
  INNER JOIN BCSDocUpdateSupplements on BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID
  INNER JOIN ProsTicker on BCSDocUpdateSupplements.CUSIP = ProsTicker.CUSIP
  INNER JOIN Prospectus on BCSDocUpdateSupplements.ProsID = Prospectus.ProsID
  WHERE IsRemoved = 0 AND IsExported = 0


  UNION

  SELECT DISTINCT ZIPFileName 
  FROM BCSDocUpdateARSARSlink
  INNER JOIN BCSDocUpdateARSAR on BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
  INNER JOIN ProsTicker on BCSDocUpdateARSAR.CUSIP = ProsTicker.CUSIP
  INNER JOIN Prospectus on BCSDocUpdateARSAR.ProsID = Prospectus.ProsID
  WHERE IsExported = 0

End