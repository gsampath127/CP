﻿-- Delete trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_D]
ON [dbo].[ReportContent]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ReportContentId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
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
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ReportContentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
