CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteSite
@SiteID int,
@DeletedBy int
AS
BEGIN

     DELETE Site
      WHERE SiteId = @SiteID;
            
    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'Site'
		AND	[Key] = @SiteID AND CUDType = 'D';
  
END