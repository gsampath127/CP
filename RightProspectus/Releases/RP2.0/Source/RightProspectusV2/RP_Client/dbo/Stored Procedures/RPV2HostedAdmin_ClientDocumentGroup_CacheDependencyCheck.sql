CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentGroup_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentGroupId,COUNT_BIG(*)	 
   FROM
     ClientDocumentGroup
   GROUP BY ClientDocumentGroupId        
END