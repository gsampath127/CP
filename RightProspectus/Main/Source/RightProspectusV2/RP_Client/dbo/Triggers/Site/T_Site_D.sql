
-- Delete trigger for Site
CREATE TRIGGER T_Site_D
ON dbo.Site
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.SiteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[TemplateId]) AS [TemplateId],
			CONVERT(NVARCHAR(MAX), d.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), d.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
		WHEN 'TemplateId' THEN d.TemplateId
		WHEN 'DefaultPageId' THEN d.DefaultPageId
		WHEN 'ParentSiteId' THEN d.ParentSiteId
		WHEN 'Description' THEN d.[Description]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;