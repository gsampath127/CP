CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSiteNavigation]
@SiteNavigationId int,
@Version int,
@PageId int,
@SiteId int,
@NavigationKey varchar(200),
@NavigationXML XML,
@IsProofing bit,
@ModifiedBy int

as
BEGIN

  DECLARE @CurrentPageTextId INT
  
  IF @SiteNavigationId = 0 AND @Version = 0
  Begin
    INSERT INTO SiteNavigation(
						SiteId,
						PageId,
						NavigationKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				VALUES(@SiteId,
						@PageId,
						@NavigationKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy)
    
    SELECT @CurrentPageTextId = SCOPE_IDENTITY()
    
    INSERT INTO SiteNavigationVersion(
						SiteNavigationId,
						[Version],
						[NavigationXML],		
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageTextId,
						1,
						@NavigationXML,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE SiteNavigationVersion
			  SET [NavigationXML] = @NavigationXML			 
			  WHERE SiteNavigationId = @SiteNavigationId  AND [Version] = @Version     
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO SiteNavigationVersion
	      (
			SiteNavigationId,
			[Version],
			[NavigationXML],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @SiteNavigationId,
	      @Version+1,
	      @NavigationXML,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END