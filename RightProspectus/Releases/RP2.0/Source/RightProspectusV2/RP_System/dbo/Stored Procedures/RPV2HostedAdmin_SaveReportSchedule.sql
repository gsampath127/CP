CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportSchedule]	

	@reportscheduleid int,
	@reportid int,
	@clientid int,
	@isenabled bit,
	@frequencyType int,
	@frequencyInterval int,
	@UtcfirstScheduledRunDate DateTime,
	@UtcLastScheduledRunDate DateTime,
	@email nvarchar(256),
	@ftpServerIP nvarchar(100),
	@ftpFilePath nvarchar(256),
	@ftpUsername nvarchar(50),
	@ftpPassword nvarchar(50),
	@modifiedBy int
AS
BEGIN
DECLARE @identityReportScheduleId int
	
	IF(@reportscheduleid=0) 
		BEGIN
			INSERT
			     INTO
			 ReportSchedule
			               (
							reportId,
							ClientId,
							IsEnabled,
							FrequencyType,
							FrequencyInterval,
							UtcFirstScheduledRunDate,
							UtcLastScheduledRunDate,
							UtcModifiedDate,
							ModifiedBy) 

					VALUES (
							@reportid,
							@clientid,
							@isenabled,
							@frequencyType,
							@frequencyInterval,
							@UtcfirstScheduledRunDate,
							@UtcLastScheduledRunDate,
							GetUtcDate(),
							@modifiedBy)

				SET @identityReportScheduleId = SCOPE_IDENTITY()

				INSERT INTO ReportScheduleRecipients
				(ReportScheduleId,
				Email,
				FTPServerIP,
				FTPFilePath,
			    FTPUsername,
				FTPPassword,
				UtcModifiedDate,
				ModifiedBy
				)
				VALUES(
				@identityReportScheduleId,
				@email,
				@ftpServerIP,
				@ftpFilePath,
				@ftpUsername,
				@ftpPassword,
	            GetUtcDate(),
				@modifiedBy
				)
		END
	ELSE
		BEGIN
			UPDATE ReportSchedule
			
			 SET 
					
					reportId = @reportid,
					ClientId = @clientid,
					IsEnabled = @isenabled,
					FrequencyType = @frequencyType,
					FrequencyInterval = @frequencyInterval,
					UtcFirstScheduledRunDate = @UtcfirstScheduledRunDate,
					UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
					UtcModifiedDate = GetUtcDate(),
					ModifiedBy= @modifiedBy

			WHERE 
			        reportScheduleId = @reportscheduleid

		    UPDATE ReportScheduleRecipients
			
			 SET
			    Email=@email,
				FTPServerIP=@ftpServerIP,
				FTPFilePath=@ftpFilePath,
			    FTPUsername=@ftpUsername,
				FTPPassword=@ftpPassword,
				UtcModifiedDate=GetUtcDate(),
				ModifiedBy = @modifiedBy
				WHERE 
			        ReportScheduleId = @reportscheduleid

		END

END
GO

