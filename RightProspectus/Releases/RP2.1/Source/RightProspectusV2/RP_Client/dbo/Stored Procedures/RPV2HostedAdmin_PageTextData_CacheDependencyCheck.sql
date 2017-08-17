CREATE PROCEDURE [dbo].[RPV2HostedAdmin_PageTextData_CacheDependencyCheck]
AS
BEGIN
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageText
	GROUP BY PageTextId;
	
   	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageTextVersion
	GROUP BY PageTextId;
END