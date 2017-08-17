



ALTER VIEW [dbo].[vwSymetra]
AS
SELECT     dbo.IngestorFlags(E.isSecurityBenefit, E.isSymetra, E.isGenworth, E.isTRowePrice, E.isAXA, E.isForethought, E.isHostedFund,E.isNewYorkLife,E.isOhioNational,E.isMutualOfAmerica,E.isDelawareLife,E.IsAllianz,E.isTransamerica,E.isAllianceBernstein) AS IngestorFlag,
				      E.IsXBRL, E.IsXBRLParsed, E.IsXBRLPushedToProd, E.IsOriginalFound, E.IsManual, E.IsXBRLReadyForPush, E.IsEditing, E.EdgarID, E.FileID, 
                      E.AccNum, E.CIKNum, E.FileName, E.FormType, E.DateFiled, E.DocumentType, E.EffectiveDate, E.Notes, E.DateUpdated, E.Company, 
                      E.IFCDocumentFullPath, E.URL, E.Completed, E.DateCompleted, E.SeriesData, E.WSDate, E.ReceivedDate, E.AcceptedDate, CAST(CONVERT(char(8), 
                      E.ReceivedDate, 112) AS datetime) AS ReceivedDateOnly, E.ReceivedDate - CAST(CONVERT(char(8), E.ReceivedDate, 112) AS datetime) 
                      AS ReceivedTimeOnly, CASE WHEN TV.ErrorCount IS NULL THEN 0 ELSE TV.ErrorCount END AS ErrorCount, COALESCE (CP.Priority, 999999) 
                      AS Priority
FROM         dbo.tblEdgar AS E LEFT OUTER JOIN
                          (SELECT     EdgarID, COUNT(*) AS ErrorCount
                            FROM          dbo.tblEdgarError
                            GROUP BY EdgarID) AS TV ON TV.EdgarID = E.EdgarID LEFT OUTER JOIN
                      dbo.tblCS_PC_CikPriority AS CP ON CP.CIK = E.CIKNum
WHERE		E.isSymetra = 1                      









GO


