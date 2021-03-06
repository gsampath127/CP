USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSLiveUpdateTransamericaAllCusipData]    Script Date: 3/24/2016 6:19:30 PM ******/

ALTER PROCEDURE [dbo].[BCS_GetBCSLiveUpdateTransamericaAllCusipData] -- exec [BCS_GetBCSLiveUpdateTransamericaAllCusipData] Null,Null,Null,0,600
@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@DocIDs TT_SLinkDocumentId READONLY,
@Acc# nvarchar(250),
@Status nvarchar(200),
@StartIndex INT,
@EndIndex INT

AS

BEGIN

DECLARE @CUSIPDetails Table(Id int, CUSIP nvarchar(10),EdgarID int,	Acc# nvarchar(250) ,RRDPDFURL nvarchar(512) , DocumentType nvarchar(50), DocumentID nvarchar(200),
							DocumentDate dateTime, FundName nvarchar(255), Status nvarchar(50), StatusDate dateTime, ReportType varchar(30),PdfName varchar(512));

DECLARE @TaxonomyMarketIDCount INT = 0
SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t
DECLARE @DocIDsCount INT = 0
SELECT @DocIDsCount = COUNT(d.DocumentId) FROM @DocIDs d
--Fetch All cusip details and insert into @CUSIPDetails according to page size.	

	INSERT INTO @CUSIPDetails(Id, CUSIP, EdgarID, Acc#, DocumentType,DocumentID, DocumentDate, FundName, Status, StatusDate, RRDPDFURL,PdfName)	
      SELECT Id,CUSIP, EdgarID, Acc#, DocumentType,DocumentID , DocumentDate, FundName, Status, StatusDate, RRDPDFURL,PdfName
	   FROM
       (
	   			SELECT BCSDocUpdateSupplementsId as Id, [BCSDocUpdateSupplements].CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc as 'DocumentType'
				, CASE WHEN BCSDocUpdateSupplements.IsProcessed =0 THEN 'NA'
			          WHEN BCSDocUpdateSupplements.DocumentType IN ('SP','SPS','RSP') THEN 'SP' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'
		               WHEN BCSDocUpdateSupplements.DocumentType IN ('P','PS','RP') THEN 'P' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'			
	             END AS 'DocumentID',
				DocumentDate, BCSDocUpdateSupplements.FundName, 
				CASE WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 and ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) = 0 THEN 'AP'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN 'Processed'
				WHEN BCSDocUpdateSupplements.IsProcessed = 0 THEN 'Filed'
				ELSE NULL
				END AS 'Status',
				CASE WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN BCSDocUpdateSupplementsSlink.APCReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN BCSDocUpdateSupplementsSlink.OPCReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN BCSDocUpdateSupplementsSlink.APFReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN BCSDocUpdateSupplementsSlink.OPFReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN BCSDocUpdateSupplementsSlink.ExportedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN BCSDocUpdateSupplements.ProcessedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 0 THEN BCSDocUpdateSupplements.FilingStatusAddedDate
				ELSE NULL
				END AS 'StatusDate',             
				'Supplements' as ReportType,BCSDocUpdateSupplementsSlink.ZipFileName as RRDPDFURL,PdfName
            FROM BCSDocUpdateSupplements
				Left  JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID=BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID 
				AND BCSDocUpdateSupplements.IsRemoved=0 
				INNER JOIN FirstDollarDocType ON BCSDocUpdateSupplements.DocumentType = FirstDollarDocType.DocTypeId
				Inner Join BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = BCSDocUpdateSupplements.CUSIP
            WHERE
			(@Acc# IS NULL OR BCSDocUpdateSupplements.Acc# = @Acc#) And BCSDocUpdateSupplements.isRemoved=0					
			AND  (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateSupplements.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
			

		UNION
            SELECT BCSDocUpdateARSARID as Id, BCSDocUpdateARSAR.CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc as 'DocumentType', 
			CASE WHEN BCSDocUpdateARSAR.IsProcessed =0 THEN 'NA'
		       	 ELSE Replace(Replace(Replace(BCSDocUpdateARSAR.DocumentType,'RSAR','SAR'), 'RAR', 'AR'), 'RQR', 'QR') +  Replace(BCSDocUpdateARSAR.PDFName,'.pdf','') + 'GIM'
	        END AS 'DocumentID' ,
			DocumentDate, BCSDocUpdateARSAR.FundName, 
			   CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN 'Processed'
				WHEN BCSDocUpdateARSAR.IsProcessed = 0 THEN 'Filed'
				ELSE NULL
				END AS 'Status',
				CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN BCSDocUpdateARSARSlink.APCReceivedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN BCSDocUpdateARSARSlink.OPCReceivedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN BCSDocUpdateARSARSlink.APFReceivedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN BCSDocUpdateARSARSlink.OPFReceivedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN BCSDocUpdateARSARSlink.ExportedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN BCSDocUpdateARSAR.ProcessedDate
				WHEN BCSDocUpdateARSAR.IsProcessed = 0 THEN BCSDocUpdateARSAR.FilingStatusAddedDate
				ELSE NULL
				END AS 'StatusDate', 			  
			   'ARSAR' as ReportType,BCSDocUpdateARSARSlink.ZipFileName as RRDPDFURL,PdfName
			 FROM BCSDocUpdateARSAR 			   
			   Left  JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID=BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
			   AND BCSDocUpdateARSAR .IsRemoved=0 
			   INNER JOIN FirstDollarDocType ON BCSDocUpdateARSAR.DocumentType = FirstDollarDocType.DocTypeId
			   Inner Join [BCSTransamericaWatchListCUSIPs] On [BCSTransamericaWatchListCUSIPs].CUSIP = BCSDocUpdateARSAR.CUSIP
             WHERE
			 (@Acc# IS NULL OR BCSDocUpdateARSAR.Acc# = @Acc#) 
			 AND  (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateARSAR.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))			
       ) AS tblCUSIPDetails
	   WHERE (@DocIDsCount = 0 OR tblCUSIPDetails.DocumentId IN (SELECT d.DocumentId FROM @DocIDs d)) And (@Status IS NULL OR tblCUSIPDetails.[Status] = @Status)

--Select All CUSIPDetails according.
SELECT * FROM (
	SELECT ROW_NUMBER() OVER(ORDER By CUSIP) AS 'RowNumber', Id , CUSIP, EdgarID, Acc#, DocumentType, DocumentID,DocumentDate, FundName, Status, StatusDate, ReportType,RRDPDFURL,PdfName
	FROM @CUSIPDetails Cd
)t
WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex
order by CUSIP, DocumentType
	


--Find	AllCUSIPDetailsTotalCount
SELECT Count(CUSIP) As AllCUSIPDetailsTotalCount from  @CUSIPDetails

---CUSIP Not Preset
	SELECT t.marketId AS 'CUSIP'
	FROM @TaxonomyMarketIDs t
	LEFT JOIN (
		SELECT BCSDocUpdateSupplements.CUSIP FROM BCSDocUpdateSupplements
		INNER JOIN BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = BCSDocUpdateSupplements.CUSIP 
		WHERE BCSDocUpdateSupplements.Isremoved = 0
		
		UNION

		SELECT BCSDocUpdateARSAR.CUSIP FROM BCSDocUpdateARSAR 
		INNER JOIN BCSTransamericaWatchListCUSIPs On BCSTransamericaWatchListCUSIPs.CUSIP = BCSDocUpdateARSAR.CUSIP 
		WHERE BCSDocUpdateARSAR.Isremoved = 0
		 
		)cu ON cu.CUSIP = t.marketId
	WHERE cu.CUSIP IS NULL

	--Doc ID Not Present
	SELECT d.documentId AS 'DocumentID'
	FROM @DocIDs d
	LEFT JOIN @CUSIPDetails cd ON cd.DocumentID = d.documentId
	WHERE cd.DocumentID IS NULL


End
GO