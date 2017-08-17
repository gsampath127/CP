-- Update trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_U
ON dbo.TaxonomyAssociationMetaData
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationID,i.[Key], 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationID,i.[Key],
			CONVERT(NVARCHAR(MAX), i.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), i.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), i.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), i.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), i.StringValue) AS StringValue
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationID,d.[Key],
			CONVERT(NVARCHAR(MAX), d.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), d.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), d.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), d.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), d.StringValue) AS StringValue
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DataType' THEN d.DataType	
			WHEN 'Order' THEN d.[Order]	
			WHEN 'IntegerValue' THEN d.IntegerValue	
			WHEN 'BooleanValue' THEN d.BooleanValue	
			WHEN 'DateTimeValue' THEN d.DateTimeValue	
			WHEN 'StringValue' THEN d.StringValue			
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DataType' THEN i.DataType	
			WHEN 'Order' THEN i.[Order]	
			WHEN 'IntegerValue' THEN i.IntegerValue	
			WHEN 'BooleanValue' THEN i.BooleanValue	
			WHEN 'DateTimeValue' THEN i.DateTimeValue	
			WHEN 'StringValue' THEN i.StringValue			
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationID = d.TaxonomyAssociationID and i.[Key] = d.[Key]
			CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationID = c.[Key] AND i.[Key] = c.SecondKey
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
