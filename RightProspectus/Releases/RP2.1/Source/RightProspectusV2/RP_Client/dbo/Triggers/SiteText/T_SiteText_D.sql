-- Delete trigger for SiteText
CREATE TRIGGER T_SiteText_D
ON dbo.SiteText
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.SiteTextId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN d.SiteId
		WHEN 'ResourceKey' THEN d.ResourceKey
		WHEN 'CurrentVersion' THEN d.CurrentVersion
		WHEN 'LanguageCulture' THEN d.LanguageCulture
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;