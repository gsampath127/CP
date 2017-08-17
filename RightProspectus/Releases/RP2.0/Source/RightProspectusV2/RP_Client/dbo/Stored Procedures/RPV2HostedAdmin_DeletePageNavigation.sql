CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeletePageNavigation]
@PageNavigationID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE PageNavigationVersion
      WHERE PageNavigationId = @PageNavigationID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE PageNavigationVersion
      WHERE PageNavigationId = @PageNavigationID      
     DELETE PageNavigation
       WHERE PageNavigationId = @PageNavigationID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'PageNavigation'
		AND	[Key] = @PageNavigationID AND CUDTYPE = 'D' ;
  END
END