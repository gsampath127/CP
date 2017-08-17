CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteFeature_CustomizeTaxonomyOrder]
@SiteId Int,
@ModifiedBy Int
AS
BEGIN

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int, MarketId nvarchar(100), isproofing bit)
	DECLARE @DefaultPageID int
	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		
		SELECT DISTINCT TaxonomyAssociationId, MarketId, isproofing           
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteID

		UNION
   
		SELECT CTA.TaxonomyAssociationId, CTA.MarketId, CTA.isproofing  
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
		SELECT DISTINCT TaxonomyAssociationId, MarketId, isproofing FROM TaxonomyAssociation WHERE SiteID = @SiteID
	END
	ELSE IF @DefaultPageID = 7 --TAD case
	BEGIN

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
	END

	-- Update Entries in TaxonomyAssociation with SiteId having value
	;WITH FilteredTA AS(
		 Select TA.TaxonomyAssociationId,ROW_NUMBER() OVER (PARTITION BY TAFS.isproofing ORDER BY NameOverride ASC) AS RowNum  
		 FROM TaxonomyAssociation TA
		 INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId and TA.SiteId = @SiteId

	)

	Update TA  
	Set TA.[Order] =FilteredTA.RowNum,
		TA.UtcModifiedDate = GetUtcDate(),
		TA.ModifiedBy = @ModifiedBy
	FROM TaxonomyAssociation TA
	INNER JOIN FilteredTA ON TA.TaxonomyAssociationId = FilteredTA.TaxonomyAssociationId


	-- Update Entries in TaxonomyAssociationHierachy

	;WITH FilteredTAHD AS(
		 Select TAHD.ChildTaxonomyAssociationId,
				TAHD.ParentTaxonomyAssociationId ,
				ROW_NUMBER() OVER (PARTITION BY TAFS.isproofing, TAHD.ParentTaxonomyAssociationId ORDER BY TA.NameOverride ASC) AS RowNum  
		 FROM TaxonomyAssociationHierachy TAHD
		 INNER JOIN TaxonomyAssociation TA ON TAHD.ChildTaxonomyAssociationId = TA.TaxonomyAssociationId
		 INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId
	)

	Update TAHD 
	Set TAHD.[Order] = FilteredTAHD.RowNum,
		TAHD.UtcModifiedDate = GetUtcDate(),
		TAHD.ModifiedBy = @ModifiedBy
	FROM TaxonomyAssociationHierachy TAHD
	INNER JOIN FilteredTAHD ON TAHD.ParentTaxonomyAssociationId = FilteredTAHD.ParentTaxonomyAssociationId AND TAHD.ChildTaxonomyAssociationId = FilteredTAHD.ChildTaxonomyAssociationId


	-- Update Entries in TaxonomyAssociationGroup with Order is null 

	DECLARE @TAGD TABLE (Name nvarchar (200), TaxonomyAssociationGroupId int, ParentTaxonomyAssociationGroupId int, [Order] int, IsProofing BIT, Level int)

	;WITH TopLevelTaxonomyAssociationGroup 
		AS (SELECT Name , 
					TaxonomyAssociationGroupId, 
					ParentTaxonomyAssociationGroupId,
					[Order],
					IsProofing,
					0 As Level
			FROM   TaxonomyAssociationGroup 
			WHERE  SiteID = @SiteId
			UNION ALL 
			SELECT TAG.Name,
					TAG.TaxonomyAssociationGroupId, 
					TAG.ParentTaxonomyAssociationGroupId,
					TAG.[Order],
					TAG.IsProofing,
					Level + 1
			FROM   TopLevelTaxonomyAssociationGroup TLTAG
			INNER JOIN TaxonomyAssociationGroup TAG ON TLTAG.TaxonomyAssociationGroupId = TAG.ParentTaxonomyAssociationGroupId)
			

		INSERT INTO @TAGD 
			SELECT * FROM TopLevelTaxonomyAssociationGroup

	
	;WITH FilteredTAGD AS(  
		SELECT ROW_NUMBER() OVER (Partition by IsProofing, Level, ParentTaxonomyAssociationGroupId ORDER BY Name) AS RowNum, * FROM @TAGD							  
	)


	Update TAGD 
	Set TAGD.[Order] = FilteredTAGD.RowNum,
		TAGD.UtcModifiedDate = GetUtcDate(),
		TAGD.ModifiedBy = @ModifiedBy
	FROM TaxonomyAssociationGroup TAGD
	INNER JOIN FilteredTAGD ON TAGD.TaxonomyAssociationGroupId = FilteredTAGD.TaxonomyAssociationGroupId

	-- Update Entries in TaxonomyAssociationGroupTaxonomyAssociation with Order is null 

	;WITH FilteredTAGTA AS(
		SELECT TAGTA.TaxonomyAssociationGroupId,
			TAGTA.TaxonomyAssociationId ,
			ROW_NUMBER() OVER (PARTITION BY TAFS.isproofing, TAGTA.TaxonomyAssociationGroupId ORDER BY TA.NameOverride ASC) AS RowNum  
		FROM TaxonomyAssociationGroupTaxonomyAssociation TAGTA
		INNER JOIN TaxonomyAssociation TA ON TAGTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId
 
	)

	Update TAGDTA 
	Set TAGDTA.[Order] = FilteredTAGTA.RowNum,
		TAGDTA.UtcModifiedDate = GetUtcDate(),
		TAGDTA.ModifiedBy = @ModifiedBy
	FROM TaxonomyAssociationGroupTaxonomyAssociation TAGDTA
	INNER JOIN FilteredTAGTA ON TAGDTA.TaxonomyAssociationGroupId = FilteredTAGTA.TaxonomyAssociationGroupId AND 
								TAGDTA.TaxonomyAssociationId = FilteredTAGTA.TaxonomyAssociationId

					


END
GO
