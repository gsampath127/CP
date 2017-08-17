 /*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetAllRoles]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To GET ALL Roles
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllRoles]
AS
BEGIN
  SELECT 
	RoleId,
	Name,
    ModifiedBy,
    UtcModifiedDate as UtcLastModified
  FROM Roles
   
END