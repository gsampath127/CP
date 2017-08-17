CREATE PROCEDURE [dbo].[BCS_GetEdgarOnlineData]
@CUSIPs TT_TaxonomyMarketId READONLY,
@CIKs TT_TaxonomyMarketId READONLY,
@Series TT_TaxonomyMarketId READONLY,
@Class TT_TaxonomyMarketId READONLY,
@Ticker TT_TaxonomyMarketId READONLY,
@startIndex INT = 0 ,
@endIndex INT =0
AS

BEGIN

DECLARE @CUSIPCount INT = 0 , @CIKCount INT =0 ,@SeriesCount INT =0 , @ClassCount INT =0 , @TickerCount INT =0;
SELECT @CUSIPCount = Count(c.marketId) FROM @CUSIPs c
SELECT @CIKCount = Count(ci.marketId) FROM @CIKs ci
SELECT @SeriesCount = Count(s.marketId) FROM @Series s
SELECT @ClassCount = Count(cl.marketId) FROM @Class cl
SELECT @TickerCount = Count(t.marketId) FROM @Ticker t

	;WITH Result 
	AS
	(
		SELECT  ECUSIP, ECompanyName, EFundName, ECIK, ESeriesID, EClassContractID, Eticker, ROW_NUMBER() OVER (ORDER BY ECUSIP) as RowNum
		FROM EdgarOnlineFeed 
		WHERE (@CUSIPCount = 0 OR ECUSIP IN (SELECT c.marketId from @CUSIPs c) ) 
			AND  (@CIKCount = 0 OR ECIK IN (SELECT ci.marketId from @CIKs ci) ) 
			AND  (@SeriesCount = 0 OR ESeriesID IN (SELECT s.marketId from @Series s) ) 
			AND  (@ClassCount = 0 OR EClassContractID IN (SELECT cl.marketId from @Class cl) ) 
			AND  (@TickerCount = 0 OR Eticker IN (SELECT t.marketId from @Ticker t) ) 

	),counts as (
    select count(*) as virtualCount from Result
)

SELECT Result.*,counts.virtualCount
FROM Result
CROSS APPLY counts 
WHERE RowNum BETWEEN @startIndex and @endIndex

	

END