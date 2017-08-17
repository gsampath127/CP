CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocument]
@ClientDocumentID int,
@DeletedBy int
AS
BEGIN

	DELETE ClientDocumentData
      WHERE ClientDocumentId = @ClientDocumentID;

	DELETE ClientDocumentGroupClientDocument
	  WHERE ClientDocumentId = @ClientDocumentID;

     DELETE ClientDocument
      WHERE ClientDocumentId = @ClientDocumentID;
            
	UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocumentData'
		AND	[Key] = @ClientDocumentID AND CUDType = 'D';

    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocument'
		AND	[Key] = @ClientDocumentID AND CUDType = 'D';
  
END