-- Update trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_U
ON dbo.TaxonomyAssociationGroup
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), i.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'SiteId' THEN d.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN d.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN d.CssClass		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
			WHEN 'SiteId' THEN i.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN i.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN i.CssClass		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationGroupId = d.TaxonomyAssociationGroupId
			CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationGroupId = c.[Key]
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
