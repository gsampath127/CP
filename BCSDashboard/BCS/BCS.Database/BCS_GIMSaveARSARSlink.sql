CREATE PROCEDURE BCS_GIMSaveARSARSlink
@ZipFileName nvarchar(255),
@BCSDocUpdateARSARSlinkID int output
AS
BEGIN
 INSERT INTO BCSDocUpdateARSARSlink(ZipFileName)
	VALUES(@ZipFileName)
	
	SET @BCSDocUpdateARSARSlinkID = @@IDENTITY
END