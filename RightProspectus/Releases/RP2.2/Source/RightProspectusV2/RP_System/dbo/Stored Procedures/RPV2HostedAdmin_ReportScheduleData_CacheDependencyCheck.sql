CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ReportScheduleData_CacheDependencyCheck]
AS
BEGIN
	SELECT	ReportScheduleId, COUNT_BIG(*) AS Total
	FROM	dbo.ReportSchedule
	GROUP BY ReportScheduleId;

	SELECT	ReportId, COUNT_BIG(*) AS Total
	FROM	dbo.Reports
	GROUP BY ReportId;
	
END