-- Insert trigger for Site
CREATE TRIGGER T_Site_I
ON dbo.Site
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteId, 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'TemplateId' THEN i.TemplateId
		WHEN 'DefaultPageId' THEN i.DefaultPageId
		WHEN 'ParentSiteId' THEN i.ParentSiteId
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
