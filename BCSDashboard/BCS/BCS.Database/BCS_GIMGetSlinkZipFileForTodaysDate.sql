Alter Procedure BCS_GIMGetSlinkZipFileForTodaysDate
@PDFName nvarchar(55),
@ZipFilePath nvarchar(255),
@BCSDocupdateID int
as
Begin
  Declare @ZipFileName nvarchar(255)
  IF @BCSDocupdateID > -1
	  Begin
		  SELECT TOP 1 @ZipFileName=ZipFileName				   
				  FROM BCSDocUpdateGIMSlink
						INNER JOIN BCSDocUpdate on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateGIMSlink.DocUpdateID
				  WHERE PDFName = @PDFName AND ZipFileName like @ZipFilePath + '%'
				  AND BCSDocUpdate.BCSDocUpdateId != @BCSDocupdateID
	  END
  ELSE
      BEGIN -- for Initial Load purpose
	  		  SELECT TOP 1 @ZipFileName=ZipFileName				   
				  FROM BCSDocUpdateGIMSlink
						INNER JOIN BCSDocUpdate on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateGIMSlink.DocUpdateID
				  WHERE PDFName = @PDFName AND ZipFileName like @ZipFilePath + '%'  
      END
       
   SELECT @ZipFileName		  
End