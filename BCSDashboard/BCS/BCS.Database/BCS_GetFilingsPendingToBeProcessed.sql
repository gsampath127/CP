ALTER Procedure [dbo].[BCS_GetFilingsPendingToBeProcessed]
@FromDateTime datetime,
@ToDateTime datetime
as
Begin

	(	
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
		INNER JOIN BCSDocUpdate on BCSDocUpdate.ProsID = EdgarFunds.FundID  
   WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null  
		AND Edgar.DocumentDate  >= GETDATE()-1 and Edgar.DocumentDate <= GETDATE()  
		AND Edgar.DocumentType like 'Summary%' AND BCSDocUpdate.DocumentType IN ('P', 'PS', 'RP') 
		AND BCSDocUpdate.EdgarID != Edgar.EdgarID  
		AND BCSDocUpdate.IsRemoved=0  
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
		  INNER JOIN BCSDocUpdate on BCSDocUpdate.ProsID = EdgarFunds.FundID
	  WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null
		  AND Edgar.DocumentDate  >= GETDATE()-1 and Edgar.DocumentDate <= GETDATE()
		  AND Edgar.DocumentType like 'Summary%'
		  AND BCSDocUpdate.EdgarID != Edgar.EdgarID
		  AND BCSDocUpdate.DocumentDate <= Edgar.DocumentDate and BCSDocUpdate.IsRemoved=0
		  AND ISNULL(Prosticker.cusip,'') != ''
	UNION	  
		SELECT DISTINCT Edgar.EdgarID,
				Edgar.Acc#,
				Edgar.DocumentType,
				ProsTicker.CUSIP,
				EdgarFunds.TickerID,
				Edgar.EffectiveDate,
				Edgar.DocumentDate edgardocdate,
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
		  WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null
			  AND Edgar.DocumentDate  >= GETDATE()-1 and Edgar.DocumentDate <= GETDATE()
			  AND Edgar.DocumentType like 'Summary%'
			  AND ISNULL(Prosticker.cusip,'') != ''		
			  AND CUSIP NOT IN (SELECT DISTINCT CUSIP FROM BCSDocUpdate WHERE IsRemoved =0)	
	  UNION	  
		SELECT DISTINCT Edgar.EdgarID,
				Edgar.Acc#,
				Edgar.DocumentType,
				ProsTicker.CUSIP,
				EdgarFunds.TickerID,
				Edgar.EffectiveDate,
				Edgar.DocumentDate edgardocdate,
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
			  INNER JOIN BCSDocUpdate on BCSDocUpdate.ProsID = EdgarFunds.FundID and BCSDocUpdate.IsRemoved=0  
		  WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null
			  AND Edgar.DocumentDate <= GETDATE()
			  AND Edgar.DateFiled >= GETDATE()-1 and Edgar.DateFiled <= GETDATE()
			  AND Edgar.DocumentType like 'Summary%'
			  AND BCSDocUpdate.EdgarID != Edgar.EdgarID 
			  AND BCSDocUpdate.IsRemoved=0 AND BCSDocUpdate.DocumentDate < Edgar.DocumentDate			  
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
			  INNER JOIN BCSDocUpdate on BCSDocUpdate.ProsID = EdgarFunds.FundID
		  WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null
			  AND Edgar.DocumentDate  >= GETDATE()-1 and Edgar.DocumentDate <= GETDATE()
			  AND Edgar.DocumentType like 'Prospectus%'
			  AND BCSDocUpdate.EdgarID != Edgar.EdgarID
			  AND BCSDocUpdate.DocumentType in ('P','PS','RP')
			  AND BCSDocUpdate.DocumentDate <= Edgar.DocumentDate and BCSDocUpdate.IsRemoved=0
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
			  INNER JOIN BCSDocUpdate on BCSDocUpdate.ProsID = EdgarFunds.FundID
		  WHERE EdgarFunds.Processed='0' AND Edgar.DateUpdated is null
			  AND Edgar.DocumentDate <= GETDATE()
			  AND Edgar.DateFiled >= GETDATE()-1 and Edgar.DateFiled <= GETDATE()			  
			  AND Edgar.DocumentType like 'Prospectus%'
			  AND BCSDocUpdate.EdgarID != Edgar.EdgarID
			  AND BCSDocUpdate.DocumentType in ('P','PS','RP')
			  AND BCSDocUpdate.DocumentDate <= Edgar.DocumentDate and BCSDocUpdate.IsRemoved=0
			  AND ISNULL(Prosticker.cusip,'') != ''			  	
			  
	  )  
	  ORDER BY ProsID

End
