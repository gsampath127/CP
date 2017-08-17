/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetUserById]
	Added By: Ashok
	Date: 09/07/2015
	Reason : To get the the User details 
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetUserById]
				 @userId int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetUserById]
 --exec RPV2System_GetUserById 2
AS BEGIN 

			SELECT 
				USERS.UserId, 				
				USERS.Email,
				USERS.EmailConfirmed,
				USERS.PasswordHash,
				USERS.SecurityStamp,
				USERS.PhoneNumber,
				USERS.PhoneNumberConfirmed,
				USERS.TwoFactorEnabled,
				USERS.LockOutEndDateUtc,
				USERS.LockoutEnabled,
				USERS.AccessFailedCount,
				USERS.UserName,
				USERS.FirstName,
				USERS.LastName,					
				USERS.UtcModifiedDate,
				USERS.ModifiedBy
				FROM USERS
				WHERE USERS.UserId=@userId
			
	END
