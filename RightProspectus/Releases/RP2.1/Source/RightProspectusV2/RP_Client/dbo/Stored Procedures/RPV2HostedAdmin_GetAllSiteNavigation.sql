CREATE PROCEDURE   [dbo].[RPV2HostedAdmin_GetAllSiteNavigation]
AS
BEGIN
	SELECT DISTINCT SiteNavigation.SiteNavigationId, 
		SiteNavigationVersion.[Version],
		[Site].SITEID,
		[Site].Name as SiteName,
		NavigationKey,		 
		SiteNavigation.PageId,
		 CAST(SiteNavigationVersion.[NavigationXml] AS  NVARCHAR(MAX) ) NavigationXML,		 
		 CONVERT(bit,Case When CurrentVersion = SiteNavigationVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForSiteNavigationId,
		 LanguageCulture,
		 SiteNavigation.UtcModifiedDate as UtcLastModified,
		 SiteNavigation.ModifiedBy as ModifiedBy
         FROM SiteNavigation
         INNER JOIN [Site] on SiteNavigation.SiteId = [Site].SiteId
         LEFT OUTER JOIN 
           SiteNavigationVersion ON [SiteNavigation].SiteNavigationId = SiteNavigationVersion.SiteNavigationId 
			AND SiteNavigationVersion.[Version] >= SiteNavigation.CurrentVersion 
         LEFT OUTER JOIN 
           SiteNavigationVersion Proofing ON [SiteNavigation].SiteNavigationId = Proofing.SiteNavigationId 
			AND Proofing.[Version] > SiteNavigation.CurrentVersion 			
	ORDER BY SiteNavigation.SiteNavigationId
END