-- Update trigger for Client
CREATE TRIGGER T_Client_U
ON dbo.Clients
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'U', i.ModifiedBy
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
	),
	deletedConverted AS
	(
		SELECT	d.ClientId,
			CONVERT(NVARCHAR(MAX), d.ClientName) AS ClientName,
			CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), d.VerticalMarketId) as VerticalMarketId,
			CONVERT(NVARCHAR(MAX), d.ClientDescription) as ClientDescription
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientName' THEN d.ClientName
			WHEN 'ConnectionStringName' THEN d.ConnectionStringName
			WHEN 'DatabaseName' THEN d.DatabaseName
			WHEN 'VerticalMarketId' THEN d.VerticalMarketId
			WHEN 'ClientDescription' THEN d.ClientDescription		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ClientName' THEN i.ClientName
			WHEN 'ConnectionStringName' THEN i.ConnectionStringName
			WHEN 'DatabaseName' THEN i.DatabaseName
			WHEN 'VerticalMarketId' THEN i.VerticalMarketId
			WHEN 'ClientDescription' THEN i.ClientDescription		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientId = d.ClientId
			CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientId = c.[Key]
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
