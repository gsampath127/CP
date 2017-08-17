CREATE PROCEDURE [dbo].RPV2HostedAdmin_StaticTemplatePage_CacheDependencyCheck
AS
BEGIN
 	SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
   	SELECT	PageID, COUNT_BIG(*) AS Total
	FROM	dbo.Page
	GROUP BY PageID;
	
	SELECT TemplateId,PageId,COUNT_BIG(*) AS Total
	FROM dbo.TemplatePage
	GROUP BY TemplateId,PageId
END