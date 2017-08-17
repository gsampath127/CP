CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationLinks]
	@RPV2HostedSitesTaxonomyAssociationLinkDataType [dbo].[RPV2HostedSitesTaxonomyAssociationLinkDataType] READONLY
AS
BEGIN
	SELECT 
		[ClientTable].[TaxonomyID],
		CASE WHEN [ClientTable].[IsNameOverrideProvided]=0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName
	FROM [dbo].[Prospectus]  
		INNER JOIN [dbo].[Company] ON [dbo].[Prospectus].[CompanyID] = Company.CompanyID
		INNER JOIN @RPV2HostedSitesTaxonomyAssociationLinkDataType ClientTable ON Prospectus.Prosid = ClientTable.TaxonomyID
END