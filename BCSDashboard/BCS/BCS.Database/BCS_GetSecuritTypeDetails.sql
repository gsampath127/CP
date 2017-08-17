
CREATE PROCEDURE [dbo].[BCS_GetSecuritTypeDetails]
@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@CompanyID INT = NULL,
@SecurityTypeID INT = NULL,
@sortDirection NVARCHAR(10),
@sortColumn    NVARCHAR(100),
@startIndex INT,
@endIndex INT
AS
BEGIN



DECLARE @TaxonomyMarketIDCount INT = 0
SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t



  SELECT distinct RowNum,* FROM(

	SELECT 

	CASE
		WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc)
		WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc) 
		WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc)
		WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
		WHEN @sortColumn = 'CompanyCIK' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CIK Asc)
		WHEN @sortColumn = 'CompanyCIK' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CIK Desc)
		WHEN @sortColumn = 'FundName' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY FundName Asc)
		WHEN @sortColumn = 'FundName' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY FundName Desc)
		WHEN @sortColumn = 'ShareClass' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Asc)
		WHEN @sortColumn = 'ShareClass' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Desc)
		WHEN @sortColumn = 'Ticker' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.TickerSymbol Asc)
		WHEN @sortColumn = 'Ticker' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.TickerSymbol Desc)
		WHEN @sortColumn = 'SecurityType' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY SecurityType.SecurityTypeCode Asc)
		WHEN @sortColumn = 'SecurityType' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY SecurityType.SecurityTypeCode Desc)
		WHEN @sortColumn = 'Loadtype' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN SecurityTypeFeedSource.SourceName IS NULL THEN '' ELSE SecurityTypeFeedSource.SourceName END  Asc)
	    WHEN @sortColumn = 'Loadtype' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY CASE WHEN SecurityTypeFeedSource.SourceName IS NULL THEN '' ELSE SecurityTypeFeedSource.SourceName END Desc)

   END AS RowNum,
	ProsTicker.CUSIP, CompanyName, CIK 'CompanyCIK', BCSDocUpdate.FundName,Class 'ShareClass', TickerSymbol 'Ticker',
	ISNULL(SecurityTypeCode, 'NA') 'SecurityType', ISNULL(SourceName, '') 'Loadtype'

	FROM ProsTicker
	INNER JOIN BCSDocUpdate ON ProsTicker.CUSIP = BCSDocUpdate.CUSIP
	INNER JOIN Prospectus On Prospectus.ProsId = ProsTicker.ProspectusID
	INNER JOIN Company On Company.CompanyId = Prospectus.CompanyID
	INNER JOIN SecurityType ON ProsTicker.SecurityTypeID = SecurityType.SecurityTypeID
	INNER JOIN SecurityTypeFeedSource On ProsTicker.SecurityTypeFeedSourceID = SecurityTypeFeedSource.SecurityTypeFeedSourceID
	WHERE ISNULL(ProsTicker.CUSIP, '') <> '' AND IsRemoved = 0
	      AND (@CompanyID IS NULL OR Company.CompanyID = @CompanyID)
		  AND (@SecurityTypeID IS NULL OR SecurityType.SecurityTypeID = @SecurityTypeID)
		  AND  (@TaxonomyMarketIDCount = 0 OR BCSDocUpdate.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t) ) 
	
	) Report

    WHERE Report.RowNum >@startIndex AND Report.RowNum<=@endIndex
	ORDER BY Report.RowNum

   SELECT COUNT(*) as virtualCount FROM  ProsTicker
	INNER JOIN BCSDocUpdate ON ProsTicker.CUSIP = BCSDocUpdate.CUSIP
	INNER JOIN Prospectus On Prospectus.ProsId = ProsTicker.ProspectusID
	INNER JOIN Company On Company.CompanyId = Prospectus.CompanyID
	INNER JOIN SecurityType ON ProsTicker.SecurityTypeID = SecurityType.SecurityTypeID
	INNER JOIN SecurityTypeFeedSource On ProsTicker.SecurityTypeFeedSourceID = SecurityTypeFeedSource.SecurityTypeFeedSourceID
	WHERE ISNULL(ProsTicker.CUSIP, '') <> '' AND IsRemoved = 0
	      AND (@CompanyID IS NULL OR Company.CompanyID = @CompanyID)
		  AND (@SecurityTypeID IS NULL OR SecurityType.SecurityTypeID = @SecurityTypeID)
		  AND  (@TaxonomyMarketIDCount = 0 OR BCSDocUpdate.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t) )

END
GO

