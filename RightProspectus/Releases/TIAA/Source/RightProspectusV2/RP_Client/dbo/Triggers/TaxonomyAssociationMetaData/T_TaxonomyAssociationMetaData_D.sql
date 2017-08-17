-- Delete trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_D
ON dbo.TaxonomyAssociationMetaData
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationID,d.[Key], 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationID,d.[Key],
			CONVERT(NVARCHAR(MAX), d.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), d.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), d.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), d.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), d.StringValue) AS StringValue
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DataType' THEN d.DataType	
			WHEN 'Order' THEN d.[Order]	
			WHEN 'IntegerValue' THEN d.IntegerValue	
			WHEN 'BooleanValue' THEN d.BooleanValue	
			WHEN 'DateTimeValue' THEN d.DateTimeValue	
			WHEN 'StringValue' THEN d.StringValue			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationID = c.[Key] AND d.[Key] = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;