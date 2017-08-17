CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationGroups]   --  exec RPV2HostedSites_GetTaxonomyAssociationGroups 6
	@SiteName NVARCHAR(100)=NULL,
	@IsProofing BIT
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
               WHERE  SiteID = @SiteID AND IsProofing = @IsProofing
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
					RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.IsProofing = @IsProofing 
					LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID And TALevelDTA.IsProofing = @IsProofing
					--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
					--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
					LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
					LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]		
				WHERE TA.TaxonomyAssociationId IS NOT NULL
	)TA ON TLTAG.TaxonomyAssociationGroupId = TA.TAGTATaxonomyAssociationGroupId
	ORDER BY 1 desc, 2,3,5, 16

END
GO
