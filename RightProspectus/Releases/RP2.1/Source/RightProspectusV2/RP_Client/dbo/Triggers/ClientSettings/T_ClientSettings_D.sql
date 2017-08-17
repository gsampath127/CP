

-- Delete trigger for ClientSettings
CREATE TRIGGER [dbo].[T_ClientSettings_D]
ON [dbo].[ClientSettings]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientId, 'D'
	FROM	deleted d;

	
	
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientId,
		CONVERT(NVARCHAR(MAX), d.DefaultSiteId) AS DefaultSiteId		
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DefaultSiteId' THEN d.DefaultSiteId	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;



