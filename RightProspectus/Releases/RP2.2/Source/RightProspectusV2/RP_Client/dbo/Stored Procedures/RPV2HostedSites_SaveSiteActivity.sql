
/*
	Procedure Name:[dbo].[RPV2HostedSites_SaveSiteActivity]
	Added By: Noel Dsouza
	Date: 09/20/2015	
	Reason : To add SiteActivity
*/

CREATE PROCEDURE [dbo].[RPV2HostedSites_SaveSiteActivity]
@SiteName nvarchar(100),
@ClientIPAddress varchar(15),
@UserAgentString nvarchar(max),
@HttpMethod varchar(20),
@RequestUriString nvarchar(2083),
@ParsedRequestUriString nvarchar(2083),
@ServerName varchar(15),
@ReferrerUriString nvarchar(2083),
@InitDoc bit,
@RequestBatchId uniqueIdentifier,
@UserId int,
@PageId int,
@Level int,
@DocumentTypeExternalID nvarchar(100),
@TaxonomyExternalId nvarchar(100),
@TaxonomyAssociationGroupId int,
@TaxonomyAssociationId int,
@DocumentTypeId int,
@ClientDocumentGroupId int,
@ClientDocumentId int,
@XBRLDocumentName varchar(100),
@XBRLItemType int,
@BadRequestIssue int,
@BadRequestParameterName nvarchar(200),
@BadRequestParameterValue nvarchar(max)
AS
BEGIN
  DECLARE @UserAgentID int
  
  DECLARE @SiteID int
  
  DECLARE @RequestUri int
  
  DECLARE @ParsedRequestUri int
  
  DECLARE @ReferrerUri int
  
  SELECT @UserAgentID=UserAgentId 
    FROM UserAgent 
   WHERE UserAgentString = @UserAgentString
  
  IF  @UserAgentID IS NULL
  BEGIN
    INSERT INTO UserAgent(UserAgentString)
		VALUES(@UserAgentString)
		
	SELECT @UserAgentID = SCOPE_IDENTITY()
  END
  
  SELECT @SiteID = SiteID 
    FROM [Site] 
  WHERE Name = @SiteName
  
  IF @SiteID IS NULL
  BEGIN
    SELECT @SiteID = DefaultSiteID 
      FROM ClientSettings
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
  
  SELECT @ParsedRequestUri = UriId
    FROM Uri
  WHERE UriString = @ParsedRequestUriString
  
  IF @ParsedRequestUri is NULL
  BEGIN
    INSERT INTO Uri(UriString)
     VALUES(@ParsedRequestUriString)
     SELECT @ParsedRequestUri = SCOPE_IDENTITY()
  END
  
    SELECT @ReferrerUri = UriId
    FROM Uri
  WHERE UriString = @ReferrerUriString
  
  IF @ReferrerUri is NULL
  BEGIN
    INSERT INTO Uri(UriString)
     VALUES(@ReferrerUriString)
     SELECT @ReferrerUri = SCOPE_IDENTITY()
  END
  
  IF @DocumentTypeId is NULL and ISNULL(@DocumentTypeExternalID, '') != ''
  BEGIN
     SELECT @DocumentTypeId=DocumentTypeId
      FROM DocumentTypeExternalId
     WHERE ExternalId = @DocumentTypeExternalID
  END
  
  IF @TaxonomyAssociationId is NULL
  BEGIN
     SELECT @TaxonomyAssociationId= TaxonomyAssociationId
      FROM TaxonomyAssociation TA
     INNER JOIN TaxonomyLevelExternalId  TLE on TA.TaxonomyId = TLE.TaxonomyId 
			AND TLE.[Level] = TA.[Level]
     WHERE TLE.ExternalId = @TaxonomyExternalId AND TLE.[Level]= @Level
  END
  
  INSERT INTO SiteActivity
   (
	 SiteID,
     ClientIPAddress,
     UserAgentId,
     RequestUtcDate,
     HttpMethod,
     RequestUri,
     ParsedRequestUri,
     ServerName,
     ReferrerUri,
     InitDoc,
     RequestBatchId,
     UserId,
     PageId,
     TaxonomyAssociationGroupId,
     TaxonomyAssociationId,
     DocumentTypeId,
     ClientDocumentGroupId,
     ClientDocumentId,
     XBRLDocumentName,
     XBRLItemType
     )
   VALUES
     (
       @SiteID,
       @ClientIPAddress,
       @UserAgentID,
       GETUTCDATE(),
       @HttpMethod,
       @RequestUri,
       @ParsedRequestUri,
       @ServerName,
       @ReferrerUri,
       @InitDoc,
       @RequestBatchId,
       @UserId,
       @PageId,
       @TaxonomyAssociationGroupId,
       @TaxonomyAssociationId,
       @DocumentTypeId,
       @ClientDocumentGroupId,
       @ClientDocumentId,
       @XBRLDocumentName,
       @XBRLItemType
     )  

	 IF(ISNULL(@BadRequestIssue, 0) <> 0)
	 BEGIN

		INSERT INTO BadRequest
           ([SiteActivityId]
           ,[RequestIssue]
           ,[ParameterName]
           ,[ParameterValue])
		VALUES
           (SCOPE_IDENTITY()
           ,@BadRequestIssue
           ,@BadRequestParameterName
           ,@BadRequestParameterValue)
		   		
	 END

END