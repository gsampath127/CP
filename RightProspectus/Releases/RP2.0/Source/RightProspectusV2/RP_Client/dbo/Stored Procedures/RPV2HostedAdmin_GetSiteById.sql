-- =============================================
-- Author:		Arshdeep Kaur
-- Create date: 9-sept-2015
-- Description:	Getting site data by siteid
-- =============================================

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSiteById] 

				 @siteId int 
-- exec [dbo].[RPV2HostedAdmin_GetSiteById]	1	     
--DROP PROCEDURE [RPV2ClientDb1_GetSiteById]

AS BEGIN 

			SELECT 

				Site.Name,
				Site.TemplateId,
				Site.DefaultPageId,
				Site.ParentSiteId,
				Site.[Description],
				Site.UtcModifiedDate,
				Site.ModifiedBy

				FROM Site
				WHERE  Site.SiteId = @siteId
	
END

