CREATE VIEW [dbo].[DocumentType]
AS
SELECT     DocumentTypeID, DocTypeDesc as Name, DocTypeDesc AS Description, DocPriority,
 DocTypeId AS MarketID
FROM         dbo.DocType


GO
