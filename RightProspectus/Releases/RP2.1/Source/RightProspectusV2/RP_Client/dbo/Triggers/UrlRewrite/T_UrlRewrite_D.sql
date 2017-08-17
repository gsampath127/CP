-- Delete trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_D
ON dbo.UrlRewrite
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.UrlRewriteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), d.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), d.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), d.PatternName) as PatternName
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MatchPattern' THEN d.MatchPattern
		WHEN 'RewriteFormat' THEN d.RewriteFormat
		WHEN 'PatternName' THEN d.PatternName	
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.UrlRewriteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;