CREATE PROCEDURE [dbo].[RPV2HostedSites_GetBrowserVersion]
	@BrowserName NVARCHAR(100)=NULL
AS
BEGIN

	SELECT	Id,
			Name,
			[Version],
			DownloadUrl
	FROM BrowserVersion
	WHERE Name = @BrowserName
	
END
GO

