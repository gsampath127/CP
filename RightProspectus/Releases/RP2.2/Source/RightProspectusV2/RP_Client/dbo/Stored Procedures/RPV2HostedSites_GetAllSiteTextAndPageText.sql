
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetAllSiteTextAndPageText]
as
BEGIN

	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteId,
			-1 as PageID,
			'S' as Entity
	  FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteId,
			-1 as PageID,
			'S' as Entity
	  FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
	   AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	UNION
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId,
			'P' as Entity	
	  FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId,
			'P' as Entity
	  FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	 Order by 6,2,1  
 END