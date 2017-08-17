-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015
-- Updated date : 22nd-Sept-2015 By Noel
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClients]
AS
BEGIN
  SELECT 
	Clients.ClientId,
    Clients.ClientName,
    VerticalMarkets.MarketName AS VerticalMarketName,
    Clients.ClientDescription,
    Clients.ConnectionStringName AS ClientConnectionStringName,    
    Clients.DatabaseName AS ClientDatabaseName,
	VerticalMarkets.DatabaseName AS VerticalMarketDatabaseName,
	VerticalMarkets.ConnectionStringName AS VerticalMarketConnectionStringName,
    Clients.VerticalMarketId,
    ClientDns.ClientDnsId,
	ClientDns.SiteId As ClientDnsSiteId,
    ClientDns.Dns,
    ClientUsers.UserId,
    Clients.ModifiedBy,
    Clients.UtcModifiedDate AS UtcLastModified
  FROM Clients
   INNER JOIN VerticalMarkets ON Clients.VerticalMarketId = VerticalMarkets.VerticalMarketId
   LEFT OUTER JOIN ClientDNS ON Clients.ClientId = ClientDNS.ClientId  
   LEFT OUTER JOIN ClientUsers ON Clients.ClientId = ClientUsers.ClientId
  ORDER BY Clients.ClientId
END