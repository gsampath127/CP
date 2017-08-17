CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetCUDHistory]
	@CUDHistoryId INT=NULL,
	@TableName NVARCHAR(100)=NULL,
	@CUDType NVARCHAR(100)=NULL,
	@UTCFromCUDDate DATETIME =NULL,
	@UTCToCUDDate DATETIME =NULL,
	@PageSize int,
	@PageIndex int,
    @SortDirection NVARCHAR(10),
	@SortColumn NVARCHAR(100),
	@UserID int=NULL
AS
BEGIN
	SELECT DISTINCT
				RowRank,
				CUDHistoryId,
				TableName,
				CUDType,
				UtcCUDDate,
				UserId
			FROM 
			(
			SELECT
       		cudhistory.CUDHistoryId,
			cudhistory.TableName,
			cudhistory.CUDType,
			cudhistory.UtcCUDDate,
            cudhistory.UserId,
			CASE	WHEN @SortColumn = 'CUDHistoryId' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDHistoryId Asc)
					WHEN @SortColumn = 'CUDHistoryId' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDHistoryId Desc)
					WHEN @SortColumn = 'TableName' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  cudhistory.TableName Asc)
					WHEN @SortColumn = 'TableName' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  cudhistory.TableName Desc)
					WHEN @SortColumn = 'CUDType' AND @SortDirection = 'Ascending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDType Asc)
					WHEN @SortColumn = 'CUDType' AND @SortDirection = 'Descending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDType Desc)
					WHEN @SortColumn = 'UtcCUDDate' AND @SortDirection = 'Ascending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.UtcCUDDate Asc)
					WHEN @SortColumn = 'UtcCUDDate' AND @SortDirection = 'Descending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.UtcCUDDate Desc)
 		End AS RowRank
	FROM cudhistory
    WHERE	(cudhistory.CUDHistoryId = @CUDHistoryId OR @CUDHistoryId IS NULL)
				AND(cudhistory.TableName = @TableName OR @TableName IS NULL)
				AND(cudhistory.CUDType = @CUDType OR @CUDType IS NULL)
				AND ((CONVERT(Varchar(50), cudhistory.UtcCUDDate,102) BETWEEN  CONVERT(VARCHAR(50), @UTCFromCUDDate,102) AND CONVERT(VARCHAR(50),@UTCToCUDDate, 102)) OR @UTCFromCUDDate IS NULL)
				AND(CUDHistory.UserId = @UserID OR @UserID IS NULL)
				)AS cudhistoryTable 
	WHERE RowRank BETWEEN @PageIndex*@PageSize-@PageSize+1  AND @PageSize*@PageIndex
	ORDER BY RowRank

	SELECT count(*) 
	FROM CUDHistory 
	WHERE  (cudhistory.CUDHistoryId = @CUDHistoryId OR @CUDHistoryId IS NULL)
	AND(cudhistory.TableName = @TableName OR @TableName IS NULL)
	AND(cudhistory.CUDType = @CUDType OR @CUDType IS NULL)	
	AND ((CONVERT(Varchar(50), cudhistory.UtcCUDDate,102) BETWEEN  CONVERT(VARCHAR(50), @UTCFromCUDDate,102) AND CONVERT(VARCHAR(50),@UTCToCUDDate, 102)) OR @UTCFromCUDDate IS NULL)
	AND(CUDHistory.UserId = @UserID OR @UserID IS NULL)
END