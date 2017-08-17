CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllBrowserVersion]
AS
BEGIN

	SELECT 
		   Id,
		   Name,
		   Version,
		   DownloadUrl,
		   ModifiedBy,
		   UtcModifiedDate as UtcLastModified
	FROM BrowserVersion

END

