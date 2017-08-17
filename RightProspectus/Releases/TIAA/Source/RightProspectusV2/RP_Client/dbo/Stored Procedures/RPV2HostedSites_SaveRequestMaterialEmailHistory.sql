CREATE PROCEDURE [dbo].[RPV2HostedSites_SaveRequestMaterialEmailHistory]  
@SiteName nvarchar(100),
@RecipEmail nvarchar(300),  
@UniqueID uniqueidentifier,
@RequestBatchId uniqueidentifier,  
@RequestUriString nvarchar(500), 
@UserAgent nvarchar(500),  
@IPAddress nvarchar(500),  
@Referer nvarchar(500),  
@Sent bit,  
@RequestMaterialEmailProsDetail TT_RequestMaterialProsDetail READONLY  
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
	 
	DECLARE @identityRequestMaterialEmailHistoryId int  
	INSERT INTO  RequestMaterialEmailHistory(
		SiteId,  
		RecipEmail,  
		UniqueID,
		RequestBatchId,
		RequestUri,  
		UserAgentId,  
		IPAddress,  
		ReferrerUri,  
		[Sent]  
	)  
	VALUES(  
		@SiteID,
		@RecipEmail,  
		@UniqueID,
		@RequestBatchId,
		@RequestUri, 
		@UserAgentID,  
		@IPAddress,  
		@ReferrerUri,  
		@Sent  
	)  
  
	SET @identityRequestMaterialEmailHistoryId=SCOPE_IDENTITY()  
  
	INSERT INTO RequestMaterialEmailProsDetail(
		RequestMaterialEmailHistoryId,  
		TaxonomyAssociationId,  
		DocumentTypeId
	)
		Select @identityRequestMaterialEmailHistoryId,
			   TaxonomyAssociationId,  
			   DocumentTypeId 
		From @RequestMaterialEmailProsDetail dt		
        
END