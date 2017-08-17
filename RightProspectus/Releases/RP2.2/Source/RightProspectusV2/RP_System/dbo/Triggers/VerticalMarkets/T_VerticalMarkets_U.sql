-- Update trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_U
ON dbo.VerticalMarkets
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.VerticalMarketId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.MarketDescription) as MarketDescription
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), d.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), d.MarketDescription) as MarketDescription
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'MarketName' THEN d.MarketName
			WHEN 'ConnectionStringName' THEN d.ConnectionStringName
			WHEN 'DatabaseName' THEN d.DatabaseName
			WHEN 'MarketDescription' THEN d.MarketDescription		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'MarketName' THEN i.MarketName
			WHEN 'ConnectionStringName' THEN i.ConnectionStringName
			WHEN 'DatabaseName' THEN i.DatabaseName
			WHEN 'MarketDescription' THEN i.MarketDescription		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.VerticalMarketId = d.VerticalMarketId
			CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'), ('MarketDescription')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.VerticalMarketId = c.[Key]
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
