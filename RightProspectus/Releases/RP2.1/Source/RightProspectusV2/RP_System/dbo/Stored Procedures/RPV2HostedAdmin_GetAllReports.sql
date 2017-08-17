CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReports]
AS
BEGIN
  SELECT 
	ReportId,
	ReportName,
	ReportDescription,
	ModifiedBy=0,
	UtcLastModified = GetUTCDATE()
  FROM Reports
END