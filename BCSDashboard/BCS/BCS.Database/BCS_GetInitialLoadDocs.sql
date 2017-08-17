Alter Procedure BCS_GetInitialLoadDocs
as
Begin

SELECT FundName,ProsID,ProsDocId,CUSIP,TickerID,URL,DocDate
 FROM
(
SELECT distinct Company.CompanyName + '-' + Prospectus.ProsName as FundName,
	 Prospectus.ProsID,Prosdocid,CUSIP,TickerID,
	CASE ProsDocUseAltURL
          WHEN 1 then ProsDocAltURL
          ELSE ProsDocURL 
    END AS URL,
    Case When ISNULL(Prospectus.RevisedSPDate,'01/01/1980') > Prospectus.SPDate
								 Then Prospectus.RevisedSPDate
							  Else 
							     Prospectus.SPDate
						 End as DocDate
     FROM ProsDocs
    INNER JOIN Prospectus on Prospectus.ProsID = ProsDocs.ProsID
    INNER JOIN ProsTicker on Prospectus.ProsID = ProsTicker.ProspectusID
    INNER JOIN Company on Company.CompanyID = Prospectus.CompanyID
  WHERE ProsDocTypeId = 'SP' and ISNULL(CUSIP,'') != '' And ClientID is null
  ) AS SPTable
  WHERE URL not like '%SECPDFs%' and DocDate is not null
  order by ProsID,URL,CUSIP
End  