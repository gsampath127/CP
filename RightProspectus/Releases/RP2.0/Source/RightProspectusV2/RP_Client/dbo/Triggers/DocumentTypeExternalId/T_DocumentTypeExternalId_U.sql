-- Update trigger for DocumentTypeExternalId
CREATE TRIGGER T_DocumentTypeExternalId_U
ON dbo.DocumentTypeExternalId
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'DocumentTypeExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName,i.DocumentTypeId, 
				i.ExternalId , 'U', i.ModifiedBy
	FROM	inserted i;
			
    WITH insertedConverted AS
	(
		SELECT	i.DocumentTypeId,i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.DocumentTypeId,d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	),

	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN d.IsPrimary			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN i.IsPrimary			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.DocumentTypeId = d.DocumentTypeId  AND i.ExternalId = d.ExternalId
			CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.DocumentTypeId = c.[Key] AND i.ExternalId = c.SecondKey
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
