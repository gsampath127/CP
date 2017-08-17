
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveSiteText
@SiteTextId int,
@Version int,
@SiteId int,
@ResourceKey varchar(200),
@Text nvarchar(max),
@IsProofing bit,
@ModifiedBy int
as
BEGIN

  DECLARE @CurrentSiteTextId int
  
  IF @SiteTextId = 0 AND @Version = 0
  Begin
    INSERT INTO SiteText(
						SiteId,
						ResourceKey,
						CurrentVersion,
						UtcModifiedDate,						
						ModifiedBy)
				VALUES(@SiteId,						
						@ResourceKey,
						1,		
						GETUTCDATE(),				
						@ModifiedBy)
    
    SET @CurrentSiteTextId = SCOPE_IDENTITY()
    
    INSERT INTO SiteTextVersion(
						SiteTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
				VALUES(
						@CurrentSiteTextId,
						1,
						@Text,
						GETUTCDATE(),						
						@ModifiedBy)
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE SiteTextVersion
			  SET [Text] = @Text			 
			  WHERE SiteTextId = @SiteTextId  AND [Version] = @Version        
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO SiteTextVersion(SiteTextId,[Version],[Text],UtcCreateDate, CreatedBy)
	      VALUES(@SiteTextId,@Version+1,@Text,GETUTCDATE(),@ModifiedBy)	        
	    END      
  END
END