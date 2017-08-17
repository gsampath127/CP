
CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_PageFeature_CacheDependencyCheck]
AS
BEGIN
  SELECT PageId, SiteId , [Key] ,  COUNT_BIG(*) AS Total
  FROM dbo.pageFeature
  GROUP BY  PageId , SiteId, [Key]
END