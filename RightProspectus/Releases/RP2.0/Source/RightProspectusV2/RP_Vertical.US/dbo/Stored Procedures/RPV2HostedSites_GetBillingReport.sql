CREATE PROCEDURE [dbo].[RPV2HostedSites_GetBillingReport]
@RPV2HostedSitesTaxonomyBillingReport [dbo].[RPV2HostedSites_BillingReport] READONLY
AS
BEGIN
SELECT DISTINCT CUSIP, SeriesID,	CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'List of Funds',	CASE WHEN DocumentTypeID=18 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'SummaryProspectusFlag' 
FROM ProsTicker
INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID
INNER JOIN @RPV2HostedSitesTaxonomyBillingReport BR ON BR.MarketId=ProsTicker.CUSIP
LEFT  JOIN Document ON ProsTicker.ProspectusID=Document.TaxonomyID and Document.ClientID is null and Document.DocumentTypeID = 18
END
