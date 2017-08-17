-- Insert trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_I
ON dbo.ClientDocumentData
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.Data) AS Data,
		CONVERT(NVARCHAR(MAX), i.HasData) AS HasData,
		CONVERT(NVARCHAR(MAX), i.[DataLength]) AS [DataLength],
		CONVERT(NVARCHAR(MAX), i.DataHash) as DataHash
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Data' THEN i.Data
		WHEN 'HasData' THEN i.HasData
		WHEN 'DataLength' THEN i.[DataLength]
		WHEN 'DataHash' THEN i.DataHash
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
