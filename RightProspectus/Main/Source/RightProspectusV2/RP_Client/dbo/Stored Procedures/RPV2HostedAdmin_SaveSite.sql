/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveSite]
	Added By: Noel Dsouza
	Date: 09/15/2015
	Reason : To add and update the Site
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSite]	
	@siteId int,
	@ClientId int,
	@name NVARCHAR(100),
	@templateId int,
	@defaultPageId int,	
	@IsDefaultSite bit,
	@parentSiteId int,
	@description NVARCHAR(400),
	@ModifiedBy int,
	@TemplateTextData TT_TemplateText readonly,
	@TemplatePageTextData TT_TemplatePageText readonly,
	@TemplateNavigationData TT_TemplateNavigation readonly,
	@TemplatePageNavigationData TT_TemplatePageNavigation readonly
AS
BEGIN
	
	IF @siteId = 0 
		BEGIN
			INSERT INTO Site(name,
							templateId,
							defaultPageId,
							parentSiteId,
							[description],
							utcModifiedDate,
							ModifiedBy) 
					VALUES (@name,
							@templateId,
							@defaultPageId,
							@parentSiteId,
							@description,
							GETUTCDATE(),
							@modifiedBy)

			SET @siteId = SCOPE_IDENTITY()

			IF @IsDefaultSite = 1
			BEGIN
				IF EXISTS(SELECT DefaultSiteId from ClientSettings)  
				BEGIN  
					UPDATE ClientSettings  
					SET DefaultSiteId = @siteId ,   
						[UtcModifiedDate] = GETUTCDATE(),  
						[ModifiedBy] = @modifiedBy					
				END  
				ELSE  
				BEGIN  
					INSERT INTO [ClientSettings]  
					([ClientId]  
					,[DefaultSiteId]  
					,[UtcModifiedDate]  
					,[ModifiedBy])  
					VALUES  
					(@ClientId  
					,@siteId  
					,GETUTCDATE(),  
					@modifiedBy)
				END			
			END

			
			--INSERT Default Site Setting - START

			--INSERT Default Document Types
			INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy, DescriptionOverride)
				Values(18,@SiteId,1,'Summary Prospectus','Summary Prospectus','SP', GetUTCDate(), @modifiedBy, '')
			INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
				Values(11,@SiteId,2,'Prospectus','Prospectus','P', GetUTCDate(), @modifiedBy, '')
			INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
				Values(15,@SiteId,3,'Statement ofnewlineAdditional Information','Statement ofnewlineAdditional Information','S', GetUTCDate(), @modifiedBy, '')
			INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
				Values(16,@SiteId,5,'Semiannual Report','Semiannual Report','SAR', GetUTCDate(), @modifiedBy, '')
			INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
				Values(1,@SiteId,4,'Annual Report','Annual Report','AR', GetUTCDate(), @modifiedBy, '')			

			INSERT INTO DocumentTypeExternalID(DocumentTypeId,ExternalId,IsPrimary,UtcModifiedDate,ModifiedBy)
				SELECT DocumentTypeAssociation.DocumentTypeId, MarketId, 0, GetUTCDate(), @modifiedBy
				FROM DocumentTypeAssociation
				LEFT JOIN DocumentTypeExternalID ON DocumentTypeAssociation.DocumentTypeId = DocumentTypeExternalID.DocumentTypeId
				WHERE DocumentTypeExternalID.DocumentTypeId IS NULL

			--INSERT Default Site Text for newly added sites

			INSERT INTO SiteText(
						SiteId,
						ResourceKey,
						CurrentVersion,
						UtcModifiedDate,						
						ModifiedBy)
					SELECT @SiteId,						
						ResourceKey,
						1,		
						GETUTCDATE(),				
						@ModifiedBy
					FROM @TemplateTextData


			INSERT INTO SiteTextVersion(
						SiteTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
					SELECT SiteText.SiteTextId,
						1,
						tt.DefaultText,
						GETUTCDATE(),						
						@ModifiedBy
					FROM @TemplateTextData TT
					INNER JOIN SiteText on SiteText.SiteId = @siteId AND SiteText.ResourceKey = TT.ResourceKey


			--INSERT Default Page Text for newly added sites


			INSERT INTO PageText(
						SiteId,
						PageId,
						ResourceKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				SELECT  @SiteId,
						PageID,
						ResourceKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy
				FROM @TemplatePageTextData
    
			INSERT INTO PageTextVersion(
								PageTextId,
								[Version],
								[Text],		
								UtcCreateDate,				
								CreatedBy)
					SELECT	PageText.PageTextId,
								1,
								tpt.DefaultText,		
								GETUTCDATE(),				
								@ModifiedBy
					FROM @TemplatePageTextData TPT
					INNER JOIN PageText ON PageText.SiteId = @SiteId AND PageText.PageId = TPT.PageId  AND PageText.ResourceKey = TPT.ResourceKey


			--INSERT Default Site Navigation for newly added sites

			INSERT INTO SiteNavigation(
						SiteId,
						PageId,
						NavigationKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				SELECT  @SiteId,
						NULL,
						NavigationKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy
				FROM @TemplateNavigationData
    
    
			INSERT INTO SiteNavigationVersion(
								SiteNavigationId,
								[Version],
								[NavigationXML],		
								UtcCreateDate,				
								CreatedBy)
					SELECT 	SiteNavigation.SiteNavigationId,
								1,
								DefaultNavigationXml,		
								GETUTCDATE(),				
								@ModifiedBy
					FROM @TemplateNavigationData TN
					INNER JOIN SiteNavigation ON SiteNavigation.SiteId = @SiteId AND SiteNavigation.NavigationKey = TN.NavigationKey


			--INSERT Default Page Navigation for newly added sites

			INSERT INTO PageNavigation( 
								SiteId,
								PageID ,
								NavigationKey ,
								CurrentVersion,
								UtcModifiedDate,
								ModifiedBy)
					SELECT  @SiteId,
							PageID,
							NavigationKey,
							1,
							GETUTCDATE(),
							@ModifiedBy
					FROM @TemplatePageNavigationData
    
			INSERT INTO PageNavigationVersion(
								PageNavigationId,
								[Version],	
								NavigationXml,	
								UtcCreateDate,				
								CreatedBy)
						SELECT PageNavigation.PageNavigationId,
								1,
								DefaultNavigationXml,		
								GETUTCDATE(),				
								@ModifiedBy
						FROM @TemplatePageNavigationData TPN
						INNER JOIN PageNavigation ON PageNavigation.SiteId = @siteId AND PageNavigation.PageId = TPN.PageId AND PageNavigation.NavigationKey = TPN.NavigationKey

			--INSERT Default Site Setting END


		END
	ELSE
		BEGIN
			UPDATE Site 
			SET name=@name,
				templateId=@templateId,
				defaultPageId=@defaultPageId,
				parentSiteId=@parentSiteId,
				[description]=@description,
				utcModifiedDate=GETUTCDATE(),
				ModifiedBy=@modifiedBy
			WHERE SiteId = @siteId

			IF @IsDefaultSite = 1
			BEGIN
				UPDATE ClientSettings
				SET DefaultSiteId = @siteId,
				utcModifiedDate = GETUTCDATE(),
				ModifiedBy = @modifiedBy		
			END
		END

END
Go