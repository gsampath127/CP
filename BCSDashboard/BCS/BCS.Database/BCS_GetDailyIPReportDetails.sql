USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetDailyIPReportDetails]    Script Date: 2/17/2016 6:20:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER Procedure [dbo].[BCS_GetDailyIPReportDetails] 
@ClientName nvarchar(200),
@CreatedDate DateTime,
@sortDirection NVARCHAR(10),
@sortColumn NVARCHAR(100)
AS  
BEGIN


	IF @ClientName = 'Transamerica'
	BEGIN

		SELECT * ,
			CASE  
					WHEN @sortColumn = 'BCSTransamericaDailyIPDetailsID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.BCSTransamericaDailyIPDetailsID Asc) 									  
					WHEN @sortColumn = 'BCSTransamericaDailyIPDetailsID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.BCSTransamericaDailyIPDetailsID Desc)
					WHEN @sortColumn = 'IPDocUpdateFileName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.IPDocUpdateFileName Asc) 									  
					WHEN @sortColumn = 'IPDocUpdateFileName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.IPDocUpdateFileName Desc)
					WHEN @sortColumn = 'HeaderDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.HeaderDate Asc) 									  
					WHEN @sortColumn = 'HeaderDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.HeaderDate Desc)
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.CUSIP Asc) 									  
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.CUSIP Desc)
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.FundName Asc) 									  
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.FundName Desc)
					WHEN @sortColumn = 'PDFName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PDFName Asc) 									  
					WHEN @sortColumn = 'PDFName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PDFName Desc)
					WHEN @sortColumn = 'DocumentType' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.DocumentType Asc) 									  
					WHEN @sortColumn = 'DocumentType' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.DocumentType Desc)
					WHEN @sortColumn = 'EffectiveDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.EffectiveDate Asc) 									  
					WHEN @sortColumn = 'EffectiveDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.EffectiveDate Desc)
					WHEN @sortColumn = 'DocumentDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.DocumentDate Asc) 									  
					WHEN @sortColumn = 'DocumentDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.DocumentDate Desc)
					WHEN @sortColumn = 'RRDInternalDocumentID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.RRDInternalDocumentID Asc) 									  
					WHEN @sortColumn = 'RRDInternalDocumentID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.RRDInternalDocumentID Desc)
					WHEN @sortColumn = 'SECAcc#' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECAcc# Asc) 									  
					WHEN @sortColumn = 'SECAcc#' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECAcc# Desc)
					WHEN @sortColumn = 'SECDateFiled' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECDateFiled Asc) 									  
					WHEN @sortColumn = 'SECDateFiled' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECDateFiled Desc)
					WHEN @sortColumn = 'SECFormType' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECFormType Asc) 									  
					WHEN @sortColumn = 'SECFormType' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.SECFormType Desc)
					WHEN @sortColumn = 'EdgarID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.EdgarID Asc) 									  
					WHEN @sortColumn = 'EdgarID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.EdgarID Desc)
					WHEN @sortColumn = 'PageCount' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.[PageCount] Asc) 									  
					WHEN @sortColumn = 'PageCount' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.[PageCount] Desc)
					WHEN @sortColumn = 'PageSizeHeight' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PageSizeHeight Asc) 									  
					WHEN @sortColumn = 'PageSizeHeight' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PageSizeHeight Desc)
					WHEN @sortColumn = 'PageSizeWidth' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PageSizeWidth Asc) 									  
					WHEN @sortColumn = 'PageSizeWidth' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.PageSizeWidth Desc)
					WHEN @sortColumn = 'RPProcessStep' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.RPProcessStep Asc) 									  
					WHEN @sortColumn = 'RPProcessStep' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.RPProcessStep Desc)
					WHEN @sortColumn = 'CreatedDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.CreatedDate Asc) 									  
					WHEN @sortColumn = 'CreatedDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSTransamericaDailyIPDetails.CreatedDate Desc)
			END
			FROM BCSTransamericaDailyIPDetails
		WHERE CONVERT(VARCHAR, @CreatedDate, 101) = CONVERT(VARCHAR, CreatedDate, 101)
	
	END
	ELSE IF @ClientName = 'AllianceBernstein'
	BEGIN
	
		SELECT *,
		CASE  
					WHEN @sortColumn = 'BCSAllianceBernsteinDailyIPDetailsID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.BCSAllianceBernsteinDailyIPDetailsID Asc) 									  
					WHEN @sortColumn = 'BCSAllianceBernsteinDailyIPDetailsID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.BCSAllianceBernsteinDailyIPDetailsID Desc)
					WHEN @sortColumn = 'IPDocUpdateFileName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.IPDocUpdateFileName Asc) 									  
					WHEN @sortColumn = 'IPDocUpdateFileName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.IPDocUpdateFileName Desc)
					WHEN @sortColumn = 'HeaderDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.HeaderDate Asc) 									  
					WHEN @sortColumn = 'HeaderDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.HeaderDate Desc)
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.CUSIP Asc) 									  
					WHEN @sortColumn = 'CUSIP' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.CUSIP Desc)
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.FundName Asc) 									  
					WHEN @sortColumn = 'FundName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.FundName Desc)
					WHEN @sortColumn = 'PDFName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PDFName Asc) 									  
					WHEN @sortColumn = 'PDFName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PDFName Desc)
					WHEN @sortColumn = 'DocumentType' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.DocumentType Asc) 									  
					WHEN @sortColumn = 'DocumentType' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.DocumentType Desc)
					WHEN @sortColumn = 'EffectiveDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.EffectiveDate Asc) 									  
					WHEN @sortColumn = 'EffectiveDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.EffectiveDate Desc)
					WHEN @sortColumn = 'DocumentDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.DocumentDate Asc) 									  
					WHEN @sortColumn = 'DocumentDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.DocumentDate Desc)
					WHEN @sortColumn = 'RRDInternalDocumentID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.RRDInternalDocumentID Asc) 									  
					WHEN @sortColumn = 'RRDInternalDocumentID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.RRDInternalDocumentID Desc)
					WHEN @sortColumn = 'SECAcc#' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECAcc# Asc) 									  
					WHEN @sortColumn = 'SECAcc#' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECAcc# Desc)
					WHEN @sortColumn = 'SECDateFiled' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECDateFiled Asc) 									  
					WHEN @sortColumn = 'SECDateFiled' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECDateFiled Desc)
					WHEN @sortColumn = 'SECFormType' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECFormType Asc) 									  
					WHEN @sortColumn = 'SECFormType' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.SECFormType Desc)
					WHEN @sortColumn = 'EdgarID' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.EdgarID Asc) 									  
					WHEN @sortColumn = 'EdgarID' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.EdgarID Desc)
					WHEN @sortColumn = 'PageCount' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.[PageCount] Asc) 									  
					WHEN @sortColumn = 'PageCount' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.[PageCount] Desc)
					WHEN @sortColumn = 'PageSizeHeight' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PageSizeHeight Asc) 									  
					WHEN @sortColumn = 'PageSizeHeight' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PageSizeHeight Desc)
					WHEN @sortColumn = 'PageSizeWidth' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PageSizeWidth Asc) 									  
					WHEN @sortColumn = 'PageSizeWidth' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.PageSizeWidth Desc)
					WHEN @sortColumn = 'RPProcessStep' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.RPProcessStep Asc) 									  
					WHEN @sortColumn = 'RPProcessStep' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.RPProcessStep Desc)
					WHEN @sortColumn = 'CreatedDate' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.CreatedDate Asc) 									  
					WHEN @sortColumn = 'CreatedDate' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY BCSAllianceBernsteinDailyIPDetails.CreatedDate Desc)
			END

		 FROM BCSAllianceBernsteinDailyIPDetails
		WHERE CONVERT(VARCHAR, @CreatedDate, 101) = CONVERT(VARCHAR, CreatedDate, 101)
	
	END 
END
