
CREATE PROCEDURE [dbo].[BCS_GetMissingReports]
@ReportType NVARCHAR(30),
@startIndex INT,
@endIndex INT,
@sortDirection NVARCHAR(10),
@sortColumn NVARCHAR(100),
@count INT OUT
AS
BEGIN
	IF @ReportType = 'CUSIP'
	BEGIN
		SELECT * FROM 
		(
			SELECT DISTINCT 
				EdgarOnlineFeed.ECUSIP AS 'CUSIP',
				EdgarOnlineFeed.ECompanyName AS 'CompanyName',
				EdgarOnlineFeed.EFundName AS 'FundName',
				EdgarOnlineFeed.ECIK AS 'CIK',
				EdgarOnlineFeed.ESeriesID AS 'SeriesID',
				EdgarOnlineFeed.EClassContractID AS 'ClassContractID',
				EdgarOnlineFeed.Eticker AS 'TickerSymbol',
				CASE        
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ECUSIP Asc) 									  
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ECUSIP Desc)
					WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ECompanyName Asc) 									  
					WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ECompanyName Desc) 
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EFundName Asc) 									  
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EFundName Desc)
					WHEN @sortColumn = 'CIK' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ECIK Asc) 									  
					WHEN @sortColumn = 'CIK' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ECIK Desc)
					WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ESeriesID Asc) 									  
					WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ESeriesID Desc)
					WHEN @sortColumn = 'ClassContractID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY EClassContractID Asc) 									  
					WHEN @sortColumn = 'ClassContractID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY EClassContractID Desc)
					WHEN @sortColumn = 'Ticker' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Eticker Asc) 									  
					WHEN @sortColumn = 'Ticker' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Eticker Desc)
				END	AS RowRank
			FROM EdgarOnlineFeed
			LEFT JOIN 
			(
				SELECT ProsTicker.* from ProsTicker 
				INNER JOIN BCSDocUpdate ON BCSDocUpdate.CUSIP = ProsTicker.CUSIP
			)PT ON EdgarOnlineFeed.EClassContractID = PT.ClassContractID
			WHERE PT.ClassContractID IS NULL
		) AS Report
		WHERE RowRank > @startIndex  AND RowRank <= @endIndex
		ORDER BY RowRank	
				
	
		SELECT Count(*) AS virtualCount from 
		(
			SELECT DISTINCT ECUSIP,ECompanyName,EFundName, ECIK,ESeriesID,EClassContractID, Eticker
			FROM EdgarOnlineFeed
			LEFT JOIN ProsTicker ON EdgarOnlineFeed.EClassContractID = ProsTicker.ClassContractID
			LEFT JOIN BCSDocUpdate ON BCSDocUpdate.CUSIP = ProsTicker.CUSIP
			WHERE ProsTicker.ClassContractID IS NULL			
		)t
		
	END

	ELSE IF @ReportType = 'Security Types'
	BEGIN
		SELECT * FROM
			(SELECT DISTINCT ProsTicker.CUSIP, CompanyName, FundName, CIK, SeriesID, ClassContractID, TickerSymbol,
				CASE        
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc) 									  
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc)
					WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc) 									  
					WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc) 
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc) 									  
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc)
					WHEN @sortColumn = 'CIK' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY CIK Asc) 									  
					WHEN @sortColumn = 'CIK' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY CIK Desc)
					WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY SeriesID Asc) 									  
					WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY SeriesID Desc)
					WHEN @sortColumn = 'ClassContractID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY ClassContractID Asc) 									  
					WHEN @sortColumn = 'ClassContractID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY ClassContractID Desc)
					WHEN @sortColumn = 'Ticker' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY TickerSymbol Asc) 									  
					WHEN @sortColumn = 'Ticker' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY TickerSymbol Desc)
				END	
			AS RowRank
			FROM ProsTicker
			INNER JOIN BCSDocUpdate ON BCSDocUpdate.CUSIP = ProsTicker.CUSIP
			INNER JOIN Prospectus ON Prospectus.ProsId=ProsTicker.ProspectusID
			INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
			WHERE SecurityTypeID IS NULL AND IsRemoved = 0 AND
			ISNULL(ProsTicker.CUSIP, '') <> ''			
			) AS SecurityTypeReport
			WHERE RowRank > @startIndex  AND RowRank <= @endIndex
			ORDER BY RowRank

		SELECT COUNT(*) AS virtualCount from
		(
			SELECT DISTINCT ProsTicker.CUSIP, CompanyName, FundName, CIK, SeriesID, ClassContractID, TickerSymbol
			FROM ProsTicker
			INNER JOIN BCSDocUpdate ON BCSDocUpdate.CUSIP = ProsTicker.CUSIP
			INNER JOIN Prospectus ON Prospectus.ProsId=ProsTicker.ProspectusID
			INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
			WHERE SecurityTypeID IS NULL AND IsRemoved = 0 AND
			ISNULL(ProsTicker.CUSIP, '') <> ''
		)t
	END
END

GO

