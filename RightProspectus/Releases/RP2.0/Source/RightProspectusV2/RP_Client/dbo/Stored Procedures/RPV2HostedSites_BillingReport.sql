CREATE PROCEDURE [dbo].[RPV2HostedSites_BillingReport]
@MarketID NVARCHAR(100) = NULL,
@SearchSiteName NVARCHAR(100) = NULL,
@StartDate DATETIME=NULL,
@EndDate DATETIME=NULL
AS  
BEGIN
	DECLARE @SiteDetails TABLE(RowNum INT, SiteId INT, Name NVARCHAR(200), DefaultPageId INT)	
	DECLARE @URLDetails TABLE(SiteName NVARCHAR(100), MarketID NVARCHAR(100),NameOverride NVARCHAR(200))
	DECLARE @SiteCount INT, @RowCount INT = 1, @DefaultPageID INT, @SiteID INT, @SiteName NVARCHAR(100)
	-- FETCH Site Details based on Search creteria
	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]
	WHERE @SearchSiteName IS NULL OR Name = @SearchSiteName
	--Get the total site counts
	SELECT @SiteCount = COUNT(*) FROM @SiteDetails
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN
		SELECT @SiteID = SiteID, @SiteName = Name, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount
		IF @DefaultPageID = 1 --TAL case
		BEGIN
			--logic for TAL
		INSERT INTO @URLDetails(SiteName, MarketID,NameOverride)
		SELECT @SiteName, MarketID , NameOverride              
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteID
		UNION
   
		SELECT @SiteName, CTA.MarketId,CTA.NameOverride
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
		SELECT TaxonomyAssociationID 
		FROM TaxonomyAssociation WHERE SiteId = @SiteID
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL
		END

		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN
			--Logic for TAD
			INSERT INTO @URLDetails(SiteName, MarketID,NameOverride)
			SELECT @SiteName, MarketID,NameOverride FROM TaxonomyAssociation WHERE SiteID = @SiteID
		END
		SET @RowCount = @RowCount + 1

	END
	--Get count of new funds
	SELECT * FROM @URLDetails ORDER BY SiteName,MarketID
	SELECT count(*) FROM CUDHistory WHERE TableName='TaxonomyAssociation' and CUDType='I'
	AND UtcCUDDate BETWEEN @StartDate AND @EndDate

	--Get count of deleted funds
	SELECT count(*) FROM CUDHistory CH 
	INNER JOIN CUDHistoryData CHD 
	ON CH.CUDHistoryID=CHD.CUDHistoryId 
	WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='D' AND CHD.ColumnName='MarketId'
	AND UtcCUDDate BETWEEN @StartDate AND @EndDate
	
	--Get Details of the deleted funds
	SELECT CH.UtcCUDDate,CHD.OldValue FROM CUDHistory CH 
	INNER JOIN CUDHistoryData CHD 
	ON CH.CUDHistoryID=CHD.CUDHistoryId 
	WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='D' AND CHD.ColumnName='MarketId'
	AND CH.UtcCUDDate BETWEEN @StartDate AND @EndDate
END