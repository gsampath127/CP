-- =============================================
-- Author:		Arshdeep
-- Create date: 6th-Oct-2015
-- RPV2HostedAdmin_ClientDocumentTypeData_CacheDependencyCheck
-- =============================================
Create PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentTypeData_CacheDependencyCheck]
AS
BEGIN
 SELECT DISTINCT
	 ClientDocumentTypeID,COUNT_BIG(*)	 
   FROM
     ClientDocumentType
   GROUP BY ClientDocumentTypeID   
   
   Select Distinct  ClientDocumentTypeID,COUNT_BIG(*)	 
   FROM
     ClientDocument
   GROUP BY ClientDocumentTypeID 
END