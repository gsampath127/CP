/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteReportContent]
	Added By: Nimmy Rose Antony
	Date: 15 Oct 2015
	Reason : To delete the Report Content
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteReportContent]
@ReportContentId int,
@DeletedBy int
AS
BEGIN

	DELETE [dbo].[ReportContent]
      WHERE ReportContentId = @ReportContentId;

	DELETE [dbo].[ReportContentData]
	  WHERE [ReportContentId] = @ReportContentId;
            
	UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ReportContent'
		AND	[Key] = @ReportContentId AND CUDType = 'D';

    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ReportContentData'
		AND	[Key] = @ReportContentId AND CUDType = 'D';
  
END