
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetARSARFilingsPendingToBeProcessed]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetARSARFilingsPendingToBeProcessed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetARSARFilingsPendingToBeProcessed]
GO

CREATE Procedure [dbo].[BCS_GetARSARFilingsPendingToBeProcessed]  
@FromDateTime datetime,  
@ToDateTime datetime  
as  
Begin  
  
	SELECT DISTINCT Edgar.EdgarID,
			Edgar.Acc#,
			Edgar.DocumentType,
			ProsTicker.CUSIP,
			EdgarFunds.TickerID,
			Edgar.EffectiveDate,
			Edgar.DocumentDate,
			Edgar.FormType,
			Prospectus.ProsID,
			COMPANY.CompanyName + '-' + Prospectus.ProsName as FundName,
			Edgar.DateFiled,
			EdgarFunds.DateUpdated		
	  FROM EdgarFunds
		  INNER JOIN Edgar on Edgar.EdgarID = EdgarFunds.EdgarID
		  INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID
		  INNER JOIN Prospectus on EdgarFunds.FundID = Prospectus.ProsID
		  INNER JOIN COMPANY on Prospectus.CompanyID = Company.CompanyID  
		  INNER JOIN BCSWatchListCUSIPView ON BCSWatchListCUSIPView.CUSIP = ProsTicker.CUSIP		  
	  WHERE EdgarFunds.Processed = '0' AND Edgar.DateUpdated is null
		  AND Edgar.DocumentDate  >= GETDATE()-1 and Edgar.DocumentDate <= GETDATE()
		  AND (Edgar.DocumentType like '%Annual%' OR Edgar.DocumentType like '%Quarterly%')		 
		  AND ISNULL(Prosticker.cusip,'') != ''
		  
		  
	  UNION	  
	  
	  SELECT DISTINCT Edgar.EdgarID,
			Edgar.Acc#,
			Edgar.DocumentType,
			ProsTicker.CUSIP,
			EdgarFunds.TickerID,
			Edgar.EffectiveDate,
			Edgar.DocumentDate,
			Edgar.FormType,
			Prospectus.ProsID,
			COMPANY.CompanyName + '-' + Prospectus.ProsName as FundName,
			Edgar.DateFiled,
			EdgarFunds.DateUpdated		
	  FROM EdgarFunds
		  INNER JOIN Edgar on Edgar.EdgarID = EdgarFunds.EdgarID
		  INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID
		  INNER JOIN Prospectus on EdgarFunds.FundID = Prospectus.ProsID
		  INNER JOIN COMPANY on Prospectus.CompanyID = Company.CompanyID  
		  INNER JOIN BCSWatchListCUSIPView ON BCSWatchListCUSIPView.CUSIP = ProsTicker.CUSIP
	   WHERE EdgarFunds.Processed = '0' AND Edgar.DateUpdated is null
			  AND Edgar.DocumentDate <= GETDATE()
			  AND Edgar.DateFiled >= GETDATE()-1 and Edgar.DateFiled <= GETDATE()
			  AND (Edgar.DocumentType like '%Annual%' OR Edgar.DocumentType like '%Quarterly%')
		      AND ISNULL(Prosticker.cusip,'') != ''
End
GO