USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetBCSTRPReportRPMissingCUSIPData]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetBCSTRPReportRPMissingCUSIPData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetBCSTRPReportRPMissingCUSIPData]
GO

CREATE PROCEDURE [dbo].[BCS_GetBCSTRPReportRPMissingCUSIPData]
@StartIndex INT,
@EndIndex INT
AS
BEGIN

	DECLARE @Details Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10))
		
	--Find	RPMissingCUSIPDataTotalCount	
	
	SELECT Count(BCSTRPFLT.BCSTRPFLTID) AS 'RPMissingCUSIPDataTotalCount'
	FROM BCSTRPFLT	
	LEFT JOIN ProsTicker ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
	WHERE ProsTicker.CUSIP IS NULL
	
	--Fetch All FLTFTPInfoData details and insert into @Details according to page size.
	INSERT INTO @Details(CompanyName, FundName, FLTCUSIP)	
	SELECT 'T. Rowe Price', FUNDNAME, FUNDCUSIPNUMBER
	FROM
	(
		SELECT BCSTRPFLT.FUNDTYPE + ' - ' + BCSTRPFLT.FUNDNAME AS 'FUNDNAME', BCSTRPFLT.FUNDCUSIPNUMBER
		,ROW_NUMBER() OVER(ORDER By BCSTRPFLT.DatePDFReceivedonFTP) AS 'RowNumber'
		FROM BCSTRPFLT	
		LEFT JOIN ProsTicker ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
		WHERE ProsTicker.CUSIP IS NULL
	)AS tblCUSIPDetails
	WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex
	
	
	SELECT CompanyName, t.FundName, t.FLTCUSIP,
	CASE 
	WHEN TickerCUSIP.CUSIP IS NOT NULL THEN TickerCUSIP.CUSIP ELSE VACUSIP.CUSIP 
	END AS 'LIPPERCUSIP',EdgarOnlineFeed.ECUSIP AS 'EOnlineCUSIP'
	FROM @Details t
	LEFT JOIN TickerCUSIP ON TickerCUSIP.CUSIP like t.FLTCUSIP + '%' 
	LEFT JOIN VACUSIP ON VACUSIP.CUSIP  like t.FLTCUSIP + '%' 
	LEFT JOIN EdgarOnlineFeed ON EdgarOnlineFeed.ECUSIP like t.FLTCUSIP + '%'
	
End
GO

