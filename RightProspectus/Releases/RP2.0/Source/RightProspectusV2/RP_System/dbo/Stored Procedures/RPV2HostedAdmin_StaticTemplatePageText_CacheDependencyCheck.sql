CREATE PROCEDURE [dbo].RPV2HostedAdmin_StaticTemplatePageText_CacheDependencyCheck
AS
BEGIN
 	SELECT	TemplateID,PageId,ResourceKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageText
	GROUP BY TemplateID,PageId,ResourceKey;

END