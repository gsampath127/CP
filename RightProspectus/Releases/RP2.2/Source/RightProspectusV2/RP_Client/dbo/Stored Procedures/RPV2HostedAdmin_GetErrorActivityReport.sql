CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetErrorActivityReport]
@FromDate DATETIME,
@ToDate DATETIME
AS
BEGIN

    SELECT DISTINCT 
	       BadRequest.SiteActivityId,
		   [Site].Name AS SiteName,
           RequestIssue AS BadRequestIssue,
           ParameterName AS BadRequestParameterName,
           ParameterValue AS BadRequestParameterValue,
		   URI.UriString,
		   RequestUtcDate,
		   ClientIPAddress,
		   UserAgentString
	FROM BadRequest
	INNER JOIN SiteActivity ON BadRequest.SiteActivityId = SiteActivity.SiteActivityId
	INNER JOIN URI  ON SiteActivity.RequestURI=URI.UriId
	INNER JOIN UserAgent ON UserAgent.UserAgentId = SiteActivity.UserAgentId
	INNER JOIN [SITE]  ON SiteActivity.siteId=[Site].siteId		    
	WHERE SiteActivity.RequestUtcDate between @FromDate and @ToDate 
	ORDER BY BadRequest.SiteActivityId

END
GO

