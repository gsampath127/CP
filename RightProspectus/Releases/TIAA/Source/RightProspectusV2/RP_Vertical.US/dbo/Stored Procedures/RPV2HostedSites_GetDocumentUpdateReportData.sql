CREATE PROCEDURE [dbo].[RPV2HostedSites_GetDocumentUpdateReportData]
@RPV2HostedSitesVerifyTaxonomyByMarketIdDataType [dbo].[RPV2HostedSitesVerifyTaxonomyByMarketIdDataType] READONLY,
@StartDate DateTime,
@EndDate DateTime
AS  
BEGIN

	--------Verify hostedAdmin.dbo.tblHostedUploadedPDF and get all file names updated between start date and end date
	DECLARE @tblHostedUploadedPDF TABLE(Company nvarchar(200), OriginalFileName nvarchar(500), DocumentUpdatedDate DateTime)

	INSERT INTO @tblHostedUploadedPDF(Company, OriginalFileName, DocumentUpdatedDate)
	SELECT Company, OriginalFileName, DocumentUpdatedDate 
	FROM (
			SELECT Company, ROW_NUMBER() OVER (PARTITION BY Company, OriginalFileName ORDER BY DATE DESC) AS 'RowNum' , OriginalFileName, Date 'DocumentUpdatedDate'
			FROM (
					SELECT   Company,DATE , SUBSTRING([Path], 
										LEN([Path]) - CHARINDEX('/', 
										REVERSE([Path])) + 2, 
										LEN([Path])) AS OriginalFileName from hostedAdmin.dbo.tblHostedUploadedPDF
					WHERE PATH like 'http://%' AND Date Between @startDate AND @EndDate
					UNION
					SELECT   Company,DATE , SUBSTRING([Path], 
											LEN([Path]) - CHARINDEX('\', 
											REVERSE([Path])) + 2, 
											LEN([Path])) AS OriginalFileName from  hostedAdmin.dbo.tblHostedUploadedPDF
					WHERE PATH not like 'http://%' AND Date Between @startDate AND @EndDate
			)as UploadedPDF
	)t
	WHERE t.RowNum = 1

	---  GET Document date details from Edgar table where EdgarFunds.DateUpdated Between @startDate AND @EndDate

	DECLARE @TempEdgar TABLE(ProsId INT, EdgarID INT, DocumentType varchar(250), DocumentDate DateTime)

	INSERT INTO @TempEdgar(ProsId, EdgarID,  DocumentType, DocumentDate)
	SELECT ProsId, EdgarID,  DocumentType, DocumentDate  FROM (	
	SELECT ROW_NUMBER() Over (partition by ProsID, DocumentType order by ProsID, DocumentOrder, DocumentDate desc) As rownum , * FROM (	
			SELECT DISTINCT Edgar.EdgarID,
								Edgar.DocumentDate,
								Prospectus.ProsID,			   			     		   
							   CASE WHEN Edgar.DocumentType LIKE 'Summary%' THEN 'SP'
									WHEN Edgar.DocumentType LIKE 'Prospectus%' THEN 'P'
									WHEN Edgar.DocumentType LIKE 'SAI%' THEN 'S'
									WHEN Edgar.DocumentType LIKE 'Semi Annual%' THEN 'SAR'
									WHEN Edgar.DocumentType LIKE 'Annual%' THEN 'AR'
									WHEN Edgar.DocumentType LIKE 'Proxy Voting Record%' THEN 'PVR'
								END 'DocumentType',
								CASE WHEN Edgar.DocumentType LIKE 'Summary%' THEN 1
									WHEN Edgar.DocumentType LIKE 'Prospectus%' THEN 2
									WHEN Edgar.DocumentType LIKE 'SAI%' THEN 3
									WHEN Edgar.DocumentType LIKE 'Semi Annual%' THEN 4
									WHEN Edgar.DocumentType LIKE 'Annual%' THEN 5
									WHEN Edgar.DocumentType LIKE 'Proxy Voting Record%' THEN 6
								END 'DocumentOrder'						
			   FROM EdgarFunds  
					INNER JOIN Edgar on Edgar.EdgarID = EdgarFunds.EdgarID  and  Edgar.DateFiled >= '01/01/2013'
					INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID					
					INNER JOIN Prospectus on EdgarFunds.FundID = Prospectus.ProsID	
					INNER JOIN @RPV2HostedSitesVerifyTaxonomyByMarketIdDataType TEMPMarketIDs ON  TEMPMarketIDs.MarketId = ProsTicker.CUSIP
					INNER JOIN Company on Company.CompanyID = Prospectus.CompanyID	
			   WHERE EdgarFunds.Processed='1' 			   
			   --AND EdgarFunds.DateUpdated Between @startDate AND @EndDate
	   )t
	)t1
	WHERE rownum = 1 	




	------SELECT Document update data

	SELECT TA.MarketId,
		DocumentType.DocumentTypeID,	
		CASE WHEN TA.IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,				
		CASE WHEN DocumentType.MarketID ='SP' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedSPDate,'1/1/1980') > SPDate THEN RevisedSPDate ELSE SPDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedSPDate,'1/1/1980') > SPDate THEN RevisedSPDate ELSE SPDate END)
							END
						)
			WHEN DocumentType.MarketID ='P' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedProsDate,'1/1/1980') > ProsDate THEN RevisedProsDate ELSE ProsDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedProsDate,'1/1/1980') > ProsDate THEN RevisedProsDate ELSE ProsDate END)
							END
						)
			WHEN DocumentType.MarketID ='S' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedSDate,'1/1/1980') > SDate THEN RevisedSDate ELSE SDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedSDate,'1/1/1980') > SDate THEN RevisedSDate ELSE SDate END)
							END
						)
			WHEN DocumentType.MarketID ='AR' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedARDate,'1/1/1980') > ARDate THEN RevisedARDate ELSE ARDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedARDate,'1/1/1980') > ARDate THEN RevisedARDate ELSE ARDate END)
							END
						)
			WHEN DocumentType.MarketID ='SAR' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedSARDate,'1/1/1980') > SARDate THEN RevisedSARDate ELSE SARDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedSARDate,'1/1/1980') > SARDate THEN RevisedSARDate ELSE SARDate END)
							END
						)
			WHEN DocumentType.MarketID ='PVR' 
					THEN (
							CASE WHEN ISNULL(tempEdgar.DocumentDate,'1/1/1980') >					
								(CASE WHEN ISNULL(RevisedPVRDate,'1/1/1980') > PVRDate THEN RevisedPVRDate ELSE PVRDate END)
								THEN tempEdgar.DocumentDate
								ELSE (CASE WHEN ISNULL(RevisedPVRDate,'1/1/1980') > PVRDate THEN RevisedPVRDate ELSE PVRDate END)
							END
						)
		END AS DocumentDate, 
		tblHPDF.DocumentUpdatedDate,
		DocPriority
	FROM @RPV2HostedSitesVerifyTaxonomyByMarketIdDataType TA
	INNER JOIN ProsTicker ON TA.MarketId = ProsTicker.CUSIP	
	INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsId	
	INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID	
	INNER JOIN Document ON Document.TaxonomyID = Prospectus.ProsId
	INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID	
	LEFT JOIN @TempEdgar tempEdgar ON tempEdgar.ProsId = Prospectus.ProsID	AND tempEdgar.DocumentType = DocumentType.MarketID
	LEFT JOIN @tblHostedUploadedPDF tblHPDF On tblHPDF.Company = Company.CompanyName AND tblHPDF.OriginalFileName = Document.FileName
	WHERE DocumentType.MarketID in ('SP','P','S','AR','SAR','PVR')


	UNION

	SELECT MarketId, DocumentTypeID, TaxonomyName, DocumentDate, DocumentUpdatedDate, DocPriority FROM (
		SELECT DISTINCT TA.MarketId , 21 As DocumentTypeID,
				CASE WHEN TA.IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
				Edgar.DocumentDate AS DocumentDate,		
				EdgarXBRL.CreatedDate 'DocumentUpdatedDate',
				21 As DocPriority,
				ROW_NUMBER() Over(partition by ProsTicker.CUSIP order by EdgarXBRL.CreatedDate desc) As 'RowNum'
			FROM EdgarXBRL	
			LEFT OUTER JOIN Edgar ON EdgarXBRL.EdgarID = Edgar.EdgarID AND Edgar.DateFiled >= '01/01/2013'
			INNER JOIN EdgarXBRLFunds ON EdgarXBRL.EdgarXBRLID = EdgarXBRLFunds.EdgarXBRLID 
			INNER JOIN ProsTicker  ON ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID
									AND ProsTicker.CIK = EdgarXBRLFunds.CIK	
			INNER JOIN 	@RPV2HostedSitesVerifyTaxonomyByMarketIdDataType TA On ProsTicker.CUSIP = TA.MarketId
			INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
			INNER JOIN Company ON EdgarXBRL.CompanyID = Company.CompanyID		
		)t 
	where t.RowNum = 1
		

	ORDER BY MarketId, DocPriority
	

END
GO
