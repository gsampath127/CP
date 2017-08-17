-- =============================================
-- Author:		Noel Dsouza
-- Create date: 2nd-Oct-2015
-- RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck
AS
BEGIN
  SELECT TaxonomyId,COUNT_BIG(*) AS Total
   FROM dbo.TaxonomyAssociation
  GROUP BY TaxonomyId
END