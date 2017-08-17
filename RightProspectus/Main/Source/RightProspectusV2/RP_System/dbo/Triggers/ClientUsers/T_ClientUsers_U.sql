-- Update trigger for ClientUsers
CREATE TRIGGER T_ClientUsers_U
ON dbo.ClientUsers
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientUsers';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientId, i.UserId, 'U', i.ModifiedBy
	FROM	inserted i;
	
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
