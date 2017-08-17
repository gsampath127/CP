CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ProcessResetReportSchedule]
	@ReportScheduleId INT,
	@MaxProcessAttempts INT = 2
AS

IF EXISTS(SELECT 1 FROM [ReportSchedule] WHERE [Status] = 1 AND ExecutionCount >= @MaxProcessAttempts AND ReportScheduleId = @ReportScheduleId)
BEGIN
	
	-- Mark the schedule as failed ---
	UPDATE ReportSchedule
	SET [Status] = 3
	WHERE ReportScheduleId = @ReportScheduleId
END
ELSE
BEGIN
	-- Reset any remaining records and increment ProcessAttempts by 1
	UPDATE ReportSchedule
	SET [Status] = 0, ServiceName = NULL, ExecutionCount = ExecutionCount + 1
	WHERE ReportScheduleId = @ReportScheduleId
END
GO

