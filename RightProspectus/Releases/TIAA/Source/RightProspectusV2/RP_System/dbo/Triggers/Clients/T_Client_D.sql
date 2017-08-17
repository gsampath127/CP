-- Delete trigger for Client
CREATE TRIGGER T_Client_D
ON dbo.Clients
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientId,
		CONVERT(NVARCHAR(MAX), d.ClientName) AS ClientName,
		CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
		CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
		CONVERT(NVARCHAR(MAX), d.VerticalMarketId) as VerticalMarketId,
		CONVERT(NVARCHAR(MAX), d.ClientDescription) as ClientDescription
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientName' THEN d.ClientName
		WHEN 'ConnectionStringName' THEN d.ConnectionStringName
		WHEN 'DatabaseName' THEN d.DatabaseName
		WHEN 'VerticalMarketId' THEN d.VerticalMarketId
		WHEN 'ClientDescription' THEN d.ClientDescription		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;