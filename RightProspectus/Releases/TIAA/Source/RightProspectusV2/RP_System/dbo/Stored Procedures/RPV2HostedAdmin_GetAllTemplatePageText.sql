CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplatePageText
AS
BEGIN
  SELECT TemplateId,
		PageId,
		ResourceKey,
		[Name],
		IsHtml,
		DefaultText,
		[Description]		
	FROM TemplatePageText
END