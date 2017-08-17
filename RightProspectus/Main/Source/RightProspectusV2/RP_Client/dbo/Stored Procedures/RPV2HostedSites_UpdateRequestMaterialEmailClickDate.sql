CREATE PROCEDURE [dbo].[RPV2HostedSites_UpdateRequestMaterialEmailClickDate]
@UniqueId UNIQUEIDENTIFIER ,   
@DocumentTypeId INT,
@TaxonomyAssociationId INT output
AS  
BEGIN  
  
   
	DECLARE @FClickDateUtc DATETIME   
	SELECT @FClickDateUtc = FClickDateUtc FROM RequestMaterialEmailHistory WHERE UniqueID = @UniqueId  
   
	IF @FClickDateUtc IS NULL  
	BEGIN  
		UPDATE RequestMaterialEmailHistory  
		SET FClickDateUtc = GetUtcDate()  
		WHERE UniqueID = @UniqueId  
	END  
  
   
	DECLARE @SClickDateUtc datetime   
	SELECT @SClickDateUtc = SClickDateUtc From RequestMaterialEmailHistory  
	INNER JOIN RequestMaterialEmailProsDetail ON RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
	WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId  
   
	IF @SClickDateUtc IS NULL
	BEGIN  
		UPDATE RequestMaterialEmailProsDetail  
		SET SClickDateUtc = GetUtcDate()
		FROM RequestMaterialEmailProsDetail  
		INNER JOIN RequestMaterialEmailHistory on RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
		WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId  
	END
	
	SELECT @TaxonomyAssociationId = RequestMaterialEmailProsDetail.TaxonomyAssociationId
	FROM RequestMaterialEmailProsDetail  
	INNER JOIN RequestMaterialEmailHistory on RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
	WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId 
  
END
GO  