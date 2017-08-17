USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetBCSTRPReportFLTFTPInfoData]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetBCSTRPReportFLTFTPInfoData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetBCSTRPReportFLTFTPInfoData]
GO

CREATE PROCEDURE [dbo].[BCS_GetBCSTRPReportFLTFTPInfoData]
@CUSIP Varchar(10),
@IsPDFReceived BIT,
@StartIndex INT,
@EndIndex INT
AS
BEGIN

	DECLARE @Details Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10), RPCUSIP nvarchar(10), DatePDFReceivedOnFTP date)	
	
	DECLARE @IsPDFReceivedBCSTRPFLTID Table(BCSTRPFLTID INT)
	
	IF @IsPDFReceived = 0
	BEGIN
		INSERT INTO @IsPDFReceivedBCSTRPFLTID(BCSTRPFLTID)
		SELECT BCSTRPFLTID FROM BCSTRPFLT WHERE DatePDFReceivedonFTP IS NULL
	END
	ELSE IF @IsPDFReceived = 1
	BEGIN
		INSERT INTO @IsPDFReceivedBCSTRPFLTID(BCSTRPFLTID)
		SELECT BCSTRPFLTID FROM BCSTRPFLT WHERE DatePDFReceivedonFTP IS NOT NULL
	END
	
	--Find	FLTFTPInfoDataTotalCount	
	
	SELECT Count(BCSTRPFLT.BCSTRPFLTID) AS 'FLTFTPInfoDataTotalCount'
	FROM ProsTicker
	INNER JOIN BCSTRPFLT ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
	INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
	INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
	WHERE (@CUSIP IS NULL OR FUNDCUSIPNUMBER = @CUSIP OR ProsTicker.CUSIP = @CUSIP) AND (@IsPDFReceived IS NULL OR BCSTRPFLT.BCSTRPFLTID IN (SELECT BCSTRPFLTID FROM @IsPDFReceivedBCSTRPFLTID))
	AND ISNULL(BCSTRPFLT.FUNDCUSIPNUMBER, '') != ''
	
	--Fetch All FLTFTPInfoData details and insert into @@Details according to page size.
	INSERT INTO @Details(CompanyName, FundName, FLTCUSIP, RPCUSIP, DatePDFReceivedOnFTP)	
	SELECT CompanyName, ProsName, FUNDCUSIPNUMBER, CUSIP , DatePDFReceivedonFTP
	FROM
	(
		SELECT Company.CompanyName, BCSTRPFLT.FUNDTYPE + ' - ' + BCSTRPFLT.FUNDNAME as 'ProsName', BCSTRPFLT.FUNDCUSIPNUMBER, ProsTicker.CUSIP, BCSTRPFLT.DatePDFReceivedonFTP
		,ROW_NUMBER() OVER(ORDER By BCSTRPFLT.DatePDFReceivedonFTP) AS 'RowNumber'
		FROM ProsTicker
		INNER JOIN BCSTRPFLT ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
		INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
		INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
		WHERE (@CUSIP IS NULL OR FUNDCUSIPNUMBER = @CUSIP OR ProsTicker.CUSIP = @CUSIP) AND (@IsPDFReceived IS NULL OR BCSTRPFLT.BCSTRPFLTID IN (SELECT BCSTRPFLTID FROM @IsPDFReceivedBCSTRPFLTID))
		AND ISNULL(BCSTRPFLT.FUNDCUSIPNUMBER, '') != ''
	)AS tblCUSIPDetails
	WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex
	
	
	SELECT CompanyName, FundName, FLTCUSIP, RPCUSIP, DatePDFReceivedonFTP
	FROM @Details
	
End
GO

