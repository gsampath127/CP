
CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_StaticTemplateFeature_CacheDependencyCheck]
AS
BEGIN
    SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
	
	SELECT TemplateId,[Key],COUNT_BIG(*) AS Total
	FROM dbo.TemplateFeature
	GROUP BY TemplateId,[Key]
END