-- Insert trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_I
ON dbo.UrlRewrite
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UrlRewriteId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UrlRewriteId,
		CONVERT(NVARCHAR(MAX), i.MatchPattern) AS MatchPattern,
		CONVERT(NVARCHAR(MAX), i.RewriteFormat) AS RewriteFormat,
		CONVERT(NVARCHAR(MAX), i.PatternName) as PatternName
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MatchPattern' THEN i.MatchPattern
		WHEN 'RewriteFormat' THEN i.RewriteFormat
		WHEN 'PatternName' THEN i.PatternName	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.UrlRewriteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;