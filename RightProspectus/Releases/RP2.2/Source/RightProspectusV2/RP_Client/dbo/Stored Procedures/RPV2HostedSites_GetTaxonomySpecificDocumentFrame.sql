CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomySpecificDocumentFrame]
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



