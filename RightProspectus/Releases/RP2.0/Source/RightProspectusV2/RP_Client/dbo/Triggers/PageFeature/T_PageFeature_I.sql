-- Insert trigger for PageFeature
CREATE TRIGGER T_PageFeature_I
ON dbo.PageFeature
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey],[ThirdKey], CUDType, UserId)
	SELECT	@TableName, i.SiteId, i.PageId, i.[Key] , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
		i.PageId,
		i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN i.FeatureMode
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteId = c.[Key]
			AND i.PageId = c.[SecondKey]
			AND i.[Key] = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;