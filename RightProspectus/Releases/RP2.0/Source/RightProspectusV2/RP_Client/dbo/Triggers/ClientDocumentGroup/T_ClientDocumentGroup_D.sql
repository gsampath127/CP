
-- Delete trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_D
ON dbo.ClientDocumentGroup
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentGroupId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.CssClass) AS CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN d.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN d.CssClass
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;