CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplateSiteNavigation]

AS

BEGIN

SELECT  TemplateId,
		NavigationKey,
		Name,
		XslTransform,
		DefaultNavigationXml,
		[Description]		
     	FROM TemplateNavigation

End