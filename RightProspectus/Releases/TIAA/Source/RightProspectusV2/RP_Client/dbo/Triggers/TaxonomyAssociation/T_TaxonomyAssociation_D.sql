-- Delete trigger for TaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociation_D
ON dbo.TaxonomyAssociation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.TaxonomyAssociationId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Level]) AS [Level],
			CONVERT(NVARCHAR(MAX), d.TaxonomyId) AS TaxonomyId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
			CONVERT(NVARCHAR(MAX), d.NameOverride) as NameOverride,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,			
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Level' THEN d.[Level]
			WHEN 'TaxonomyId' THEN d.TaxonomyId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId
			WHEN 'NameOverride' THEN d.NameOverride
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride	
			WHEN 'MarketId' THEN d.MarketId					
			WHEN 'CssClass' THEN d.CssClass			
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;