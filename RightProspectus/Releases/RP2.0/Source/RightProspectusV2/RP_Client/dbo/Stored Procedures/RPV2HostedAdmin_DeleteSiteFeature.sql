CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteSiteFeature] 
@SiteId int,
@SiteKey varchar(200),
@deletedBy int
AS

BEGIN
      DELETE SiteFeature
		   WHERE SiteId = @SiteId AND [Key] = @SiteKey

      UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'SiteFeature'
				AND	[Key] = @SiteId
				AND [CUDType] = 'D' 

END