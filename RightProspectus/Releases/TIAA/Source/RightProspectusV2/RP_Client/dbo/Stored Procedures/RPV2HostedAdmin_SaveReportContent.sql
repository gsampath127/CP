/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveReportContent]
	Added By: Nimmy Rose Antony
    Date: 15 Oct 2015
	Reason : To add and update the Report Content
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportContent]	
	@ReportScheduleId int,
	@MimeType NVARCHAR(127),
	@IsPrivate BIT,
	@ContentUri NVARCHAR(2083),
	@Name NVARCHAR(100),
	@ReportRunDate DATETIME ,
	@ModifiedBy INT,
	@FileName NVARCHAR(260),
	@Description NVARCHAR(400),
    @Data VARBINARY(MAX)
AS
BEGIN
	DECLARE @identityReportContentId int
	
		
			INSERT INTO [dbo].[ReportContent]
							(
							       [ReportScheduleId]
							      ,[MimeType]
								  ,[IsPrivate]
								  ,[ContentUri]
								  ,[Name]
								  ,[ReportRunDate]
								  ,[FileName]
								  ,[Description]
								  ,[ModifiedBy]								  								 
								  ,[UtcModifiedDate]
							) 
					VALUES (
									 @ReportScheduleId
									,@MimeType
									,@IsPrivate
									,@ContentUri
									,@Name
									,@ReportRunDate
									,@FileName
									,@Description
									,@ModifiedBy
									,GETUTCDATE()
							)

     SET @identityReportContentId = SCOPE_IDENTITY()
					 INSERT INTO [dbo].[ReportContentData]
							  (
									[ReportContentId]
									,[Data]
									,[UtcModifiedDate]
									,[ModifiedBy]
							   )
							   VALUES
							  (
									@identityReportContentId
									,@Data
									,GETUTCDATE()
									,@ModifiedBy
							  )
	
	
END