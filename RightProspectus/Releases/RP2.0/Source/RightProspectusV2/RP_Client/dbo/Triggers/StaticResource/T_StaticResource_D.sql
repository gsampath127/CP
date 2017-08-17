-- Delete trigger for StaticResource
CREATE TRIGGER T_StaticResource_D
ON dbo.StaticResource
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, d.StaticResourceId , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.StaticResourceId ,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.Size) AS Size,
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'Size' THEN d.Size
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'Data' THEN d.Data
	END AS OldValue			
	FROM deletedConverted d
		CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.StaticResourceId = c.[Key] 
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;