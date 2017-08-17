/*
	Procedure Name:[dbo].[RPV2HostedAdmin_UrlRewriteData_CacheDependencyCheck]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : Cache Dependency UrlRewriteId
*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_UrlRewriteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	UrlRewriteId, COUNT_BIG(*) AS Total
	FROM	dbo.UrlRewrite
	GROUP BY UrlRewriteId;
END