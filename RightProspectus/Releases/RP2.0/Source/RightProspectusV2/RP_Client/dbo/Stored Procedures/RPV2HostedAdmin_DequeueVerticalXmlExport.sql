/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DequeueVerticalXmlExport]
	Added By: Noel Dsouza
	Date: 10/13/2015	
	Reason : To Dequeue VerticalXMLExport
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DequeueVerticalXmlExport
AS
BEGIN
SET NOCOUNT ON

DECLARE @BATCHSIZE INT
SET @BATCHSIZE = 1

UPDATE TOP(@BATCHSIZE) VerticalXmlExport WITH (UPDLOCK, READPAST)
 SET STATUS = 1
 OUTPUT INSERTED.VerticalXmlExportId,
		 INSERTED.ExportedBy, 
		 INSERTED.ExportDate, 
		 INSERTED.ExportTypes,
		 INSERTED.ExportDescription, 
		 INSERTED.[Status]
 WHERE STATUS = 0
END