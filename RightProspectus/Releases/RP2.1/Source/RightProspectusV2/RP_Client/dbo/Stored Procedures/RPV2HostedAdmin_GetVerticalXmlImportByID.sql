-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE dbo.[RPV2HostedAdmin_GetVerticalXmlImportByID]
@VerticalXmlImportId int
AS
BEGIN
  SELECT VerticalXmlImportId,
		ImportTypes,
		ImportDate,
		ImportXml,
		ImportedBy,
		ImportDescription,
		[Status],
		ExportBackupId
    FROM VerticalXmlImport
  WHERE VerticalXmlImportId = @VerticalXmlImportId
END