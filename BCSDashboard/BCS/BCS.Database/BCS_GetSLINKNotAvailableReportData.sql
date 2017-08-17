USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetSLINKNotAvailableReportData]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetSLINKNotAvailableReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetSLINKNotAvailableReportData]
GO

CREATE PROCEDURE [dbo].[BCS_GetSLINKNotAvailableReportData]
AS
BEGIN

	SELECT BCSDocUpdateId, EdgarID, CUSIP, FundName, ProcessedDate
	FROM BCSDocUpdate
	WHERE IsRemoved = 0 and  IsProcessed = 1 and BCSDocUpdateId NOT IN (SELECT DocUpdateId FROM BCSDocUpdateGIMSlink)

End
GO

