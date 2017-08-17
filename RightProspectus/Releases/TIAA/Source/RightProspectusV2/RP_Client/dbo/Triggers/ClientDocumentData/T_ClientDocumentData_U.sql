-- Update trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_U
ON dbo.ClientDocumentData
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data,
			CONVERT(NVARCHAR(MAX), i.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), i.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), i.DataHash) as DataHash
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data,
			CONVERT(NVARCHAR(MAX), d.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), d.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), d.DataHash) as DataHash
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Data' THEN d.Data
			WHEN 'HasData' THEN d.HasData
			WHEN 'DataLength' THEN d.[DataLength]
			WHEN 'DataHash' THEN d.DataHash
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Data' THEN i.Data
			WHEN 'HasData' THEN i.HasData
			WHEN 'DataLength' THEN i.[DataLength]
			WHEN 'DataHash' THEN i.DataHash
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
