
Create Procedure [dbo].RPV2HostedSites_GetTaxonomyAssociationLinks
@SiteName nvarchar(100)=null
as
Begin
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
	  TAH.ParentTaxonomyAssociationId,
	  TA.TaxonomyID,
	  NameOverride,
	  TA.DescriptionOverride
  FROM TaxonomyAssociationHierachy TAH
	INNER JOIN TaxonomyAssociation TA on TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationID 	
    INNER JOIN [Site] on TA.SiteID = [Site].SiteID AND [Site].SiteId = @SiteID
    LEFT OUTER JOIN TaxonomyLevelExternalId TALE ON TA.TaxonomyId = TALE.TaxonomyId
						AND TA.[Level] = TALE.[Level]
End