-- =============================================
-- Author:		Noel Dsouza
-- Create date: 22nd-Sep-2015
-- RPV2HostedAdmin_DocumentTypeData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DocumentTypeData_CacheDependencyCheck
AS
BEGIN
  SELECT DISTINCT
	 DocumentTypeID,COUNT_BIG(*)	 
   FROM
     DocumentTypeAssociation
   GROUP BY DocumentTypeID        
END