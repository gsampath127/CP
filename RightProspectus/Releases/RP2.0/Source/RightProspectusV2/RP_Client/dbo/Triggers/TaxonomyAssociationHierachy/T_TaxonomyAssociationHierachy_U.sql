-- Update trigger for TaxonomyAssociationHierachy
CREATE TRIGGER T_TaxonomyAssociationHierachy_U
ON dbo.TaxonomyAssociationHierachy
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.ParentTaxonomyAssociationId,i.ChildTaxonomyAssociationId, 
				i.RelationshipType , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ParentTaxonomyAssociationId,i.ChildTaxonomyAssociationId, 
				i.RelationshipType,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ParentTaxonomyAssociationId,d.ChildTaxonomyAssociationId, 
				d.RelationshipType,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ParentTaxonomyAssociationId = d.ParentTaxonomyAssociationId
				 AND i.ChildTaxonomyAssociationId = d.ChildTaxonomyAssociationId
				 AND i.RelationshipType = d.RelationshipType
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ParentTaxonomyAssociationId = c.[Key] 
					AND i.ChildTaxonomyAssociationId = c.SecondKey
					AND i.RelationshipType = c.ThirdKey
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
