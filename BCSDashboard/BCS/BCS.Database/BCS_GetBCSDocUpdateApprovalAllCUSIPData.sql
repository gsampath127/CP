USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSDocUpdateApprovalAllCUSIPData]    Script Date: 3/24/2016 6:14:30 PM ******/

ALTER PROCEDURE [dbo].[BCS_GetBCSDocUpdateApprovalAllCUSIPData]
@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@DocIDs TT_SLinkDocumentId READONLY,
@Acc# nvarchar(250),
@Status nvarchar(250),
@StartIndex INT,
@EndIndex INT

AS

BEGIN

DECLARE @CUSIPDetails Table(BCSDocUpdateId int, CUSIP nvarchar(10),EdgarID int,	Acc# nvarchar(250) ,RRDPDFURL nvarchar(512) , DocumentType nvarchar(50), 
							DocumentDate dateTime, DocumentID nvarchar(200) ,FundName nvarchar(255), Status nvarchar(50), StatusDate dateTime);

DECLARE @TaxonomyMarketIDCount INT = 0
SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t
DECLARE @DocIDsCount INT = 0
SELECT @DocIDsCount = COUNT(d.DocumentId) FROM @DocIDs d	


--Fetch All cusip details and insert into @CUSIPDetails according to page size.
	INSERT INTO @CUSIPDetails(BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate, DocumentID,FundName, Status, StatusDate)
		SELECT BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate, DocumentID, FundName, Status, StatusDate
		FROM
		(
			SELECT BCSDocUpdateId, BCSDocUpdate.CUSIP, EdgarID, Acc#, RRDPDFURL, FirstDollarDocType.DocTypeDesc AS 'DocumentType', DocumentDate,
				CASE WHEN BCSDocUpdate.IsProcessed = 0 THEN 'NA'
					 WHEN BCSDocUpdate.DocumentType IN ('SP','SPS','RSP') THEN 'SP' + Replace(BCSDocUpdate.PDFName,'.pdf','') + 'GIM'
					 WHEN BCSDocUpdate.DocumentType IN ('P','PS','RP') THEN 'P' + Replace(BCSDocUpdate.PDFName,'.pdf','') + 'GIM'			
				END AS 'DocumentID'
				, FundName,
				CASE WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) != 0 THEN 'APC'
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) != 0 THEN 'OPC'
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) != 0 THEN 'AP'
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) != 0 THEN 'OP'
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) != 0 THEN 'EX'
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) = 0 THEN 'Processed'
					WHEN BCSDocUpdate.IsProcessed = 0 THEN 'Filed'
				ELSE NULL
				END AS 'Status',
				CASE WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) != 0 THEN BCSDocUpdateGIMSlink.APCReceivedDate
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) != 0 THEN BCSDocUpdateGIMSlink.OPCReceivedDate
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) != 0 THEN BCSDocUpdateGIMSlink.APFReceivedDate
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) != 0 THEN BCSDocUpdateGIMSlink.OPFReceivedDate
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) != 0 THEN BCSDocUpdateGIMSlink.ExportedDate
					WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) = 0 THEN BCSDocUpdate.ProcessedDate
					WHEN BCSDocUpdate.IsProcessed = 0 THEN BCSDocUpdate.FilingStatusAddedDate
				ELSE NULL
				END AS 'StatusDate'			
			FROM BCSDocUpdate
			Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  BCSDocUpdate.BCSDocUpdateId
			INNER JOIN FirstDollarDocType ON BCSDocUpdate.DocumentType = FirstDollarDocType.DocTypeId	
			WHERE 
			BCSDocUpdate.IsRemoved = 0
			And (@Acc# IS NULL OR BCSDocUpdate.Acc# = @Acc#)			  
			AND  (@TaxonomyMarketIDCount = 0 OR CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
		) AS tblCUSIPDetails
		WHERE (@DocIDsCount = 0 OR tblCUSIPDetails.DocumentId IN (SELECT d.DocumentId FROM @DocIDs d)) And (@Status IS NULL OR tblCUSIPDetails.[Status] = @Status)

--Find	AllCUSIPDetailsTotalCount	

		SELECT Count(CUSIP) As AllCUSIPDetailsTotalCount FROM @CUSIPDetails

--Select duplicate CUSIPDetails according.	
   
		SELECT * FROM (
			SELECT ROW_NUMBER() OVER(ORDER By CUSIP) AS 'RowNumber', BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate,DocumentID, FundName, Status, StatusDate 
			FROM @CUSIPDetails
		)t
		WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex
	 
   

	---CUSIP Not Preset
	SELECT t.marketId AS 'CUSIP'
	FROM @TaxonomyMarketIDs t
	LEFT JOIN BCSDocUpdate ON BCSDocUpdate.CUSIP = t.marketId
	WHERE BCSDocUpdate.CUSIP IS NULL

	--Doc ID Not Present
	SELECT d.documentId AS 'DocumentID'
	FROM @DocIDs d
	LEFT JOIN @CUSIPDetails cd ON cd.DocumentID = d.documentId
	WHERE cd.DocumentID IS NULL
	
End
GO