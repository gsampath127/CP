-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_SaveTaxonomyAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_SaveTaxonomyAssociation
@TaxonomyAssociationId int,
@Level int,
@TaxonomyId int,
@SiteId int,
@ParentTaxonomyAssociationId int,
@NameOverride nvarchar(200),
@DescriptionOverride nvarchar(40),
@CssClass varchar(50),
@MarketId nvarchar(50),
@ModifiedBy int
AS
BEGIN
 IF @TaxonomyAssociationId = 0
	  BEGIN
	    INSERT INTO TaxonomyAssociation
				(
					[Level],
					TaxonomyId,
					SiteId,
					ParentTaxonomyAssociationId,
					NameOverride,
					DescriptionOverride,					
					CssClass,
					MarketId,
					UtcModifiedDate,
					ModifiedBy				
				)
		VALUES
			 (
				@Level,
				@TaxonomyId,
				@SiteId,
				@ParentTaxonomyAssociationId,
				@NameOverride,
				@DescriptionOverride,
				@CssClass,
				@MarketId,
				GETUTCDATE(),
				@ModifiedBy
			 )
	  END
  ELSE
      BEGIN
		UPDATE TaxonomyAssociation
			SET [Level] = @Level,
				TaxonomyId = @TaxonomyId,
				SiteId = @SiteId,
				ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId,
				NameOverride = @NameOverride,
				DescriptionOverride = @DescriptionOverride,
				CssClass = @CssClass,
				MarketId = @MarketId,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @ModifiedBy
		WHERE TaxonomyAssociationId = @TaxonomyAssociationId
		
      END

END