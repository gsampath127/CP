CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_SiteFeature_CacheDependencyCheck]
AS
BEGIN
  SELECT SiteId , [Key] ,  COUNT_BIG(*) AS Total
  FROM dbo.SiteFeature
  GROUP BY  SiteId , [Key]
END