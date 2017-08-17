-- Insert trigger for TaxonomyAssociationHierachy
CREATE TRIGGER T_TaxonomyAssociationHierachy_I
ON dbo.TaxonomyAssociationHierachy
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.ParentTaxonomyAssociationId ,i.ChildTaxonomyAssociationId,
				i.RelationshipType , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ParentTaxonomyAssociationId ,i.ChildTaxonomyAssociationId,
				i.RelationshipType, 
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ParentTaxonomyAssociationId = c.[Key] AND i.ChildTaxonomyAssociationId = c.SecondKey
				AND i.RelationshipType = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
