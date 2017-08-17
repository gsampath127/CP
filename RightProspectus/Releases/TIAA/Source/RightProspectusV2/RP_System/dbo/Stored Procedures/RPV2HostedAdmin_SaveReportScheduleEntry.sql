CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportScheduleEntry]
(
	@ReportScheduleId INT,
	@Status INT,
	@MaxProcessAttempts INT = 2,
	@UtcLastScheduledRunDate DATETIME
)
AS
BEGIN
	IF(@Status = 2)
	BEGIN
		UPDATE ReportSchedule
		SET 
			Status = @Status,
			UtcLastDataEndDate = UtcNextDataEndDate,
			UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
			ServiceName = '',
			ExecutionCount = 0
		WHERE ReportScheduleId = @ReportScheduleId

	END
	ELSE IF(@Status = 3)
	BEGIN
		IF EXISTS(SELECT * FROM [ReportSchedule] WHERE [Status] = 1 AND ExecutionCount >= @MaxProcessAttempts AND ReportScheduleId = @ReportScheduleId)
		BEGIN
			-- Mark the schedule as failed ---
			UPDATE ReportSchedule
			SET 
				[Status] = 3,
				UtcLastDataEndDate = UtcNextDataEndDate,
				UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
				ServiceName = '',
				ExecutionCount = 0
			WHERE ReportScheduleId = @ReportScheduleId
		END
		ELSE
		BEGIN
			-- Reset any remaining records and increment ProcessAttempts by 1
			UPDATE ReportSchedule
			SET [Status] = 0, ServiceName = NULL
			WHERE ReportScheduleId = @ReportScheduleId
		END
	END
	ELSE
	BEGIN
		UPDATE ReportSchedule
		SET 
			Status = @Status,
			UtcLastDataEndDate = UtcNextDataEndDate,
			UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
			ServiceName = ''		
		WHERE ReportScheduleId = @ReportScheduleId
	END
END
GO

