CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteDocumentTypeExternalId
@DocumentTypeId int,
@ExternalId nvarchar(100),
@DeletedBy int
AS
BEGIN


     DELETE DocumentTypeExternalId
      WHERE DocumentTypeId = @DocumentTypeId
       AND ExternalId = @ExternalId      
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'DocumentTypeExternalId'
		AND	[Key] = @DocumentTypeId
		AND [SecondKey] = @ExternalId
		AND [CUDType] = 'D'; 

END