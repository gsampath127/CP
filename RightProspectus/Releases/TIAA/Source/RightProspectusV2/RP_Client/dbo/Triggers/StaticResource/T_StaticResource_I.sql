-- Insert trigger for StaticResource
CREATE TRIGGER T_StaticResource_I
ON dbo.StaticResource
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.StaticResourceId , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.StaticResourceId,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.Size) AS Size,
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data
	    FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FileName' THEN i.[FileName]
		WHEN 'Size' THEN i.Size
		WHEN 'MimeType' THEN i.MimeType
		WHEN 'Data' THEN i.Data
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.StaticResourceId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;