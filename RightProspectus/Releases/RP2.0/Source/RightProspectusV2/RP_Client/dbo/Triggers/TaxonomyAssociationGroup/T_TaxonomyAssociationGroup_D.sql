-- Delete trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_D
ON dbo.TaxonomyAssociationGroup
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.TaxonomyAssociationGroupId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'SiteId' THEN d.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN d.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN d.CssClass		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;