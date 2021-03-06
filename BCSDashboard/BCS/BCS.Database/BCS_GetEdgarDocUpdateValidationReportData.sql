USE [db1029]
GO

ALTER PROCEDURE [dbo].[BCS_GetEdgarDocUpdateValidationReportData]
AS
BEGIN
	
	-- 1. GetEdgarDocUpdateValidationReportData
	
	
	select BCSDocUpdate.BCSDocUpdateId,CUSIP,BCSDocUpdate.EdgarID AS 'BCSDocUpdate - EdgarID',
       BCSURLDownloadQueue.EdgarID AS 'BCSURLDownloadQueue - EdgarID', BCSURLDownloadQueue.ProsID
	from BCSDocUpdate 
	inner join BCSURLDownloadQueue on BCSDocUpdate.ProsID = BCSURLDownloadQueue.ProsID and IsDequeued = 1
	inner join Edgar on BCSURLDownloadQueue.EdgarID = Edgar.EdgarID
	where BCSDocUpdate.DocumentDate < Edgar.DocumentDate and IsRemoved = 0 
	and Edgar.DocumentType not like '%Annual%'
	and Edgar.DocumentType not like '%Quarterly%'

End