-- Insert trigger for SiteText
CREATE TRIGGER T_SiteText_I
ON dbo.SiteText
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteTextId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteTextId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
		CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'ResourceKey' THEN i.ResourceKey
		WHEN 'CurrentVersion' THEN i.CurrentVersion
		WHEN 'LanguageCulture' THEN i.LanguageCulture
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;