CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplateFeature]
AS
BEGIN

   SELECT  TemplateFeature.TemplateId,
		Template.[Name] AS TemplateName,
		TemplateFeature.[Key] as FeatureKey,
		TemplateFeature.[Description] as FeatureDescription
    FROM Template
	INNER JOIN TemplateFeature ON Template.TemplateID = TemplateFeature.TemplateID
    ORDER BY TemplateFeature.TemplateId
END