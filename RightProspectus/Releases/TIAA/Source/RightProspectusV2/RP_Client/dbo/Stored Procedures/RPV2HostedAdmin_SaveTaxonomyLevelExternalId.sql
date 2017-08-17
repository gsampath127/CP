/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveTaxonomyLevelExternalId]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the TaxonomyLevelExternalId
*/ 
  
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveTaxonomyLevelExternalId]  
 @Level int,
 @TaxonomyID int,
 @ExternalId NVARCHAR(100),  
 @modifiedBy int,  
 @IsPrimary bit,
 @SendDocumentUpdate bit
AS  
BEGIN

	IF @IsPrimary = 1
	BEGIN
		UPDATE TaxonomyLevelExternalId  
		SET   
			IsPrimary = 0, 
			SendDocumentUpdate=1, 
			ModifiedBy = @ModifiedBy,  
			UtcModifiedDate = GETUTCDATE()  
		WHERE  [Level] = @Level AND TaxonomyId = @TaxonomyID AND IsPrimary = 1 AND ExternalId != @ExternalId
	
	END

	IF EXISTS (SELECT Top 1 TaxonomyId  FROM TaxonomyLevelExternalId WHERE [Level] = @Level AND TaxonomyId = @TaxonomyID AND ExternalId = @ExternalId)  
	BEGIN	
		UPDATE TaxonomyLevelExternalId  
		SET   
			IsPrimary = @IsPrimary,
			SendDocumentUpdate=@SendDocumentUpdate,  
			ModifiedBy = @ModifiedBy,  
			UtcModifiedDate = GETUTCDATE()  
		WHERE  [Level] = @Level AND TaxonomyId =@TaxonomyID AND ExternalId=@ExternalId  
	END
	ELSE  
	BEGIN
	
		INSERT INTO TaxonomyLevelExternalId(
			[Level],
			TaxonomyId, 
			ExternalId,
			utcModifiedDate,  
			ModifiedBy,
			IsPrimary,
			SendDocumentUpdate) 
		VALUES(
			@Level,
			@TaxonomyID,
			@ExternalId,
			GETUTCDATE(), 
			@modifiedBy,
			@IsPrimary,
			@SendDocumentUpdate)  
	END  
  
END
