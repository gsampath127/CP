﻿-- Update trigger for ClientDocumentGroupClientDocument
CREATE TRIGGER T_ClientDocumentGroupClientDocument_U
ON dbo.ClientDocumentGroupClientDocument
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentGroupClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId,i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,d.ClientDocumentId,
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
				ON	i.ClientDocumentGroupId = d.ClientDocumentGroupId and i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentGroupId = c.[Key] AND i.ClientDocumentId = c.SecondKey
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
