CREATE TRIGGER [dbo].[T_BrowserVersion_D]
ON [dbo].[BrowserVersion]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'BrowserVersion';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.Id, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.Id,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Version]) AS [Version],
			CONVERT(NVARCHAR(MAX), d.DownloadUrl) AS DownloadUrl
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
		WHEN 'Version' THEN d.Version
		WHEN 'DownloadUrl' THEN d.DownloadUrl	
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('Version'), ('DownloadUrl')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.Id = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;