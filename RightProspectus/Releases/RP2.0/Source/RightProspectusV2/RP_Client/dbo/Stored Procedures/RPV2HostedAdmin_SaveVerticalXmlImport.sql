--Author : Krishnan KV
--Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlImport
@VerticalXmlImportId int,
@ImportTypes int,
@ImportXml XML,
@ImportedBy int,
@ImportDescription nvarchar(400),
@Status int
AS
BEGIN
  IF @VerticalXmlImportId = 0
	  BEGIN
	    INSERT INTO VerticalXmlImport
			(
			 ImportTypes,
			 ImportXml,
			 ImportDate,
			 ImportedBy,
			 ImportDescription,
			 [Status]
			)
		VALUES
			(
			 @ImportTypes,
			 @ImportXml,
			 GETUTCDATE(),
			 @ImportedBy,
			 @ImportDescription,
			 @Status			
			)
	  END
  ELSE
      BEGIN
		UPDATE VerticalXmlImport
		SET [Status] = @Status	
		WHERE VerticalXmlImportId = @VerticalXmlImportId	
      END
END

