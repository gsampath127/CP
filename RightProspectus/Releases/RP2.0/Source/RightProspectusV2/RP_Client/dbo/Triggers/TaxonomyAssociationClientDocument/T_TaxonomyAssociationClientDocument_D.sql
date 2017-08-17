﻿-- Delete trigger for TaxonomyAssociationClientDocument
CREATE TRIGGER T_TaxonomyAssociationClientDocument_D
ON dbo.TaxonomyAssociationClientDocument
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationId,d.ClientDocumentId, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,d.ClientDocumentId,
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
			ON	d.TaxonomyAssociationId = c.[Key] AND d.ClientDocumentId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;