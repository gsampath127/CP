CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroupClientDocument]
	@ClientDocumentGroupId INT,
	@ClientDocumentId INT,
	@deletedBy INT
AS
BEGIN
	DELETE FROM ClientDocumentGroupClientDocument WHERE
		ClientDocumentGroupId = @ClientDocumentGroupId and
		ClientDocumentId = @ClientDocumentId

		UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'ClientDocumentGroupClientDocument'
				AND	[Key] = @ClientDocumentGroupId
				AND [CUDType] = 'D' 
END