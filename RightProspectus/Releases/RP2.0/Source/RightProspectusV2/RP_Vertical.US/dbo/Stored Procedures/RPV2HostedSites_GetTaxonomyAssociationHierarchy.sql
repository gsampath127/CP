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
			ContentUri	  
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
			'' AS ContentUri	  	  
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
		Document.ContentUri
	FROM @RPV2HostedSitesTaxonomyAssociationHierarchyDocs TAHD	 
		INNER JOIN Prospectus ON Prospectus.ProsID = TAHD.TaxonomyID
		INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
		INNER JOIN Document ON Prospectus.ProsID = Document.TaxonomyID AND Document.DocumentTypeID = TAHD.DocumentTypeID		 
		INNER JOIN DocumentType ON Document.DocumentTypeID = DocumentType.DocumentTypeID 
	WHERE Document.ClientID IS NULL
	ORDER BY TAHD.TaxonomyID
  END

END