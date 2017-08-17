create PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplatePageNavigation_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageNavigation
	GROUP BY TemplateID,NavigationKey;

END