/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DequeueVerticalXmlImport]
	Added By: Noel Dsouza
	Date: 10/22/2015	
	Reason : To Dequeue VerticalXMLImport
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DequeueVerticalXmlImport
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @DequeueVerticalXmlImport TABLE
	(
		VerticalXmlImportId int,
		ImportedBy int,
		ImportDate datetime,
		ImportTypes int,
		ImportXml xml,
		ImportExportXML xml,
		[Status] int
	)

	DECLARE @BATCHSIZE INT
	SET @BATCHSIZE = 1

	UPDATE TOP(@BATCHSIZE) VerticalXmlImport WITH (UPDLOCK, READPAST)
	 SET STATUS = 1
	 OUTPUT INSERTED.VerticalXmlImportId,
			 INSERTED.ImportedBy, 
			 INSERTED.ImportDate, 
			 INSERTED.ImportTypes,
			 INSERTED.ImportXml,
			 INSERTED.ImportXml AS ImportExportXML, 
			 INSERTED.[Status] INTO @DequeueVerticalXmlImport
	 WHERE STATUS = 0
 
	 IF @@ROWCOUNT = 0
	 BEGIN
		 UPDATE TOP(@BATCHSIZE) VerticalXmlImport WITH (UPDLOCK, READPAST)
		 SET STATUS = 5
		 OUTPUT INSERTED.VerticalXmlImportId,
				 INSERTED.ImportedBy, 
				 INSERTED.ImportDate, 
				 INSERTED.ImportTypes,
				 INSERTED.ImportXml,
				 VerticalXmlExport.ExportXml AS ImportExportXML,				 
				 INSERTED.[Status]  INTO @DequeueVerticalXmlImport
		 FROM VerticalXmlImport 
		 INNER JOIN VerticalXmlExport ON VerticalXmlImport.ExportBackupId = VerticalXmlExport.VerticalXmlExportId
		 WHERE VerticalXmlImport.[Status] = 4
	 END
	 
	 SELECT * FROM @DequeueVerticalXmlImport
END