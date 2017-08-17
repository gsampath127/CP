CREATE PROCEDURE [dbo].[RPV2HostedAdmin_PageNavigationData_CacheDependencyCheck]
AS
BEGIN
	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigation
	GROUP BY PageNavigationId;
	
   	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigationVersion
	GROUP BY PageNavigationId;
END
