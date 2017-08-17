-- =============================================
-- Author:	Nimmy Rose Antony	
-- Create date: 15 Oct 2015
-- EXEC RPV2HostedAdmin_GetAllReportContent
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReportContent]
AS
BEGIN

SELECT  DISTINCT

	 ReportContentId
	, ReportScheduleId
	, MimeType
	, IsPrivate
	, ContentUri
	, Name
	, ReportRunDate
	, UtcModifiedDate as ModifiedDate
	, ModifiedBy
	, FileName
	, Description
	
FROM 
		ReportContent
END