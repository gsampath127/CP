CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_VerticalImport_GetTaxonomyAssociationGroupUsingSiteId]
@SiteId INT
AS
BEGIN
;WITH TopLevelTaxonomyAssociationGroup 
           AS (SELECT Name , 
					  [Description],
					  CssClass,
					  @SiteId AS SiteId,
					  TaxonomyAssociationGroupId, 
                      ParentTaxonomyAssociationGroupId,
					  ParentTaxonomyAssociationId,
					  UtcModifiedDate,
					  ModifiedBy,
                      0 As Level,
					  [Order],
					  IsProofing
               FROM   TaxonomyAssociationGroup 
               WHERE  SiteID = @SiteId 

               UNION ALL 
               SELECT 
					  TAG.Name , 
					  TAG.[Description],
					  TAG.CssClass,
					  NULL AS SiteId,
					  TAG.TaxonomyAssociationGroupId, 
                      TAG.ParentTaxonomyAssociationGroupId,
					  TAG.ParentTaxonomyAssociationId,
					  TAG.UtcModifiedDate,
					  TAG.ModifiedBy,
					  Level + 1,
					  TAG.[Order],
					  TAG.IsProofing
               FROM   TopLevelTaxonomyAssociationGroup TLTAG
                      INNER JOIN TaxonomyAssociationGroup TAG 
                              ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId
							  )

							  SELECT * FROM TopLevelTaxonomyAssociationGroup
							  where IsProofing = 1

END
Go