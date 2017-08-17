-- Delete trigger for PageFeature
CREATE TRIGGER T_PageFeature_D
ON dbo.PageFeature
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey],[ThirdKey], CUDType, UserId)
	SELECT	@TableName, d.SiteId, d.PageId, d.[Key] , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteId,
			d.PageId,
			d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN d.FeatureMode	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteId = c.[Key]
			AND d.PageId = c.[SecondKey]
			AND d.[Key] = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;