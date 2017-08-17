CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetErrorActivityReport]
@FromDate Datetime,
@ToDate Datetime
AS
BEGIN
    Select distinct 
	       BadRequest.SiteActivityId,
		   [Site].Name as SiteName,
           RequestIssue as BadRequestIssue,
           ParameterName as BadRequestParameterName,
           ParameterValue as BadRequestParameterValue,
		   URI.UriString,
		   RequestUtcDate,
		   ClientIPAddress,
		   UserAgentString
		   

		 From BadRequest

		INNER JOIN SiteActivity on BadRequest.SiteActivityId = SiteActivity.SiteActivityId
		INNER JOIN URI  on SiteActivity.RequestURI=URI.UriId
		INNER JOIN UserAgent on UserAgent.UserAgentId = SiteActivity.UserAgentId
		INNER JOIN [SITE]  on SiteActivity.siteId=[Site].siteId
		    
		WHERE cast(SiteActivity.RequestUtcDate as datetime) between @FromDate and @ToDate 
END
GO

