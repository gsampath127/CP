CREATE PROCEDURE [dbo].[RPV2HostedSites_GetDocumentUpdateReportData]
@TT_DocumentUpdateVerticalData dbo.TT_DocumentUpdateVerticalData READONLY,
@StartDate DATETIME,
@EndDate DATETIME
AS  
BEGIN
	
	--STEP 1: Convert @EndDate into CST TIME
	DECLARE @CST_Actual_EndDate DATETIME =  Convert(datetime, SWITCHOFFSET(convert(DateTimeOffSet, @ENDDATE), DATENAME(tz, SYSDATETIMEOFFSET())))
	DECLARE @CST_Actual_StartDate DATETIME =  Convert(datetime, SWITCHOFFSET(convert(DateTimeOffSet, @StartDate), DATENAME(tz, SYSDATETIMEOFFSET())))
	
	--STEP 2: ACTUAL Start and END Date in UTC
	DECLARE @UTC_Actual_EndDate DATETIME = 	@EndDate
	DECLARE @UTC_Actual_StartDate DATETIME = @StartDate

	--STEP 3: GET NEWLY ADDED CUSIPs  (by considering above actual start and end date logic)
	DECLARE @NewlyAddedTaxonomyIds Table(TaxonomyId INT)

	INSERT INTO @NewlyAddedTaxonomyIds

		SELECT CHD.NewValue 
		FROM CUDHistory CH 
		INNER JOIN CUDHistoryData CHD 
		ON CH.CUDHistoryID=CHD.CUDHistoryId 
		WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='I' AND CHD.ColumnName='TaxonomyId'
		AND UtcCUDDate BETWEEN @UTC_Actual_StartDate AND @UTC_Actual_EndDate



	----------------------------------------

	-- Step 1:  Remove all INVALID taxonomy Ids from TaxonomyLevelDocUpdate table which are not present in vertical market

	DELETE TaxonomyLevelDocUpdate
	FROM TaxonomyLevelDocUpdate 
	LEFT JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.MarketId IS NULL AND DocUpDt.DocumentTypeID IS NULL

	-- Step 2: Update  TaxonomyLevelDocUpdate for all changes in DocumentDate

	UPDATE TaxonomyLevelDocUpdate
	SET DocumentDate = DocUpDt.DocumentDate
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.DocumentDate IS NOT NULL AND ISNULL(TaxonomyLevelDocUpdate.DocumentDate, '1901-01-01') <> DocUpDt.DocumentDate

	-- Step 3: Update  TaxonomyLevelDocUpdate for all changes in DocumentUpdateDate

	UPDATE TaxonomyLevelDocUpdate
	SET DocumentUpdatedDate = DocUpDt.DocumentUpdatedDate
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.DocumentUpdatedDate IS NOT NULL AND DocUpDt.DocumentUpdatedDate >= ISNULL(TaxonomyLevelDocUpdate.DocumentUpdatedDate, '1901-01-01')


	-- Step 4: Update  TaxonomyLevelDocUpdate for all changes in TAXONOMY NAME CHANGES

	UPDATE TaxonomyLevelDocUpdate
	SET TaxonomyName = DocUpDt.TaxonomyName
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE ISNULL(DocUpDt.TaxonomyName, '') != '' AND TaxonomyLevelDocUpdate.TaxonomyName <> DocUpDt.TaxonomyName

	-- Step 5: INSERT New records in TaxonomyLevelDocUpdate

	INSERT INTO TaxonomyLevelDocUpdate(MarketId, DocumentTypeID, TaxonomyName, DocumentDate, DocumentUpdatedDate)

	SELECT DocUpDt.MarketId, DocUpDt.DocumentTypeID, DocUpDt.TaxonomyName, DocUpDt.DocumentDate, DocUpDt.DocumentUpdatedDate
	FROM @TT_DocumentUpdateVerticalData DocUpDt
	LEFT JOIN TaxonomyLevelDocUpdate ON TaxonomyLevelDocUpdate.MarketId = DocUpDt.MarketId AND TaxonomyLevelDocUpdate.DocumentTypeID = DocUpDt.DocumentTypeID
	WHERE TaxonomyLevelDocUpdate.MarketId IS NULL AND TaxonomyLevelDocUpdate.DocumentTypeID IS NULL


	--Step 6 Select Final data to be sent in Document update


	DECLARE @SiteCount int, @RowCount int = 1, @DefaultPageID int, @SiteID int
	DECLARE @SiteDetails TABLE(RowNum int, SiteId int, Name nvarchar(200), DefaultPageId int)
	DECLARE @DocUPDTDetails TABLE(MarketId nvarchar(100), DocumentTypeMarketId NVARCHAR(100), DocumentDate DateTime, DocumentUpdatedDate DateTime, DocumentUpdated char(1), DocumentTypeOrder int)

	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]

	--Get the total site counts
	SELECT @SiteCount = COUNT(*) from @SiteDetails
	
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN

		SELECT @SiteID = SiteID, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount

		IF @DefaultPageID = 7 --TAG case
		BEGIN

			INSERT INTO @DocUPDTDetails(MarketId, DocumentTypeMarketId, DocumentDate, DocumentUpdatedDate, DocumentUpdated, DocumentTypeOrder)
			
			SELECT DISTINCT TA.MarketId,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
				TaxonomyLevelDocUpdate.DocumentDate,
				DATEADD(HOUR, 1, TaxonomyLevelDocUpdate.DocumentUpdatedDate),				
				CASE WHEN newTA.TaxonomyId IS NULL AND DocumentUpdatedDate BETWEEN @CST_Actual_StartDate AND @CST_Actual_EndDate THEN 'Y' ELSE 'N' END,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder
			FROM TaxonomyLevelDocUpdate
			INNER JOIN TaxonomyAssociation TA On TA.MarketId = TaxonomyLevelDocUpdate.MarketId
			INNER JOIN TaxonomyAssociationGroupTaxonomyAssociation TAGTA ON TAGTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			INNER JOIN TaxonomyAssociationGroup TAG ON TAG.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID AND TALevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			LEFT JOIN @NewlyAddedTaxonomyIds newTA ON TA.TaxonomyId = newTA.TaxonomyId
			WHERE TA.TaxonomyAssociationId IS NOT NULL AND TAG.SiteId = @SiteID AND TALE.SendDocumentUpdate = 1 
	
			
			
		END
		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN

			INSERT INTO @DocUPDTDetails(MarketId, DocumentTypeMarketId, DocumentDate, DocumentUpdatedDate, DocumentUpdated, DocumentTypeOrder)
			
			SELECT DISTINCT TA.MarketId,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
				TaxonomyLevelDocUpdate.DocumentDate,
				DATEADD(HOUR, 1, TaxonomyLevelDocUpdate.DocumentUpdatedDate),
				CASE WHEN newTA.TaxonomyId IS NULL AND DocumentUpdatedDate BETWEEN @CST_Actual_StartDate AND @CST_Actual_EndDate THEN 'Y' ELSE 'N' END,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder
			FROM TaxonomyLevelDocUpdate
			INNER JOIN TaxonomyAssociation TA On TA.MarketId = TaxonomyLevelDocUpdate.MarketId
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID AND TALevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			LEFT JOIN @NewlyAddedTaxonomyIds newTA ON TA.TaxonomyId = newTA.TaxonomyId
			WHERE TA.TaxonomyAssociationId IS NOT NULL AND TA.SiteId = @SiteID AND TALE.SendDocumentUpdate = 1

		END
		--TODO   : Need to add code for TAL and TAHD

		SET @RowCount = @RowCount + 1

	END

	SELECT DISTINCT *
	FROM @DocUPDTDetails
	ORDER BY MarketId, DocumentTypeOrder

END
GO
