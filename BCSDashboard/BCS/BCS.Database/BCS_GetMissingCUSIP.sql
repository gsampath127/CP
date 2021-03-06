USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetMissingCUSIP]    Script Date: 12/12/2016 3:44:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BCS_GetMissingCUSIP]
@Company nvarchar(20),
@startIndex INT ,
@endIndex INT ,
@sortColumn nvarchar(20),
@sortDirection nvarchar(10)

AS
BEGIN


DECLARE @TRP Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10),LIPPERCUSIP nvarchar(10),EOnlineCUSIP nvarchar(10))
DECLARE @AllianceBernstein Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10),LIPPERCUSIP nvarchar(10),EOnlineCUSIP nvarchar(10))
DECLARE @Transamerica Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10),LIPPERCUSIP nvarchar(10),EOnlineCUSIP nvarchar(10))
DECLARE @Temp Table(CompanyName nvarchar(100), FundName nvarchar(100), FLTCUSIP nvarchar(10),LIPPERCUSIP nvarchar(10),EOnlineCUSIP nvarchar(10) , RowNum INT)


INSERT INTO @TRP
  SELECT 'T Rower Price' CompanyName,BCSTRPFLT.FUNDTYPE + ' - ' + BCSTRPFLT.FUNDNAME AS 'FundName', BCSTRPFLT.FUNDCUSIPNUMBER AS 'FLTCUSIP',
        
		CASE 
		WHEN TickerCUSIP.CUSIP IS NOT NULL THEN TickerCUSIP.CUSIP ELSE VACUSIP.CUSIP 
		END AS 'LIPPERCUSIP',EdgarOnlineFeed.ECUSIP AS 'EOnlineCUSIP'
		--,ROW_NUMBER() OVER(ORDER By BCSTRPFLT.DatePDFReceivedonFTP) AS 'RowNumber'
		FROM BCSTRPFLT	
		LEFT JOIN ProsTicker ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'   
		LEFT JOIN TickerCUSIP ON TickerCUSIP.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%' 
		LEFT JOIN VACUSIP ON VACUSIP.CUSIP  like BCSTRPFLT.FUNDCUSIPNUMBER + '%' 
		LEFT JOIN EdgarOnlineFeed ON EdgarOnlineFeed.ECUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'   
		WHERE ProsTicker.CUSIP IS NULL

INSERT INTO @AllianceBernstein  
  Select 'AllianceBernstein' As CompanyName ,  BCSAllianceBernstein_FTP_RT1.clmn6 As 'FundName',BCSAllianceBernstein_FTP_RT1.clmn5 As 'FLTCUSIP',
     CASE 
		WHEN TickerCUSIP.CUSIP IS NOT NULL THEN TickerCUSIP.CUSIP ELSE VACUSIP.CUSIP 
		END AS 'LIPPERCUSIP',EdgarOnlineFeed.ECUSIP AS 'EOnlineCUSIP'		
FROM BCSAllianceBernstein_FTP_RT1
Left JOIN ProsTicker On BCSAllianceBernstein_FTP_RT1.clmn5 = ProsTicker.CUSIP
LEFT JOIN TickerCUSIP ON TickerCUSIP.CUSIP like BCSAllianceBernstein_FTP_RT1.clmn5 + '%' 
LEFT JOIN VACUSIP ON VACUSIP.CUSIP  like BCSAllianceBernstein_FTP_RT1.clmn5+ '%' 
LEFT JOIN EdgarOnlineFeed ON EdgarOnlineFeed.ECUSIP like BCSAllianceBernstein_FTP_RT1.clmn5 + '%'   
WHere ProsTicker.CUSIP IS NULL 

INSERT INTO @Transamerica 
 Select 'Transamerica' As CompanyName ,
 BCSTransamerica_FTP_RT1.clmn6 As 'FundName',BCSTransamerica_FTP_RT1.clmn5 As 'FLTCUSIP', 
 
  CASE 
		WHEN TickerCUSIP.CUSIP IS NOT NULL THEN TickerCUSIP.CUSIP ELSE VACUSIP.CUSIP 
		END AS 'LIPPERCUSIP',EdgarOnlineFeed.ECUSIP AS 'EOnlineCUSIP'	
 	

FROM BCSTransamerica_FTP_RT1
LEFT JOIN ProsTicker On BCSTransamerica_FTP_RT1.clmn5 = ProsTicker.CUSIP
LEFT JOIN TickerCUSIP ON TickerCUSIP.CUSIP like BCSTransamerica_FTP_RT1.clmn5 + '%' 
LEFT JOIN VACUSIP ON VACUSIP.CUSIP  like BCSTransamerica_FTP_RT1.clmn5+ '%' 
LEFT JOIN EdgarOnlineFeed ON EdgarOnlineFeed.ECUSIP like BCSTransamerica_FTP_RT1.clmn5 + '%'
WHERE ProsTicker.CUSIP IS NULL

IF @Company = 'TRP'

BEGIN
DELETE FROM @Temp
INSERT INTO @Temp
SELECT * , CASE  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc) 									  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc) 									  
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc) 
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Asc) 									  
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Desc) 
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Asc) 									  
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Desc)	
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Asc) 									  
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Desc)
    END AS RowNum  FROM @TRP t
	

	SELECT * FROM @Temp temp WHERE RowNum Between @startIndex and @endIndex
	SELECT count(*) as virtualCount FROM @TRP c
END

ELSE IF  @Company ='AllianceBernstein'
BEGIN
DELETE FROM @Temp
INSERT INTO @Temp
SELECT *, CASE  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc) 									  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc) 									  
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc) 
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Asc) 									  
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Desc) 
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Asc) 									  
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Desc)	
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Asc) 									  
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Desc)
    END AS RowNum  FROM @AllianceBernstein t

	SELECT * FROM @Temp temp WHERE RowNum Between @startIndex and @endIndex
	
	SELECT  Count(*) as virtualCount FROM @AllianceBernstein c

END

ELSE IF @Company ='Transamerica'
BEGIN
DELETE FROM @Temp
INSERT INTO @Temp
SELECT *, CASE  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc) 									  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc) 									  
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc) 
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Asc) 									  
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Desc) 
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Asc) 									  
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Desc)	
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Asc) 									  
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Desc)
    END AS RowNum  FROM @Transamerica t

	SELECT * FROM @Temp temp WHERE RowNum Between @startIndex and @endIndex
	
	SELECT Count(*) as virtualCount FROM @Transamerica c
END

ELSE
BEGIN

INSERT INTO @Temp
SELECT * ,CASE  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc) 									  
			WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc) 									  
			WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc) 
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Asc) 									  
			WHEN @sortColumn = 'FLTCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FLTCUSIP Desc) 
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Asc) 									  
			WHEN @sortColumn = 'LIPPERCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY LIPPERCUSIP Desc)	
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Asc) 									  
			WHEN @sortColumn = 'EOnlineCUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EOnlineCUSIP Desc)
    END AS RowNum  FROM(
		   SELECT * FROM @TRP t
		   UNION
		   SELECT * FROM @AllianceBernstein a
		   UNION
		   SELECT * FROM @Transamerica ta
   ) as p 


SELECT *   FROM @Temp
WHERE RowNum Between @startIndex AND @endIndex

SELECT Count(*) as virtualCount from @Temp p




END

END




   