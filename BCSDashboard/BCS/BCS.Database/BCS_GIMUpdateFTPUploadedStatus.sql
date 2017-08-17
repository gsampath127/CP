ALTER Procedure BCS_GIMUpdateFTPUploadedStatus
@ZIPFileName nvarchar(255),
@ExportedDate datetime
as
Begin


	UPDATE BCSDocUpdateGIMSlink
	 SET IsExported=1,ExportedDate=@ExportedDate
	WHERE ZipFileName = @ZIPFileName
	AND IsExported=0


	UPDATE BCSDocUpdateSupplementsSlink  
	SET IsExported = 1, ExportedDate = @ExportedDate  
	WHERE ZipFileName = @ZIPFileName AND IsExported=0


	UPDATE BCSDocUpdateARSARSlink  
	SET IsExported = 1, ExportedDate = @ExportedDate  
	WHERE ZipFileName = @ZIPFileName AND IsExported=0
	  
End

