/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetClients]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To add and update the client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClient]	
	@clientId int,
	@clientName NVARCHAR(400),
	@clientconnectionStringName VARCHAR(200),
	@clientdatabaseName VARCHAR (200),
	@verticalMarketId int,
	@Description NVARCHAR(800),
	@modifiedBy int,
	@ClientUsers TT_ClientUsers READONLY	
	--exec [RPV2HostedAdmin_SaveClient] 'Wipro','wiproConnect1','wiproConnect1wiproConnect1',2,'Added for Test',0,sysdate,0,1
	--DROP PROCEDURE [RPV2HostedAdmin_SaveClient]
AS
BEGIN
	
	IF(@clientId=0) 
		BEGIN
		 DECLARE @identityClientId int
			INSERT INTO Clients(
				clientName,
				connectionStringName,
				databaseName,
				verticalMarketId,
				clientDescription,
				utcModifiedDate,
				ModifiedBy) 
			VALUES (
				@clientName,
				@clientconnectionStringName,
				@clientdatabaseName,
				@verticalMarketId,
				@Description,
				GETUTCDATE(),
				@modifiedBy)


			SELECT @identityClientId=SCOPE_IDENTITY()	
				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT @identityClientId,UserId,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers

		END
	ELSE
		BEGIN
			UPDATE Clients 
				SET clientName=@clientName,
				connectionStringName=@clientconnectionStringName,
				databaseName=@clientdatabaseName,
				verticalMarketId=@verticalMarketId,
				clientDescription=@Description,
				ModifiedBy=@modifiedBy,
				utcModifiedDate=GETUTCDATE()
			WHERE ClientId = @clientId
			
			DECLARE @DeletedClientUsers TT_ClientUsers				
				
			 DELETE FROM ClientUsers
			   OUTPUT deleted.ClientId,
					 deleted.UserId
			   INTO @DeletedClientUsers						
			 WHERE ClientId = @clientId 
			   AND UserId NOT IN 
			   (SELECT UserId FROM @ClientUsers)
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'ClientUsers'
					AND	[KEY] = @clientId
					AND [CUDType] = 'D' 
					AND [SecondKey]  IN (SELECT UserId from @DeletedClientUsers)
					AND UserId IS NULL;


			INSERT INTO CLIENTUSERS
			(
				CLIENTID,
				USERID,
				UtcModifiedDate,
				ModifiedBy
			)
			SELECT CLIENTID,UserID,GETUTCDATE(),@modifiedBy 
			FROM @ClientUsers WHERE UserId NOT IN
			(SELECT UserId FROM ClientUsers WHERE ClientId = @clientId)
				

		END


		
END


