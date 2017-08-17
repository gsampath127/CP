CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveDocumentTypeExternalId  
@DocumentTypeId int,  
@ExternalId nvarchar(100),  
@IsPrimary bit,  
@ModifiedBy int  
  
AS  
BEGIN  
  
	IF @IsPrimary = 1
	BEGIN
		UPDATE DocumentTypeExternalId   
		SET IsPrimary = 0,  
			UtcModifiedDate = GETUTCDATE(),  
			ModifiedBy = @ModifiedBy  
		WHERE DocumentTypeId = @DocumentTypeId AND IsPrimary = 1 AND ExternalId != @ExternalId	
	END
  
	IF EXISTS (SELECT Top 1 DocumentTypeId FROM DocumentTypeExternalId WHERE DocumentTypeId = @DocumentTypeId AND ExternalId = @ExternalId)  
	BEGIN  
		UPDATE DocumentTypeExternalId   
		SET IsPrimary = @IsPrimary,  
			UtcModifiedDate = GETUTCDATE(),  
			ModifiedBy = @ModifiedBy  
		WHERE DocumentTypeId = @DocumentTypeId  AND ExternalId = @ExternalId  
	END	
	ELSE  
	BEGIN  
		INSERT INTO DocumentTypeExternalId(  
			DocumentTypeId,  
			ExternalId,  
			IsPrimary,  
			UtcModifiedDate,        
			ModifiedBy)  
		VALUES
			(@DocumentTypeId,        
			@ExternalId,  
			@IsPrimary,  
			GETUTCDATE(),     
			@ModifiedBy)  
	END
END  