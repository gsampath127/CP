CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplateText_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,ResourceKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplateText
	GROUP BY TemplateID,ResourceKey;

END