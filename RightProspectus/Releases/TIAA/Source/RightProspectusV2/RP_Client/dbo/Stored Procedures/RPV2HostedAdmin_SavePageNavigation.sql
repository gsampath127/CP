
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageNavigation]
@PageNavigationId int,
@SiteId int,
@PageID int,
@NavigationKey varchar(200),
@IsProofing bit,
@ModifiedBy int,
@NavigationXml xml,
@Version int
as
BEGIN 
  
  DECLARE @CurrentPageNavigationId INT
  
  IF @PageNavigationId = 0 AND @Version = 0
  Begin
    INSERT INTO PageNavigation( 
	SiteId,
	PageID ,
	NavigationKey ,
	CurrentVersion,
	UtcModifiedDate,
	ModifiedBy)
		  VALUES(@SiteId,
		  @PageID,
		  @NavigationKey,
		  1,
		  GETUTCDATE(),
		  @ModifiedBy)
    
    SELECT @CurrentPageNavigationId = SCOPE_IDENTITY()
    
    INSERT INTO PageNavigationVersion(
						PageNavigationId,
						[Version],	
						NavigationXml,	
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageNavigationId,
						1,
						@NavigationXml,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE PageNavigationVersion
			  SET [NavigationXml] = @NavigationXml			 
			  WHERE PageNavigationId = @PageNavigationId AND [Version] = @Version 
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO PageNavigationVersion
	      (
			PageNavigationId,
			[Version],
			[NavigationXml],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @PageNavigationId,
	      @Version+1,
	      @NavigationXml,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END