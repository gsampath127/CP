-- Insert trigger for ClientDns
CREATE TRIGGER T_ClientDns_I
ON dbo.ClientDns
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDnsId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDnsId,
			CONVERT(NVARCHAR(MAX), i.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), i.Dns) AS Dns
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientId' THEN i.ClientId
		WHEN 'Dns' THEN i.Dns
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
