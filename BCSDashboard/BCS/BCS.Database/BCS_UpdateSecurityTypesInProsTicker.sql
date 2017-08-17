CREATE PROCEDURE [BCS_UpdateSecurityTypesInProsTicker]
@CUSIP NVARCHAR(50),
@SecurityTypeID INT,
@SecurityTypeFeedSourceName NVARCHAR(50)
AS
BEGIN

	DECLARE @SecurityTypeFeedSourceID INT 
	SELECT @SecurityTypeFeedSourceID = SecurityTypeFeedSourceID FROM SecurityTypeFeedSource WHERE SourceName = @SecurityTypeFeedSourceName
	UPDATE ProsTicker SET 
		SecurityTypeID = @SecurityTypeID, 
		SecurityTypeFeedSourceID = @SecurityTypeFeedSourceID
	WHERE CUSIP = @CUSIP

END
GO

