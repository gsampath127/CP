CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetErrorLog]
@ErrorCode int = NULL,
@FromErrorDate DateTime = NULL,
@ToErrorDate DateTime = NULL,
@Title nvarchar(512) = NULL,
@ProcessName nvarchar(1024) = NULL,
@EventId int = NULL
AS
BEGIN
  
  IF @FromErrorDate IS NULL
  BEGIN
	SET @FromErrorDate = '1900-01-01'
  END

  IF @ToErrorDate IS NULL
  BEGIN
	SET @ToErrorDate = GETDATE()+1
  END


  SELECT * 
  FROM ErrorLog 
  WHERE ErrorCode = ISNULL(@ErrorCode, ErrorCode) 
  AND ErrorUtcDate BETWEEN @FromErrorDate AND @ToErrorDate
  AND Title = ISNULL(@Title,Title)
  AND ProcessName = ISNULL(@ProcessName, ProcessName)
  AND EventId = ISNULL(@EventId, EventID)
END

GO

