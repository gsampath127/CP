CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroup]
	@ClientDocumentGroupId INT, 
	@deletedBy INT
AS
BEGIN
	DELETE FROM ClientDocumentGroupClientDocument WHERE
		ClientDocumentGroupId = @ClientDocumentGroupId 
	DELETE FROM ClientDocumentGroup WHERE 
		ClientDocumentGroupId = @ClientDocumentGroupId

	UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	(TableName = N'ClientDocumentGroup' or TableName = N'ClientDocumentGroupClientDocument')
				AND	[Key] = @ClientDocumentGroupId
				AND [CUDType] = 'D' 
END