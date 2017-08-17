-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
--RPV2HostedAdmin_GetAllUrlRewrite
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllUrlRewrite]
AS
BEGIN
	SELECT 
		   UrlRewriteId,
		   MatchPattern,
		   RewriteFormat,
		   PatternName,		
		   UtcModifiedDate as UtcLastModified,
		   ModifiedBy as ModifiedBy
	  FROM [dbo].[UrlRewrite]
		  
END 