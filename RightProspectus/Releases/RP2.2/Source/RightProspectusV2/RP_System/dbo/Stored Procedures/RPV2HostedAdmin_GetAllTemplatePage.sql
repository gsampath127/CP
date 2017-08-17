CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplatePage
AS
BEGIN
	SELECT  TemplatePage.TemplateId,
		Template.[Name] AS TemplateName,
		Page.PageID,
		Page.Name as PageName,
		Page.[Description] as PageDescription  
	FROM Template
	INNER JOIN TemplatePage ON Template.TemplateID = TemplatePage.TemplateID
	INNER JOIN Page ON TemplatePage.PageId =  Page.PageID
	ORDER BY TemplatePage.TemplateId,Page.PageID
END	