CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteBrowserVersion
@BrowserVersionId int,
@DeletedBy int
AS
BEGIN
	DELETE BrowserVersion
	WHERE Id = @BrowserVersionId  

	UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'BrowserVersion'
	AND	[Key] = @BrowserVersionId AND CUDType = 'D';

END

