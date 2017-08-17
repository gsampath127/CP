CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllRolesByUserId]
@UserId int
AS

BEGIN

   Select DISTINCT
          UserId ,
          UserRoles.RoleId,
          Name ,
		  UserRoles.UtcModifiedDate as UtcLastModified,
		  UserRoles.ModifiedBy as ModifiedBy
          FROM UserRoles
          INNER JOIN Roles on UserRoles.RoleId = Roles.RoleId
          WHERE UserRoles.UserId = @UserId


END