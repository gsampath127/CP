-- Delete trigger for DocumentTypeExternalId
CREATE TRIGGER [dbo].T_DocumentTypeExternalId_D
ON [dbo].DocumentTypeExternalId
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.DocumentTypeId,
				d.ExternalId, 'D'
	FROM	deleted d;

	WITH deletedConverted AS
	(
		SELECT	
		d.DocumentTypeId ,
		d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN d.IsPrimary
	END AS NewValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.DocumentTypeId  = c.[Key]
			AND d.ExternalId = c.[SecondKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;