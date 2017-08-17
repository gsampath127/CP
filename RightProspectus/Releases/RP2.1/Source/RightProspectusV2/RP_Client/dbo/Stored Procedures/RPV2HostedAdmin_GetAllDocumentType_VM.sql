CREATE PROCEDURE RPV2HostedAdmin_GetAllDocumentType
AS
BEGIN
SELECT DocumentTypeID,
       Name AS DocumentTypeName,
	   Description,
	   DocPriority,
	   MarketID

FROM   DocumentType
END