/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveRoles]
	Added By: Noel
	Date: 09/19/2015
	Reason : To add and update the role
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveRoles]	
	@RoleId int,
	@Name NVARCHAR(256),
	@modifiedBy int
AS
BEGIN
	
	IF(@RoleId=0) 
	BEGIN
	  INSERT INTO Roles(Name,
						UtcModifiedDate,
						ModifiedBy)
				VALUES(@Name,
						GETUTCDATE(),
						@modifiedBy)
	END
	ELSE
	BEGIN
	  UPDATE Roles
	   SET Name = @Name,
	   UtcModifiedDate = GETUTCDATE(),
	   ModifiedBy = @modifiedBy	    
	END
END