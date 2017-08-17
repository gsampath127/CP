CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentGroupClientDocument_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentGroupId,ClientDocumentId,COUNT_BIG(*)	 
   FROM
     ClientDocumentGroupClientDocument
   GROUP BY ClientDocumentGroupId , ClientDocumentId      
END