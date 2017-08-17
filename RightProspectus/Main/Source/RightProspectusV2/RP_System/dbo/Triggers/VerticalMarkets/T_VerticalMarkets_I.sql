-- Insert trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_I
ON dbo.VerticalMarkets
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.VerticalMarketId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.MarketDescription) as MarketDescription
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MarketName' THEN i.MarketName
		WHEN 'ConnectionStringName' THEN i.ConnectionStringName
		WHEN 'DatabaseName' THEN i.DatabaseName
		WHEN 'MarketDescription' THEN i.MarketDescription		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'), ('MarketDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.VerticalMarketId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
