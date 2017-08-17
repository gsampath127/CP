USE [db1029]
GO

ALTER PROCEDURE [dbo].[BCS_GetBCSSLINKReportData]
AS
BEGIN	
		
	SELECT DISTINCT 	
	CASE WHEN BCSDocUpdate.DocumentType IN ('SP','SPS','RSP') THEN 'SP' + Replace(BCSDocUpdate.PDFName,'.pdf','') + 'GIM'
		 WHEN BCSDocUpdate.DocumentType IN ('P','PS','RP') THEN 'P' + Replace(BCSDocUpdate.PDFName,'.pdf','') + 'GIM'			
	END AS 'SLINKFileName', 
	BCSDocUpdateGIMSlink.ZipFileName	
	FROM BCSDocUpdate
	INNER JOIN BCSDocUpdateGIMSlink On BCSDocUpdateGIMSlink.DocUpdateID = BCSDocUpdate.BCSDocUpdateId
	WHERE BCSDocUpdateGIMSlink.IsExported = 1 and BCSDocUpdateGIMSlink.ExportedDate >= DATEADD(minute,-5,DATEADD(day,-1,GETDATE()))
	
	UNION

	SELECT DISTINCT 	
		CASE WHEN BCSDocUpdateSupplements.DocumentType IN ('SP', 'RSP') THEN 'SP' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'
				WHEN BCSDocUpdateSupplements.DocumentType IN ('P', 'RP') THEN 'P' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'
				WHEN BCSDocUpdateSupplements.DocumentType IN ('SPS', 'PS') 
										THEN BCSDocUpdateSupplements.DocumentType + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + Convert(varchar, BCSDocUpdateSupplements.EdgarID) + 'GIM'
		END AS 'SLINKFileName', 
		BCSDocUpdateSupplementsSlink.ZipFileName	
	FROM BCSDocUpdateSupplements
	INNER JOIN BCSDocUpdateSupplementsSlink On BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID
	WHERE BCSDocUpdateSupplementsSlink.IsExported = 1 and BCSDocUpdateSupplementsSlink.ExportedDate >= DATEADD(minute,-5,DATEADD(day,-1,GETDATE()))
	
	UNION

	SELECT DISTINCT 	
		Replace(Replace(Replace(BCSDocUpdateARSAR.DocumentType,'RSAR','SAR'), 'RAR', 'AR'), 'RQR', 'QR') +  Replace(BCSDocUpdateARSAR.PDFName,'.pdf','') + 'GIM' AS 'SLINKFileName', 
		BCSDocUpdateARSARSlink.ZipFileName
	FROM BCSDocUpdateARSAR
	INNER JOIN BCSDocUpdateARSARSlink On BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
	WHERE BCSDocUpdateARSARSlink.IsExported = 1 and BCSDocUpdateARSARSlink.ExportedDate >= DATEADD(minute,-5,DATEADD(day,-1,GETDATE()))
	
	
End
Go

