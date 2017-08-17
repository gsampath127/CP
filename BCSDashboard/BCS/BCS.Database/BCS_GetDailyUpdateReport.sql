CREATE PROCEDURE [dbo].[BCS_GetDailyUpdateReport]  --exec [BCS_GetDailyUpdateReport] '2016-11-14', 'asc', 'FundName', 0, 1000
@SelectedDate DATETIME,
@sortDirection NVARCHAR(10),
@sortColumn    NVARCHAR(100),
@startIndex INT,
@endIndex INT
AS
BEGIN


	DECLARE @OldSecurityTypeDetails Table(TickerID int, OldSecurityType nvarchar(10))

	INSERT INTO @OldSecurityTypeDetails(TickerID, OldSecurityType)
		SELECT TickerID, OldSecurityType FROM (
		select TickerID, SecurityType.SecurityTypeCode 'OldSecurityType', ROW_NUMBER() over(partition by TickerID order by TickerIDHistory desc) As 'RowNum' 
		from ProsTickerHistory
		LEFT JOIN SecurityType ON ProsTickerHistory.SecurityTypeID = SecurityType.SecurityTypeID
		where UpdatedField = 'SecurityTypeID' and Convert(varchar(50), UpdateDate, 101) = Convert(varchar(50), @SelectedDate, 101)
	)t
	Where RowNum = 1

	--select * from @OldSecurityTypeDetails
	
 SELECT * FROM(

		SELECT DISTINCT 
		 ProsTicker.CUSIP,
	     CompanyName,
		 ProsTicker.CIK 'CompanyCIK',
		 Prospectus.ProsName 'FundName' ,
		 ProsTicker.Class 'Class',
		 ProsTicker.TickerSymbol 'Ticker',
	     SecurityType.SecurityTypeCode 'SecurityType',
		 OldSecurityType,
	     ProsTicker.SeriesID 'SeriesID',

		CASE

		WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Asc) 
		WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CUSIP Desc)  
		WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Asc)
		WHEN @sortColumn = 'CompanyName' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY CompanyName Desc)
		WHEN @sortColumn = 'CompanyCIK' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CIK Asc)
		WHEN @sortColumn = 'CompanyCIK' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.CIK Desc)
		WHEN @sortColumn = 'FundName' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Asc)
		WHEN @sortColumn = 'FundName' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY Prospectus.ProsName Desc)
		WHEN @sortColumn = 'Class' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Asc)
		WHEN @sortColumn = 'Class' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.Class Desc)
		WHEN @sortColumn = 'Ticker' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.TickerSymbol Asc)
		WHEN @sortColumn = 'Ticker' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.TickerSymbol Desc)
		WHEN @sortColumn = 'SecurityType' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY SecurityType.SecurityTypeCode Asc)
		WHEN @sortColumn = 'SecurityType' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY SecurityType.SecurityTypeCode Desc)
		WHEN @sortColumn = 'OldSecurityType' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY OldSecurityType Asc)
	    WHEN @sortColumn = 'OldSecurityType' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY OldSecurityType Desc)
		WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'ASC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.SeriesID Asc)
		WHEN @sortColumn = 'SeriesID' AND @sortDirection = 'DESC' THEN ROW_NUMBER() OVER(ORDER BY ProsTicker.SeriesID Desc)

       END AS RowRank

	   FROM ProsTicker
		  INNER JOIN @OldSecurityTypeDetails oldSecurityTypes on oldSecurityTypes.TickerID = ProsTicker.TickerID
		  INNER JOIN Prospectus On Prospectus.ProsId = ProsTicker.ProspectusID
		  INNER JOIN Company On Company.CompanyId = Prospectus.CompanyID
		  LEFT JOIN SecurityType On SecurityType.SecurityTypeId = ProsTicker.SecurityTypeId
		  WHERE ISNULL(ProsTicker.CUSIP, '') <> ''
		) AS Report
		
		 WHERE RowRank >@startIndex AND RowRank<=@endIndex		
	     ORDER BY RowRank

     SELECT COUNT(*) as virtualCount FROM  ProsTicker
	 INNER JOIN @OldSecurityTypeDetails oldSecurityTypes on oldSecurityTypes.TickerID = ProsTicker.TickerID
	 INNER JOIN Prospectus On Prospectus.ProsId = ProsTicker.ProspectusID
	 INNER JOIN Company On Company.CompanyId = Prospectus.CompanyID
	 LEFT JOIN SecurityType On SecurityType.SecurityTypeId = ProsTicker.SecurityTypeId
	 WHERE ISNULL(ProsTicker.CUSIP, '') <> ''	 
	 
END
GO

