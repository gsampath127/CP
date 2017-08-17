CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationClientDocumentFrame]  --- EXEC RPV2HostedSites_GetTaxonomyAssociationClientDocumentFrame NULL, '024936742', 'MMD'
@TAID INT = NULL,
@ExternalId NVARCHAR(100) = NULL,
@ClientDocumentType NVARCHAR(100),
@SiteName nvarchar(100)=null
AS
BEGIN

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int)
	DECLARE @SiteID int, @DefaultPageID int
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  	BEGIN
  	    SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  	END

	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		
		SELECT DISTINCT TaxonomyAssociationId           
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteID

		UNION
   
		SELECT CTA.TaxonomyAssociationId 
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
			SELECT TaxonomyAssociationID FROM TaxonomyAssociation WHERE SiteId = @SiteID
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL		
		
	END
	ELSE IF @DefaultPageID = 4 --TAD case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		SELECT DISTINCT TaxonomyAssociationId FROM TaxonomyAssociation WHERE SiteID = @SiteID
	END

	DECLARE @HostedDocumentsDisplayCount int

	SELECT @HostedDocumentsDisplayCount = HostedDocumentsDisplayCount from ClientDocumentType WHERE Name = @ClientDocumentType
 
	IF @HostedDocumentsDisplayCount <> 0
	BEGIN

		SELECT DISTINCT TOP (@HostedDocumentsDisplayCount) 
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			TA.NameOverride as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.MarketId,
			CD.ClientDocumentId,			
			CD.Name,
			CD.[FileName],
			CD.[Description]
		FROM ClientDocument CD
		INNER JOIN ClientDocumentType CDT ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId AND CDT.Name = @ClientDocumentType
		INNER JOIN TaxonomyAssociationClientDocument TACD ON TACD.ClientDocumentId = CD.ClientDocumentId
		INNER JOIN TaxonomyAssociation TA ON TA.TaxonomyAssociationId = TACD.TaxonomyAssociationId
		INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId 		
		LEFT OUTER JOIN TaxonomyLevelExternalId TLE  ON TA.TaxonomyId = TLE.TaxonomyId AND TA.[Level] = TLE.[Level] 
		WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
							AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)	
		ORDER BY CD.[FileName] DESC	
	END
	ELSE
	BEGIN

		SELECT DISTINCT
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			TA.NameOverride as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.MarketId,
			CD.ClientDocumentId,			
			CD.Name,
			CD.[FileName],
			CD.[Description]
		FROM ClientDocument CD
		INNER JOIN ClientDocumentType CDT ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId AND CDT.Name = @ClientDocumentType
		INNER JOIN TaxonomyAssociationClientDocument TACD ON TACD.ClientDocumentId = CD.ClientDocumentId
		INNER JOIN TaxonomyAssociation TA ON TA.TaxonomyAssociationId = TACD.TaxonomyAssociationId
		INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId		
		LEFT OUTER JOIN TaxonomyLevelExternalId TLE  ON TA.TaxONomyID = TLE.TaxonomyId AND TA.[Level] = TLE.[Level] 
		WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
							AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)	
		ORDER BY CD.[FileName] DESC	
	
	END

END
GO