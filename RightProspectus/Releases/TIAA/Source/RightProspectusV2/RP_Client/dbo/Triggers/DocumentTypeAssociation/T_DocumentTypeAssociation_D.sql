-- Delete trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_D
ON dbo.DocumentTypeAssociation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.DocumentTypeAssociationId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
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
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.DocumentTypeAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;