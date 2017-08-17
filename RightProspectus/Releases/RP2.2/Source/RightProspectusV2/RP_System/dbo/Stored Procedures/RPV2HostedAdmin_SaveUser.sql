/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveUser]
	Added By: Ashok
	Date: 09/07/2015
	Updated : 09/19/2015 By Noel Dsouza
	Reason : To add and update the client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUser] 
				 @userId int,
                 @email nVARCHAR(512),
				 @emailConfirmed bit,
                 @passwordHash nvarchar,                
				 @securityStamp nvarchar,
                 @PhoneNumber nvarchar, 
				 @phoneNumberConfirmed bit, 
				 @twoFactorEnabled bit,
				 @userName nvarchar(512),
				 @firstName nvarchar(200),
				 @lastName nvarchar(200),
				 @modifiedBy int,
				 @UserRoles TT_UserRoles READONLY,
				 @ClientUsers TT_ClientUsers READONLY	
--DROP PROCEDURE [RPV2HostedAdmin_SaveUser]
AS
BEGIN 
	IF @userId = 0 
	BEGIN
	   DECLARE @identityUserId int
					INSERT INTO USERS(					
										Email,
										EmailConfirmed,
										PasswordHash,
										SecurityStamp,
										PhoneNumber,
										PhoneNumberConfirmed,
										TwoFactorEnabled,
										UserName,
										FirstName,
										LastName,					
										UtcModifiedDate,
										ModifiedBy									
										) 
								VALUES (
										@email ,
										@emailConfirmed,
										@passwordHash,
										@securityStamp,
										@PhoneNumber,
										@phoneNumberConfirmed,
										@twoFactorEnabled,
										@userName,
										@firstName,
										@lastName,
										GETUTCDATE(),
										@modifiedBy									
										)
					
				SELECT @identityUserId=SCOPE_IDENTITY()			
			

				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT CLIENTID,@identityUserId,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers
				
				
				INSERT INTO UserRoles
				(
					USERID,				
					RoleId,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT @identityUserId,RoleID,GETUTCDATE(),@modifiedBy
				 FROM @UserRoles


	END 
	ELSE
		BEGIN
			UPDATE USERS
				SET 														
					Email=@email ,
					EmailConfirmed=@emailConfirmed,
					PasswordHash=@passwordHash,
					SecurityStamp=@securityStamp,
					PhoneNumber=@PhoneNumber,
					PhoneNumberConfirmed=@phoneNumberConfirmed,
					TwoFactorEnabled=@twoFactorEnabled,
					UserName=@userName,
					FirstName=@firstName,
					LastName=@lastName,
					UtcModifiedDate=GETUTCDATE(),
					ModifiedBy=@modifiedBy
				WHERE UserId=@userId
				
				DECLARE @DeletedClientUsers TT_ClientUsers				
				
				DELETE FROM ClientUsers
				  OUTPUT deleted.ClientId,
						 deleted.UserId
				  INTO @DeletedClientUsers						
				 WHERE UserId = @userId 
				   AND ClientId NOT IN 
				   (SELECT ClientId FROM @ClientUsers)
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'ClientUsers'
					AND	[SecondKey] = @userId
					AND [CUDType] = 'D' 
					AND [Key]  in (SELECT ClientID from @DeletedClientUsers)
					AND UserId IS NULL;
				   
				   
				DECLARE @DeletedUserRoles TT_UserRoles
				
				DELETE FROM UserRoles
				  OUTPUT deleted.UserId,
						 deleted.RoleId
				  INTO @DeletedUserRoles						
				 WHERE UserId = @userId 
				   AND RoleId NOT IN 
				   (SELECT RoleID FROM @UserRoles)		
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'UserRoles'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 
					AND [SecondKey]  in (SELECT RoleID from @DeletedUserRoles)
					AND UserId IS NULL;
				   		   
				 
				
				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT CLIENTID,UserID,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers WHERE ClientID NOT IN
				(SELECT CLIENTID FROM ClientUsers WHERE UserId = @userId)
				
				
				INSERT INTO UserRoles
				(
					USERID,				
					RoleId,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT UserID,RoleID,GETUTCDATE(),@modifiedBy
				 FROM @UserRoles WHERE RoleID NOT IN
				(SELECT RoleID FROM UserRoles  WHERE UserId = @userId)


	END

END
