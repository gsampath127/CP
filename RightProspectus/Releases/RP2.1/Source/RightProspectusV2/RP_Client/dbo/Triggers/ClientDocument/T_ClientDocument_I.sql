-- Insert trigger for ClientDocument
CREATE TRIGGER T_ClientDocument_I
ON dbo.ClientDocument
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.ClientDocumentTypeId) AS ClientDocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
		CONVERT(NVARCHAR(MAX), i.MimeType) as MimeType,
		CONVERT(NVARCHAR(MAX), i.IsPrivate) AS IsPrivate,
		CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientDocumentTypeId' THEN i.ClientDocumentTypeId
		WHEN 'FileName' THEN i.[FileName]
		WHEN 'MimeType' THEN i.MimeType	
		WHEN 'IsPrivate' THEN i.IsPrivate
		WHEN 'ContentUri' THEN i.ContentUri			
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES ('ClientDocumentTypeId'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri') , ('Name'),  ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
