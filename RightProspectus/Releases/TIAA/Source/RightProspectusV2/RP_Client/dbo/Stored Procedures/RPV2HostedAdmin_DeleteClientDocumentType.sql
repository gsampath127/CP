Create PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentType]
@ClientDocumentTypeID int,
@DeletedBy int
AS
BEGIN

     DELETE ClientDocumentType
      WHERE ClientDocumentTypeId = @ClientDocumentTypeID;
            
    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocumentType'
		AND	[Key] = @ClientDocumentTypeID AND CUDType = 'D';
  
END

