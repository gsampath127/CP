ALTER Procedure [dbo].[BCS_GIMSaveSlinkZipDetails]
@BCSDocUpdateID int,
@ZipFileName nvarchar(255),
@PDFName nvarchar(55),
@IsSlinkExists bit
as
Begin

 IF NOT EXISTS(SELECT DocUpdateID from BCSDocUpdateGIMSlink WHERE ZipFileName = @ZipFileName and DocUpdateID=@BCSDocUpdateID)
 Begin

  Delete BCSDocUpdateGIMSlink 
  WHERE DocUpdateID = @BCSDocUpdateID 
 
	IF @IsSlinkExists=1
		Begin
		  	
		
		  INSERT INTO BCSDocUpdateGIMSlink(DocUpdateID,ZipFileName,
											IsExported,ExportedDate,
											IsAPF,APFReceivedDate,
											IsOPF,OPFReceivedDate)
		  SELECT TOP 1 @BCSDocUpdateID,ZipFileName,
					   IsExported,ExportedDate,
					   IsAPF,APFReceivedDate,
					   IsOPF,OPFReceivedDate				   
		  FROM BCSDocUpdateGIMSlink
				INNER JOIN BCSDocUpdate on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateGIMSlink.DocUpdateID
		  WHERE PDFName = @PDFName AND ZipFileName = @ZipFileName -- Zip filename that has been already added for today's date.
		  
		  
		End	
	Else
		Begin		  
			INSERT INTO BCSDocUpdateGIMSlink(DocUpdateID,ZipFileName)
				VALUES(@BCSDocUpdateID,@ZipFileName)
		End
 End
End
