-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetVerticalXmlExportByID
@VerticalXmlExportId int
AS
BEGIN
  SELECT VerticalXmlExportId,
		ExportTypes,
		ExportDate,
		ExportXml,
		ExportedBy,
		ExportDescription,
		[Status]
    FROM VerticalXmlExport
  WHERE VerticalXmlExportId = @VerticalXmlExportId
END