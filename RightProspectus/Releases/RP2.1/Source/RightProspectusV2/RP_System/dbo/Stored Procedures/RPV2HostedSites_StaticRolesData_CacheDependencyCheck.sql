/*
	Procedure Name:[dbo].[RPV2HostedSites_StaticRolesData_CacheDependencyCheck]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : Cache Dependency Roles
*/

CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticRolesData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	RoleId, COUNT_BIG(*) AS Total
	FROM	dbo.Roles
	GROUP BY RoleId;
END