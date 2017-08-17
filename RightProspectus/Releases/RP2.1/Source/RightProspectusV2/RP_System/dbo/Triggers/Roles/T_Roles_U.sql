-- Update trigger for Roles
CREATE TRIGGER T_Roles_U
ON dbo.Roles
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Roles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.RoleId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.RoleId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.RoleId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.RoleId = d.RoleId
			CROSS JOIN (VALUES('Name')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.RoleId = c.[Key]
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
