CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetTaxonomyAssociationUsingSiteId]
@SiteId Int,
@IsProofing BIT
AS
BEGIN

	DECLARE @DefaultPageID int
	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		
		SELECT *           
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteId
		And IsProofing = @IsProofing

		UNION
   
		SELECT DISTINCT CTA.*
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
			SELECT TaxonomyAssociationID FROM TaxonomyAssociation WHERE SiteId = @SiteId And IsProofing = @IsProofing
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL	
		And CTA.IsProofing = @IsProofing
		
	END
	ELSE IF @DefaultPageID = 4 --TAD case
	BEGIN
		SELECT * FROM TaxonomyAssociation WHERE SiteID = @SiteId And IsProofing = @IsProofing
	END
	ELSE IF @DefaultPageID = 7 --TAD case
	BEGIN

		DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
		
		;WITH TopLevelTaxonomyAssociationGroup 
		AS (SELECT 
					@SiteId AS SiteId,
					TaxonomyAssociationGroupId                
			FROM   TaxonomyAssociationGroup 
			WHERE  SiteID = @SiteId 

			UNION ALL 
			SELECT 				
					NULL AS SiteId,
					TAG.TaxonomyAssociationGroupId               
			FROM   TopLevelTaxonomyAssociationGroup TLTAG
			INNER JOIN TaxonomyAssociationGroup TAG ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)

		INSERT INTO @TaxonomyAssociationIdsForSite
			SELECT DISTINCT TaxonomyAssociationId, MarketId, IsProofing FROM TopLevelTaxonomyAssociationGroup
			LEFT JOIN (
				SELECT TaxonomyAssociationGroupId, TaxonomyAssociation.TaxonomyAssociationId, MarketId, IsProofing
				FROM TaxonomyAssociationGroupTaxonomyAssociation 
				INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TaxonomyAssociationGroupTaxonomyAssociation.TaxonomyAssociationId
			)TAGTA ON TopLevelTaxonomyAssociationGroup.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
			WHERE TaxonomyAssociationId IS NOT NULL

		SELECT * FROM TaxonomyAssociation TA
		INNER JOIN @TaxonomyAssociationIdsForSite t on t.TaxonomyAssociationId = TA.TaxonomyAssociationId
		WHERE TA.IsProofing = @IsProofing
	END

END