-- Update trigger for ClientDns
CREATE TRIGGER T_ClientDns_U
ON dbo.ClientDns
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDnsId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDnsId,
			CONVERT(NVARCHAR(MAX), i.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), i.Dns) AS Dns
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDnsId,
			CONVERT(NVARCHAR(MAX), d.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), d.Dns) AS Dns
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientId' THEN d.ClientId
			WHEN 'Dns' THEN d.Dns
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ClientId' THEN i.ClientId
			WHEN 'Dns' THEN i.Dns
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDnsId = d.ClientDnsId
			CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDnsId = c.[Key]
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
