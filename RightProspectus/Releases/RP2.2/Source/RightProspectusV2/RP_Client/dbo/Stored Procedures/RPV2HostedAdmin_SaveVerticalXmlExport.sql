--Author : Noel Dsouza
--Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlExport
@VerticalXmlExportId int,
@ExportTypes int,
@ExportXml XML,
@ExportedBy int,
@ExportDescription nvarchar(400),
@Status int
AS
BEGIN
  IF @VerticalXmlExportId = 0
	  BEGIN
	    INSERT INTO VerticalXmlExport
			(
			 ExportTypes,
			 ExportXml,
			 ExportDate,
			 ExportedBy,
			 ExportDescription,
			 [Status]
			)
		VALUES
			(
			 @ExportTypes,
			 @ExportXml,
			 GETUTCDATE(),
			 @ExportedBy,
			 @ExportDescription,
			 @Status			
			)
	  END
  ELSE
      BEGIN
		UPDATE VerticalXmlExport
		SET ExportTypes = @ExportTypes,
			ExportXml = @ExportXml,
			[Status] = @Status	
		WHERE VerticalXmlExportId = @VerticalXmlExportId	
      END
END