CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteSiteNavigation]
@SiteNavigationId int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE SiteNavigationVersion
      WHERE SiteNavigationId = @SiteNavigationId
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE SiteNavigationVersion
      WHERE SiteNavigationId = @SiteNavigationId      
     DELETE SiteNavigation
       WHERE SiteNavigationId = @SiteNavigationId
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'SiteNavigation'
		AND	[Key] = @SiteNavigationId AND CUDTYPE = 'D' ;
  END
END