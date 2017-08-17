-- Delete trigger for Roles
CREATE TRIGGER T_Roles_D
ON dbo.Roles
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Roles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.RoleId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.RoleId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.RoleId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;