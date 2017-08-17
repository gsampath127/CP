-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllVerticalXmlImport
AS
BEGIN
  SELECT VerticalXmlImportId,
		ImportTypes,
		ImportDate,
		ImportedBy,
		ExportBackupId,
		ImportDescription,
		[Status]
    FROM VerticalXmlImport
END