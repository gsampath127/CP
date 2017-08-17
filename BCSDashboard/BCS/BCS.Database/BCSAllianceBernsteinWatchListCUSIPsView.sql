Create View BCSAllianceBernsteinWatchListCUSIPsView
AS

SELECT DISTINCT CUSIP FROM BCSAllianceBernsteinWatchListCUSIPs
UNION
SELECT DISTINCT CUSIP FROM (
          SELECT CUSIP, DeletionDate, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
          FROM BCSAllianceBernsteinWatchListCUSIPsHistory) t
          WHERE rownum = 1 and DeletionDate IS NOT NULL and DeletionDate > GETDATE()-365
