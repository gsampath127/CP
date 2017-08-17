/* Company is Level 0,Fund(prospectus) is Level 1 ,Share Class is Level 3*/
CREATE VIEW [dbo].[Document]
AS
SELECT     dbo.ProsDocs.ProsDocId AS DocumentId, dbo.DocType.DocumentTypeID, 1 AS TaxonomyLevel, dbo.ProsDocs.ProsId AS TaxonomyID, 
                      SUBSTRING(CASE dbo.ProsDocs.ProsDocUseAltURL WHEN 1 THEN ProsDocAltURL ELSE ProsDocURL END, 
                      LEN(CASE dbo.ProsDocs.ProsDocUseAltURL WHEN 1 THEN ProsDocAltURL ELSE ProsDocURL END) - CHARINDEX('/', 
                      REVERSE(CASE dbo.ProsDocs.ProsDocUseAltURL WHEN 1 THEN ProsDocAltURL ELSE ProsDocURL END)) + 2, 
                      LEN(CASE dbo.ProsDocs.ProsDocUseAltURL WHEN 1 THEN ProsDocAltURL ELSE ProsDocURL END)) AS FileName, 
                      CASE dbo.ProsDocs.ProsDocUseAltURL WHEN 1 THEN ProsDocAltURL ELSE ProsDocURL END AS ContentUri, NULL AS MimeType, NULL AS Size, 
                      CASE ProsDocLevel WHEN 1 THEN 0 ELSE 1 END AS IsPrivate, NULL AS Name, NULL AS Description, 
                      dbo.ProsDocs.ClientID,
                      dbo.ProsDocs.PageCount, dbo.ProsDocs.PageSizeHeight, 
                      dbo.ProsDocs.PageSizeWidth
FROM         dbo.ProsDocs INNER JOIN
                      dbo.DocType ON dbo.ProsDocs.ProsDocTypeId = dbo.DocType.DocTypeId


GO
