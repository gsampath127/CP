-- Insert trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_I
ON dbo.DocumentTypeAssociation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.DocumentTypeAssociationId, 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
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
		CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.DocumentTypeAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
