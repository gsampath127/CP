CREATE PROCEDURE [dbo].[RPV2HostedAdmin_BrowserVersionData_CacheDependencyCheck]
AS
BEGIN
   	SELECT Name,
	COUNT_BIG(*) AS Total
	FROM	dbo.BrowserVersion
	GROUP BY Name;
END

