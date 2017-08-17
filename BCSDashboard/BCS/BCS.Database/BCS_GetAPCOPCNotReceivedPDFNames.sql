
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetAPCOPCNotReceivedPDFNames]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetAPCOPCNotReceivedPDFNames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetAPCOPCNotReceivedPDFNames]
GO

CREATE Procedure [dbo].[BCS_GetAPCOPCNotReceivedPDFNames]
AS  
BEGIN  

   SELECT DISTINCT EdgarID, PDFName, DocumentType, IsAPF, 1 As 'FullyLoaded', 'BCSDocUpdate' As 'TableName'  
   FROM BCSDocUpdate
   INNER JOIN BCSDocUpdateGIMSlink ON BCSDocUpdateGIMSlink.DocUpdateID  = BCSDocUpdate.BCSDocUpdateId  
   WHERE (BCSDocUpdateGIMSlink.IsAPF = 1 OR BCSDocUpdateGIMSlink.IsOPF = 1)  
   AND (BCSDocUpdateGIMSlink.IsAPC = 0 AND BCSDocUpdateGIMSlink.IsOPC = 0)  
   AND BCSDocUpdate.IsRemoved = 0

   UNION
  
   SELECT DISTINCT EdgarID, PDFName, DocumentType, IsAPF, 0 As 'FullyLoaded', 'BCSDocUpdateSupplements' As 'TableName'   
   FROM BCSDocUpdateSupplements  
   INNER JOIN BCSDocUpdateSupplementsSlink ON BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID  
   WHERE (BCSDocUpdateSupplementsSlink.IsAPF = 1 OR BCSDocUpdateSupplementsSlink.IsOPF = 1)  
   AND (BCSDocUpdateSupplementsSlink.IsAPC = 0 AND BCSDocUpdateSupplementsSlink.IsOPC = 0)  
   AND BCSDocUpdateSupplements.IsRemoved = 0  
     
   UNION  
     
   SELECT DISTINCT EdgarID, PDFName, DocumentType, IsAPF, 0 As 'FullyLoaded', 'BCSDocUpdateARSAR' As 'TableName'
   FROM BCSDocUpdateARSAR  
   INNER JOIN BCSDocUpdateARSARSlink ON BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID  
   WHERE (BCSDocUpdateARSARSlink.IsAPF = 1 OR BCSDocUpdateARSARSlink.IsOPF = 1)  
   AND (BCSDocUpdateARSARSlink.IsAPC = 0 AND BCSDocUpdateARSARSlink.IsOPC = 0) 
	
END
GO