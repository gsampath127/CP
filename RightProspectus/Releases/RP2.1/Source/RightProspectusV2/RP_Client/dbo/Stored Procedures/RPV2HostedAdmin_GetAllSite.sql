CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllSite
AS
BEGIN
  SELECT DISTINCT SiteID,
		NAME,
		TemplateId,
		DefaultPageID,
		ParentSiteId,
		[Description],
		CONVERT(bit,Case When ClientSettings.DefaultSiteId IS NOT NULL Then 1 Else 0 End) AS 'IsDefaultSite',			
		[SITE].UtcModifiedDate as UtcLastModified,
		[SITE].ModifiedBy as ModifiedBy   
    FROM [SITE]
	LEFT JOIN ClientSettings ON [SITE].SiteId = ClientSettings.DefaultSiteId
END