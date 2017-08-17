CREATE PROCEDURE [dbo].RPV2HostedSites_GetTaxonomySpecificDocumentFrame
@RPV2HostedSitesTaxonomyAssociationHierarchyDocs [dbo].[RPV2HostedSitesTaxonomyAssociationHierarchyDocs] READONLY,
@ClientName NVARCHAR(200)
AS
BEGIN

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

					SELECT DISTINCT
						TAHD.TaxonomyID,
						TAHD.IsParent,
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
						TAHD.DocumentTypeID,
						Document.DocumentID,
						Document.ContentUri,
						CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN DocumentType.[Name] ELSE '' END AS DocumentType,
						MarketId as VerticalMarketId	  	  	  
					FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
						INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND Document.DocumentTypeID = TAHD.DocumentTypeID AND ClientID IS NULL	
						INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
				UNION
					SELECT DISTINCT
						TAHD.TaxonomyID,
						TAHD.IsParent,
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
						TAHD.DocumentTypeID,
						-1 AS DocumentID,	
						'' AS ContentUri,	  
						CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN @XBRLDocumentTypeName ELSE '' END AS DocumentType,
						MarketId as VerticalMarketId	  	  
					FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
						INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID 
						INNER JOIN ProsTicker ON Prospectus.ProsID = ProsTicker.ProspectusID
						INNER JOIN EdgarXBRLFunds on ProsTicker.CIK = EdgarXBRLFunds.CIK 
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID 
									AND ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
						INNER JOIN EdgarXBRL ON EdgarXBRLFunds.EdgarXBRLId = EdgarXBRL.EdgarXBRLId
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN XBRLCompanies on Company.CompanyID = XBRLCompanies.CompanyID
						INNER JOIN DocumentType ON TAHD.DocumentTypeID = DocumentType.DocumentTypeID
					WHERE TAHD.DocumentTypeID = @XBRLDocumentTypeID
				ORDER BY 1
			END	
		ELSE
			BEGIN		    
					SELECT DISTINCT
						TAHD.TaxonomyID,
						TAHD.IsParent,
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
						TAHD.DocumentTypeID,
						Document.DocumentID,
						ISNULL(CustomDocs.ContentUri,Document.ContentUri) AS ContentUri,
						CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN DocumentType.[Name] ELSE '' END AS DocumentType,
						MarketId as VerticalMarketId	  	  	  
					FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
						INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND Document.DocumentTypeID = TAHD.DocumentTypeID AND Document.ClientID IS NULL	
						INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
						LEFT OUTER JOIN Document CustomDocs ON Prospectus.ProsID = CustomDocs.TaxonomyID AND CustomDocs.DocumentTypeID = TAHD.DocumentTypeID AND CustomDocs.ClientID = @ClientID
				UNION
					SELECT DISTINCT
						TAHD.TaxonomyID,
						TAHD.IsParent,
						CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
						TAHD.DocumentTypeID,
						-1 AS DocumentID,	
						'' AS ContentUri,	  
						CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN @XBRLDocumentTypeName ELSE '' END AS DocumentType,
						MarketId as VerticalMarketId	  	  
					FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
						INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID 
						INNER JOIN ProsTicker ON Prospectus.ProsID = ProsTicker.ProspectusID
						INNER JOIN EdgarXBRLFunds on ProsTicker.CIK = EdgarXBRLFunds.CIK 
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID 
									AND ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
						INNER JOIN EdgarXBRL ON EdgarXBRLFunds.EdgarXBRLId = EdgarXBRL.EdgarXBRLId
						INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
						INNER JOIN XBRLCompanies on Company.CompanyID = XBRLCompanies.CompanyID
						INNER JOIN DocumentType ON TAHD.DocumentTypeID = DocumentType.DocumentTypeID
					WHERE TAHD.DocumentTypeID = @XBRLDocumentTypeID

				ORDER BY 1   
			END


END