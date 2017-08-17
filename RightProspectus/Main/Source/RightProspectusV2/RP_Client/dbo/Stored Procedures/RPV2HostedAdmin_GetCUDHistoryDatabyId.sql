
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetCUDHistoryDatabyId]
@CUDHistoryId INT = NULL,
@PageSize int,
@PageIndex int,
@SortDirection NVARCHAR(10),
@SortColumn NVARCHAR(100)
AS
BEGIN

	SELECT DISTINCT
		RowRank,
		CUDHistoryId,
		ColumnName,
		SqlDbType,
		OldValue,
		NewValue,
		NewValueBinary,
		OldValueBinary
	FROM
	(
		SELECT
		cudhistorydata.CUDHistoryId,
		cudhistorydata.ColumnName,
		cudhistorydata.SqlDbType,
		ISNULL(cudhistorydata.OldValue, '') AS 'OldValue',
		ISNULL(cudhistorydata.NewValue, '') AS 'NewValue',
		Case When SqlDbType <> 165 THEN NULL ELSE CONVERT(varbinary(MAX), NewValue) END AS 'NewValueBinary',
		Case When SqlDbType <> 165 THEN NULL ELSE CONVERT(varbinary(MAX), OldValue) END AS 'OldValueBinary',
		CASE
		WHEN @sortColumn = 'ColumnName' AND @sortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY cudhistorydata.columnname Asc)
		WHEN @sortColumn = 'ColumnName' AND @sortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY cudhistorydata.columnname Desc)
		END AS RowRank
		FROM
		cudhistorydata
		WHERE CUDHistoryData.CUDHistoryId = @CUDHistoryId)
	AS CUDHistoryDataTable
	WHERE RowRank BETWEEN @pageIndex * @pageSize - @pageSize + 1  AND @pageSize * @pageIndex
	ORDER BY RowRank

	SELECT count(*) FROM CUDHistoryData WHERE CUDHistoryData.CUDHistoryId = @CUDHistoryId

END