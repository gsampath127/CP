
-- =============================================
-- Author:		Nimmy Rose Antony
-- Create date: 14th-Oct-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ReportContent_CacheDependencyCheck]
AS
BEGIN
   	SELECT	ReportContentId, COUNT_BIG(*) AS Total
	FROM	dbo.ReportContent
	GROUP BY ReportContentId;

	SELECT   ReportContentId, COUNT_BIG(*) AS Total
	FROM     dbo.ReportContentData
	GROUP BY ReportContentId;
END