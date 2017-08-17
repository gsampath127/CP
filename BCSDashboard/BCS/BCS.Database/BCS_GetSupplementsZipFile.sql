CREATE Procedure BCS_GetSupplementsZipFile
@EdgarID int,
@ProsID int,
@PDFName nvarchar(55)
AS
BEGIN
  Declare @BCSDocUpdateSupplementsSlinkID int
  Declare @SLINKExtractFrom int
  Declare @SLINKExtractTo int
  Declare @PageCount int
  
  SELECT @BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplementsSlinkID,
		 @PageCount=[PageCount]
   FROM BCSDocUpdateSupplements
   WHERE EdgarID = @EdgarID AND PDFName = @PDFName
   
	IF @BCSDocUpdateSupplementsSlinkID IS NULL
	BEGIN
     SELECT @SLINKExtractFrom=SLINKExtractFrom,
			@SLINKExtractTo=SLINKExtractTo FROM EdgarFunds
     WHERE EdgarID = @EdgarID and FundID = @ProsID
	END
   
   SELECT @BCSDocUpdateSupplementsSlinkID AS BCSDocUpdateSupplementsSlinkID,
			@PageCount as [PageCount],
			@SLINKExtractFrom AS SLINKExtractFrom,
			@SLINKExtractTo AS SLINKExtractTo
END