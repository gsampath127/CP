-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_VerticalXmlExportData_CacheDependencyCheck
AS
BEGIN
  SELECT VerticalXmlExportId,COUNT_BIG(*) AS Total
    FROM VerticalXmlExport
  GROUP BY VerticalXmlExportId
END