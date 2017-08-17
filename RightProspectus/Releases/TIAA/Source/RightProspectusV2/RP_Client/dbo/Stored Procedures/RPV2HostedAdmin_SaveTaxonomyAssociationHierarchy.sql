-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy
@ParentTaxonomyAssociationId int,
@ChildTaxonomyAssociationId int,
@RelationshipType int,
@Order int,
@ModifiedBy int
AS
BEGIN
  IF NOT EXISTS(SELECT ParentTaxonomyAssociationId
				 FROM TaxonomyAssociationHierachy
				WHERE ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId
				AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
				AND RelationshipType = @RelationshipType
				)
	  BEGIN
	    INSERT INTO TaxonomyAssociationHierachy
					(
					  ParentTaxonomyAssociationId,
					  ChildTaxonomyAssociationId,
					  RelationshipType,
					  [Order]
					 )
		VALUES(
				@ParentTaxonomyAssociationId,
				@ChildTaxonomyAssociationId,
				@RelationshipType,
				@Order
			  )
	  END
  ELSE
     BEGIN
			UPDATE TaxonomyAssociationHierachy
			SET 
				[Order] = @Order
			WHERE 		
				ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId 
				AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
				AND RelationshipType = @RelationshipType			
     END
END