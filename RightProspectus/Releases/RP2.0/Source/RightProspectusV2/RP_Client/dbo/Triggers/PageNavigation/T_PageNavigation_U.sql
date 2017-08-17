-- Update trigger for PageNavigation
CREATE TRIGGER T_PageNavigation_U
ON dbo.PageNavigation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'PageNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageNavigationId , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageNavigationId ,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.PageNavigationId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
		END
		AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'PageId' THEN i.PageId
			WHEN 'NavigationKey' THEN i.NavigationKey
			WHEN 'LanguageCulture' THEN i.LanguageCulture
			WHEN 'CurrentVersion' THEN i.CurrentVersion
		END
		AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.PageNavigationId = d.PageNavigationId
			CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.PageNavigationId = c.[Key] 
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
