CREATE PROCEDURE [dbo].RPV2HostedSites_GetTaxonomyAssociationHierarchy
@RPV2HostedSitesTaxonomyAssociationHierarchyDocs [dbo].[RPV2HostedSitesTaxonomyAssociationHierarchyDocs] READONLY
AS
BEGIN


  DECLARE @XBRLDocumentTypeID int
  
  DECLARE @XBRLDocumentTypeName varchar(255)
  
  SELECT @XBRLDocumentTypeID = DocumentTypeID,
		@XBRLDocumentTypeName = Name
    FROM DocumentType
    WHERE MarketID = 'XBRL'

  IF EXISTS(SELECT TOP 1 RPV2HostedSitesTaxonomyAssociationHierarchyDocs.DocumentTypeID FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs 
						RPV2HostedSitesTaxonomyAssociationHierarchyDocs 
						INNER JOIN DocumentType on RPV2HostedSitesTaxonomyAssociationHierarchyDocs.DocumentTypeID = DocumentType.DocumentTypeID
						WHERE DocumentType.DocumentTypeID = @XBRLDocumentTypeID)
  BEGIN
		SELECT DISTINCT
			TAHD.TaxonomyID,
			TAHD.IsParent,
			CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
			TAHD.DocumentTypeID,
			Document.DocumentID,		  
			CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN DocumentType.[Name] ELSE '' END AS DocumentType,
			MarketId as VerticalMarketId,
			ContentUri	,
			CASE WHEN DocumentType.MarketID ='SP' THEN (CASE WHEN isnull(RevisedSPDate,'1/1/1980') > SPDate THEN RevisedSPDate ELSE SPDate END)
			 WHEN DocumentType.MarketID ='P'  THEN (CASE WHEN isnull(RevisedProsDate,'1/1/1980') > ProsDate THEN RevisedProsDate ELSE ProsDate END)
			 WHEN DocumentType.MarketID ='S'  THEN (CASE WHEN isnull(RevisedSDate,'1/1/1980') > SDate THEN RevisedSDate ELSE SDate END)
			 WHEN DocumentType.MarketID ='AR' THEN (CASE WHEN isnull(RevisedARDate,'1/1/1980') > ARDate THEN RevisedARDate ELSE ARDate END)
			 WHEN DocumentType.MarketID ='SAR'THEN (CASE WHEN isnull(RevisedSARDate,'1/1/1980') > SARDate THEN RevisedSARDate ELSE SARDate END)
			 WHEN DocumentType.MarketID ='PVR'THEN (CASE WHEN isnull(RevisedPVRDate,'1/1/1980') > PVRDate THEN RevisedPVRDate ELSE PVRDate END)
		END AS DocumentDate  
		FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
			INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID 
			INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
			INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID 
						AND Document.DocumentTypeID = TAHD.DocumentTypeID		 
			INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
		WHERE TAHD.DocumentTypeID != @XBRLDocumentTypeID AND Document.ClientID IS NULL
	UNION
		SELECT DISTINCT
			TAHD.TaxonomyID,
			TAHD.IsParent,
			CASE WHEN IsNameOverrideProvided = 0 THEN CompanyName + ' ' + ProsName ELSE '' END AS TaxonomyName,
			TAHD.DocumentTypeID,
			-1 AS DocumentID,		  
			CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN @XBRLDocumentTypeName ELSE '' END AS DocumentType,
			MarketId AS VerticalMarketId,
			'' AS ContentUri,
			NULL as DocumentDate				  	  
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
		CASE WHEN IsDocumentTypeNameOverrideProvided=1 THEN DocumentType.[Name] ELSE '' END AS DocumentType,
			MarketId as VerticalMarketId,
		Document.ContentUri,
		CASE WHEN DocumentType.MarketID ='SP' THEN (CASE WHEN isnull(RevisedSPDate,'1/1/1980') > SPDate THEN RevisedSPDate ELSE SPDate END)
			 WHEN DocumentType.MarketID ='P'  THEN (CASE WHEN isnull(RevisedProsDate,'1/1/1980') > ProsDate THEN RevisedProsDate ELSE ProsDate END)
			 WHEN DocumentType.MarketID ='S'  THEN (CASE WHEN isnull(RevisedSDate,'1/1/1980') > SDate THEN RevisedSDate ELSE SDate END)
			 WHEN DocumentType.MarketID ='AR' THEN (CASE WHEN isnull(RevisedARDate,'1/1/1980') > ARDate THEN RevisedARDate ELSE ARDate END)
			 WHEN DocumentType.MarketID ='SAR'THEN (CASE WHEN isnull(RevisedSARDate,'1/1/1980') > SARDate THEN RevisedSARDate ELSE SARDate END)
			 WHEN DocumentType.MarketID ='PVR'THEN (CASE WHEN isnull(RevisedPVRDate,'1/1/1980') > PVRDate THEN RevisedPVRDate ELSE PVRDate END)
		END AS DocumentDate 
	FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
		INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID
		INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
		INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND Document.DocumentTypeID = TAHD.DocumentTypeID		 
		INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
	WHERE Document.ClientID IS NULL
	ORDER BY TAHD.TaxonomyID
  END

END
GO
