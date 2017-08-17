
Create PROCEDURE [dbo].[RPV2HostedSites_ClientsSiteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteText
	GROUP BY SiteTextId;
	
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageText
	GROUP BY PageTextId;

   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteTextVersion
	GROUP BY SiteTextId;
	
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageTextVersion
	GROUP BY PageTextId;
	
	SELECT	SiteID, COUNT_BIG(*) AS Total
	FROM	dbo.Site
	GROUP BY SiteID;
	
	SELECT StaticResourceId, COUNT_BIG(*) AS Total 
	FROM dbo.StaticResource 
	GROUP BY StaticResourceId

	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigation
	GROUP BY SiteNavigationId;

	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigationVersion
	GROUP BY SiteNavigationId;

	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigation
	GROUP BY PageNavigationId;

	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigationVersion
	GROUP BY PageNavigationId;

	SELECT	SiteId, [Key], COUNT_BIG(*) AS Total
	FROM	dbo.SiteFeature
	GROUP BY SiteId, [Key]

	SELECT	SiteId, PageId, [Key], COUNT_BIG(*) AS Total
	FROM	dbo.PageFeature
	GROUP BY SiteId, PageId, [Key]

END