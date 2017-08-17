USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GIMGetDocUpdateDetails]    Script Date: 08/25/2015 11:00:47 ******/



ALTER Procedure [dbo].[BCS_GIMGetDocUpdateDetails]
as
Begin
	SELECT distinct BCSDocUpdate.BCSDocUpdateId,BCSDocUpdate.CUSIP,
			FundName,
			PDFName,BCSDocUpdate.DocumentType,
			BCSDocUpdate.EffectiveDate,BCSDocUpdate.DocumentDate,ProsDocID as RRDInternalDocumentID,
			RRDPDFURL,IsFiled, BCSDocUpdate.FilingStatusAddedDate,
			isnull(BCSDocUpdateGIMSlink.IsExported,0) as IsExported ,BCSDocUpdateGIMSlink.ExportedDate,
			isnull(BCSDocUpdateGIMSlink.IsAPF,0) as IsAPF ,BCSDocUpdateGIMSlink.APFReceivedDate,
			isnull(BCSDocUpdateGIMSlink.IsOPF,0) as IsOPF ,BCSDocUpdateGIMSlink.OPFReceivedDate,
			BCSDocUpdateSECDetails.Acc# as SECAcc#, BCSDocUpdateSECDetails.DateFiled as SECDateFiled,
			BCSDocUpdateSECDetails.EffectiveDate as SECEffectiveDate,
			BCSDocUpdateSECDetails.DocumentDate as SECDocumentDate,BCSDocUpdateSECDetails.FormType as SECFormType,
			BCSDocUpdateSECDetails.DocumentType as SECDocumentType,isnull(BCSDocUpdateSECDetails.DocPriority,0) as DocPriority,
			BCSDocUpdate.[PageCount],BCSDocUpdate.PageSizeHeight,BCSDocUpdate.PageSizeWidth,
			SecurityTypeCode
	 FROM BCSDocUpdate
		 INNER JOIN ProsTicker on Prosticker.CUSIP = BCSDocUpdate.CUSIP
		 INNER JOIN Prospectus on BCSDocUpdate.ProsID = Prospectus.ProsID
		 LEFT OUTER JOIN BCSDocUpdateGIMSlink on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateGIMSlink.DocUpdateID		 
		 LEFT Outer JOIN
		 ( 
		   SELECT DocUpdateID,Acc#,
					DateFiled,DocumentDate,EffectiveDate,
					DocumentType,DocPriority,FormType 
		    FROM BCSDocUpdateSECDetails
		    INNER JOIN FirstDollarDocType on BCSDocUpdateSECDetails.DocumentType = FirstDollarDocType.DocTypeId
		  ) as BCSDocUpdateSECDetails on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateSECDetails.DocUpdateID
		  LEFT OUTER JOIN SecurityType ON SecurityType.SecurityTypeID = ProsTicker.SecurityTypeID
		  WHERE BCSDocUpdate.IsRemoved=0 
		  
	 ORDER BY BCSDocUpdate.CUSIP,DocPriority
 End