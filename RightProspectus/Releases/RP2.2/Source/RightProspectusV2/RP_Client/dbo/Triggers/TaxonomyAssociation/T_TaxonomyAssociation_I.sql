-- Insert trigger for TaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociation_I
ON dbo.TaxonomyAssociation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.[Level]) AS [Level],
		CONVERT(NVARCHAR(MAX), i.TaxonomyId) AS TaxonomyId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
		CONVERT(NVARCHAR(MAX), i.NameOverride) as NameOverride,
		CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
		CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
		CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Level' THEN i.[Level]
		WHEN 'TaxonomyId' THEN i.TaxonomyId
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId
		WHEN 'NameOverride' THEN i.NameOverride		
		WHEN 'DescriptionOverride' THEN i.DescriptionOverride		
		WHEN 'MarketId' THEN i.MarketId		
		WHEN 'CssClass' THEN i.CssClass						
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
