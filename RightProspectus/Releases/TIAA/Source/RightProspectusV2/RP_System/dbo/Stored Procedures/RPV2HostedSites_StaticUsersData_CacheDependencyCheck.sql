-- =============================================
-- Author:		Noel
-- Create date: 18-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticUsersData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	UserId, COUNT_BIG(*) AS Total
	FROM	dbo.[Users]
	GROUP BY UserId;

	SELECT	ClientId, COUNT_BIG(*) AS Total
	FROM	dbo.ClientUsers
	GROUP BY ClientId;

END