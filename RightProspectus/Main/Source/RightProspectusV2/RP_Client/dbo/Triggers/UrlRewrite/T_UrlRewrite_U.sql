-- Update trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_U
ON dbo.UrlRewrite
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UrlRewriteId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), i.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), i.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), i.PatternName) as PatternName
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), d.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), d.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), d.PatternName) as PatternName
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'MatchPattern' THEN d.MatchPattern
			WHEN 'RewriteFormat' THEN d.RewriteFormat
			WHEN 'PatternName' THEN d.PatternName	
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'MatchPattern' THEN i.MatchPattern
			WHEN 'RewriteFormat' THEN i.RewriteFormat
			WHEN 'PatternName' THEN i.PatternName	
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.UrlRewriteId = d.UrlRewriteId
			CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.UrlRewriteId = c.[Key]
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