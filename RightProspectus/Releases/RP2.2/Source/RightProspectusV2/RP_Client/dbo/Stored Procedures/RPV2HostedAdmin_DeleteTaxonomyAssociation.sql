/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete TaxonomyAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteTaxonomyAssociation
@TaxonomyAssociationId int,
@deletedBy int
AS
BEGIN
  DELETE TaxonomyAssociation
  WHERE TaxonomyAssociationId = @TaxonomyAssociationId

   UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'TaxonomyAssociation'
				AND	[Key] = @TaxonomyAssociationId
				AND [CUDType] = 'D' 
END