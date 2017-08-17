-- Update trigger for ClientSettings
CREATE TRIGGER [dbo].[T_ClientSettings_U]
ON [dbo].[ClientSettings]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
		  CONVERT(NVARCHAR(MAX), i.DefaultSiteId) AS DefaultSiteId
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientId,
     		CONVERT(NVARCHAR(MAX), d.DefaultSiteId) AS DefaultSiteId		
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DefaultSiteId' THEN d.DefaultSiteId		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DefaultSiteId' THEN i.DefaultSiteId		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientId = d.ClientId
			CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
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
