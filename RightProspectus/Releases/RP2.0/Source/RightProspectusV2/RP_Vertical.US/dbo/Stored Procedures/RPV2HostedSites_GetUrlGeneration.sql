create PROCEDURE [dbo].[RPV2HostedSites_GetUrlGeneration]
@RPV2HostedSitesTaxonomyUrlGeneration [dbo].[RPV2HostedSitesTaxonomyAssociationLinkDataType] READONLY
AS
BEGIN
SELECT URLG.TaxonomyId,
CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'List of Funds' 
FROM Prospectus
INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID
INNER JOIN @RPV2HostedSitesTaxonomyUrlGeneration URLG ON Prospectus.ProsID = URLG.TaxonomyId

END