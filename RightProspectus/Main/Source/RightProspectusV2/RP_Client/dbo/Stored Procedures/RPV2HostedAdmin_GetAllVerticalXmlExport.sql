-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllVerticalXmlExport
AS
BEGIN
  SELECT VerticalXmlExportId,
		ExportTypes,
		ExportDate,
		ExportedBy,
		ExportDescription,
		[Status]
    FROM VerticalXmlExport
END