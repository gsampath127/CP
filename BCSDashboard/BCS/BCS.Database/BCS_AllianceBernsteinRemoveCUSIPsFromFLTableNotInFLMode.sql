CREATE PROCEDURE BCS_AllianceBernsteinRemoveCUSIPsFromFLTableNotInFLMode
AS
BEGIN
 DELETE BCSAllianceBernsteinWatchListCUSIPsForFLPurpose
  FROM BCSAllianceBernsteinWatchListCUSIPsForFLPurpose 
	LEFT OUTER JOIN 
	  (SELECT CUSIP FROM BCSDocUpdateSupplements
	   WHERE IsProcessed=0
	   UNION
	   SELECT CUSIP FROM BCSDocUpdateARSAR
	   WHERE IsProcessed=0
	   ) AS BCSDocUpdateCUSIPs ON BCSAllianceBernsteinWatchListCUSIPsForFLPurpose.CUSIP = BCSDocUpdateCUSIPs.CUSIP
   WHERE BCSDocUpdateCUSIPs.CUSIP IS NULL
END