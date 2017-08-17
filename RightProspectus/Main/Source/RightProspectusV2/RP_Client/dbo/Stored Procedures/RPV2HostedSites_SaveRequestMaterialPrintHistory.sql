CREATE  PROCEDURE [dbo].[RPV2HostedSites_SaveRequestMaterialPrintHistory]
@SiteName nvarchar(100),
@ClientCompanyName nvarchar(100) ,   
@ClientFirstName nvarchar(100) ,    
@ClientMiddleName nvarchar(100) ,    
@ClientLastName nvarchar(100) ,    
@ClientFullName nvarchar(200) ,    
@Address1 nvarchar(100) ,    
@Address2 nvarchar(100) ,    
@City nvarchar(100) ,    
@StateOrProvince nvarchar(100) ,    
@PostalCode nvarchar(20) ,    
@UniqueID uniqueidentifier , 
@RequestBatchId uniqueidentifier, 
@RequestUriString nvarchar(500),    
@UserAgent nvarchar(250) ,    
@IPAddress nvarchar(250) ,    
@Referer nvarchar(max) ,    
@RequestMaterialPrintProsDetail TT_RequestMaterialProsDetail READONLY  
AS
BEGIN  
  
	DECLARE @SiteID int
	SELECT @SiteID = DefaultSiteId from ClientSettings	IF EXISTS(SELECT SiteId FROM [Site] WHERE Name = @SiteName)	BEGIN		SELECT @SiteID = SiteId FROM [Site] WHERE Name = @SiteName	END

	DECLARE @UserAgentID int, @RequestUri int, @ReferrerUri int
  
	SELECT @UserAgentID = UserAgentId 
	FROM UserAgent 
	WHERE UserAgentString = @UserAgent
  
	IF  @UserAgentID IS NULL
	BEGIN
		INSERT INTO UserAgent(UserAgentString)
		VALUES(@UserAgent)	
			
		SELECT @UserAgentID = SCOPE_IDENTITY()
	END

	SELECT @RequestUri = UriId
	FROM Uri
	WHERE UriString = @RequestUriString
  
	IF @RequestUri is NULL
	BEGIN
	INSERT INTO Uri(UriString)
	VALUES(@RequestUriString)
	SELECT @RequestUri = SCOPE_IDENTITY()     
	END

	SELECT @ReferrerUri = UriId
	FROM Uri
	WHERE UriString = @Referer
  
	IF @ReferrerUri is NULL
	BEGIN
		INSERT INTO Uri(UriString)
		VALUES(@Referer)

		SELECT @ReferrerUri = SCOPE_IDENTITY()
	END    
  
	DECLARE  @identityRequestMaterialPrintHistoryID int  
  
  
  
	INSERT INTO RequestMaterialPrintHistory(  
		SiteId,
		ClientCompanyName,  
		ClientFirstName,
		ClientMiddleName,   
		ClientLastName,    
		ClientName,    
		Address1,    
		Address2,    
		City,    
		StateOrProvince,    
		PostalCode,    
		UniqueID,  
		RequestBatchId,
		RequestUri,    
		UserAgentId,
		IPAddress,    
		ReferrerUri
	)
	VALUES( 
		@SiteID,
		@ClientCompanyName ,     
		@ClientFirstName ,     
		@ClientMiddleName ,     
		@ClientLastName ,    
		@ClientFullName ,    
		@Address1 ,    
		@Address2 ,    
		@City ,    
		@StateOrProvince ,    
		@PostalCode ,    
		@UniqueID ,
		@RequestBatchId, 
		@RequestUri,   
		@UserAgentID,    
		@IPAddress,    
		@ReferrerUri
	)  


	SET @identityRequestMaterialPrintHistoryID = SCOPE_IDENTITY() 
  
	INSERT INTO RequestMaterialPrintProsDetail(  
		RequestMaterialPrintHistoryID ,		
		TaxonomyAssociationId ,    
		DocumentTypeId ,    
		Quantity    
	)
		Select @identityRequestMaterialPrintHistoryID , TaxonomyAssociationId, DocumentTypeId, Quantity
		From @RequestMaterialPrintProsDetail dt
	END  
  
  