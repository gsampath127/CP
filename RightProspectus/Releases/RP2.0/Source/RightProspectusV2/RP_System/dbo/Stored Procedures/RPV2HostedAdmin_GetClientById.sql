

/*

	Procedure Name:[dbo].[RPV2HostedAdmin_GetClientById]

	Added By: Ashok

	Date: 09/07/2015

	Reason : To get the the Client details 

*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetClientById] 

				 @clientId int                				 			

				

 --DROP PROCEDURE [RPV2SystemHostedAdmin_GetClientById]

AS BEGIN 



			SELECT 

				CLIENTS.clientName, 

				CLIENTS.ConnectionStringName,

				CLIENTS.DatabaseName,

				CLIENTS.VerticalMarketId,

				CLIENTS.ClientDescription,

				CLIENTS.UtcModifiedDate,

				CLIENTS.ModifiedBy,

				CLIENTDNS.Dns

				FROM CLIENTS,CLIENTDNS

				WHERE CLIENTS.ClientId=CLIENTDNS.ClientId

				AND CLIENTS.ClientId=@clientId



			

	END
