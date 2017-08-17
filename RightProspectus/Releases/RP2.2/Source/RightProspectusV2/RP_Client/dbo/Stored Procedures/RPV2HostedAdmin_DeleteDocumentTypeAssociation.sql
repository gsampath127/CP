/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteDocumentTypeAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteDocumentTypeAssociation
@DocumentTypeAssociationId int,
@deletedBy int
AS
BEGIN
  DELETE DocumentTypeAssociation
  WHERE DocumentTypeAssociationId = @DocumentTypeAssociationId

    UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'DocumentTypeAssociation'
				AND	[Key] = @DocumentTypeAssociationId
				AND [CUDType] = 'D' 
END