USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetNewlyAddedorModifiedCUSIPDetails]    Script Date: 08/13/2015 17:09:26 ******/


ALTER PROCEDURE [dbo].[BCS_GetNewlyAddedorModifiedCUSIPDetails]  
AS  
BEGIN  

   
SELECT CUSIP, TickerID, ProspectusID, FundName, ProsDocID, URL, DocDate ,ProsDocTypeId 
 FROM (  
 SELECT DISTINCT ProsTicker.CUSIP, ProsTicker.TickerID, ProsTicker.ProspectusID, Company.CompanyName + '-' + Prospectus.ProsName As FundName,  
        ProsDocs.ProsDocID, ProsDocs.Prosdoctypeid, 
     CASE ProsDocUseAltURL  
       WHEN 1 then ProsDocAltURL  
       ELSE ProsDocURL   
     END AS URL,  
     CASE ProsDocs.ProsDocTypeID
      WHEN 'SP' Then 
		 Case When ISNULL(Prospectus.RevisedSPDate,'01/01/1980') > Prospectus.SPDate  
			 Then Prospectus.RevisedSPDate  
			 Else   
				Prospectus.SPDate  
		   End
	  WHEN 'P' Then 
       Case When ISNULL(Prospectus.RevisedProsDate,'01/01/1980') > Prospectus.ProsDate  
         Then Prospectus.RevisedProsDate  
          Else   
            Prospectus.ProsDate  
         End   
     End as DocDate  
 FROM ProsTicker  
 INNER JOIN BCSDocUpdate ON RTRIM(LTRIM(BCSDocUpdate.CUSIP)) = RTRIM(LTRIM(ProsTicker.CUSIP))  
		AND BCSDocUpdate.IsRemoved=0  
		AND BCSDocUpdate.IsProcessed=1
 INNER JOIN ProsDocs ON ProsTicker.ProspectusID = ProsDocs.ProsID 
					AND ProsDocs.ProsDocTypeID in ('P', 'SP' )
					AND ProsDocs.ClientID  IS NULL  
					AND REPLACE(  --  to handle merger scenario and making sure P is not mixed with SP comparison
									REPLACE(
											REPLACE(
													REPLACE(BCSDocUpdate.DocumentType,'SPS','SP')
													,'RSP','SP')
													,'PS','P')
													,'RP','P') = Prosdocs.ProsDocTypeId 
 INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID    
 INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID  
 WHERE ProsTicker.ProspectusID <> BCSDocUpdate.ProsID  and ISNULL(ProsTicker.CUSIP,'') != ''
 )t  
 WHERE URL not like '%SECPDFs%' AND DocDate is not null 
 UNION
 SELECT INNERTABLE.CUSIP, INNERTABLE.TickerID, ProspectusID, innertable.FundName, 
	INNERTABLE.ProsDocID, URL, DocDate,ProsDocTypeId from BCSDocUpdate
  INNER JOIN
  (
	SELECT DISTINCT ProsTicker.CUSIP, ProsTicker.TickerID, ProsTicker.ProspectusID, Company.CompanyName + '-' + Prospectus.ProsName As FundName,  
			ProsDocs.ProsDocID,  ProsDocs.ProsDoctypeid,
		 CASE ProsDocUseAltURL  
		   WHEN 1 then ProsDocAltURL  
		   ELSE ProsDocURL   
		 END AS URL,  
		 CASE ProsDocs.ProsDocTypeID
		  WHEN 'SP' Then 
			 Case When ISNULL(Prospectus.RevisedSPDate,'01/01/1980') > Prospectus.SPDate  
				 Then Prospectus.RevisedSPDate  
				 Else   
					Prospectus.SPDate  
			   End
		  WHEN 'P' Then 
		   Case When ISNULL(Prospectus.RevisedProsDate,'01/01/1980') > Prospectus.ProsDate  
			 Then Prospectus.RevisedProsDate  
			  Else   
				Prospectus.ProsDate  
			 End   
		 End as DocDate  
		   FROM ProsTicker
		   INNER JOIN ProsDocs ON ProsTicker.ProspectusID = ProsDocs.ProsID 
					AND ProsDocs.ProsDocTypeID in ('P', 'SP' )
					AND ProsDocs.ClientID  IS NULL  
	 INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID    
	 INNER JOIN Company ON Prospectus.CompanyID = Company.CompanyID    
	  LEFT OUTER JOIN BCSDocUpdate on ProsTicker.TickerID = BCSDocUpdate.TickerID 
	 WHERE BCSDocUpdate.CUSIP is null AND ISNULL(Prosticker.cusip,'') != ''
 ) as INNERTABLE on BCSDocUpdate.ProsID  = INNERTABLE.ProspectusID 
							AND REPLACE( --  Newly added cusip for same doctype and fund
									REPLACE(
											REPLACE(
													REPLACE(BCSDocUpdate.DocumentType,'SPS','SP')
													,'RSP','SP')
													,'PS','P')
													,'RP','P') = INNERTABLE.ProsDocTypeId
 WHERE URL not like '%SECPDFs%' 
		AND DocDate is not null 
		AND BCSDocUpdate.IsProcessed=1

 Order by ProspectusID, URL   
END
