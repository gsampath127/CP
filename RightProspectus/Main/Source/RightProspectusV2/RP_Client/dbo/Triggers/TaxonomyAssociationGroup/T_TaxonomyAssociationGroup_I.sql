-- Insert trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_I
ON dbo.TaxonomyAssociationGroup
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId, 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
		WHEN 'SiteId' THEN i.SiteId		
		WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId								
		WHEN 'ParentTaxonomyAssociationGroupId' THEN i.ParentTaxonomyAssociationGroupId
		WHEN 'CssClass' THEN i.CssClass		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;