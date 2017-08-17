-- =============================================
-- Author:		Noel
-- Create date: 19-Sep-2015
-- RPV2HostedAdmin_StaticResourceData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticResourceData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	StaticResourceId, COUNT_BIG(*) AS Total
	FROM	dbo.StaticResource
	GROUP BY StaticResourceId;
END