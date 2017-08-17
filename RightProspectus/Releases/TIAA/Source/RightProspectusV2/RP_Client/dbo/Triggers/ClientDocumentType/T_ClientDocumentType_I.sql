-- Insert trigger for ClientDocumentType
CREATE TRIGGER T_ClientDocumentType_I
ON dbo.ClientDocumentType
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentType';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentTypeId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentTypeId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
