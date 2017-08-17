ALTER Procedure [dbo].[BCS_GetClientsForWatchlistFailureNotification]  
AS  
BEGIN  
  
 SET NOCOUNT ON  
  
 SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
   
 Declare @ProcessedDate DATETIME = GETDATE()   
  
 Declare @WatchlistArchiveBatch Table  
 (  
  BCSClientWatchlistFileConfigID int,
  ClientPrefix NVARCHAR(100),  
  ClientName NVARCHAR(1000)  
 )  
 
 --- DAILY Schedule Checks   
	 INSERT INTO @WatchlistArchiveBatch(BCSClientWatchlistFileConfigID,ClientPrefix,ClientName)
	 SELECT    BCSClientWatchlistFileConfigID,BCSClientConfig.ClientPrefix,ClientName
	  FROM BCSClientWatchlistFileConfig
	  INNER JOIN BCSClientConfig ON BCSClientConfig.ClientPrefix = BCSClientWatchlistFileConfig.ClientPrefix
	  INNER JOIN BCSScheduleFrequency ON BCSClientWatchlistFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
	  WHERE BCSClientConfig.IsCUSIPWatchListProvided = 1  AND BCSScheduleFrequency.FrequencyType = 'Daily'
	  AND  (
			DATEPART(weekday,  @ProcessedDate) BETWEEN BCSClientWatchlistFileConfig.DailyDayOfWeekFrom AND BCSClientWatchlistFileConfig.DailyDayOfWeekTo
			AND  
			Convert(Date, @ProcessedDate) <> Convert(Date, BCSClientWatchlistFileConfig.LastFileReceivedDate) 
			AND 
			@ProcessedDate > DateAdd(minute, 30, Convert(Date, @ProcessedDate) + BCSClientWatchlistFileConfig.LastFileReceivedDate - Convert(Date, BCSClientWatchlistFileConfig.LastFileReceivedDate)) 
			)
	  AND MissedFileSLAEmailSent IS NULL
  
  --LastBusinessDayOfMonthToSendFrom Checks--
  
    INSERT INTO @WatchlistArchiveBatch(BCSClientWatchlistFileConfigID,ClientPrefix,ClientName)
	 SELECT    BCSClientWatchlistFileConfigID,BCSClientConfig.ClientPrefix,ClientName
	  FROM BCSClientWatchlistFileConfig
  	  INNER JOIN BCSClientConfig ON BCSClientConfig.ClientPrefix = BCSClientWatchlistFileConfig.ClientPrefix
	  INNER JOIN BCSScheduleFrequency ON BCSClientWatchlistFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
	  WHERE BCSClientConfig.IsCUSIPWatchListProvided = 1  AND BCSScheduleFrequency.FrequencyType = 'LastBusinessDayOfMonthToSendFrom'
	  AND  (
			Convert(Date, @ProcessedDate) = Convert(Date, dbo.FN_GetBusinessDayofMonthByDaysToCountFrom(BCSClientWatchlistFileConfig.LastBusinessDayToCountFrom, BCSClientConfig.HolidayTypeId)) 
			AND
			Convert(Date, @ProcessedDate) <> Convert(Date, BCSClientWatchlistFileConfig.LastFileReceivedDate)
			AND 
			@ProcessedDate > DateAdd(minute, 30, Convert(Date, @ProcessedDate) + BCSClientWatchlistFileConfig.LastFileReceivedDate - Convert(Date, BCSClientWatchlistFileConfig.LastFileReceivedDate)) 
			)
	 AND MissedFileSLAEmailSent IS NULL
  
    
   UPDATE BCSClientWatchlistFileConfig 
   SET MissedFileSLAEmailSent = 'Y'
   FROM  BCSClientWatchlistFileConfig
   INNER JOIN @WatchlistArchiveBatch ArchiveBatch ON BCSClientWatchlistFileConfig.BCSClientWatchlistFileConfigID  = ArchiveBatch.BCSClientWatchlistFileConfigID  

    
 SELECT * FROM @WatchlistArchiveBatch
    
End  
Go	 