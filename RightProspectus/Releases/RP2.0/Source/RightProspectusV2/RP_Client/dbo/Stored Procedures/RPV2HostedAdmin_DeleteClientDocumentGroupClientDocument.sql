--create procedure [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]
--as
--begin
--	select 
--		ClientDocumentGroupId,
--		Name,
--		[Description],
--		ParentClientDocumentGroupId,
--		CssClass,
--		UtcModifiedDate,
--		ModifiedBy
--	from ClientDocumentGroup
--end

--sp_rename '[dbo].[RPV2HostedAdmin_GetAllUrlClientDocumentGroup]','[dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]'

--CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocument]
--AS
--BEGIN
--	SELECT 
--		ClientDocumentId,
--		ClientDocumentTypeId,
--		[FileName],
--		MimeType,
--		IsPrivate,
--		ContentUri,
--		Name,
--		[Description],
--		UtcModifiedDate,
--		ModifiedBy
--	FROM ClientDocument
--END

--exec [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]

--CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroupClientDocument]
--AS
--BEGIN
--	SELECT 
--		ClientDocumentGroupId,
--		ClientDocumentId,
--		[Order],
--		UtcModifiedDate,
--		ModifiedBy
--	FROM ClientDocumentGroupClientDocument
--END

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