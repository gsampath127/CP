-- Update trigger for ClientDocument
CREATE TRIGGER T_ClientDocument_U
ON dbo.ClientDocument
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'U', i.ModifiedBy
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
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
		    CONVERT(NVARCHAR(MAX), d.ClientDocumentTypeId) AS ClientDocumentTypeId,		
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) as MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) AS IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,		
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientDocumentTypeId' THEN d.ClientDocumentTypeId
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'MimeType' THEN d.MimeType	
			WHEN 'IsPrivate' THEN d.IsPrivate
			WHEN 'ContentUri' THEN d.ContentUri				
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
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
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('ClientDocumentTypeId'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri') , ('Name'),  ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentId = c.[Key]
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
