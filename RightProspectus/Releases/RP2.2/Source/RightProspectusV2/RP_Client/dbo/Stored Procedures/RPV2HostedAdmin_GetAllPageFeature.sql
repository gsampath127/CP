

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllPageFeature]
AS
BEGIN
    SELECT DISTINCT 
	         SiteId,
			 PageId,
			 [Key] as PageKey,
			 FeatureMode,
			 UtcModifiedDate as UtcLastModified,
		     ModifiedBy as ModifiedBy
             FROM
			 dbo.[PageFeature]
END