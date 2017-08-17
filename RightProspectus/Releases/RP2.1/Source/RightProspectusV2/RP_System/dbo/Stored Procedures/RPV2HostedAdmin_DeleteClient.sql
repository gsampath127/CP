/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteClient]
	Added By: Ashok
	Updated By: Noel Dsouza
	Date: 09/07/2015
	Reason : To DELETE THE Client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClient] 
				 @clientId int,
				 @DeletedBy int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_DeleteClient]
AS 
BEGIN 


	DELETE FROM ClientUsers
	WHERE ClientId = @clientId 
	
	DELETE  [Clients]				
	WHERE   ClientId=@clientId	

				   
	UPDATE	CUDHistory				 
  		SET	UserId = @deletedBy
	WHERE	TableName = N'ClientUsers'
		AND	[Key] = @clientId
		AND [CUDType] = 'D';

    UPDATE	CUDHistory
  	 SET	UserId = @DeletedBy
	WHERE	TableName = N'Clients'
		AND	[Key] = @clientId AND [CUDType] = 'D';
END
