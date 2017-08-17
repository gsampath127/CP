CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllSiteFeature]
AS
BEGIN
    SELECT DISTINCT 
	         SiteId,
			 [Key] as SiteKey,
			 FeatureMode,
			 UtcModifiedDate as UtcLastModified,
		     ModifiedBy as ModifiedBy
             FROM
			 dbo.[SiteFeature]
END