-- Insert trigger for UserRoles
CREATE TRIGGER T_UserRoles_I
ON dbo.UserRoles
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.UserId,i.RoleId, 'I', i.ModifiedBy
	FROM	inserted i;
	

			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;