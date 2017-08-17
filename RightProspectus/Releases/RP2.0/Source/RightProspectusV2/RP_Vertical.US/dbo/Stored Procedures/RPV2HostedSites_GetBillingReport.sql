﻿CREATE PROCEDURE [dbo].[RPV2HostedSites_GetBillingReport]
@RPV2HostedSitesTaxonomyBillingReport [dbo].[RPV2HostedSites_BillingReport] READONLY
AS
BEGIN
SELECT DISTINCT CUSIP, SeriesID,
FROM ProsTicker
INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID
INNER JOIN @RPV2HostedSitesTaxonomyBillingReport BR ON BR.MarketId=ProsTicker.CUSIP
LEFT  JOIN Document ON ProsTicker.ProspectusID=Document.TaxonomyID and Document.ClientID is null and Document.DocumentTypeID = 18
END