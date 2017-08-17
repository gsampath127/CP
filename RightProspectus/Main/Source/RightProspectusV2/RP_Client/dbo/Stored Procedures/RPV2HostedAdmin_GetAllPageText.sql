
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllPageText
AS
BEGIN

  SELECT DISTINCT PageText.PageTextId, 
		PageTextVersion.[Version],
		PageText.PageId,		
		[Site].TemplateId,				
		[Site].SITEID,
		[Site].Name as SiteName,
		 ResourceKey,		 
		 PageTextVersion.[Text],		 
		 CONVERT(bit,Case When CurrentVersion =PageTextVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForPageTextID,
		 LanguageCulture,
		 PageText.UtcModifiedDate as UtcLastModified,
		 PageText.ModifiedBy as ModifiedBy
         FROM PageText
         INNER JOIN [Site] on PageText.SiteId = [Site].SiteId         
         LEFT OUTER JOIN 
           PageTextVersion ON [PageText].PageTextId = PageTextVersion.PageTextId 
			AND PageTextVersion.[Version] >= PageText.CurrentVersion 
         LEFT OUTER JOIN 
           PageTextVersion Proofing ON [PageText].PageTextId = Proofing.PageTextId 
			AND Proofing.[Version] > PageText.CurrentVersion 			
	ORDER BY PageText.PageTextId
	



END