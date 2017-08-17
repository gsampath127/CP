/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteRoles]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE Roles
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteRoles] 
				 @rolesId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE Roles
		   WHERE RoleId = @rolesId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'Roles'
				AND	[Key] = @rolesId
				AND [CUDType] = 'D' 

END