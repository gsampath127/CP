

/*

	Procedure Name:[dbo].[RPV2HostedAdmin_GetClientById]

	Added By: Ashok

	Date: 09/07/2015

	Reason : To get the the Client details 

*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetClientById] 
@clientId int 
AS 
BEGIN

	SELECT
		CLIENTS.clientName, 
		CLIENTS.ConnectionStringName,
		CLIENTS.DatabaseName,
		CLIENTS.VerticalMarketId,
		CLIENTS.ClientDescription,
		Clients.IsClientDocumentsAvailableFromFTP,
		CLIENTS.UtcModifiedDate,
		CLIENTS.ModifiedBy,
		CLIENTDNS.Dns
	FROM Clients
	LEFT OUTER JOIN ClientDNS ON Clients.ClientId = ClientDNS.ClientId  
	WHERE CLIENTS.ClientId = @clientId			

END
GO
