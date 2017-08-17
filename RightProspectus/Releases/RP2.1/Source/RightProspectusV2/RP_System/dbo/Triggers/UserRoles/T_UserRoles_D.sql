-- Delete trigger for UserRoles
CREATE TRIGGER T_UserRoles_D
ON dbo.UserRoles
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.UserId,d.RoleId, 'D'
	FROM	deleted d;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;