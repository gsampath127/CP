
-- =============================================
-- Author:		Arshdeep
-- Create date: 16-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteId, COUNT_BIG(*) AS Total
	FROM	dbo.[Site]
	GROUP BY SiteId;
END