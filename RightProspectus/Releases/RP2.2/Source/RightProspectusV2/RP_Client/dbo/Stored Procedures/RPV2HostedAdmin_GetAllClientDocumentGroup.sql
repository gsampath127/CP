CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]
AS
BEGIN
	SELECT 
		[ClientDocumentGroup].ClientDocumentGroupId,
		[ClientDocumentGroup].[Name],
		[ClientDocumentGroup].[Description],
		ParentClientDocumentGroupId,
		CssClass,
		[ClientDocumentGroup].UtcModifiedDate as UtcLastModified,
		[ClientDocumentGroup].ModifiedBy,
		[ClientDocumentGroupClientDocument].ClientDocumentId,
		[Order],
		[FileName],
		[MimeType]
	FROM [dbo].[ClientDocumentGroup] 
	LEFT JOIN [dbo].[ClientDocumentGroupClientDocument]
	ON [ClientDocumentGroup].ClientDocumentGroupId = [ClientDocumentGroupClientDocument].ClientDocumentGroupId
	LEFT JOIN [dbo].[ClientDocument]
	ON [ClientDocumentGroupClientDocument].ClientDocumentId = [ClientDocument].ClientDocumentId
END