-- Update trigger for StaticResource
CREATE TRIGGER T_StaticResource_U
ON dbo.StaticResource
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.StaticResourceId , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.StaticResourceId ,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.Size) AS Size,
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.StaticResourceId,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.Size) AS Size,
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'Size' THEN d.Size
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'Data' THEN d.Data
		END
		AS OldValue,
		CASE cn.ColumnName
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'Size' THEN i.Size
			WHEN 'MimeType' THEN i.MimeType
			WHEN 'Data' THEN i.Data
		END
		AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.StaticResourceId = d.StaticResourceId
					CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.StaticResourceId = c.[Key] 
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
