

-- Update trigger for ClientDocumentType
CREATE TRIGGER [dbo].[T_ClientDocumentType_U]
ON [dbo].[ClientDocumentType]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentType';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentTypeId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentTypeId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentTypeId = d.ClientDocumentTypeId
			CROSS JOIN (VALUES('Name'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentTypeId = c.[Key]
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