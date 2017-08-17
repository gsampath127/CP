CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetSearchComboDocType
AS
BEGIN
SELECT 
	   DISTINCT DTE.DocumentTypeId,
	   DT.Name as DocumentTypeName
	FROM 
	   DocumentTypeExternalId DTE
	INNER JOIN 
	   RPV2USDB.dbo.DocumentType DT
	        ON DTE.DocumentTypeId = DT.DocumentTypeID
END