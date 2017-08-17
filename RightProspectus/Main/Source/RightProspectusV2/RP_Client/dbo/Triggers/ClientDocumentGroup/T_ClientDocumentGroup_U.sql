-- Update trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_U
ON dbo.ClientDocumentGroup
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), i.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), i.CssClass) AS CssClass
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.CssClass) AS CssClass
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN d.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN d.CssClass
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN i.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN i.CssClass
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentGroupId = d.ClientDocumentGroupId
			CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentGroupId = c.[Key]
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
