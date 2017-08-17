-- Update trigger for SiteFeature
CREATE TRIGGER T_SiteFeature_U
ON dbo.SiteFeature
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'SiteFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.SiteId,i.[Key], 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteId,d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN d.FeatureMode			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN i.FeatureMode			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteId = d.SiteId AND i.[Key] = d.[Key]
			CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteId = c.[Key] AND  i.[Key] = c.SecondKey
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
