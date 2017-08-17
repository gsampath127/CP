-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllUsers]
AS
BEGIN
	SELECT 
		   [Users].[UserId]
		  ,[Email]
		  ,[EmailConfirmed]
		  ,[PasswordHash]
		  ,[SecurityStamp]
		  ,[PhoneNumber]
		  ,[PhoneNumberConfirmed]
		  ,[TwoFactorEnabled]
		  ,[LockOutEndDateUtc]
		  ,[LockoutEnabled]
		  ,[AccessFailedCount]
		  ,[UserName]
		  ,[FirstName]
		  ,[LastName]
		  ,[Users].[UtcModifiedDate] as LastModified
		  ,[Users].[ModifiedBy]
		  ,[UserRoles].RoleId
		  ,[Roles].Name 
		  ,[ClientUsers].ClientId
	  FROM [dbo].[Users]
		  LEFT OUTER JOIN [UserRoles] ON [Users].[UserId] = [UserRoles].[UserId]
		  LEFT OUTER JOIN [Roles] ON [Roles].[RoleId] = [UserRoles].[RoleId]
		  LEFT OUTER JOIN [ClientUsers] ON [Users].[UserId] = [ClientUsers].[UserId]
	  ORDER BY [Users].[UserId]
END
  
GO


