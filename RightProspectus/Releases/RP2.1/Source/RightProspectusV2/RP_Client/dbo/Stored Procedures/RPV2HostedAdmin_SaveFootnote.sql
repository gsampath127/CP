-- =============================================
-- Author:		Noel Dsouza
-- Create date: 13th-Oct-2015
-- RPV2HostedAdmin_SaveFootnote
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_SaveFootnote
@FootnoteId int,
@TaxonomyAssociationId int,
@TaxonomyAssociationGroupId int,
@LanguageCulture varchar(50),
@Text nvarchar(max),
@Order int,
@ModifiedBy int
AS
BEGIN
	IF @FootnoteId = 0
		BEGIN
		  INSERT INTO Footnote(
							 FootnoteId
							  ,TaxonomyAssociationId
							  ,TaxonomyAssociationGroupId
							  ,LanguageCulture
							  ,[Text]
							  ,[Order]
							  ,UtcModifiedDate
							  ,ModifiedBy
						)
			VALUES(
					@FootnoteId,
					@TaxonomyAssociationId,
					@TaxonomyAssociationGroupId,
					@LanguageCulture,
					@Text,
					@Order,
					GETUTCDATE(),
					@ModifiedBy
				  )
		END
	ELSE
		BEGIN
			UPDATE Footnote
				 SET TaxonomyAssociationId = @TaxonomyAssociationId,
					 TaxonomyAssociationGroupId = @TaxonomyAssociationGroupId,
					 LanguageCulture = @LanguageCulture,
					 [Text] = @Text,
					 [Order] = @Order,
					 UtcModifiedDate = GETUTCDATE(),
					 ModifiedBy = @ModifiedBy
				WHERE FootnoteId = @FootnoteId			
		END  
END