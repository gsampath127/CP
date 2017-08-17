-- Insert trigger for PageText
CREATE TRIGGER T_PageText_I
ON dbo.PageText
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageTextId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageTextId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
		CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
		CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'PageId' THEN i.PageId
		WHEN 'ResourceKey' THEN i.ResourceKey
		WHEN 'CurrentVersion' THEN i.CurrentVersion
		WHEN 'LanguageCulture' THEN i.LanguageCulture
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'),('PageId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.PageTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;