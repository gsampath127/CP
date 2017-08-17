-- =============================================
-- Author:		
-- Create date: 
-- RPV2HostedAdmin_ClientDocumentData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentData_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentID,COUNT_BIG(*)	 
   FROM
     ClientDocument
   GROUP BY ClientDocumentID        
END