-- Update trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_U
ON dbo.DocumentTypeAssociation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.DocumentTypeAssociationId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.DocumentTypeAssociationId,
			CONVERT(NVARCHAR(MAX), i.DocumentTypeId) AS DocumentTypeId,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), i.[Order]) as [Order],
			CONVERT(NVARCHAR(MAX), i.HeaderText) as HeaderText,
			CONVERT(NVARCHAR(MAX), i.LinkText) as LinkText,
			CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass				
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.DocumentTypeAssociationId,
			CONVERT(NVARCHAR(MAX), d.DocumentTypeId) AS DocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order],
			CONVERT(NVARCHAR(MAX), d.HeaderText) as HeaderText,
			CONVERT(NVARCHAR(MAX), d.LinkText) as LinkText,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass				
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DocumentTypeId' THEN d.DocumentTypeId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'Order' THEN d.[Order]
			WHEN 'HeaderText' THEN d.HeaderText		
			WHEN 'LinkText' THEN d.LinkText		
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride	
			WHEN 'MarketId' THEN d.MarketId			
			WHEN 'CssClass' THEN d.CssClass								
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DocumentTypeId' THEN i.DocumentTypeId
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
			WHEN 'Order' THEN i.[Order]
			WHEN 'HeaderText' THEN i.HeaderText		
			WHEN 'LinkText' THEN i.LinkText		
			WHEN 'DescriptionOverride' THEN i.DescriptionOverride	
			WHEN 'MarketId' THEN i.MarketId			
			WHEN 'CssClass' THEN i.CssClass								
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.DocumentTypeAssociationId = d.DocumentTypeAssociationId
					CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.DocumentTypeAssociationId = c.[Key]
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
