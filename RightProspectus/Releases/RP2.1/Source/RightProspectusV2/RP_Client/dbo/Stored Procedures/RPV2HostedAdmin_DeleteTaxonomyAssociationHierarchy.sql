/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy
@ParentTaxonomyAssociationId int,
@ChildTaxonomyAssociationId int,
@RelationshipType int,
@deletedBy int
AS
BEGIN
  DELETE TaxonomyAssociationHierachy
  WHERE ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId
	AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
	AND RelationshipType = @RelationshipType

    UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'TaxonomyAssociationHierachy'
				AND	[Key] = @ParentTaxonomyAssociationId
				AND SecondKey = @ChildTaxonomyAssociationId
				AND ThirdKey = @RelationshipType
				AND [CUDType] = 'D' 
END