-- Update trigger for UserRoles
CREATE TRIGGER T_UserRoles_U
ON dbo.UserRoles
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.UserId,i.RoleId, 'U', i.ModifiedBy
	FROM	inserted i;
	
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
