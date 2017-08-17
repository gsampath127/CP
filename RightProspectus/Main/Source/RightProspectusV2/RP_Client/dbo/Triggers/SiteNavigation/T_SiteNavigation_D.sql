-- Delete trigger for SiteNavigation
CREATE TRIGGER T_SiteNavigation_D
ON dbo.SiteNavigation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, d.SiteNavigationId , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteNavigationId ,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
	END AS OldValue			
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteNavigationId = c.[Key] 
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;