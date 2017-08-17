-- Delete trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_D
ON dbo.VerticalMarkets
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.VerticalMarketId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.VerticalMarketId,
		CONVERT(NVARCHAR(MAX), d.MarketName) AS MarketName,
		CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
		CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
		CONVERT(NVARCHAR(MAX), d.MarketDescription) as MarketDescription
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MarketName' THEN d.MarketName
		WHEN 'ConnectionStringName' THEN d.ConnectionStringName
		WHEN 'DatabaseName' THEN d.DatabaseName
		WHEN 'MarketDescription' THEN d.MarketDescription		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'),  ('MarketDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.VerticalMarketId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;