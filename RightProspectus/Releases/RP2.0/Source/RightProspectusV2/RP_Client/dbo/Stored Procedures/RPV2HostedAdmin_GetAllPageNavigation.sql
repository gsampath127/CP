CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllPageNavigation]
AS
BEGIN

 SELECT DISTINCT PageNavigation.PageNavigationId, 
		PageNavigationVersion.[Version],
		PageNavigation.PageId,		
		[Site].TemplateId,				
		[Site].SITEID,
		[Site].Name as SiteName,
		NavigationKey,		 
		CAST(PageNavigationVersion.[NavigationXml] AS  NVARCHAR(MAX) ) NavigationXML,	 
		 CONVERT(bit,Case When CurrentVersion =PageNavigationVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForPageNavigationID,
		 LanguageCulture,
		 PageNavigation.UtcModifiedDate as UtcLastModified,
		 PageNavigation.ModifiedBy as ModifiedBy
         FROM PageNavigation
         INNER JOIN [Site] on PageNavigation.SiteId = [Site].SiteId         
         LEFT OUTER JOIN 
           PageNavigationVersion ON [PageNavigation].PageNavigationId = PageNavigationVersion.PageNavigationId 
			AND PageNavigationVersion.[Version] >= PageNavigation.CurrentVersion 
         LEFT OUTER JOIN 
           PageNavigationVersion Proofing ON [PageNavigation].PageNavigationId = Proofing.PageNavigationId 
			AND Proofing.[Version] > PageNavigation.CurrentVersion 			
	ORDER BY PageNavigation.PageNavigationId

END