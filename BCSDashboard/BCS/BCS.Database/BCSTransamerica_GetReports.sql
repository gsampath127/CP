USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCSTransamerica_GetReports]    Script Date: 2/18/2016 11:32:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[BCSTransamerica_GetReports] 
@ReportType varchar(30),
@StartDate datetime ,
@EndDate datetime ,
@startIndex int,
@endIndex int,
@sortDirection NVARCHAR(10),
@sortColumn NVARCHAR(100),
@count int out
as
BEGIN


IF  @ReportType = 'Difference Report' 
BEGIN
Select Distinct RowRank ,CUSIP_WL , CUSIP_RP ,FundName_WL ,FundName_RP ,Class_WL , Class_RP ,DateModified ,[Status] from (


  SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL', ProsTicker.CUSIP As 'CUSIP_RP',
	   BCSTransamericaWatchListCUSIPs.FundName As 'FundName_WL', Prospectus.ProsName As 'FundName_RP',
	   BCSTransamericaWatchListCUSIPs.Class As 'Class_WL', ProsTicker.Class As 'Class_RP',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate END As 'DateModified',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN 'Added' ELSE 'Removed' END As 'Status',
	   CASE  
							WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Desc)
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc) 
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Asc) 									  
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Desc) 
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Asc) 									  
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Desc)	
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Asc) 									  
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Desc)
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Asc) 									  
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Desc)	
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Asc) 									  
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Desc)
							WHEN @sortColumn = 'Status' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN 'Added' ELSE 'Removed' END Asc) 									  
							WHEN @sortColumn = 'Status' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN 'Added' ELSE 'Removed' END  Desc)								  
							
						End 										          
							   AS RowRank
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE (InsertionDate BETWEEN @StartDate AND @EndDate) OR (DeletionDate BETWEEN @StartDate AND @EndDate)	
			) t
			WHERE rownum = 1 and (DeletionDate IS NOT NULL OR InsertionDate IS NOT NULL)
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) as Report 


WHERE RowRank>@startIndex  AND RowRank <=@endIndex
				ORDER BY RowRank
					
Select  Count(CUSIP_WL) as virtualCount from (

SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL' 	  						
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE (InsertionDate BETWEEN @StartDate AND @EndDate) OR (DeletionDate BETWEEN @StartDate AND @EndDate)	
			) t
			WHERE rownum = 1 and (DeletionDate IS NOT NULL OR InsertionDate IS NOT NULL)
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) as Report

END
ELSE IF  @ReportType = 'Added Report' 
BEGIN
Select Distinct RowRank , CUSIP_WL , CUSIP_RP ,FundName_WL ,FundName_RP ,Class_WL , Class_RP ,DateModified ,[Status] from (
      SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL', ProsTicker.CUSIP As 'CUSIP_RP',
	   BCSTransamericaWatchListCUSIPs.FundName As 'FundName_WL', Prospectus.ProsName As 'FundName_RP',
	   BCSTransamericaWatchListCUSIPs.Class As 'Class_WL', ProsTicker.Class As 'Class_RP',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate END As 'DateModified',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN 'Added' ELSE 'Removed' END As 'Status',
	   CASE                 WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Desc)
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc) 
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Asc) 									  
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Desc) 
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Asc) 									  
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Desc)	
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Asc) 									  
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Desc)
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Asc) 									  
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Desc)
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Asc) 									  
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Desc)
							End 										          
							   AS RowRank
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE (InsertionDate BETWEEN @StartDate AND @EndDate) 
			) t
			WHERE rownum = 1 and InsertionDate IS NOT NULL
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) As Report
   WHERE RowRank> @startIndex  AND RowRank<=@endIndex
				ORDER BY RowRank
					
Select  Count(CUSIP_WL) as virtualCount from (

SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL' 	  						
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE (InsertionDate BETWEEN @StartDate AND @EndDate) 
			) t
			WHERE rownum = 1 and InsertionDate IS NOT NULL
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) As Report


END
ELSE IF @ReportType = 'Removed Report'

BEGIN
Select Distinct RowRank , CUSIP_WL , CUSIP_RP ,FundName_WL ,FundName_RP ,Class_WL , Class_RP ,DateModified ,[Status] from (
     SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL', ProsTicker.CUSIP As 'CUSIP_RP',
	   BCSTransamericaWatchListCUSIPs.FundName As 'FundName_WL', Prospectus.ProsName As 'FundName_RP',
	   BCSTransamericaWatchListCUSIPs.Class As 'Class_WL', ProsTicker.Class As 'Class_RP',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate END As 'DateModified',
	   CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN 'Added' ELSE 'Removed' END As 'Status',
	   CASE                WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.CUSIP Desc)
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc) 									  
							WHEN @sortColumn = 'CUSIP_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc) 
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Asc) 									  
							WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.FundName Desc) 
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Asc) 									  
							WHEN @sortColumn = 'FundName_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Desc)	
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Asc) 									  
							WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaWatchListCUSIPs.Class Desc)
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Asc) 									  
							WHEN @sortColumn = 'Class_RP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Desc)
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Asc) 									  
							WHEN @sortColumn = 'DateModified' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN BCSTransamericaWatchListCUSIPs.InsertionDate IS NOT NULL THEN BCSTransamericaWatchListCUSIPs.InsertionDate ELSE BCSTransamericaWatchListCUSIPs.DeletionDate End Desc)
							End 										          
							   AS RowRank
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE DeletionDate BETWEEN @StartDate AND @EndDate	
			) t
			WHERE rownum = 1 and DeletionDate IS NOT NULL 
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) as Report
  WHERE RowRank>@startIndex  AND RowRank<=@endIndex
				ORDER BY RowRank

Select  Count(CUSIP_WL) as virtualCount from (

SELECT BCSTransamericaWatchListCUSIPs.CUSIP As 'CUSIP_WL' 	  						
FROM ProsTicker
INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
INNER JOIN
(
			SELECT * FROM (
				SELECT CUSIP, DeletionDate, InsertionDate, Fundname, Class, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
				FROM BCSTransamericaWatchListCUSIPsHistory
				WHERE DeletionDate BETWEEN @StartDate AND @EndDate	
			) t
			WHERE rownum = 1 and DeletionDate IS NOT NULL 
			
)BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = ProsTicker.CUSIP ) as Report
END

ELSE 

BEGIN
Select Distinct RowRank , CUSIP_WL ,FundName_WL , Class_WL   from (
  Select BCSTransamerica_FTP_RT1.clmn5 As 'CUSIP_WL', 
  BCSTransamerica_FTP_RT1.clmn6 As 'FundName_WL',
  BCSTransamerica_FTP_RT1.clmn7 As 'Class_WL',
   CASE        WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn5 Asc) 									  
			   WHEN @sortColumn = 'CUSIP_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn5 Desc)
			   WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn6 Asc) 									  
			   WHEN @sortColumn = 'FundName_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn6 Desc) 
			   WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn7 Asc) 									  
			   WHEN @sortColumn = 'Class_WL' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamerica_FTP_RT1.clmn7 Desc)
							
			END	
			  AS RowRank			

   from BCSTransamerica_FTP_RT1
  Left JOIN ProsTicker On BCSTransamerica_FTP_RT1.clmn5 = ProsTicker.CUSIP
  WHere ProsTicker.CUSIP IS NULL ) as Report

   WHERE RowRank>@startIndex  AND RowRank<= @endIndex
					ORDER BY RowRank
					
Select  Count(CUSIP_WL) as virtualCount from (

		Select BCSTransamerica_FTP_RT1.clmn5 As 'CUSIP_WL'
		from BCSTransamerica_FTP_RT1
		Left JOIN ProsTicker On BCSTransamerica_FTP_RT1.clmn5 = ProsTicker.CUSIP
		WHere ProsTicker.CUSIP IS NULL ) as Report


END   

END


