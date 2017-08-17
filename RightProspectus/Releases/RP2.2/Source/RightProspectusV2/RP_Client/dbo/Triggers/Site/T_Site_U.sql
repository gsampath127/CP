-- Update trigger for Site
CREATE TRIGGER T_Site_U
ON dbo.Site
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.TemplateId) AS TemplateId,
			CONVERT(NVARCHAR(MAX), i.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), i.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[TemplateId]) AS [TemplateId],
			CONVERT(NVARCHAR(MAX), d.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), d.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'TemplateId' THEN d.TemplateId
			WHEN 'DefaultPageId' THEN d.DefaultPageId
			WHEN 'ParentSiteId' THEN d.ParentSiteId
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'TemplateId' THEN i.TemplateId
			WHEN 'DefaultPageId' THEN i.DefaultPageId
			WHEN 'ParentSiteId' THEN i.ParentSiteId
			WHEN 'Description' THEN i.[Description]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteId = d.SiteId
			CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteId = c.[Key]
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
