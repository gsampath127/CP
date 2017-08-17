-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DocumentTypeExternalIdData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	DocumentTypeId,ExternalId, COUNT_BIG(*) AS Total
	FROM	dbo.DocumentTypeExternalId
	GROUP BY DocumentTypeId,ExternalId;
END