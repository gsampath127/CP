CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplatePageNavigation]

AS

BEGIN

SELECT  TemplateId,
		PageId,
		NavigationKey,
		Name,
		XslTransform,
		DefaultNavigationXml,
		[Description]		
     	FROM TemplatePageNavigation

End