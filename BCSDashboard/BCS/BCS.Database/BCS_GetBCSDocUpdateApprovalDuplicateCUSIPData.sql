USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSDocUpdateApprovalDuplicateCUSIPData]    Script Date: 3/24/2016 6:16:17 PM ******/
                    
ALTER PROCEDURE [dbo].[BCS_GetBCSDocUpdateApprovalDuplicateCUSIPData]
@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@Acc# nvarchar(250),
@Status nvarchar(20),
@StartIndex INT,
@EndIndex INT
AS
BEGIN

	DECLARE @TaxonomyMarketIDCount INT = 0
	SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t
--Find the duplicate CUSIP list
	DECLARE @DuplicateCusip Table(CUSIP varchar(20))

	INSERT INTO @DuplicateCusip
	SELECT DISTINCT CUSIP
	FROM BCSDocUpdate
	Where IsRemoved = 0 AND ISNULL(CUSIP, '') <> ''
	Group by CUSIP
	Having COUNT(CUSIP) > 1	
 	
	DECLARE @CUSIPDetails Table(BCSDocUpdateId int, CUSIP nvarchar(10),EdgarID int,	Acc# nvarchar(250) ,RRDPDFURL nvarchar(512) , DocumentType nvarchar(50), DocumentDate dateTime, FundName nvarchar(255), Status nvarchar(50));
	
--Find	DuplicateCUSIPDetailsTotalCount
SELECT Count(Status) As 'DuplicateCUSIPDetailsTotalCount' from 
(

SELECT CASE WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) != 0 THEN 'APC'
                      WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) != 0 THEN 'OPC'
                      WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) != 0 THEN 'AP'
                      WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) != 0 THEN 'OP'
                      WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) != 0 THEN 'EX'
                      WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) = 0 THEN 'Processed'
                      WHEN BCSDocUpdate.IsProcessed = 0 THEN 'Filed'
                      ELSE NULL
                      END  AS 'Status'
      FROM BCSDocUpdate        
      Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  BCSDocUpdate.BCSDocUpdateId
	  INNER JOIN @DuplicateCusip dc ON dc.CUSIP = BCSDocUpdate.CUSIP AND BCSDocUpdate.IsRemoved = 0
      WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdate.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t)) 
	  AND (@Acc# IS NULL OR BCSDocUpdate.Acc# = @Acc#) And  BCSDocUpdate.IsRemoved = 0 

) t 
WHERE (@Status IS NULL OR t.Status = @Status)
	
	
--Fetch duplicate cusip details and insert into @CUSIPDetails according to page size.	
	INSERT INTO @CUSIPDetails(BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate, FundName, Status)
		SELECT BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate, FundName, Status
		FROM
		(
			SELECT BCSDocUpdateId, BCSDocUpdate.CUSIP, EdgarID, Acc#, RRDPDFURL, FirstDollarDocType.DocTypeDesc AS 'DocumentType', DocumentDate,  FundName, 
			CASE WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) != 0 THEN 'APC'
			WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) != 0 THEN 'OPC'
			WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) != 0 THEN 'AP'
			WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) != 0 THEN 'OP'
			WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) != 0 THEN 'EX'
			WHEN BCSDocUpdate.IsProcessed = 1 AND ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) = 0 THEN 'Processed'
			WHEN BCSDocUpdate.IsProcessed = 0 THEN 'Filed'
			ELSE NULL
			END AS 'Status',
							ROW_NUMBER() OVER(ORDER By BCSDocUpdate.CUSIP, FirstDollarDocType.DocPriority) AS 'RowNumber'
			FROM BCSDocUpdate 	
			Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  BCSDocUpdate.BCSDocUpdateId		
			INNER JOIN @DuplicateCusip dc ON dc.CUSIP = BCSDocUpdate.CUSIP AND BCSDocUpdate.IsRemoved = 0
			INNER JOIN FirstDollarDocType ON BCSDocUpdate.DocumentType = FirstDollarDocType.DocTypeId
			WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdate.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
			AND (@Acc# IS NULL OR BCSDocUpdate.Acc# = @Acc#)	
			And (
			@status Is null 
			or (@status='APC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) != 0) 
			or (@status='OPC'  and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) != 0)
			or (@status='AP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) != 0 and ISNULL(BCSDocUpdateGIMSlink.IsAPC, 0) = 0)
			or (@status='OP'  and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) != 0 and ISNULL(BCSDocUpdateGIMSlink.IsOPC, 0) = 0)
			or (@status='EX'  and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) != 0 and (ISNULL(BCSDocUpdateGIMSlink.IsAPF, 0) = 0  and ISNULL(BCSDocUpdateGIMSlink.IsOPF, 0) = 0 ))
			or (@status='Processed' and IsProcessed = 1 and ISNULL(BCSDocUpdateGIMSlink.IsExported, 0) = 0) 
			or (@status='Filed' and IsProcessed = 0 ) 			 
			)
		
		) AS tblCUSIPDetails
		WHERE (@StartIndex = 0 AND @EndIndex = 0) OR (RowNumber >  @StartIndex AND RowNumber <= @EndIndex)
	
--Select duplicate CUSIPDetails according.			
	SELECT BCSDocUpdateId, CUSIP, EdgarID, Acc#, RRDPDFURL, DocumentType, DocumentDate, FundName, Status  FROM @CUSIPDetails 
	
End


