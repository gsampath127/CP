-- Delete trigger for ClientDns
CREATE TRIGGER T_ClientDns_D
ON dbo.ClientDns
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDnsId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDnsId,
			CONVERT(NVARCHAR(MAX), d.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), d.Dns) AS Dns
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientId' THEN d.ClientId
		WHEN 'Dns' THEN d.Dns
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDnsId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;