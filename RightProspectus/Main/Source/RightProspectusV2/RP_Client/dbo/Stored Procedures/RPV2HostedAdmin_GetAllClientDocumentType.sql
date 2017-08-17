CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentType]
AS
BEGIN

   SELECT DISTINCT
		ClientDocumentTypeId,
		Name,
		[Description],
		UtcModifiedDate as UtcLastModified,
		ModifiedBy as ModifiedBy,
		HostedDocumentsDisplayCount,
		FTPName,
		FTPUsername,
		FTPPassword,
		IsSFTP
	FROM ClientDocumentType     

END

