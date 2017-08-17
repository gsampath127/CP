-- Insert trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_I
ON dbo.TaxonomyAssociationMetaData
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationID,i.[Key], 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DataType' THEN i.DataType	
		WHEN 'Order' THEN i.[Order]	
		WHEN 'IntegerValue' THEN i.IntegerValue	
		WHEN 'BooleanValue' THEN i.BooleanValue	
		WHEN 'DateTimeValue' THEN i.DateTimeValue	
		WHEN 'StringValue' THEN i.StringValue			
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationID = c.[Key] AND i.[Key] = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
