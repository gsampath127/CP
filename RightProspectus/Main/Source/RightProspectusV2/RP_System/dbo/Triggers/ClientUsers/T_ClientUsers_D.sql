-- Delete trigger for ClientUsers
CREATE TRIGGER T_ClientUsers_D
ON dbo.ClientUsers
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientUsers';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.ClientId, d.UserId, 'D'
	FROM	deleted d;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;