
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllSiteText
AS
BEGIN

  SELECT DISTINCT SiteText.SiteTextId, 
		SiteTextVersion.[Version],
		[Site].SITEID,
		[Site].Name as SiteName,
		 ResourceKey,		 
		 SiteTextVersion.[Text],		 
		 CONVERT(bit,Case When CurrentVersion =SiteTextVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForSiteTextID,
		 LanguageCulture,
		 SiteText.UtcModifiedDate as UtcLastModified,
		 SiteText.ModifiedBy as ModifiedBy
         FROM SiteText
         INNER JOIN [Site] on SiteText.SiteId = [Site].SiteId
         LEFT OUTER JOIN 
           SiteTextVersion ON [SiteText].SiteTextId = SiteTextVersion.SiteTextId 
			AND SiteTextVersion.[Version] >= SiteText.CurrentVersion 
         LEFT OUTER JOIN 
           SiteTextVersion Proofing ON [SiteText].SiteTextId = Proofing.SiteTextId 
			AND Proofing.[Version] > SiteText.CurrentVersion 			
	ORDER BY SiteText.SiteTextId
	



END