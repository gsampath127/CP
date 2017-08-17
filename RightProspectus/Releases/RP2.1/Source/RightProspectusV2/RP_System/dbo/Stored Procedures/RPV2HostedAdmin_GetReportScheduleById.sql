CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetReportScheduleById]
@ReportScheduleId int
AS
BEGIN  

	SELECT DISTINCT
		ReportSchedule.ReportScheduleId,  
		ReportSchedule.ReportId,  
		Reports.ReportName,  
		ReportSchedule.ClientId,  
		ReportSchedule.IsEnabled,  
		ReportSchedule.[Status],  
		ReportSchedule.ServiceName,  
		ReportSchedule.FrequencyType,  
		ReportSchedule.FrequencyInterval,  
		ReportSchedule.UtcFirstScheduledRunDate,  
		ReportSchedule.UtcNextScheduledRunDate,  
		ReportSchedule.UtcLastScheduledRunDate,  
		ReportSchedule.UtcLastActualRunDate,  
		ReportSchedule.FrequencyDescription,  
		ReportSchedule.ModifiedBy,  
		ReportSchedule.UtcModifiedDate AS UtcLastModified,  
		ReportScheduleRecipients.Email,  
		ReportScheduleRecipients.FTPServerIP,  
		ReportScheduleRecipients.FTPFilePath,  
		ReportScheduleRecipients.FTPUsername,  
		ReportScheduleRecipients.FTPPassword,
		ReportScheduleRecipients.IsSFTP
  
	FROM ReportSchedule  
	INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId  
	LEFT OUTER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId  
	WHERE ReportSchedule.ReportScheduleId = @ReportScheduleId
   
    
END