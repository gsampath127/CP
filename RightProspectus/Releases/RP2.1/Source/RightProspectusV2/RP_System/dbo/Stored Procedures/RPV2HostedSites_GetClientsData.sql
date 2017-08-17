CREATE Procedure [dbo].RPV2HostedSites_GetClientsData
AS
BEGIN

	SELECT Clients.ClientID,
			ClientName,
			DNS as ClientDNS,
			Clients.ConnectionStringName AS ClientConnectionStringName,
			Clients.DatabaseName AS ClientDatabaseName,
			VerticalMarkets.ConnectionStringName as VerticalMarketConnectionStringName,
			VerticalMarkets.DatabaseName as VerticalMarketsDatabaseName
	FROM Clients
		INNER JOIN VerticalMarkets ON Clients.VerticalMarketId = VerticalMarkets.VerticalMarketId
		LEFT OUTER JOIN ClientDns on Clients.ClientId = ClientDns.ClientId

	SELECT  TemplatePage.TemplateId,
		Template.[Name] AS TemplateName,
		Page.PageID,
		Page.Name as PageName  
	FROM Template
	INNER JOIN TemplatePage ON Template.TemplateID = TemplatePage.TemplateID
	INNER JOIN Page ON TemplatePage.PageId =  Page.PageID
	ORDER BY TemplatePage.TemplateId,Page.PageID

	
	SELECT  TemplateId, NavigationKey, XslTransform, DefaultNavigationXml
	FROM TemplateNavigation


	SELECT  TemplateId, PageId, NavigationKey, XslTransform, DefaultNavigationXml
	FROM TemplatePageNavigation

	
END	