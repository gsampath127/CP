CREATE TRIGGER [dbo].[T_BrowserVersion_U]
ON [dbo].[BrowserVersion]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'BrowserVersion';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.Id, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.Id,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.Version) AS Version,
			CONVERT(NVARCHAR(MAX), i.DownloadUrl) AS DownloadUrl
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.Id,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.Version) AS Version,
			CONVERT(NVARCHAR(MAX), d.DownloadUrl) AS DownloadUrl
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Version' THEN d.Version
			WHEN 'DownloadUrl' THEN d.DownloadUrl
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Version' THEN i.Version
			WHEN 'DownloadUrl' THEN i.DownloadUrl
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.Id = d.Id
			CROSS JOIN (VALUES('Name'),('Version'), ('DownloadUrl')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.Id = c.[Key]
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
