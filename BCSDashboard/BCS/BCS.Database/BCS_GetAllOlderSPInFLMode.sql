USE db1029
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetAllOlderSPInFLMode]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetAllOlderSPInFLMode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetAllOlderSPInFLMode]
GO

CREATE PROCEDURE [dbo].[BCS_GetAllOlderSPInFLMode]
AS
BEGIN

	
   DECLARE @ProsIDsToExclude TABLE
   (
     Prosid int
   )
   
   
   INSERT INTO @ProsIDsToExclude(Prosid)
   		SELECT  DISTINCT FundID FROM Edgar 
	    INNER JOIN EdgarFunds ON Edgar.EdgarID = EdgarFunds.EdgarID
		WHERE EdgarFunds.Processed = '0' AND Edgar.DateUpdated IS NULL
		AND Edgar.DocumentType LIKE 'Prospectus%'


	INSERT INTO @ProsIDsToExclude(Prosid)
   	SELECT  Prosid FROM ProsDocs
	where ProsDocs.ProsDocTypeId IN ('PS')

   DECLARE @CUSIPSToExclude TABLE
   (
     CUSIP  varchar(10)
   )
   
	INSERT INTO @CUSIPSToExclude(CUSIP) -- In CASE Operations added the original SP and skipped a FL or haven't processed the filing yet for SP.
		SELECT BCSDocUpdate.CUSIP 
		FROM BCSDocUpdate
		INNER JOIN Prospectus ON BCSDocUpdate.ProsID = Prospectus.ProsID
		INNER JOIN Company on Company.CompanyID = Prospectus.CompanyID
		INNER JOIN ProsTicker on Prospectus.ProsID = ProsTicker.ProspectusID
		INNER JOIN ProsDocs ON Prospectus.ProsID = ProsDocs.ProsId AND ProsDocs.ProsDocTypeId IN ('SP') 
							   AND (Case When ProsDocs.ProsDocUseAltURL = 1 THEN  ProsDocs.ProsDocAltURL ELSE ProsDocs.ProsDocURL End) 
									NOT LIKE 'http://www.rightprospectus.com/documents/SECPDFs%'
							   AND ClientID IS NULL

	SELECT DISTINCT Company.CompanyName + '-' + Prospectus.ProsName as FundName, Prospectus.ProsID, ProsDocs.ProsDocId, ProsTicker.CUSIP, ProsTicker.TickerID,
	CASE ProsDocUseAltURL
          WHEN 1 then ProsDocAltURL
          ELSE ProsDocURL 
    END AS URL,
    Case When ISNULL(Prospectus.RevisedProsDate,'01/01/1980') > Prospectus.ProsDate
								 Then Prospectus.RevisedProsDate
							  Else 
							     Prospectus.ProsDate
						 End as DocDate, ProsDocs.ProsDocTypeId
	FROM BCSDocUpdate
	INNER JOIN Prospectus ON BCSDocUpdate.ProsID = Prospectus.ProsID	
	INNER JOIN Company on Company.CompanyID = Prospectus.CompanyID
	INNER JOIN ProsTicker on Prospectus.ProsID = ProsTicker.ProspectusID AND ISNULL(Prosticker.CUSIP,'') != ''
	INNER JOIN ProsDocs ON Prospectus.ProsID = ProsDocs.ProsId AND ProsDocs.ProsDocTypeId IN ('P')	
							AND (Case When ProsDocs.ProsDocUseAltURL = 1 THEN  ProsDocs.ProsDocAltURL ELSE ProsDocs.ProsDocURL End) not like 'http://www.rightprospectus.com/documents/SECPDFs%'	
							AND ISNULL(Case When ProsDocs.ProsDocUseAltURL = 1 THEN  ProsDocs.ProsDocAltURL ELSE ProsDocs.ProsDocURL End,'') != '' 
							AND (Case When ISNULL(Prospectus.RevisedProsDate,'01/01/1980') > Prospectus.ProsDate
														 Then Prospectus.RevisedProsDate
													  Else 
														 Prospectus.ProsDate
												 End) IS NOT NULL
							AND ClientID IS NULL
	LEFT OUTER JOIN @ProsIDsToExclude ProsIDsToExclude ON Prospectus.ProsID = ProsIDsToExclude.Prosid							
	LEFT OUTER JOIN @CUSIPSToExclude CUSIPSToExclude ON ProsTicker.CUSIP = CUSIPSToExclude.CUSIP							
	WHERE IsFiled = 1 AND IsProcessed = 0 AND IsRemoved = 0 AND DocumentType IN ('SP', 'SPS', 'RSP')
	AND DocumentDate <= DATEADD(day,-1,GETDATE())	
	AND ProsIDsToExclude.Prosid is null 
	AND CUSIPSToExclude.CUSIP is null 
	ORDER BY Prospectus.ProsID


End
GO