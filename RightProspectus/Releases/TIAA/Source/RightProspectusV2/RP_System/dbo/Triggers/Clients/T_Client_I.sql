-- Insert trigger for Client
CREATE TRIGGER T_Client_I
ON dbo.Clients
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
			CONVERT(NVARCHAR(MAX), i.ClientName) AS ClientName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.VerticalMarketId) as VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.ClientDescription) as ClientDescription
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientName' THEN i.ClientName
		WHEN 'ConnectionStringName' THEN i.ConnectionStringName
		WHEN 'DatabaseName' THEN i.DatabaseName
		WHEN 'VerticalMarketId' THEN i.VerticalMarketId
		WHEN 'ClientDescription' THEN i.ClientDescription		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
