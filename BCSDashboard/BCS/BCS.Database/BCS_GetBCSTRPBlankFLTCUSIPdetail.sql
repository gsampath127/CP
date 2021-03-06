USE [db1029]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Alter PROCEDURE [dbo].[BCS_GetBCSTRPBlankFLTCUSIPdetail] --exec [BCS_GetBCSTRPBlankFLTCUSIPdetail] 0,10
@StartIndex INT,
@EndIndex INT
	
AS
BEGIN

Select Count(FundCode) as TotalBlankFLTCUSIPDetailCount 
from BCSTRPFLT 
WHERE ISNULL(FUNDCUSIPNUMBER, '') = ''

DECLARE @Details Table(FUNDCODE nvarchar(100),FUNDNAME nvarchar(100),FUNDTYPE nvarchar(100),FUNDTELEACCESSCODE nvarchar(100),
						FUNDCUSIPNUMBER nvarchar(100),FUNDCHKHEADINGCODE nvarchar(100),FUNDGROUPNUMBER nvarchar(100),FUNDPROSPECTUSINSERT nvarchar(100),
						FUNDPROSPECTUSINSERT2 nvarchar(100),FUNDTICKERSYMBOL nvarchar(100),FUNDDocName nvarchar(100),DateFLTRecordHasChanged datetime)

INSERT INTO @Details(FUNDCODE,FUNDNAME,FUNDTYPE,FUNDTELEACCESSCODE,FUNDCUSIPNUMBER,FUNDCHKHEADINGCODE,FUNDGROUPNUMBER,FUNDPROSPECTUSINSERT,FUNDPROSPECTUSINSERT2,FUNDTICKERSYMBOL,FUNDDocName,DateFLTRecordHasChanged)	
	
	SELECT 
		FUNDCODE,		
		FUNDNAME,
		FUNDTYPE,
		FUNDTELEACCESSCODE,
		FUNDCUSIPNUMBER,
		FUNDCHKHEADINGCODE,
		FUNDGROUPNUMBER,
		FUNDPROSPECTUSINSERT,
		FUNDPROSPECTUSINSERT2,
		FUNDTICKERSYMBOL,
		FUNDDocName,
		DateFLTRecordHasChanged
	FROM
		(
			SELECT 
					FUNDCODE,		
					FUNDNAME,
					FUNDTYPE,
					FUNDTELEACCESSCODE,
					FUNDCUSIPNUMBER,
					FUNDCHKHEADINGCODE,
					FUNDGROUPNUMBER,
					FUNDPROSPECTUSINSERT,
					FUNDPROSPECTUSINSERT2,
					FUNDTICKERSYMBOL,
					FUNDDocName,
					DateFLTRecordHasChanged,
					ROW_NUMBER() OVER(ORDER By BCSTRPFLT.FUNDCODE) AS RowNumber
			FROM BCSTRPFLT 
			Where  ISNULL(FUNDCUSIPNUMBER, '') = ''
		)as B

	WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex

	SELECT *
	FROM @Details

	
END
GO
