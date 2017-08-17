-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_VerticalXmlImportData_CacheDependencyCheck
AS
BEGIN
  SELECT VerticalXmlImportId,COUNT_BIG(*) AS Total
    FROM VerticalXmlImport
  GROUP BY VerticalXmlImportId
END