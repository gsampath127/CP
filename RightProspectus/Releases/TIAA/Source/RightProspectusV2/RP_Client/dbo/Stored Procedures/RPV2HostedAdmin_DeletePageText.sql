CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeletePageText
@PageTextID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE PageTextVersion
      WHERE PageTextId = @PageTextID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE PageTextVersion
      WHERE PageTextId = @PageTextID      
     DELETE PageText
       WHERE PageTextId = @PageTextID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'PageText'
		AND	[Key] = @PageTextID AND CUDTYPE = 'D' ;
  END
END