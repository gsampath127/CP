USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSCustomerDocUpdateDuplicateCUSIPData]    Script Date: 3/24/2016 6:13:31 PM ******/

ALTER PROCEDURE [dbo].[BCS_GetBCSCustomerDocUpdateDuplicateCUSIPData] --exec [BCS_GetBCSCustomerDocUpdateDuplicateCUSIPData] Null,Null,Null,0,1000
@TaxonomyMarketIDs TT_TaxonomyMarketId READONLY,
@Acc# nvarchar(250),
@Status nvarchar(20),
@StartIndex INT,
@EndIndex INT

AS

BEGIN

--Find the duplicate CUSIP list
	DECLARE @TaxonomyMarketIDCount INT = 0
	SELECT @TaxonomyMarketIDCount = Count(t.marketId) FROM @TaxonomyMarketIDs t

	DECLARE @DuplicateCusip Table(CUSIP varchar(20),DocumentType nvarchar(8),DocumentDate Datetime)

	INSERT INTO @DuplicateCusip

	Select CUSIP,DocumentType,DOCUMENTDATE
    from BCSDocUpdateSupplements
	WHERE BCSDocUpdateSupplements.IsRemoved=0 AND BCSDocUpdateSupplements.IsNotDuplicate= 0
	AND (Convert(varchar, BCSDocUpdateSupplements.ProcessedDate, 101) = Convert(varchar, GETDATE(), 101)
		 OR Convert(varchar, BCSDocUpdateSupplements.FilingStatusAddedDate, 101) = Convert(varchar, GETDATE(), 101))
    group by DocumentType, CUSIP,DOCUMENTDATE
	having count(*) > 1	



   DECLARE @DuplicateCusipARSAR Table(CUSIP varchar(20),DocumentType nvarchar(8),DocumentDate Datetime)



	INSERT INTO @DuplicateCusipARSAR
    select CUSIP,DocumentType,DOCUMENTDATE
    from BCSDocUpdateARSAR
	WHERE BCSDocUpdateARSAR.IsRemoved=0 AND BCSDocUpdateARSAR.IsNotDuplicate= 0
	AND (Convert(varchar, BCSDocUpdateARSAR.ProcessedDate, 101) = Convert(varchar, GETDATE(), 101)
		 OR Convert(varchar, BCSDocUpdateARSAR.FilingStatusAddedDate, 101) = Convert(varchar, GETDATE(), 101))
    group by DocumentType, CUSIP,DOCUMENTDATE
    having count(*) > 1	





	DECLARE @CUSIPDetails Table(RowNumber int,Id int, CUSIP nvarchar(10),EdgarID int,	Acc# nvarchar(250) ,RRDPDFURL nvarchar(512) , DocumentType nvarchar(50), DocumentDate dateTime, FundName nvarchar(255), Status nvarchar(50),ReportType varchar(30),PdfName varchar(512));

--Fetch duplicate cusip details and insert into @CUSIPDetails according to page size.	
	INSERT INTO @CUSIPDetails(RowNumber,Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status,ReportType,RRDPDFURL,PdfName)
	Select ROW_NUMBER() OVER(ORDER By CUSIP) AS 'RowNumber' , Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status,ReportType,RRDPDFURL,PdfName From
	(
    	SELECT Id, CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status,ReportType,RRDPDFURL,PdfName
     	FROM
			(
			 SELECT BCSDocUpdateSupplementsId as Id, [BCSDocUpdateSupplements].CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc AS 'DocumentType', [BCSDocUpdateSupplements].DocumentDate,  FundName, 
				Case WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN 'AP'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) = 0 THEN 'Processed'
				WHEN [BCSDocUpdateSupplements].IsProcessed = 0 THEN 'Filed'
				ELSE NULL
				END AS 'Status',    											
				'Supplements' as ReportType,BCSDocUpdateSupplementsSlink.ZipFileName as RRDPDFURL,PdfName
			 FROM [BCSDocUpdateSupplements] 
			      --Left Join BCSDocupdateGIMSlink on BCSDocupdateGIMSlink.DocUpdateId =  [BCSDocUpdateSupplements].BCSDocUpdateSupplementsId
				  Left JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID=BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID
				  INNER JOIN @DuplicateCusip dc ON dc.CUSIP = [BCSDocUpdateSupplements].CUSIP				  
				  INNER JOIN FirstDollarDocType ON BCSDocUpdateSupplements.DocumentType = FirstDollarDocType.DocTypeId
				  AND  dc.documentType = [BCSDocUpdateSupplements].DocumentType 
				  AND  dc.documentDate = [BCSDocUpdateSupplements].DocumentDate 
				  AND [BCSDocUpdateSupplements].IsRemoved=0 AND [BCSDocUpdateSupplements].IsNotDuplicate= 0
			 WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateSupplements.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
			 AND (@Acc# IS NULL OR [BCSDocUpdateSupplements].Acc# = @Acc#)
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

     SELECT BCSDocUpdateARSARID as Id, [BCSDocUpdateARSAR].CUSIP, EdgarID, Acc#,FirstDollarDocType.DocTypeDesc AS 'DocumentType', [BCSDocUpdateARSAR].DocumentDate,  FundName, 
     	      CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
				WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) = 0 THEN 'Processed'
				WHEN BCSDocUpdateARSAR.IsProcessed = 0 THEN 'Filed'
				ELSE NULL
				END AS 'Status',     
		'ARSAR' as ReportType,BCSDocUpdateARSARSlink.ZipFileName as RRDPDFURL,PdfName
     FROM [BCSDocUpdateARSAR] 
	 Left JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID=BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
	    INNER JOIN @DuplicateCusipARSAR dcArSar ON dcArSar.CUSIP = [BCSDocUpdateARSAR].CUSIP		
		INNER JOIN FirstDollarDocType ON BCSDocUpdateARSAR.DocumentType = FirstDollarDocType.DocTypeId
        AND  dcArSar.documentType = [BCSDocUpdateARSAR].DocumentType 
        AND  dcArSar.documentDate = [BCSDocUpdateARSAR].DocumentDate 
     	AND [BCSDocUpdateARSAR].IsRemoved=0 AND [BCSDocUpdateARSAR].IsNotDuplicate= 0
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
	    ) AS tblCUSIPDetails)as t

--Select duplicate CUSIPDetails according.	

	SELECT Id , CUSIP, EdgarID, Acc#, DocumentType, DocumentDate, FundName, Status,ReportType,RRDPDFURL,PdfName  FROM @CUSIPDetails as cd
	WHERE ((@StartIndex = 0 AND @EndIndex = 0) OR (RowNumber >  @StartIndex AND RowNumber <= @EndIndex))	
	order by CUSIP , DocumentDate,DocumentType

	--Find	DuplicateCUSIPDetailsTotalCount
SELECT Count(Status) As DuplicateCUSIPDetailsTotalCount from 
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
      FROM [BCSDocUpdateSupplements]      
      Left JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID=BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID 
	  INNER JOIN @DuplicateCusip dc ON dc.CUSIP = [BCSDocUpdateSupplements].CUSIP   
	  AND  dc.documentType = [BCSDocUpdateSupplements].DocumentType 
	 AND  dc.documentDate = [BCSDocUpdateSupplements].DocumentDate 
	 AND [BCSDocUpdateSupplements].IsRemoved=0 AND [BCSDocUpdateSupplements].IsNotDuplicate= 0
	  WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateSupplements.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
	  AND (@Acc# IS NULL OR [BCSDocUpdateSupplements].Acc# = @Acc#) 
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
		FROM [BCSDocUpdateARSAR] 			
		Left JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID=BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID	
		INNER JOIN @DuplicateCusipARSAR dcArSar ON dcArSar.CUSIP = [BCSDocUpdateARSAR].CUSIP
		 AND  dcArSar.documentType = [BCSDocUpdateARSAR].DocumentType 
        AND  dcArSar.documentDate = [BCSDocUpdateARSAR].DocumentDate 
		 AND [BCSDocUpdateARSAR] .IsRemoved=0 AND [BCSDocUpdateARSAR].IsNotDuplicate= 0			    			  
		WHERE (@TaxonomyMarketIDCount = 0 OR BCSDocUpdateARSAR.CUSIP IN (SELECT t.marketId from @TaxonomyMarketIDs t))
		AND (@Acc# IS NULL OR [BCSDocUpdateARSAR].Acc# = @Acc#) 
) t 
WHERE (@Status IS NULL OR t.Status = @Status)
End