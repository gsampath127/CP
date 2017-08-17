Alter Procedure BCS_GIMUpdateDASStatus
@CUSIP nvarchar(10),
@PDFName nvarchar(55),
@DocumentType nvarchar(4),
@RPProcessStep nvarchar(4),
@DASStatus nvarchar(5),
@DASReportingStatus nvarchar(1000),
@DASStatusReceivedDate datetime
as
Begin
  IF @RPProcessStep = 'FL'
	  Begin
	    UPDATE BCSDocUpdate
	     SET DASStatus = @DASStatus,
			DASReportingStatus = @DASReportingStatus,
			DASStatusReceivedDate=@DASStatusReceivedDate
		 WHERE CUSIP = @CUSIP 
		 AND  PDFName = @PDFName 
		 AND  DocumentType = @DocumentType				
	  End
  ELSE
    IF @RPProcessStep in('EX','oP','aP')
       Begin
          UPDATE BCSDocUpdateGIMSlink
            SET DASStatus = @DASStatus,
            DASReportingStatus = @DASReportingStatus,
            DASStatusReceivedDate=@DASStatusReceivedDate
           WHERE DocUpdateID in
           ( 
			SELECT BCSDocUpdateId from BCSDocUpdate                        
				WHERE CUSIP = @CUSIP 
				AND  PDFName = @PDFName 
				AND  DocumentType = @DocumentType		
			)   
       END
	
End