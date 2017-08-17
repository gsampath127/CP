ALTER table [dbo].[TaxonomyAssociation] Add TabbedPageNameOverride nvarchar(200) NULL
Go
ALTER table TaxonomyAssociationGroup ADD [Order] INT NULL
Go
-------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationGroups]   --  exec RPV2HostedSites_GetTaxonomyAssociationGroups 6
	@SiteName NVARCHAR(100)=NULL
AS
BEGIN

	DECLARE @SiteID INT	
	IF @SiteName IS NULL
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END
	
	;WITH TopLevelTaxonomyAssociationGroup 
           AS (SELECT Name , 
					  TaxonomyAssociationGroupId, 
                      ParentTaxonomyAssociationGroupId,
					  [Order],
					  0 As Level
               FROM   TaxonomyAssociationGroup 
               WHERE  SiteID = @SiteID
               UNION ALL 
               SELECT TAG.Name,
			   TAG.TaxonomyAssociationGroupId, 
                      TAG.ParentTaxonomyAssociationGroupId,
					  TAG.[Order],
					  Level + 1
               FROM   TopLevelTaxonomyAssociationGroup TLTAG
                      INNER JOIN TaxonomyAssociationGroup TAG 
                              ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)


	SELECT TLTAG.[LEVEL] As 'GroupLevel',
		   TLTAG.TaxonomyAssociationGroupId,
		   TLTAG.ParentTaxonomyAssociationGroupId,
		   TLTAG.Name, 
		   TLTAG.[Order] As 'GroupOrder',
		   TA.TaxonomyAssociationId,   
		   TA.TaxonomyID,
		   TA.NameOverride,
		   TA.DescriptionOverride,
		   TA.TaxonomyNameOverRide,
		   TA.TaxonomyDesciptionOverRide,
		   TA.TaxonomyCssClass,
		   TA.[Order] As 'TaxonomyOrder',
		   TA.DocumentTypeHeaderText,
		   TA.DocumentTypeLinkText,
		   TA.DocumentTypeDescriptionOverride,
		   TA.DocumentTypeCssClass,
		   TA.DocumentTypeOrder,
		   TA.DocumentTypeMarketId,
		   TA.DocumentTypeId,
		   TA.FootnoteText,
		   TA.FootnoteOrder
		   FROM TopLevelTaxonomyAssociationGroup TLTAG
	LEFT JOIN (
				SELECT DISTINCT
					TAGTA.TaxonomyAssociationGroupId AS 'TAGTATaxonomyAssociationGroupId',	
					TA.TaxonomyAssociationId,   
					TA.TaxonomyID,
					TA.NameOverride,
					TA.DescriptionOverride,
					TA.NameOverride AS TaxonomyNameOverRide,
					TA.DescriptionOverride AS TaxonomyDesciptionOverRide,
					TA.CssClass AS TaxonomyCssClass,
					TAGTA.[Order],
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,         
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
					CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
					CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
					--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) as DocumentTypeExternalID,
					Footnote.[Text] FootnoteText,
					Footnote.[Order] FootnoteOrder		
				FROM [TaxonomyAssociationGroupTaxonomyAssociation] TAGTA
					INNER JOIN [dbo].[TaxonomyAssociation] TA ON TAGTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
					RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
					LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
					--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
					--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
					LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
					LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]		
				WHERE TA.TaxonomyAssociationId IS NOT NULL
	)TA ON TLTAG.TaxonomyAssociationGroupId = TA.TAGTATaxonomyAssociationGroupId
	ORDER BY 1 desc, 2,3,5, 16

END
GO




ALTER PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomySpecificDocumentFrame]
@ExternalId nvarchar(100)=null,
@TAID INT = NULL,
@SiteName nvarchar(100)=null
AS
BEGIN
	DECLARE @SiteID int
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  	BEGIN
  	    SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  	END


	SELECT DISTINCT
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			ISNULL(TA.TabbedPageNameOverride, TA.NameOverride) as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.CssClass as TaxonomyCssClass,
  	 				CASE 
  	 					WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText 
  	 					ELSE TALevelDTA.HeaderText 
  	 				END  AS DocumentTypeHeaderText,            
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText 
						ELSE TALevelDTA.LinkText
					END	AS DocumentTypeNameOverride,
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride 
						ELSE TALevelDTA.DescriptionOverride
					END	AS DocumentTypeDescriptionOverride,         
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass 
						ELSE TALevelDTA.CssClass
					END AS DocumentTypeCssClass,
					CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId 
						ELSE TALevelDTA.DocumentTypeId 
					END AS DocumentTypeId,         			 
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] 
						ELSE TALevelDTA.[Order]
					END AS DocumentTypeOrder,
					ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID
	FROM 
		TaxonomyAssociation TA 
			LEFT OUTER JOIN TaxonomyLevelExternalID TLE  ON TA.TaxonomyID = TLE.TaxonomyID AND TA.[Level] = TLE.[Level] 
			RIGHT OUTER JOIN DocumentTypeAssociation SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
			LEFT OUTER JOIN DocumentTypeAssociation TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			LEFT OUTER JOIN DocumentTypeExternalID SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
			LEFT OUTER JOIN DocumentTypeExternalID TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
					AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)
						   
END							   
Go

ALTER PROCEDURE [dbo].[RPV2HostedSites_URLGeneration]
@MarketID NVARCHAR(100) = NULL,
@SearchSiteName NVARCHAR(100) = NULL
AS  
BEGIN

	DECLARE @SiteDetails TABLE(RowNum int, SiteId int, Name nvarchar(200), DefaultPageId int)	
	DECLARE @URLDetails TABLE(SiteName nvarchar(100),FundName nvarchar(200), TaxonomyId int, Level int,TAExternalID NVARCHAR(100),
								DTExternalID NVARCHAR(100), DocumentType NVARCHAR(100), DocumentTypeOrder int, XBRLFeatureMode int)

	DECLARE @SiteCount int, @RowCount int = 1, @DefaultPageID int, @SiteID int, @SiteName NVARCHAR(100), @XBRLFeatureMode int = 0
	
	-- FETCH Site Details based on Search creteria
	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]
	WHERE @SearchSiteName IS NULL OR Name = @SearchSiteName
	
	--Get the total site counts
	SELECT @SiteCount = COUNT(*) from @SiteDetails
	
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN
	
		SELECT @SiteID = SiteID, @SiteName = Name, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount

		SELECT @XBRLFeatureMode = FeatureMode FROm SiteFeature where [Key] = 'XBRL' and SiteId = @SiteID

		IF @DefaultPageID = 1 --TAL case
		BEGIN		
			--logic for TAL
			INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
			SELECT DISTINCT
			@SiteName  AS SiteName,
			PTA.NameOverride AS FundName,
			PTA.TaxonomyId, PTA.Level,
			TALE.ExternalId,
		
			ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,		
			@XBRLFeatureMode
      		FROM [dbo].[TaxonomyAssociationHierachy] TAH

			INNER JOIN [dbo].[TaxonomyAssociation] PTA ON TAH.ParentTaxonomyAssociationId = PTA.TaxonomyAssociationID 
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID 
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = PTA.TaxonomyAssociationID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON PTA.TaxonomyId  = TALE.TaxonomyId AND PTA.[Level] = TALE.[Level]

			WHERE (PTA.TaxonomyAssociationId IS NOT NULL)
			And(PTA.MarketId=@MarketID or @MarketID is NULL)
			    
		UNION
        SELECT DISTINCT
	    @SiteName AS SiteName,
	    CTA.NameOverride,
		CTA.TaxonomyId, CTA.Level,
		TALE.ExternalId,
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
		
	  	CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
	    @XBRLFeatureMode
    	FROM [dbo].[TaxonomyAssociationHierachy] TAH

		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = CTA.TaxonomyAssociationID		
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
    	LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON CTA.TaxonomyId  = TALE.TaxonomyId AND CTA.[Level] = TALE.[Level]

	    WHERE CTA.TaxonomyAssociationId IS NOT NULL
	    And(CTA.MarketId=@MarketID or @MarketID is NULL)	    

		END
		
		
		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN

			--Logic for TAD

			INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		
			SELECT DISTINCT 
			@SiteName  AS SiteName,
			TA.NameOverride as FundName,	
			TA.TaxonomyId, TA.Level,
			TALE.ExternalID,
		
			ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
			@XBRLFeatureMode		
			FROM [dbo].[TaxonomyAssociation] TA 
				RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
				LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
				LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
				LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
				LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			WHERE TA.TaxonomyAssociationId IS NOT NULL AND TA.SiteId = @SiteID
			And(TA.MarketId=@MarketID or @MarketID is NULL)

		End
		ELSE IF @DefaultPageID = 7 --TAGD case
		BEGIN

			--Logic for TAD

			INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		
			SELECT DISTINCT 
			@SiteName  AS SiteName,
			TA.NameOverride as FundName,	
			TA.TaxonomyId, TA.Level,
			TALE.ExternalID,
		
			ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
			CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
			@XBRLFeatureMode		
			FROM [dbo].[TaxonomyAssociation] TA 
				RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
				LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
				LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
				LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
				LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			WHERE TA.TaxonomyAssociationId IS NOT NULL And(TA.MarketId=@MarketID or @MarketID is NULL)
		End
		  
		SET @RowCount = @RowCount + 1
	End


	SELECT * FROM @URLDetails t
	ORDER BY t.SiteName, t.FundName, t.TAExternalID, t.DocumentTypeOrder

END
Go


-------------------------------------------------------------------------------------
-- INSERT BELOW in System SB

insert into TemplatePageText values(1,7,'TAGD_GroupHeaderText','TAGD_GroupHeaderText',1,'Prospectus and Fund Documents','')
insert into TemplatePageText values(1,7,'TAGD_LogoText','TAGD_LogoText',1,'','')
insert into TemplatePageText values(1,3,'TADF_LogoText','TADF_LogoText',1,'','')
Go



--------------------------------------------------------------
--take backup
--Step 0 - Deploy changes - Hosted Engine and Hosted Admin
--Step 1 : Add New Client--TIAA
--Step 2: Add Site - TCF
--Step 3:Insert data

INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy, DescriptionOverride)
	Values(18,1,1,'Summary Prospectus','Summary Prospectus','SP', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(11,1,2,'Prospectus','Prospectus','P', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(15,1,3,'Statement ofnewlineAdditional Information','Statement ofnewlineAdditional Information','S', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(16,1,5,'Semiannual Report','Semiannual Report','SAR', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(1,1,4,'Annual Report','Annual Report','AR', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(23,1,6,'Schedule ofnewlineInvestments','Schedule ofnewlineInvestments','SOI', GetUTCDate(), 1, '')
INSERT INTO DocumentTypeAssociation(DocumentTypeId,SiteId,[Order],HeaderText,LinkText,MarketId,UtcModifiedDate,ModifiedBy,DescriptionOverride)
	Values(21,1,7,'XBRL','XBRL','XBRL', GetUTCDate(), 1, '')

Go
--------------------------------------------------------------

DECLARE @SiteID INT 
SELECT @SiteID = SiteID from Site where Name = 'TCF'

INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('TIAA-CREF Funds','TIAA-CREF Funds',@SiteID,null,getUTCdate(),1, 1)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('TIAA-CREF Lifecycle Funds','TIAA-CREF Lifecycle Funds',@SiteID,null,getUTCdate(),1, 2)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('TIAA-CREF Lifecycle Index Funds','TIAA-CREF Lifecycle Index Funds',@SiteID,null,getUTCdate(),1, 3)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('TIAA-CREF Lifestyle Funds','TIAA-CREF Lifestyle Funds',@SiteID,null,getUTCdate(),1,4 )
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('TIAA-CREF Life Funds','TIAA-CREF Life Funds',@SiteID,null,getUTCdate(),1, 5)


DECLARE @ParentGroupID INT 
SELECT @ParentGroupID = TaxonomyAssociationGroupID from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds'

INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('Global','Global',null,@ParentGroupID,getUTCdate(),1,1)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('International','International',null,@ParentGroupID,getUTCdate(),1,2)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('U.S. Equity','U.S. Equity',null,@ParentGroupID,getUTCdate(),1,3)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('Fixed Income','Fixed Income',null,@ParentGroupID,getUTCdate(),1,4)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('Asset Allocation','Asset Allocation',null,@ParentGroupID,getUTCdate(),1,5)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('Real Estate','Real Estate',null,@ParentGroupID,getUTCdate(),1,6)
INSERT INTO TaxonomyAssociationGroup(NAME, Description, SiteId, ParentTaxonomyAssociationGroupId, UtcModifiedDate, ModifiedBy, [Order]) values('Money Market','Money Market',null,@ParentGroupID,getUTCdate(),1,7)

Go


----------------------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @TaxonomyAssociationGroupId INT, @TaxonomyAssociationId INT
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'Global' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R201','Global Natural Resources','TIAA-CREF Global Natural Resources Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

----------------------------------

select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'International' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R581','Emerging Markets Debt','TIAA-CREF Emerging Markets Debt Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M269','Emerging Markets Equity','TIAA-CREF Emerging Markets Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M228','Emerging Markets Equity Index','TIAA-CREF Emerging Markets Equity Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315555','Enhanced International Equity Index ','TIAA-CREF Enhanced International Equity Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245P536','International Bond ','TIAA-CREF International Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W102','International Equity ','TIAA-CREF International Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W516','International Equity Index ','TIAA-CREF International Equity Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R631','International Opportunities','TIAA-CREF International Opportunities Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245P478','International Small-Cap Equity  ','TIAA-CREF International Small-Cap Equity  Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R474','Social Choice International Equity ','TIAA-CREF Social Choice International Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 10, GETUTCDATE(), 1)



--------------------------
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'U.S. Equity' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315548','Enhanced Large-Cap Growth Index ','TIAA-CREF Enhanced Large-Cap Growth Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315530','Enhanced Large-Cap Value Index ','TIAA-CREF Enhanced Large-Cap Value Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W508','Equity Index ','TIAA-CREF Equity Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W409','Growth & Income ','TIAA-CREF Growth & Income Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W334','Large-Cap Growth ','TIAA-CREF Large-Cap Growth Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W680','Large-Cap Growth Index ','TIAA-CREF Large-Cap Growth Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W730','Large-Cap Value ','TIAA-CREF Large-Cap Value Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W664','Large-Cap Value Index ','TIAA-CREF Large-Cap Value Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W805','Mid-Cap Growth ','TIAA-CREF Mid-Cap Growth Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W862','Mid-Cap Value ','TIAA-CREF Mid-Cap Value Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 10, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W714','S&P 500 Index ','TIAA-CREF S&P 500 Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 11, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W573','Small-Cap Blend Index ','TIAA-CREF Small-Cap Blend Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 12, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W839','Small-Cap Equity ','TIAA-CREF Small-Cap Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 13, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245P585','Small/Mid-Cap Equity ','TIAA-CREF Small/Mid-Cap Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 14, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W300','Social Choice Equity ','TIAA-CREF Social Choice Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 15, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R383','Social Choice Low Carbon Equity ','TIAA-CREF Social Choice Low Carbon Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 16, GETUTCDATE(), 1)

--------------------
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'Fixed Income' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W607','Bond ','TIAA-CREF Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M848','Bond Index ','TIAA-CREF Bond Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315506','Bond Plus ','TIAA-CREF Bond Plus Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315795','High-Yield ','TIAA-CREF High-Yield Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W482','Inflation-Linked Bond ','TIAA-CREF Inflation-Linked Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R672','Social Choice Bond','TIAA-CREF Social Choice Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315803','Short-Term Bond ','TIAA-CREF Short-Term Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R433','Short-Term Bond Index','TIAA-CREF Short-Term Bond Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315860','Tax-Exempt Bond ','TIAA-CREF Tax-Exempt Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

----------------
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'Asset Allocation' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315837','Managed Allocation ','TIAA-CREF Managed Allocation Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

----------------
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'Real Estate' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W797','Real Estate Securities ','TIAA-CREF Real Estate Securities Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

----------------
select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'Money Market' 
		and ParentTaxonomyAssociationGroupId In (select TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Funds')


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244W706','Money Market ','TIAA-CREF Money Market Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)


----------------


select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Lifecycle Funds'


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315662','Lifecycle 2010 ','TIAA-CREF Lifecycle 2010 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315654','Lifecycle 2015 ','TIAA-CREF Lifecycle 2015 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315647','Lifecycle 2020 ','TIAA-CREF Lifecycle 2020 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315639','Lifecycle 2025 ','TIAA-CREF Lifecycle 2025 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315621','Lifecycle 2030 ','TIAA-CREF Lifecycle 2030 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315613','Lifecycle 2035 ','TIAA-CREF Lifecycle 2035 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315597','Lifecycle 2040 ','TIAA-CREF Lifecycle 2040 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315589','Lifecycle 2045 ','TIAA-CREF Lifecycle 2045 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315571','Lifecycle 2050 ','TIAA-CREF Lifecycle 2050 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M210','Lifecycle 2055','TIAA-CREF Lifecycle 2055 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 10, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R573','Lifecycle 2060','TIAA-CREF Lifecycle 2060 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 11, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'886315563','Lifecycle Retirement Income ','TIAA-CREF Lifecycle Retirement Income Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 12, GETUTCDATE(), 1)


----------------

select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Lifecycle Index Funds'


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M830','Lifecycle Index 2010 ','TIAA-CREF Lifecycle Index 2010 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M822','Lifecycle Index 2015 ','TIAA-CREF Lifecycle Index 2015 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M814','Lifecycle Index 2020 ','TIAA-CREF Lifecycle Index 2020 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M798','Lifecycle Index 2025 ','TIAA-CREF Lifecycle Index 2025 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M780','Lifecycle Index 2030 ','TIAA-CREF Lifecycle Index 2030 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M772','Lifecycle Index 2035 ','TIAA-CREF Lifecycle Index 2035 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M764','Lifecycle Index 2040 ','TIAA-CREF Lifecycle Index 2040 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M756','Lifecycle Index 2045 ','TIAA-CREF Lifecycle Index 2045 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M749','Lifecycle Index 2050 ','TIAA-CREF Lifecycle Index 2050 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M178','Lifecycle Index 2055','TIAA-CREF Lifecycle Index 2055 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 10, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R565','Lifecycle Index 2060','TIAA-CREF Lifecycle Index 2060 Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 11, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245M731','Lifecycle Index Retirement Income ','TIAA-CREF Lifecycle Index Retirement Income Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 12, GETUTCDATE(), 1)


----------------

select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Lifestyle Funds'


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R607','Lifestyle Income','TIAA-CREF Lifestyle Income Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R870','Lifestyle Conservative','TIAA-CREF Lifestyle Conservative Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R839','Lifestyle Moderate','TIAA-CREF Lifestyle Moderate Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R789','Lifestyle Growth ','TIAA-CREF Lifestyle Growth Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87245R748','Lifestyle Aggressive Growth ','TIAA-CREF Lifestyle Aggressive Growth Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

--------------------------


select @TaxonomyAssociationGroupId = TaxonomyAssociationGroupId from TaxonomyAssociationGroup where Name = 'TIAA-CREF Life Funds'


INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V302','Growth & Income','TIAA-CREF Life: Growth & Income Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 3, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V104','Growth Equity','TIAA-CREF Life: Growth Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 4, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V203','International Equity','TIAA-CREF Life: International Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 5, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V609','Large-Cap Value','TIAA-CREF Life: Large-Cap Value Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 6, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V807','Real Estate Securities','TIAA-CREF Life: Real Estate Securities Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 8, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V708','Small-Cap Equity','TIAA-CREF Life: Small-Cap Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 9, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V500','Social Choice Equity','TIAA-CREF Life: Social Choice Equity Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 10, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V401','Stock Index','TIAA-CREF Life: Stock Index Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 11, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V880','Bond','TIAA-CREF Life: Bond Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 2, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V864','Balanced','TIAA-CREF Life: Balanced Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 1, GETUTCDATE(), 1)

INSERT INTO TaxonomyAssociation(Level, TaxonomyId, MarketId,NameOverride, TabbedPageNameOverride,UtcModifiedDate, ModifiedBy) values(1,0,'87244V872','Money Market','TIAA-CREF Life: Money Market Fund',getUTCdate(),1)
SET @TaxonomyAssociationId = SCOPE_IDENTITY()
INSERT INTO TaxonomyAssociationGroupTaxonomyAssociation(TaxonomyAssociationGroupId,TaxonomyAssociationId,[Order],UtcModifiedDate, ModifiedBy) Values(@TaxonomyAssociationGroupId, @TaxonomyAssociationId, 7, GETUTCDATE(), 1)

Go
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Declare @Temp table(CUSIP nvarchar(10))

INSERT INTO @Temp values('87245R201')
INSERT INTO @Temp values('87245R581')
INSERT INTO @Temp values('87245M269')
INSERT INTO @Temp values('87245M228')
INSERT INTO @Temp values('886315555')
INSERT INTO @Temp values('87245P536')
INSERT INTO @Temp values('87244W102')
INSERT INTO @Temp values('87244W516')
INSERT INTO @Temp values('87245R631')
INSERT INTO @Temp values('87245P478')
INSERT INTO @Temp values('87245R474')
INSERT INTO @Temp values('886315548')
INSERT INTO @Temp values('886315530')
INSERT INTO @Temp values('87244W508')
INSERT INTO @Temp values('87244W409')
INSERT INTO @Temp values('87244W334')
INSERT INTO @Temp values('87244W680')
INSERT INTO @Temp values('87244W730')
INSERT INTO @Temp values('87244W664')
INSERT INTO @Temp values('87244W805')
INSERT INTO @Temp values('87244W862')
INSERT INTO @Temp values('87244W714')
INSERT INTO @Temp values('87244W573')
INSERT INTO @Temp values('87244W839')
INSERT INTO @Temp values('87245P585')
INSERT INTO @Temp values('87244W300')
INSERT INTO @Temp values('87245R383')
INSERT INTO @Temp values('87244W607')
INSERT INTO @Temp values('87245M848')
INSERT INTO @Temp values('886315506')
INSERT INTO @Temp values('886315795')
INSERT INTO @Temp values('87244W482')
INSERT INTO @Temp values('87245R672')
INSERT INTO @Temp values('886315803')
INSERT INTO @Temp values('87245R433')
INSERT INTO @Temp values('886315860')
INSERT INTO @Temp values('886315837')
INSERT INTO @Temp values('87244W797')
INSERT INTO @Temp values('87244W706')
INSERT INTO @Temp values('886315662')
INSERT INTO @Temp values('886315654')
INSERT INTO @Temp values('886315647')
INSERT INTO @Temp values('886315639')
INSERT INTO @Temp values('886315621')
INSERT INTO @Temp values('886315613')
INSERT INTO @Temp values('886315597')
INSERT INTO @Temp values('886315589')
INSERT INTO @Temp values('886315571')
INSERT INTO @Temp values('87245M210')
INSERT INTO @Temp values('87245R573')
INSERT INTO @Temp values('886315563')
INSERT INTO @Temp values('87245M830')
INSERT INTO @Temp values('87245M822')
INSERT INTO @Temp values('87245M814')
INSERT INTO @Temp values('87245M798')
INSERT INTO @Temp values('87245M780')
INSERT INTO @Temp values('87245M772')
INSERT INTO @Temp values('87245M764')
INSERT INTO @Temp values('87245M756')
INSERT INTO @Temp values('87245M749')
INSERT INTO @Temp values('87245M178')
INSERT INTO @Temp values('87245R565')
INSERT INTO @Temp values('87245M731')
INSERT INTO @Temp values('87245R607')
INSERT INTO @Temp values('87245R870')
INSERT INTO @Temp values('87245R839')
INSERT INTO @Temp values('87245R789')
INSERT INTO @Temp values('87245R748')
INSERT INTO @Temp values('87244V302')
INSERT INTO @Temp values('87244V104')
INSERT INTO @Temp values('87244V203')
INSERT INTO @Temp values('87244V609')
INSERT INTO @Temp values('87244V807')
INSERT INTO @Temp values('87244V708')
INSERT INTO @Temp values('87244V500')
INSERT INTO @Temp values('87244V401')
INSERT INTO @Temp values('87244V880')
INSERT INTO @Temp values('87244V864')
INSERT INTO @Temp values('87244V872')


select 'Update TaxonomyAssociation set TaxonomyID = '+ cast(ProsTicker.ProspectusID as nvarchar)+ ' where MarketId = '''+ t.CUSIP+ '''' from @Temp t
INNER JOIN ProsTicker ON t.CUSIP = ProsTicker.CUSIP




Go
-------------------------------------------------------------------------------------
INSERT INTO TaxonomyLevelExternalId
	SELECT DISTINCT 1, TaxonomyId, MarketID, 0, GetUTCDate(), 1 , 1 
	FROM TaxonomyAssociation
Go
INSERT INTO [DocumentTypeExternalID]
	SELECT DocumentTypeId, MarketId, 0, GetUTCDate(), 1
	FROM DocumentTypeAssociation
Go
-------------------------------------------------------------------------------------
-- Add site text

--Add Page Text

-- enable XBRL

-- Add Static Resources

--Page Feature SPV

--URL Re-write
-------------------------------------------------------------------------------------


