CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportSchedule]	

	@reportscheduleid int,
	@reportid int,
	@clientid int,
	@frequencyType int,
	@frequencyInterval int,
	@UtcfirstScheduledRunDate DateTime,
	@UtcLastScheduledRunDate DateTime,
	@email nvarchar(256),
	@ftpServerIP nvarchar(100),
	@ftpFilePath nvarchar(256),
	@ftpUsername nvarchar(50),
	@ftpPassword nvarchar(50),
	@errorEmail nvarchar(500),
	@modifiedBy int,
	@IsSFTP bit,
	@WeekDays varchar(20),
	@utcDataStartDate DateTime,
	@utcDataEndDate DateTime,
	@utcScheduledEndDate DateTime
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
							FrequencyType,
							FrequencyInterval,
							UtcFirstScheduledRunDate,
							UtcLastScheduledRunDate,
							UtcModifiedDate,
							ModifiedBy,
							WeekDays,
							UtcDataStartDate,
							UtcDataEndDate,
							UtcScheduledEndDate) 

					VALUES (
							@reportid,
							@clientid,
							@frequencyType,
							@frequencyInterval,
							@UtcfirstScheduledRunDate,
							@UtcLastScheduledRunDate,
							GetUtcDate(),
							@modifiedBy,
							@WeekDays,
							@utcDataStartDate,
							@utcDataEndDate,
							@utcScheduledEndDate
							)

				SET @identityReportScheduleId = SCOPE_IDENTITY()

				INSERT INTO ReportScheduleRecipients
				(ReportScheduleId,
				Email,
				FTPServerIP,
				FTPFilePath,
			    FTPUsername,
				FTPPassword,
				UtcModifiedDate,
				ModifiedBy,
				ISSFTP,
				ErrorEmail
				)
				VALUES(
				@identityReportScheduleId,
				@email,
				@ftpServerIP,
				@ftpFilePath,
				@ftpUsername,
				@ftpPassword,
	            GetUtcDate(),
				@modifiedBy,
				@IsSFTP,
				@errorEmail
				)
		END
	ELSE
		BEGIN
			UPDATE ReportSchedule
			
			 SET 
					
					reportId = @reportid,
					ClientId = @clientid,
					FrequencyType = @frequencyType,
					FrequencyInterval = @frequencyInterval,
					UtcFirstScheduledRunDate = @UtcfirstScheduledRunDate,
					UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
					UtcModifiedDate = GetUtcDate(),
					ModifiedBy= @modifiedBy,
					WeekDays = @WeekDays,
					UtcDataStartDate = @utcDataStartDate,
					UtcDataEndDate = @utcDataEndDate,
					UtcScheduledEndDate	= @utcScheduledEndDate,
					UtcLastDataEndDate = NULL				

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
				ModifiedBy = @modifiedBy,
				IsSFTP = @IsSFTP,
				ErrorEmail=@errorEmail
				WHERE 
			        ReportScheduleId = @reportscheduleid

		END

END
GO