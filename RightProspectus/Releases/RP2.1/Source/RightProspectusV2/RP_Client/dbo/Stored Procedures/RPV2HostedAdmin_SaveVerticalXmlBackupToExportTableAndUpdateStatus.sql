--Created BY : Noel Dsouza
--Created Date : 10/22/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlBackupToExportTableAndUpdateStatus
@VerticalXmlImportId int,
@ExportXml XML,
@ExportedBy int
AS
BEGIN
  DECLARE @VerticalXmlExportId INT
  
  INSERT INTO VerticalXmlExport(ExportTypes,ExportXml,ExportDate,ExportedBy,ExportDescription,[Status])
  VALUES(1,@ExportXml,GETUTCDATE(),@ExportedBy,'Backup of XML Before Import',2)
  
  SET @VerticalXmlExportId = @@IDENTITY
  
  UPDATE VerticalXmlImport
    SET ExportBackupId = @VerticalXmlExportId,
		[Status] = 2
	WHERE VerticalXmlImportId = @VerticalXmlImportId    
END