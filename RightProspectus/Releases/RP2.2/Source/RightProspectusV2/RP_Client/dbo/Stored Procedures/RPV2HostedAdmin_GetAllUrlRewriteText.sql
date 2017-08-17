
CREATE procedure [dbo].[RPV2HostedAdmin_GetAllUrlRewriteText]

@PatternName nvarchar(100) = NULL,
@pageSize int,
@pageIndex int,
@sortDirection NVARCHAR(10),
@sortColumn NVARCHAR(100),
@count int out

AS

BEGIN

      SELECT DISTINCT
					    RowRank,
					 	UrlRewriteId,
						MatchPattern,
						RewriteFormat,
						UtcModifiedDate,
						ModifiedBy,
						PatternName
						FROM 
					  (
				  SELECT
				        UrlReWrite.UrlRewriteId,
						UrlReWrite.MatchPattern,
						UrlReWrite.RewriteFormat,
						UrlRewrite.UtcModifiedDate,
						UrlRewrite.ModifiedBy,
						UrlRewrite.PatternName,
						CASE  
							WHEN @sortColumn = 'PatternName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY UrlRewrite.PatternName Asc) 									  
							WHEN @sortColumn = 'PatternName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY UrlRewrite.PatternName Desc) 									  
							
						End 										          
							   AS RowRank

							   FROM
					             UrlReWrite
					
					 WHERE 
					  
					  UrlRewrite.PatternName LIKE '%' + ISNULL(@PatternName ,UrlRewrite.PatternName ) + '%'
				  
					) AS UrlRewriteTable  

					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
				       

   

END