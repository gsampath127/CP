CREATE PROCEDURE [dbo].[RPV2HostedSites_GetClientDocumentByClientDocumentId]
@ClientDocumentId int
AS
BEGIN

	SELECT [FileName], MimeType, Data, ClientDocumentData.UtcModifiedDate 
	FROM ClientDocument
	INNER JOIN ClientDocumentData ON ClientDocument.ClientDocumentId = ClientDocumentData.ClientDocumentId
	WHERE ClientDocument.ClientDocumentId = @ClientDocumentId

END
GO

