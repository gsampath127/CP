
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteTextData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteText
	GROUP BY SiteTextId;
	
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteTextVersion
	GROUP BY SiteTextId;
END