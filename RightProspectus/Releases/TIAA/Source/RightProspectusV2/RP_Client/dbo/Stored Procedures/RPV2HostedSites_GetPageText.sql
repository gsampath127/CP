
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetPageText]
as
BEGIN
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId
	  FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId
	  FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	 Order by 2,1  
 END