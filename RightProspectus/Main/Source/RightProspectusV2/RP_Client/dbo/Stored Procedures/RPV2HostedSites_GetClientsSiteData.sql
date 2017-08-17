

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetClientsSiteData]
AS
BEGIN
  	
	 
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId
	FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]	
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId
	FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  
	 
	 
	SELECT 1 AS IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			[PageText].SiteId,						
			PageId
	FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			[PageText].SiteId,									
			PageId
	FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  
	
	SELECT SiteID,
		Name as SiteName,
		DefaultPageId,
		ParentSiteID,
		TemplateId,		
		CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite			
	FROM 
	 Site 
	LEFT OUTER JOIN ClientSettings on Site.[SiteId] = ClientSettings.[DefaultSiteId]

	SELECT 
		[FileName], 
		Size, 
		MimeType, 
		Data, 
		UtcModifiedDate 
	FROM StaticResource
	
	
	
	SELECT 1 AS IsCurrentProductionVersion,
			SiteID,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM SiteNavigation
	  INNER JOIN SiteNavigationVersion on SiteNavigation.SiteNavigationId = SiteNavigationVersion.SiteNavigationId
			AND SiteNavigation.CurrentVersion = SiteNavigationVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			SiteID,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM SiteNavigation
	  INNER JOIN 
	   (SELECT SiteNavigationID,MAX([version]) as ProofVersion FROM SiteNavigationVersion
		GROUP by SiteNavigationID) as ProofingVersion on SiteNavigation.SiteNavigationId = ProofingVersion.SiteNavigationId  
	  INNER JOIN SiteNavigationVersion on SiteNavigation.SiteNavigationId = SiteNavigationVersion.SiteNavigationId
	   AND SiteNavigationVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  



	SELECT 1 AS IsCurrentProductionVersion,
			SiteID,
			PageId,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM PageNavigation
	  INNER JOIN PageNavigationVersion on PageNavigation.PageNavigationId = PageNavigationVersion.PageNavigationId
			AND PageNavigation.CurrentVersion = PageNavigationVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			SiteID,
			PageId,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM PageNavigation
	  INNER JOIN 
	   (SELECT PageNavigationID,MAX([version]) as ProofVersion FROM PageNavigationVersion
		GROUP by PageNavigationID) as ProofingVersion on PageNavigation.PageNavigationId = ProofingVersion.PageNavigationId  
	  INNER JOIN PageNavigationVersion on PageNavigation.PageNavigationId = PageNavigationVersion.PageNavigationId
	   AND PageNavigationVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1


	--SiteFeature

	SELECT 	SiteID,			
			[Key],
			FeatureMode
	FROM SiteFeature

	--PageFeature

	SELECT 	SiteID,
			PageId,
			[Key],
			FeatureMode
	FROM PageFeature	
END
GO


