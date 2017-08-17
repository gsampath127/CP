
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageText]
@PageTextId int,
@Version int,
@PageID int,
@SiteId int,
@ResourceKey varchar(200),
@Text nvarchar(max),
@IsProofing bit,
@ModifiedBy int
as
BEGIN

  
  
  DECLARE @CurrentPageTextId INT
  
  IF @PageTextId = 0 AND @Version = 0
  Begin
    INSERT INTO PageText(
						SiteId,
						PageId,
						ResourceKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				VALUES(@SiteId,
						@PageID,
						@ResourceKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy)
    
    SELECT @CurrentPageTextId = SCOPE_IDENTITY()
    
    INSERT INTO PageTextVersion(
						PageTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageTextId,
						1,
						@Text,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE PageTextVersion
			  SET [Text] = @Text			 
			  WHERE PageTextId = @PageTextId AND [Version] = @Version        
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO PageTextVersion
	      (
			PageTextId,
			[Version],
			[Text],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @PageTextId,
	      @Version+1,
	      @Text,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END