

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplatePageFeature]

AS

BEGIN



   SELECT  TemplatePageFeature.TemplateId,

		Template.[Name] AS TemplateName,
		TemplatePageFeature.PageId,


		TemplatePageFeature.[Key] as FeatureKey,

		TemplatePageFeature.[Description] as FeatureDescription

    FROM Template

	INNER JOIN TemplatePageFeature ON Template.TemplateID = TemplatePageFeature.TemplateID

    ORDER BY TemplatePageFeature.TemplateId

END