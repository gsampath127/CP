
CREATE PROCEDURE [dbo].[BCS_GIMSaveARSAR]
@EdgarID int,
@PDFName nvarchar(55),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@FundName nvarchar(255),
@ProsDocID int,
@ProcessedDate datetime,
@BCSDocUpdateARSARSlinkID int
AS
BEGIN

	DECLARE @PageSizeHeight decimal(5,2)
	DECLARE @PageSizeWidth decimal(5,2)
	DECLARE @PageCount int

	DECLARE @Acc# nvarchar(250)

	DECLARE @EffectiveDate datetime
	
	DECLARE @DocumentDate datetime
	
	DECLARE @FiledDate datetime
	
	DECLARE @FormType nvarchar(100)

	Declare @DocumentType nvarchar(4)

	SELECT @PageCount=[PageCount],
			@PageSizeHeight=PageSizeHeight,
			@PageSizeWidth=PageSizeWidth
	  FROM ProsDocs
	  WHERE Prosdocid = @ProsDocID
	  
	  SELECT @DocumentType=
							REPLACE(
								REPLACE(
									REPLACE(
										REPLACE(
											REPLACE(
												REPLACE(DocumentType
													,'Semi Annual Report - New','SAR')
													,'Semi Annual Report - Revised','RSAR')
													,'Annual Report - New','AR')
													,'Annual Report - Revised','RAR')
													,'Quarterly Report - New','QR')
													,'Quarterly Report - Revised','RQR'),													
					@Acc#=Acc#,
					@EffectiveDate=ISNULL(EffectiveDate,DocumentDate),
					@DocumentDate=DocumentDate,
					@FiledDate=DateFiled,
					@FormType=FormType  
		 FROM Edgar
		 WHERE EdgarID = @EdgarID

  IF EXISTS(SELECT EdgarID FROM BCSDocUpdateARSAR
			WHERE CUSIP = @CUSIP
			AND EdgarID = @EdgarID 
			AND IsProcessed=0
			)
  BEGIN
		UPDATE BCSDocUpdateARSAR			
				SET 
				 Acc#=@Acc#,
				 PDFName=@PDFName,
				 DocumentType=@DocumentType,				 
				 TickerID=@TickerID,
				 EffectiveDate=@EffectiveDate,
				 DocumentDate=@DocumentDate,	 
				 ProsID=@ProsID,
				 FundName=@FundName,
				 ProsDocID=@ProsDocID,
				 IsFiled=1,
				 FiledDate=@FiledDate,				 
				 IsProcessed=1,
				 ProcessedDate=@ProcessedDate,
				 [PageCount]=@PageCount,
				 PageSizeHeight=@PageSizeHeight,
				 PageSizeWidth=@PageSizeWidth,
				 BCSDocUpdateARSARSlinkID=@BCSDocUpdateARSARSlinkID,
				 FormType=@FormType			 
		WHERE EdgarID = @EdgarID
		  AND CUSIP = @CUSIP
			
  END  
  ELSE
  BEGIN

		  INSERT INTO BCSDocUpdateARSAR
			(
				 EdgarID,
				 Acc#,
				 PDFName,
				 DocumentType,
				 CUSIP,
				 TickerID,
				 EffectiveDate,
				 DocumentDate,	 
				 ProsID,
				 FundName,
				 ProsDocID,
				 IsFiled,
				 FiledDate,
				 FilingStatusAddedDate,
				 IsProcessed,
				 ProcessedDate,
				 [PageCount],
				 PageSizeHeight,
				 PageSizeWidth,
				 BCSDocUpdateARSARSlinkID,
				 FormType
			 )
			 VALUES
			 (
				 @EdgarID,
				 @Acc#,
				 @PDFName,
				 @DocumentType,
				 @CUSIP,
				 @TickerID,
				 @EffectiveDate,
				 @DocumentDate,
				 @ProsID,
				 @FundName,
				 @ProsDocID,
				 1,
				 @FiledDate,
				 @ProcessedDate,
				 1,
				 @ProcessedDate,
				 @PageCount,
				 @PageSizeHeight,
				 @PageSizeWidth,
				 @BCSDocUpdateARSARSlinkID,
				 @FormType
			 )
	END
END