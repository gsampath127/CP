USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSCustomerDocUpdateAllCUSIPData]    Script Date: 3/24/2016 6:12:11 PM ******/

ALTER PROCEDURE [dbo].[BCS_GetBCSCustomerDocUpdateAllCUSIPData] --exec [dbo].[BCS_GetBCSCustomerDocUpdateAllCUSIPData] Null,Null,Null,0,10

@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@Acc# nvarchar(250),
@Status nvarchar(20),
@StartIndex INT,
@EndIndex INT

AS

BEGIN

DECLARE @CUSIPDetails Table(RowNumber int,Id int, CUSIP nvarchar(10),EdgarID int,	Acc# nvarchar(250) ,RRDPDFURL nvarchar(512) , DocumentType nvarchar(50), 
							DocumentDate dateTime, FundName nvarchar(255), Status nvarchar(50), StatusDate dateTime, ReportType varchar(30),PdfName varchar(512));

DECLARE @TaxonomyMarketIDCount INT = 0
SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t
--Fetch duplicate cusip details and insert into @CUSIPDetails according to page size.	

	INSERT INTO @CUSIPDetails(RowNumber,Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status, StatusDate, RRDPDFURL,PdfName)
	Select ROW_NUMBER() OVER(ORDER By CUSIP) AS 'RowNumber' , Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status, StatusDate, RRDPDFURL,PdfName From
	(
      SELECT Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status, StatusDate, RRDPDFURL,PdfName
	   FROM
       (
			SELECT BCSDocUpdateSupplementsId as Id, [BCSDocUpdateSupplements].CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc as 'DocumentType', DocumentDate,  FundName, 
            	CASE WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN 'AP'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN 'Processed'
			WHEN [BCSDocUpdateSupplements].IsProcessed = 0 THEN 'Filed'
			ELSE NULL
			END AS 'Status',
			CASE WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN BCSDocUpdateSupplementsSlink.APCReceivedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN BCSDocUpdateSupplementsSlink.OPCReceivedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN BCSDocUpdateSupplementsSlink.APFReceivedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN BCSDocUpdateSupplementsSlink.OPFReceivedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN BCSDocUpdateSupplementsSlink.ExportedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN BCSDocUpdateSupplements.ProcessedDate
				WHEN [BCSDocUpdateSupplements].IsProcessed = 0 THEN BCSDocUpdateSupplements.FilingStatusAddedDate
				ELSE NULL
			END AS 'StatusDate',
                --ROW_NUMBER() OVER(ORDER By [BCSDocUpdateSupplements].CUSIP) AS 'RowNumber',
				'Supplements' as ReportType,BCSDocUpdateSupplementsSlink.ZipFileName as RRDPDFURL,PdfName
            FROM BCSDocUpdateSupplements
			    --Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  [BCSDocUpdateSupplements].BCSDocUpdateSupplementsId
				Left JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID=BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID 
				AND BCSDocUpdateSupplements.IsRemoved=0 
				INNER JOIN FirstDollarDocType ON [BCSDocUpdateSupplements].DocumentType = FirstDollarDocType.DocTypeId
            WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateSupplements.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
			AND (@Acc# IS NULL OR [BCSDocUpdateSupplements].Acc# = @Acc#) And 	[BCSDocUpdateSupplements].isRemoved=0					
			And (
				@status Is null 
				or (@status='APC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0) 
				or (@status='OPC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0)
				or (@status='AP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 and ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) = 0)
				or (@status='OP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 and ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) = 0)
				or (@status='EX'  and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 and (ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) = 0  and ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) = 0 ))
				or (@status='Processed' and IsProcessed = 1 and ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0) 
				or (@status='Filed' and IsProcessed = 0 ) 			 
			)

     UNION
            SELECT BCSDocUpdateARSARID as Id, [BCSDocUpdateARSAR].CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc as 'DocumentType', DocumentDate,  FundName, 
			CASE WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN 'Processed'
			WHEN [BCSDocUpdateARSAR].IsProcessed = 0 THEN 'Filed'
			ELSE NULL
			END AS 'Status',  
			CASE WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN BCSDocUpdateARSARSlink.APCReceivedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN BCSDocUpdateARSARSlink.OPCReceivedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN BCSDocUpdateARSARSlink.APFReceivedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN BCSDocUpdateARSARSlink.OPFReceivedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN BCSDocUpdateARSARSlink.ExportedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN BCSDocUpdateARSAR.ProcessedDate
				WHEN [BCSDocUpdateARSAR].IsProcessed = 0 THEN BCSDocUpdateARSAR.FilingStatusAddedDate
				ELSE NULL
				END AS 'StatusDate',
			  -- ROW_NUMBER() OVER(ORDER By [BCSDocUpdateARSAR].CUSIP) AS 'RowNumber',
			   'ARSAR' as ReportType,BCSDocUpdateARSARSlink.ZipFileName as RRDPDFURL,PdfName
			 FROM BCSDocUpdateARSAR 
			    --Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  [BCSDocUpdateARSAR].BCSDocUpdateARSARID
			   Left JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID=BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
			   AND BCSDocUpdateARSAR.IsRemoved=0 
			   INNER JOIN FirstDollarDocType ON [BCSDocUpdateARSAR].DocumentType = FirstDollarDocType.DocTypeId
             WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateARSAR.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
			 AND (@Acc# IS NULL OR [BCSDocUpdateARSAR].Acc# = @Acc#)
			 And (
				@status Is null 
				or (@status='APC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0) 
				or (@status='OPC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0)
				or (@status='AP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 and ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) = 0)
				or (@status='OP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 and ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) = 0)
				or (@status='EX'  and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 and (ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) = 0  and ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) = 0 ))
				or (@status='Processed' and IsProcessed = 1 and ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0) 
				or (@status='Filed' and IsProcessed = 0 ) 			 
			)
       ) AS tblCUSIPDetails)As t
	   
--Select duplicate CUSIPDetails according.

 SELECT Id , CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status, StatusDate, ReportType,RRDPDFURL,PdfName   FROM @CUSIPDetails AS CD
 WHERE ((@StartIndex = 0 AND @EndIndex = 0) OR (RowNumber >  @StartIndex AND RowNumber <= @EndIndex))
 --AND  (@Status is null or cd.Status = @Status)
 order by CUSIP, DocumentType


--Find	DuplicateCUSIPDetailsTotalCount
SELECT Count(Status) As AllCUSIPDetailsTotalCount from 
(

SELECT CASE WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN 'AP'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN 'Processed'
                      WHEN [BCSDocUpdateSupplements].IsProcessed = 0 THEN 'Filed'
                      ELSE NULL
                      END  AS 'Status'
      FROM BCSDocUpdateSupplements        
      Left JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID=BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID
	  --AND [BCSDocUpdateSupplements].IsRemoved=0 
      WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateSupplements.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
	  AND (@Acc# IS NULL OR [BCSDocUpdateSupplements].Acc# = @Acc#) And  [BCSDocUpdateSupplements].IsRemoved = 0 
Union all
		Select CASE WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN 'Processed'
					WHEN [BCSDocUpdateARSAR].IsProcessed = 0 THEN 'Filed'
					ELSE NULL
					END AS 'Status'
		 FROM BCSDocUpdateARSAR 
		--Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  [BCSDocUpdateARSAR].BCSDocUpdateARSARID
		Left JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID=BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
					    			  
		WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateARSAR.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
		AND (@Acc# IS NULL OR [BCSDocUpdateARSAR].Acc# = @Acc#) AND [BCSDocUpdateARSAR] .IsRemoved=0
) t 
WHERE (@Status IS NULL OR t.Status = @Status)
	

End
GO


