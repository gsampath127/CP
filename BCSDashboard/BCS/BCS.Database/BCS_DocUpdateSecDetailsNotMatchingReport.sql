USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_DocUpdateSecDetailsNotMatchingReport]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_DocUpdateSecDetailsNotMatchingReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_DocUpdateSecDetailsNotMatchingReport]
GO

CREATE Procedure [dbo].[BCS_DocUpdateSecDetailsNotMatchingReport]  
as  
Begin  
select BCSDocUpdate.BCSDocUpdateId, BCSDocUpdate.Acc# as BCSDocUpdateAccNumber,BCSDocUpdateSECDetails.Acc# as BCSDocUpdateSECDetailsAccNumber  
from BCSDocUpdate inner join BCSDocUpdateSECDetails on BCSDocUpdateSECDetails.DocUpdateID = BCSDocUpdate.BCSDocUpdateId  
where DocUpdateID in  
(  
select docupdateid from BCSDocUpdateSECDetails  
group by docupdateid  
having COUNT(*) = 1  
)  
and BCSDocUpdate.IsRemoved = 0  
and BCSDocUpdate.Acc# != BCSDocUpdateSECDetails.Acc#  
union  
select BCSDocUpdate.BCSDocUpdateId,BCSDocUpdate.Acc# as BCSDocUpdateAccNumber,BCSDocUpdateSECDetails.Acc# as BCSDocUpdateSECDetailsAccNumber  
from BCSDocUpdate inner join   
(select acc#,BCSDocUpdateSECDetails.DocUpdateID,BCSDocUpdateSECDetails.DocumentDate from   
BCSDocUpdateSECDetails inner join  
  (  
 select max(BCSDocUpdateSECDetails.BCSDocUpdateSECDetailsID) as detailsid,docupdateid   
  from BCSDocUpdateSECDetails  
  group by docupdateid  
  ) as innertable on BCSDocUpdateSECDetails.BCSDocUpdateSECDetailsID = innertable.detailsid   
 and BCSDocUpdateSECDetails.DocUpdateID = innertable.DocUpdateID  
 ) as BCSDocUpdateSECDetails on BCSDocUpdate.BCSDocUpdateId = BCSDocUpdateSECDetails.DocUpdateID  
where BCSDocUpdate.IsRemoved = 0  
and BCSDocUpdate.Acc# != BCSDocUpdateSECDetails.Acc# and BCSDocUpdate.DocumentDate != BCSDocUpdateSECDetails.DocumentDate  
  
End  
GO