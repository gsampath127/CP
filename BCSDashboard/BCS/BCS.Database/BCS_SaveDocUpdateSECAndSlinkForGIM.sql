


Alter Procedure BCS_SaveDocUpdateSECAndSlinkForGIM
@SECDetails SECDetailsType READONLY,
@RRDPDFURL nvarchar(512),
@PDFName nvarchar(55),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@FundName nvarchar(255),
@ProsDocID int,
@ZipFileName nvarchar(255)
as
Begin


DECLARE @PageCount INT
DECLARE @PageSizeHeight [decimal](5, 2)
DECLARE @PageSizeWidth [decimal](5, 2) 
				
SELECT @PageCount=[PageCount],
		@PageSizeHeight = PageSizeHeight,
		@PageSizeWidth = PageSizeWidth
	FROM ProsDocs
	WHERE ProsDocId = @ProsDocID

 INSERT INTO BCSDocUpdate(EdgarID,Acc#,
						  RRDPDFURL,PDFName,
						  DocumentType,CUSIP,
						  TickerID,EffectiveDate,
						  DocumentDate,ProsID,
						  IsFiled,FiledDate,
						  FundName,ProsDocID,
						  IsProcessed,ProcessedDate
						  ,[PageCount],PageSizeHeight
						  ,PageSizeWidth
						  )
  SELECT top 1 EdgarID,Acc#,
	     @RRDPDFURL,@PDFName,
	     SECDetails.DocumentType,@CUSIP,
	     @TickerID,SECDetails.EffectiveDate,
	     SECDetails.DocumentDate,@ProsID,
	     1,SECDetails.DateFiled,
	     @FundName,@ProsDocID,1,GETDATE(),
		 @PageCount,@PageSizeHeight,
		 @PageSizeWidth	
	FROM 	@SECDetails	SECDetails					  
	Order by SECDetails.DocumentDate desc
	
	Declare @BCSDocUpdateID int
	
	SELECT @BCSDocUpdateID = @@IDENTITY
	
	  
	INSERT INTO BCSDocUpdateGIMSlink(DocUpdateID,ZipFileName)
		VALUES(@BCSDocUpdateID,@ZipFileName)

	INSERT INTO BCSDocUpdateSECDetails(DocUpdateID,Acc#,
									   DateFiled,DocumentDate,EffectiveDate,
									   FormType,DocumentType)
     SELECT Distinct @BCSDocUpdateID,Acc#,
			DateFiled,DocumentDate,EffectiveDate,
			FormType,DocumentType
     FROM 	@SECDetails SECDetails
     Order by DocumentDate								   
	
	
						  
						  
End
