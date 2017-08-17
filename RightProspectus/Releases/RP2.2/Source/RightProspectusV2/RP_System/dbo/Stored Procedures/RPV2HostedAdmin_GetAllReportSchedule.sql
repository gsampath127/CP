
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReportSchedule]
@ClientID int
AS
BEGIN
  SELECT DISTINCT	
	Reports.ReportName,	
	ReportSchedule.IsEnabled,	
	ReportSchedule.FrequencyType,
	ReportSchedule.FrequencyInterval,	
	ReportSchedule.FrequencyDescription,
	ReportSchedule.UtcFirstScheduledRunDate,
	ReportSchedule.UtcLastScheduledRunDate,
	ReportSchedule.UtcScheduledEndDate,
	ReportSchedule.UtcLastActualRunDate,
	ReportSchedule.UtcNextScheduledRunDate,
	ReportSchedule.ModifiedBy,  
	ReportSchedule.UtcModifiedDate AS UtcLastModified
  FROM ReportSchedule
  INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
  INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
  Where ReportSchedule.ClientId = @ClientID
END

