-- =============================================
-- Author:		
-- Create date: 
-- EXEC RPV2HostedAdmin_GetAllClientDocument
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocument]
AS
BEGIN

SELECT  DISTINCT

	CD.ClientDocumentId
      ,CD.ClientDocumentTypeId
	  ,CDT.Name AS ClientDocumentTypeName
      ,CD.FileName
      ,CD.MimeType
      ,CD.IsPrivate
      ,CD.ContentUri
      ,CD.Name
      ,CD.Description
      ,CD.UtcModifiedDate AS UtcLastModified
      ,CD.ModifiedBy
  FROM 
		[dbo].[ClientDocument] CD
		INNER JOIN ClientDocumentType CDT
		ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId

END