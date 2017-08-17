
-- Delete trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_D
ON dbo.ClientDocumentData
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data,
			CONVERT(NVARCHAR(MAX), d.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), d.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), d.DataHash) as DataHash
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Data' THEN d.Data
			WHEN 'HasData' THEN d.HasData
			WHEN 'DataLength' THEN d.[DataLength]
			WHEN 'DataHash' THEN d.DataHash
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;