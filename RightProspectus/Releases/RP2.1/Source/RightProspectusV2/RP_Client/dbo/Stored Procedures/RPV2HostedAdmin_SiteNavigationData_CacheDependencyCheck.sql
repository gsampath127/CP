CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteNavigationData_CacheDependencyCheck]
AS
BEGIN
	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigation
	GROUP BY SiteNavigationId;
	
   	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigationVersion
	GROUP BY SiteNavigationId;
END