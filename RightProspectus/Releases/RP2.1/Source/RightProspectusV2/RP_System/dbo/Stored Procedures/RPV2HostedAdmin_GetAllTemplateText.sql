CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplateText
AS
BEGIN
  SELECT TemplateId,
		ResourceKey,
		[Name],
		IsHtml,
		DefaultText,
		[Description]		
	FROM TemplateText
END