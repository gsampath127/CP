

-- Update trigger for TaxonomyAssociation
CREATE TRIGGER [dbo].[T_TaxonomyAssociation_U]
ON [dbo].[TaxonomyAssociation]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId, 'U', i.ModifiedBy
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
	),
	deletedConverted AS
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
	),
	dictionary AS
	(
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
		END AS OldValue,
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
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationId = d.TaxonomyAssociationId
			CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationId = c.[Key]
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