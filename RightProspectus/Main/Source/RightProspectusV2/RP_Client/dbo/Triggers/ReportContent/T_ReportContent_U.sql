-- Update trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_U]
ON [dbo].[ReportContent]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ReportContentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ReportContentId,
			CONVERT(NVARCHAR(MAX), i.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), i.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), i.Name) as Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ReportContentId,
			CONVERT(NVARCHAR(MAX), d.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), d.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), d.Name) as Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN d.ReportScheduleId
			WHEN 'ReportRunDate' THEN d.ReportRunDate
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'IsPrivate' THEN d.IsPrivate
			WHEN 'ContentUri' THEN d.ContentUri
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN i.ReportScheduleId
			WHEN 'ReportRunDate' THEN i.ReportRunDate
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'MimeType' THEN i.MimeType
			WHEN 'IsPrivate' THEN i.IsPrivate
			WHEN 'ContentUri' THEN i.ContentUri
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ReportContentId = d.ReportContentId
			CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ReportContentId = c.[Key]
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