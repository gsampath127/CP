ALTER PROCEDURE [dbo].[BCS_CheckBCSDocUpdateValidation]
AS
BEGIN


--1. Check for any cusip that is not in the Prosticker table and update the IsRemoved=1

	--Update BCSDocUpdate 
	--SET IsRemoved = 1
	--FROM BCSDocUpdate
	--LEFT JOIN ProsTicker On ProsTicker.CUSIP = BCSDocUpdate.CUSIP	
	--WHERE ProsTicker.CUSIP IS NULL AND BCSDocUpdate.IsRemoved = 0
	
	--Update BCSDocupdate with cusips that have changed  
	
    Update BCSDocUpdate 
	SET BCSDocUpdate.CUSIP = ProsTicker.CUSIP
	FROM BCSDocUpdate
	INNER JOIN ProsTicker On ProsTicker.TickerID = BCSDocUpdate.TickerID and ProsTicker.ProspectusID = BCSDocUpdate.ProsID
	WHERE BCSDocUpdate.IsRemoved = 0 
    AND  BCSDocUpdate.CUSIP != ProsTicker.CUSIP
    
    Update BCSDocUpdate -- This will make sure that a CUSIP which is in prosticker under another tickerID is not deleted from bcsdocupdate.The newly added cusip will handle that..
	SET IsRemoved=1
	FROM BCSDocUpdate
	LEFT Outer JOIN ProsTicker On ProsTicker.TickerID = BCSDocUpdate.TickerID 
	LEFT Outer JOIN ProsTicker pcusip On pcusip.CUSIP = BCSDocUpdate.CUSIP 
	WHERE BCSDocUpdate.IsRemoved = 0 AND ProsTicker.TickerID is null AND pcusip.CUSIP is null
    


End