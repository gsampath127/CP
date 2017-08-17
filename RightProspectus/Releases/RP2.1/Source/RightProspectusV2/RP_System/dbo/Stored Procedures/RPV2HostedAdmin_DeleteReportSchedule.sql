CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteReportSchedule]
@ReportScheduleID int,  
@DeletedBy int  
AS  
BEGIN  
  
	DELETE ReportScheduleRecipients
	WHERE ReportScheduleId = @ReportScheduleID;  
  
	DELETE ReportSchedule  
	WHERE ReportScheduleID = @ReportScheduleID;  
  
  
  
	UPDATE CUDHistory   
	SET  UserId = @DeletedBy    
	WHERE TableName = N'ReportSchedule'    
	AND [Key] = @ReportScheduleID AND CUDType = 'D';    
  
  
	UPDATE CUDHistory    
	SET  UserId = @DeletedBy    
	WHERE TableName = N'ReportScheduleRecipients'    
	AND [Key] = @ReportScheduleID AND CUDType = 'D';
    
  
END