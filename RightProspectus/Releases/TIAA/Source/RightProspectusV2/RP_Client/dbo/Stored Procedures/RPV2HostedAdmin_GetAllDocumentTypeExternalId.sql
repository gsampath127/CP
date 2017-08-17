
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllDocumentTypeExternalId]
AS
BEGIN
SELECT DTE.DocumentTypeId,
	   DTE.ExternalId,
	   DTE.UtcModifiedDate as UtcLastModified,
	   ISNULL(DTE.ModifiedBy,0) as ModifiedBy,
	   DTE.IsPrimary
FROM DocumentTypeExternalId DTE
WHERE DTE.DocumentTypeId <> -1
END
GO