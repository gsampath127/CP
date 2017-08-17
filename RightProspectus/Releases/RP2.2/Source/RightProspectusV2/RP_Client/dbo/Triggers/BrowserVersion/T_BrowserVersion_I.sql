CREATE TRIGGER [dbo].[T_BrowserVersion_I]
ON [dbo].[BrowserVersion]
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'BrowserVersion';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.Id, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.Id,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.Version) AS Version,
		CONVERT(NVARCHAR(MAX), i.DownloadUrl) AS DownloadUrl
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Version' THEN i.Version
		WHEN 'DownloadUrl' THEN i.DownloadUrl
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'),('Version'), ('DownloadUrl')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.Id = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
