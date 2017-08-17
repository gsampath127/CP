CREATE PROCEDURE BCS_GIMSaveSupplementSlink
@ZipFileName nvarchar(255),
@BCSDocUpdateSupplementsSlinkID int output
AS
BEGIN
 INSERT INTO BCSDocUpdateSupplementsSlink(ZipFileName)
	VALUES(@ZipFileName)
	
	SET @BCSDocUpdateSupplementsSlinkID = @@IDENTITY
END