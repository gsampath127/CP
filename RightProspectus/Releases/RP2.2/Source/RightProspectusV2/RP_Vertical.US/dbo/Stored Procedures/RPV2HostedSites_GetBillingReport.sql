CREATE PROCEDURE [dbo].[RPV2HostedSites_GetBillingReport]
@RPV2HostedSitesTaxonomyBillingReport [dbo].[RPV2HostedSites_BillingReport] READONLY,
@ClientName NVARCHAR(200),
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
					WHERE PATH like 'http://%' AND Date > @startDate AND Date <= @EndDate
					UNION
					SELECT   Company,DATE , SUBSTRING([Path], 
											LEN([Path]) - CHARINDEX('\', 
											REVERSE([Path])) + 2, 
											LEN([Path])) AS OriginalFileName from  hostedAdmin.dbo.tblHostedUploadedPDF
					WHERE PATH not like 'http://%' AND Date > @startDate AND Date <= @EndDate
			)as UploadedPDF
	)t
	WHERE t.RowNum = 1

	  DECLARE @XBRLDocumentTypeID int
  
	  DECLARE @XBRLDocumentTypeName varchar(255)
  
	  SELECT @XBRLDocumentTypeID = DocumentTypeID,
			@XBRLDocumentTypeName = Name
		FROM DocumentType
		WHERE MarketID = 'XBRL'

		DECLARE @CLIENTID INT
		SELECT @CLIENTID = CLIENTID FROM CustomizedDocClients WHERE ClientName = @ClientName


		IF @CLIENTID IS NULL 
			BEGIN
				SELECT DISTINCT CUSIP, DocPriority, SeriesID, DocumentTypeMarketId, FundName, PDFURL, SeriesName,
				tblHPDF.DocumentUpdatedDate
				FROM (
						SELECT DISTINCT CUSIP, DocumentType.DocPriority, ProsTicker.SeriesID, DocumentType.MarketID 'DocumentTypeMarketId', 
							CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'FundName',
							Document.ContentUri As 'PDFURL'	,[FundSeries].SeriesName, FileName,
							(
								SUBSTRING(REPLACE(ContentUri, '/'+ FileName, ''), 
								LEN(REPLACE(ContentUri, '/'+ FileName, '')) - CHARINDEX('/', 
								REVERSE(REPLACE(ContentUri, '/'+ FileName, ''))) + 2, 
								LEN(REPLACE(ContentUri, '/'+ FileName, '')))
							)As PDFCompanyName					   	  	  
						FROM @RPV2HostedSitesTaxonomyBillingReport TAHD	 
							INNER JOIN ProsTicker ON ProsTicker.CUSIP = TAHD.MarketId
							INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
							INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
							INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND ClientID IS NULL	
							INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID
							LEFT JOIN FundSeries ON FundSeries.SeriesID = ProsTicker.SeriesID
							WHERE DocumentType.MarketID IN ('SP', 'P', 'S', 'AR', 'SAR', 'FS', 'PVR')
				)docDetails
				LEFT JOIN @tblHostedUploadedPDF tblHPDF On  tblHPDF.Company = docDetails.PDFCompanyName  AND tblHPDF.OriginalFileName = docDetails.[FileName]

				UNION
					SELECT DISTINCT
						CUSIP, 21 As 'DocPriority', ProsTicker.SeriesID, 'XBRL' As 'DocumentTypeMarketId', 
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'FundName',
						'' As 'PDFURL', '' SeriesName, NULL As DocumentUpdatedDate  	  
					FROM @RPV2HostedSitesTaxonomyBillingReport TAHD	 
						INNER JOIN ProsTicker ON ProsTicker.CUSIP = TAHD.MarketId
						INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
						INNER JOIN EdgarXBRLFunds on ProsTicker.CIK = EdgarXBRLFunds.CIK 
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID 
									AND ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
						INNER JOIN EdgarXBRL ON EdgarXBRLFunds.EdgarXBRLId = EdgarXBRL.EdgarXBRLId
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN XBRLCompanies on Company.CompanyID = XBRLCompanies.CompanyID
				ORDER BY 1,2
			END	
		ELSE
			BEGIN			
				SELECT DISTINCT CUSIP, DocPriority, SeriesID, DocumentTypeMarketId, FundName, PDFURL, SeriesName,
				tblHPDF.DocumentUpdatedDate
				FROM (
						SELECT DISTINCT CUSIP, DocumentType.DocPriority, ProsTicker.SeriesID, DocumentType.MarketID 'DocumentTypeMarketId', 
							CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'FundName',
							ISNULL(CustomDocs.ContentUri,Document.ContentUri) AS 'PDFURL',
							[FundSeries].SeriesName, ISNULL(CustomDocs.FileName,Document.FileName) As 'FileName',
							(
								SUBSTRING(REPLACE(ISNULL(CustomDocs.ContentUri, Document.ContentUri), '/'+ ISNULL(CustomDocs.FileName, Document.FileName), ''), 
								LEN(REPLACE(ISNULL(CustomDocs.ContentUri, Document.ContentUri), '/'+ ISNULL(CustomDocs.FileName, Document.FileName), '')) - CHARINDEX('/', 
								REVERSE(REPLACE(ISNULL(CustomDocs.ContentUri, Document.ContentUri), '/'+ ISNULL(CustomDocs.FileName, Document.FileName), ''))) + 2, 
								LEN(REPLACE(ISNULL(CustomDocs.ContentUri, Document.ContentUri), '/'+ ISNULL(CustomDocs.FileName, Document.FileName), '')))
							)As PDFCompanyName					   	  	  					   	  	  
						FROM @RPV2HostedSitesTaxonomyBillingReport TAHD	 
							INNER JOIN ProsTicker ON ProsTicker.CUSIP = TAHD.MarketId
							INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
							INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
							INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND ClientID IS NULL	
							INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
							LEFT JOIN FundSeries ON FundSeries.SeriesID = ProsTicker.SeriesID
							LEFT OUTER JOIN Document CustomDocs ON Prospectus.ProsID = CustomDocs.TaxonomyID AND CustomDocs.ClientID = @ClientID
							WHERE DocumentType.MarketID IN ('SP', 'P', 'S', 'AR', 'SAR', 'FS', 'PVR')
				)docDetails
				LEFT JOIN @tblHostedUploadedPDF tblHPDF On  tblHPDF.Company = docDetails.PDFCompanyName  AND tblHPDF.OriginalFileName = docDetails.[FileName]

				UNION
					SELECT DISTINCT
						CUSIP, 21 As 'DocPriority', ProsTicker.SeriesID, 'XBRL' As 'DocumentTypeMarketId', 
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName+ ' - '+ ProsName ELSE '' END AS 'FundName',
						'' As 'PDFURL', '' SeriesName, NULL As DocumentUpdatedDate  	  
					FROM @RPV2HostedSitesTaxonomyBillingReport TAHD	 
						INNER JOIN ProsTicker ON ProsTicker.CUSIP = TAHD.MarketId
						INNER JOIN Prospectus ON Prospectus.ProsID = ProsTicker.ProspectusID
						INNER JOIN EdgarXBRLFunds on ProsTicker.CIK = EdgarXBRLFunds.CIK 
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID 
									AND ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
						INNER JOIN EdgarXBRL ON EdgarXBRLFunds.EdgarXBRLId = EdgarXBRL.EdgarXBRLId
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN XBRLCompanies on Company.CompanyID = XBRLCompanies.CompanyID
				ORDER BY 1,2
					
			END
END
GO