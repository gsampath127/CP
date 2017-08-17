
CREATE procedure [dbo].[RPV2HostedAdmin_GetUrlRewriteText]

@UrlRewriteId int
as
BEGIN
   Select 
		UrlRewriteId 
		, MatchPattern 
		, RewriteFormat 
		, UtcModifiedDate 
		, ModifiedBy
		,PatternName 
   from 
		UrlRewrite
   where 
		UrlRewriteId = @UrlRewriteId

END