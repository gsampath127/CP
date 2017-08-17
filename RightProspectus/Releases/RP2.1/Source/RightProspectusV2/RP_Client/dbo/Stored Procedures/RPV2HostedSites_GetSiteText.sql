CREATE PROCEDURE [dbo].[RPV2HostedSites_GetSiteText]
as
BEGIN

	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId,
			[Site].Name,
			CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite			
	  FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]	
	  INNER JOIN [Site]	ON SiteText.SiteId = [Site].SiteId
	  LEFT OUTER JOIN [ClientSettings] ON [ClientSettings].DefaultSiteId = [Site].SiteId
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId,
			[Site].Name,
			CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite	
	  FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	  INNER JOIN [Site]	ON SiteText.SiteId = [Site].SiteId
	  LEFT OUTER JOIN [ClientSettings] ON [ClientSettings].DefaultSiteId = [Site].SiteId
	 Order by 2,1  
 END