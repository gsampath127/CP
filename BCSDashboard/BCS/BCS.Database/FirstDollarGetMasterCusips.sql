  
Alter Procedure [dbo].[FirstDollarGetMasterCusips]   
@ClientPrefix varchar(10)  
as   
Begin  
 IF @ClientPrefix = 'AEG'  
    SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
      FROM BCSTransamericaWatchListCUSIPs  
  Else  
 IF @ClientPrefix = 'AB'  
  BEGIN
    SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
      FROM BCSAllianceBernsteinWatchListCUSIPs  
    UNION
	SELECT DISTINCT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol
			 FROM (
			  SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol, DeletionDate, ROW_NUMBER() Over (Partition by CUSIP order by HistoryID desc) As rownum
			  FROM BCSAllianceBernsteinWatchListCUSIPsHistory) t
			  WHERE rownum = 1 and DeletionDate IS NOT NULL and DeletionDate > GETDATE()-365
  END
  Else  
  IF @ClientPrefix = 'AZL'  
    SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
      FROM Allianz  
  Else  
  IF @ClientPrefix = 'DEL'  
    SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
      FROM DelawareLife  
  Else  
    IF @ClientPrefix = 'FFG'  
   SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    FROM Forethought  
   Else      
    IF @ClientPrefix = 'SBR'  
   SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    FROM SecurityBenefits  
   Else      
    IF @ClientPrefix = 'SYA'  
   SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    FROM Symetra  
   Else      
    IF @ClientPrefix = 'OHN'  
   SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    FROM OhioNational      
   Else      
    IF @ClientPrefix = 'GEN'  
   SELECT distinct CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    From ProsTicker WHERE CUSIP in  
    (SELECT distinct CUSIP   
  FROM GenworthSubAccounts      
  )     
 Else  
   IF @ClientPrefix = 'NYL'  
    SELECT distinct CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    From ProsTicker WHERE CUSIP in  
    (SELECT distinct CUSIP   
  FROM NYLSubAccounts      
  )   
  Else  
   SELECT CUSIP,CIK,SeriesID,ClassContractID,TickerSymbol   
    FROM DelawareLife   
    WHERE 1 <> 1  
        
   
End  