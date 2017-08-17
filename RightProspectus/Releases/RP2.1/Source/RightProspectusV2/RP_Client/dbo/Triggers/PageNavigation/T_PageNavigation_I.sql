-- Insert trigger for PageNavigation
CREATE TRIGGER T_PageNavigation_I
ON dbo.PageNavigation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageNavigationId , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageNavigationId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
		CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
		CONVERT(NVARCHAR(MAX), i.[CurrentVersion]) AS [CurrentVersion]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'PageId' THEN i.PageId
		WHEN 'NavigationKey' THEN i.NavigationKey
		WHEN 'LanguageCulture' THEN i.LanguageCulture
		WHEN 'CurrentVersion' THEN i.[CurrentVersion]
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.PageNavigationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;