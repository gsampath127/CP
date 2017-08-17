CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplateSiteNavigation_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplateNavigation
	GROUP BY TemplateID,NavigationKey;

END