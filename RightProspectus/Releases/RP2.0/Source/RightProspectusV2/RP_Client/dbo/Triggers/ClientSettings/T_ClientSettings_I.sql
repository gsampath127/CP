-- Insert trigger for ClientSettings
CREATE TRIGGER T_ClientSettings_I
ON dbo.ClientSettings
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
		CONVERT(NVARCHAR(MAX), i.DefaultSiteId) AS DefaultSiteId
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DefaultSiteId' THEN i.DefaultSiteId
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
