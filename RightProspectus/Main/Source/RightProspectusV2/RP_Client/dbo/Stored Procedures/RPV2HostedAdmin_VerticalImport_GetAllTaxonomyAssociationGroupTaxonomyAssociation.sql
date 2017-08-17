CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImport_GetAllTaxonomyAssociationGroupTaxonomyAssociation]
@SiteId int 
AS 
BEGIN

	Select TAGTA.TaxonomyAssociationGroupId,TAGTA.TaxonomyAssociationId, TAGTA.[Order],TAG.Name,
		   TA.NameOverride,TA.MarketId  
	from TaxonomyAssociationGroupTaxonomyAssociation TAGTA
	INNER JOIN TaxonomyAssociation TA on TAGTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
	INNER JOIN TaxonomyAssociationGroup TAG on TAGTA.TaxonomyAssociationGroupId = TAG.TaxonomyAssociationGroupId
	WHERE TAG.IsProofing = 1
END
GO