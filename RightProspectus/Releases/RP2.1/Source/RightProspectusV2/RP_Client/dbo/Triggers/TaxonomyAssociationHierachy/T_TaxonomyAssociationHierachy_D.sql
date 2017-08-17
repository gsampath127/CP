-- Delete trigger for TaxonomyAssociationHierachy
CREATE TRIGGER [dbo].[T_TaxonomyAssociationHierachy_D]
ON [dbo].[TaxonomyAssociationHierachy]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType)
	SELECT	@TableName, d.ParentTaxonomyAssociationId ,d.ChildTaxonomyAssociationId,
				d.RelationshipType, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.ParentTaxonomyAssociationId ,d.ChildTaxonomyAssociationId,
				d.RelationshipType,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ParentTaxonomyAssociationId = c.[Key] 
				AND d.ChildTaxonomyAssociationId = c.SecondKey
				AND d.RelationshipType = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;