﻿-- Update trigger for PageText
CREATE TRIGGER T_PageText_U
ON dbo.PageText
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'PageText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageTextId, 'U', i.ModifiedBy
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
	),
	deletedConverted AS
	(
		SELECT	d.PageTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'ResourceKey' THEN d.ResourceKey
			WHEN 'CurrentVersion' THEN d.CurrentVersion
			WHEN 'LanguageCulture' THEN d.LanguageCulture
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'PageId' THEN i.PageId
			WHEN 'ResourceKey' THEN i.ResourceKey
			WHEN 'CurrentVersion' THEN i.CurrentVersion
			WHEN 'LanguageCulture' THEN i.LanguageCulture
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.PageTextId = d.PageTextId
			CROSS JOIN (VALUES('SiteId'),('PageId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.PageTextId = c.[Key]
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
