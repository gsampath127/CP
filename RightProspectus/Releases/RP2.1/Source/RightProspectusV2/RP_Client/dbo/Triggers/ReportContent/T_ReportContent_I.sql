-- Insert trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_I]
ON [dbo].[ReportContent]
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ReportContentId, 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
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
		CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ReportContentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
