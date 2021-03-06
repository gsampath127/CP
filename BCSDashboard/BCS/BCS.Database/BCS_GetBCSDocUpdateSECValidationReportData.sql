USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSDocUpdateSECValidationReportData]    Script Date: 08/18/2015 09:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[BCS_GetBCSDocUpdateSECValidationReportData]
AS
BEGIN


 --SELECT DISTINCT BCSDocUpdateId,CUSIP,BCSDocUpdate.ProsID,BCSDocUpdate.FundName,BCSDocUpdate.EdgarID 
 --FROM BCSDocUpdate
 --INNER JOIN BCSDocUpdateSECDetails on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateSECDetails.DocUpdateID
 --INNER JOIN
 --  (select prosid,ISNULL(RevisedSPDate,SPDate) as Docdate from Prospectus
 --  ) as RP on BCSDocUpdate.ProsID = RP.ProsID
 --Where BCSDocUpdate.IsRemoved =0 and BCSDocUpdateSECDetails.DocumentDate < RP.Docdate
 
 SELECT DISTINCT BCSDocUpdateId,CUSIP,BCSDocUpdate.ProsID,BCSDocUpdate.FundName,BCSDocUpdate.EdgarID 
 FROM BCSDocUpdate
 INNER JOIN BCSDocUpdateSECDetails on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateSECDetails.DocUpdateID
 INNER JOIN
   (
	select prosid,Edgar.edgarid,ISNULL(RevisedSPDate,SPDate) as Docdate from Prospectus	
	inner join EdgarFunds on Prospectus.prosid = EdgarFunds.FundID 
	inner join Edgar on EdgarFunds.EdgarID = Edgar.EdgarID 
			and Edgar.DocumentType in ('Summary Prospectus - Revised','Summary Prospectus - New','Summary Prospectus - Supplement')
	and Edgar.DocumentDate = ISNULL(RevisedSPDate,SPDate) 
	and EdgarFunds.Processed in ('1','Y')	
   ) as RP on BCSDocUpdate.ProsID = RP.ProsID
 Where BCSDocUpdate.IsRemoved =0  
 and BCSDocUpdateSECDetails.DocumentDate < RP.Docdate
 and BCSDocUpdate.EdgarID not in (247224,241189)
 UNION
  SELECT DISTINCT BCSDocUpdateId,CUSIP,BCSDocUpdate.ProsID,BCSDocUpdate.FundName,BCSDocUpdate.EdgarID 
 FROM BCSDocUpdate
 INNER JOIN BCSDocUpdateSECDetails on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateSECDetails.DocUpdateID
 INNER JOIN
   (
	select prosid,Edgar.edgarid,ISNULL(RevisedSPDate,SPDate) as Docdate from Prospectus	
	inner join EdgarFunds on Prospectus.prosid = EdgarFunds.FundID 
	inner join Edgar on EdgarFunds.EdgarID = Edgar.EdgarID 
			and Edgar.DocumentType in ('Prospectus - Revised','Prospectus - New','Prospectus - Supplement')
	and Edgar.DocumentDate = ISNULL(RevisedProsDate,ProsDate) 
	and EdgarFunds.Processed in ('1','Y')	
   ) as RP on BCSDocUpdate.ProsID = RP.ProsID
 Where BCSDocUpdate.IsRemoved =0  
 and BCSDocUpdateSECDetails.DocumentDate < RP.Docdate
--AND BCSDocUpdate.ProsID not in
-- (SELECT PROSID FROM BCSURLDownloadQueue where IsDequeued=0)
End
