CREATE Procedure BCS_GetARSARZipFile
@PDFName nvarchar(55),
@ZipFilePath nvarchar(255)
AS
BEGIN
  Declare @BCSDocUpdateSupplementsSlinkID int
 
  Declare @PageCount int
  
  SELECT BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
   FROM BCSDocUpdateARSAR
   INNER JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID =
		 BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
   WHERE  PDFName = @PDFName
   AND ZipFileName like @ZipFilePath + '%'
	
END