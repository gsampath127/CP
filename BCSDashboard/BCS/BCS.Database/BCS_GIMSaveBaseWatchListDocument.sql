CREATE PROCEDURE BCS_GIMSaveBaseWatchListDocument
@EdgarID int,
@PDFName nvarchar(55),
@DocumentType nvarchar(4),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@FundName nvarchar(255),
@ProsDocID int,
@ProcessedDate datetime,
@ZipFileName nvarchar(255)
AS
BEGIN
	DECLARE @PageCount int
	DECLARE @PageSizeHeight decimal(5,2)
	DECLARE @PageSizeWidth decimal(5,2)

	DECLARE @Acc# nvarchar(250)

	DECLARE @EffectiveDate datetime
	
	DECLARE @DocumentDate datetime
	
	DECLARE @FiledDate datetime
	
	DECLARE @FormType nvarchar(100)
	
	DECLARE @BCSDocUpdateSupplementsSlinkID int

	IF @DocumentType in ('SP','SPS','RSP')
	  BEGIN
	     UPDATE BCSDocUpdateSupplements
		   SET IsRemoved=1
		WHERE CUSIP = @CUSIP 
		 AND DocumentType in ('P','PS','RP')
	  END
	
	SELECT @BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplementsSlinkID
	FROM BCSDocUpdateSupplementsSlink
	WHERE ZipFileName = @ZipFileName
	
	IF @BCSDocUpdateSupplementsSlinkID IS NULL
	BEGIN
		INSERT INTO BCSDocUpdateSupplementsSlink(ZipFileName)
		VALUES(@ZipFileName)
		
		SET @BCSDocUpdateSupplementsSlinkID = @@IDENTITY
	END

	SELECT @Acc#=Acc#,
		@EffectiveDate=EffectiveDate,
		@DocumentDate=DocumentDate,
		@FiledDate=DateFiled,
		@FormType=FormType 
	 FROM Edgar
	 WHERE EdgarID = @EdgarID

	SELECT @PageCount=[PageCount],@PageSizeHeight=PageSizeHeight,@PageSizeWidth=PageSizeWidth
	  FROM ProsDocs
	  WHERE Prosdocid = @ProsDocID

	  IF EXISTS(SELECT EdgarID FROM BCSDocUpdateSupplements
				WHERE CUSIP = @CUSIP
				AND EdgarID = @EdgarID 
				AND IsProcessed=0 AND BCSDocUpdateSupplements.IsRemoved = 0
				)
	  BEGIN 
			UPDATE BCSDocUpdateSupplements
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
				 BCSDocUpdateSupplementsSlinkID=@BCSDocUpdateSupplementsSlinkID,
				 FormType=@FormType			 
			WHERE EdgarID = @EdgarID
			 AND CUSIP = @CUSIP 
	  END
	ELSE
	  BEGIN

		  INSERT INTO BCSDocUpdateSupplements
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
			 BCSDocUpdateSupplementsSlinkID,
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
			 @BCSDocUpdateSupplementsSlinkID,
			 @FormType
			 )
	 
	 END

END