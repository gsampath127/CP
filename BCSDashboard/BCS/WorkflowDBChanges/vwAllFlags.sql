

ALTER VIEW [dbo].[vwAllFlags]
AS
SELECT     dbo.IngestorFlags(E.isSecurityBenefit, E.isSymetra, E.isGenworth, E.isTRowePrice, E.isAXA,E.isForethought,E.isHostedFund,E.isNewYorkLife,E.isOhioNational,E.isMutualOfAmerica,E.isDelawareLife,E.isAllianz,E.isTransAmerica,E.isAllianceBernstein) AS IngestorFlag,  
			E.IsXBRL, E.IsXBRLParsed, E.IsXBRLPushedToProd, 
                      E.IsOriginalFound, E.IsManual, E.IsXBRLReadyForPush, E.IsEditing, E.EdgarID, E.FileID, E.AccNum, E.CIKNum, E.FileName, E.FormType, E.DateFiled, 
                      E.DocumentType, E.EffectiveDate, E.Notes, E.DateUpdated, E.Company,E.CanBeSentToAdmin, E.IFCDocumentFullPath, E.URL, E.Completed, E.DateCompleted, E.SeriesData, E.WSDate, 
                      E.ReceivedDate, E.AcceptedDate, CAST(CONVERT(char(8), E.ReceivedDate, 112) AS datetime) AS ReceivedDateOnly, E.ReceivedDate - CAST(CONVERT(char(8), 
                      E.ReceivedDate, 112) AS datetime) AS ReceivedTimeOnly, CASE WHEN TV.ErrorCount IS NULL THEN 0 ELSE TV.ErrorCount END AS ErrorCount, 
                      COALESCE (CP.Priority, 999999) AS Priority
FROM         dbo.tblEdgar AS E LEFT OUTER JOIN
                          (SELECT     EdgarID, COUNT(*) AS ErrorCount
                            FROM          dbo.tblEdgarError
                            GROUP BY EdgarID) AS TV ON TV.EdgarID = E.EdgarID LEFT OUTER JOIN
                      dbo.tblCS_PC_CikPriority AS CP ON CP.CIK = E.CIKNum
WHERE     (E.isGenworth = 1) OR
                      (E.isAXA = 1) OR
                      (E.isSecurityBenefit = 1) OR
                      (E.isSymetra = 1) OR
                      (E.isForethought = 1) OR
                      (E.isNewYorkLife = 1) OR
                      (E.isOhioNational = 1) OR
                      (E.isMutualOfAmerica = 1) OR
                      (E.isDelawareLife = 1) OR
                      (E.IsAllianz = 1)OR
                      (E.isTransamerica = 1)OR
                      (E.isAllianceBernstein = 1)
                   








GO


