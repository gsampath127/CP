CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticClientsData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	ClientID, COUNT_BIG(*) AS Total
	FROM	dbo.ClientDns
	GROUP BY ClientID;
	
   	SELECT	ClientID, COUNT_BIG(*) AS Total
	FROM	dbo.Clients
	GROUP BY ClientID;
	
	SELECT	VerticalMarketId, COUNT_BIG(*) AS Total
	FROM	dbo.VerticalMarkets
	GROUP BY VerticalMarketId;

	SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
   	SELECT	PageID, COUNT_BIG(*) AS Total
	FROM	dbo.Page
	GROUP BY PageID;

	SELECT	ClientId, COUNT_BIG(*) AS Total
	FROM	dbo.ClientUsers
	GROUP BY ClientId;

	SELECT	TemplateId, NavigationKey,  COUNT_BIG(*) AS Total
	FROM	dbo.TemplateNavigation
	GROUP BY TemplateId, NavigationKey;

	SELECT	TemplateId, PageId, NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageNavigation
	GROUP BY TemplateId, PageId, NavigationKey;
	
END