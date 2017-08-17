-- =============================================
-- Author:		Noel Dsouza
-- Create date: 22nd-Sep-2015
-- RPV2HostedAdmin_GetAllDocumentType
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllDocumentType
AS
BEGIN
  SELECT DISTINCT
	 DocumentTypeID,
	 HeaderText as DocumentTypeName
   FROM
     DocumentTypeAssociation
      INNER JOIN ClientSettings on DocumentTypeAssociation.SiteId = ClientSettings.DefaultSiteId   
END