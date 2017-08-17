/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteUser]
	Added By: Ashok
	Date: 09/07/2015
	Updated : 09/19/2015 BY Noel
	Reason : To DELETE THE USER
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteUser] 
				 @userId int,
				 @deletedBy int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_DeleteUser]
AS 
BEGIN 
				
				DELETE FROM ClientUsers
				 WHERE UserId = @userId 
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'ClientUsers'
					AND	[SecondKey] = @userId
					AND [CUDType] = 'D';
				   
				   
				DELETE FROM UserRoles
				 WHERE UserId = @userId 
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'UserRoles'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 

				DELETE  USERS				
				WHERE UserId=@userId	
				
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'USERS'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 
				
END