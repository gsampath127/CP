CREATE PROCEDURE [dbo].[BCS_GetSummarizedSecurityFeed]
As
BEGIN
	DECLARE @temp Table(LoadType nvarchar(200), SecurityTypeCode nvarchar(10), [Count] int)
 
	INSERT INTO @temp
	select SecurityTypeFeedSource.SourceName,
 
	SecurityType.SecurityTypeCode,
	Count(tempCUSIP.CUSIP) as [count] 
	from SecurityType
	CROSS JOIN SecurityTypeFeedSource 
	LEFT JOIN
	(
		SELECT DISTINCT ProsTicker.CUSIP, ProsTicker.SecurityTypeID, ProsTicker.SecurityTypeFeedSourceID FROM ProsTicker
		INNER JOIN BCSDocUpdate ON ProsTicker.CUSIP = BCSDocUpdate.CUSIP
		INNER JOIN Prospectus On Prospectus.ProsId = ProsTicker.ProspectusID
		INNER JOIN Company On Company.CompanyId = Prospectus.CompanyID
		INNER JOIN SecurityType ON ProsTicker.SecurityTypeID = SecurityType.SecurityTypeID
		INNER JOIN SecurityTypeFeedSource On ProsTicker.SecurityTypeFeedSourceID = SecurityTypeFeedSource.SecurityTypeFeedSourceID
		WHERE ISNULL(ProsTicker.CUSIP, '') <> '' AND IsRemoved = 0
	)
	tempCUSIP ON tempCUSIP.SecurityTypeID = SecurityType.SecurityTypeID AND tempCUSIP.SecurityTypeFeedSourceID = SecurityTypeFeedSource.SecurityTypeFeedSourceID
	GROUP BY SecurityTypeFeedSource.SourceName,SecurityType.SecurityTypeCode
	order by SourceName

 
	SELECT *
	FROM (
		SELECT
			LoadType, SecurityTypeCode, [Count]
		FROM @temp
	) as s
	PIVOT
	(
	Min(Count)
		FOR [SecurityTypeCode] IN (ETF,ETN,MF,NA,UIT)
	)AS pvt

END
GO

