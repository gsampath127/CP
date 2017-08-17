/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveDocumentTypeAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To add DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveDocumentTypeAssociation]
@DocumentTypeAssociationId int,
@DocumentTypeId int,
@SiteId int,
@TaxonomyAssociationId int,
@Order int,
@HeaderText nvarchar(100),
@LinkText nvarchar(100),
@DescriptionOverride nvarchar(400),
@CssClass varchar(50),
@MarketId nvarchar(50),
@ModifiedBy int
AS
BEGIN
  IF @DocumentTypeAssociationId = 0
	  BEGIN
	    INSERT INTO DocumentTypeAssociation
				(
					DocumentTypeId,
					SiteId,
					TaxonomyAssociationId,
					[Order],
					HeaderText,
					LinkText,
					DescriptionOverride,
					CssClass,
					MarketId,
					UtcModifiedDate,
					ModifiedBy				
				)
		VALUES
			 (
				@DocumentTypeId,
				@SiteId,
				@TaxonomyAssociationId,
				@Order,
				@HeaderText,
				@LinkText,
				@DescriptionOverride,
				@CssClass,
				@MarketId,
				GETUTCDATE(),
				@ModifiedBy
			 )
	  END
  ELSE
      BEGIN
		UPDATE DocumentTypeAssociation
			SET DocumentTypeId = @DocumentTypeId,
				SiteId = @SiteId,
				TaxonomyAssociationId = @TaxonomyAssociationId,
				[Order] = @Order,
				HeaderText = @HeaderText,
				LinkText = @LinkText,
				DescriptionOverride = @DescriptionOverride,
				CssClass = @CssClass,
				MarketId = @MarketId,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @ModifiedBy
		WHERE DocumentTypeAssociationId = @DocumentTypeAssociationId
		
      END
END
