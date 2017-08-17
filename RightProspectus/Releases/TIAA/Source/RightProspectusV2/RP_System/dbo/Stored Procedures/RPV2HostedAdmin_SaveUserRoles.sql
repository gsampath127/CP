

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUserRoles]
@UserId int,
@RoleId int,
@ModifiedBy int
AS
BEGIN
  IF EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @UserId )
		 BEGIN
			UPDATE UserRoles
			SET 
				RoleId = @RoleId,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE UserId = @UserId	
		 END
   ELSE
          BEGIN
		       INSERT INTO UserRoles(
			USerId,
			RoleId,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@UserId,
			@RoleId,
		    GETUTCDATE(),
			@ModifiedBy
			)
		  END

END