-- Insert trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_I
ON dbo.ClientDocumentGroup
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
		CONVERT(NVARCHAR(MAX), i.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.CssClass) AS CssClass
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
		WHEN 'ParentClientDocumentGroupId' THEN i.ParentClientDocumentGroupId
		WHEN 'CssClass' THEN i.CssClass
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
