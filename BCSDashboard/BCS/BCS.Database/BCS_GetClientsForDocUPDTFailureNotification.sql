ALTER Procedure [dbo].[BCS_GetClientsForDocUPDTFailureNotification]  
AS  
BEGIN  
  
 SET NOCOUNT ON  
  
 SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
   
 Declare @ProcessedDate DATETIME = GETDATE()   


 Declare @LatestDocUPDTFileDetails Table  
 (  
	BCSDocUpdateFileHistoryID int,
	BCSClientConfigID int,
	ClientName nvarchar(1000),
	MissedNextFileSLAEmailSent nvarchar(1),  
	DateCreated DateTime  
 )

 INSERT INTO @LatestDocUPDTFileDetails
 SELECT BCSDocUpdateFileHistoryID, BCSClientConfigID, ClientName, MissedNextFileSLAEmailSent, DateCreated FROM (
		SELECT BCSDocUpdateFileHistoryID, BCSClientConfig.BCSClientConfigID, BCSClientConfig.ClientName, MissedNextFileSLAEmailSent, DateCreated,
			   ROW_NUMBER()over (Partition by BCSDocUpdateFileHistory.BCSClientConfigID order by BCSDocUpdateFileHistory.DateCreated desc) 'RowNum'
		from BCSDocUpdateFileHistory
		INNER JOIN BCSClientConfig On BCSDocUpdateFileHistory.BCSClientConfigID = BCSClientConfig.BCSClientConfigID
	)t Where RowNum = 1

  
 Declare @DocUPDTArchiveBatch Table  
 (
  BCSDocUpdateFileHistoryID int,
  BCSClientConfigID int,  
  ClientName NVARCHAR(1000)  
 )


 --Check for GIM and GMS

 INSERT INTO @DocUPDTArchiveBatch(BCSDocUpdateFileHistoryID, BCSClientConfigID, ClientName)
	SELECT BCSDocUpdateFileHistoryID, BCSClientConfig.BCSClientConfigID, BCSClientConfig.ClientName
	FROM @LatestDocUPDTFileDetails latestFileDetails
	INNER JOIN BCSClientConfig ON BCSClientConfig.BCSClientConfigID = latestFileDetails.BCSClientConfigID	  
	WHERE latestFileDetails.ClientName IN ('GIM', 'GMS')
	  AND  (
			DATEPART(weekday,  @ProcessedDate) BETWEEN 3 AND 7
			AND  
			Convert(Date, @ProcessedDate) <> Convert(Date, DateCreated) 
			AND 
			@ProcessedDate > DateAdd(minute, 30, Convert(Date, @ProcessedDate) + DateCreated - Convert(Date, DateCreated)) 
			)
	  AND MissedNextFileSLAEmailSent IS NULL
   
 
 --- DAILY Schedule Checks for IP file (SendIPDocUpdate = 1) 
	 INSERT INTO @DocUPDTArchiveBatch(BCSDocUpdateFileHistoryID, BCSClientConfigID, ClientName)
		  SELECT BCSDocUpdateFileHistoryID, BCSClientConfig.BCSClientConfigID, BCSClientConfig.ClientName
		  FROM @LatestDocUPDTFileDetails latestFileDetails
		  INNER JOIN BCSClientConfig ON BCSClientConfig.BCSClientConfigID = latestFileDetails.BCSClientConfigID
		  INNER JOIN BCSClientIPFileConfig ON BCSClientConfig.ClientPrefix = BCSClientIPFileConfig.ClientPrefix
		  INNER JOIN BCSScheduleFrequency ON BCSClientIPFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
		  WHERE BCSClientConfig.SendIPDocUpdate = 1  AND BCSScheduleFrequency.FrequencyType = 'Daily'
		  AND  (
				DATEPART(weekday,  @ProcessedDate) BETWEEN BCSClientIPFileConfig.DailyDayOfWeekFrom AND BCSClientIPFileConfig.DailyDayOfWeekTo
				AND  
				Convert(Date, @ProcessedDate) <> Convert(Date, DateCreated) 
				AND 
				@ProcessedDate > DateAdd(minute, 30, Convert(Date, @ProcessedDate) + DateCreated - Convert(Date, DateCreated)) 
				)
		  AND MissedNextFileSLAEmailSent IS NULL
  
  --LastBusinessDayOfMonthToSendFrom Checks-- for IP file (SendIPDocUpdate = 1) 
  
    INSERT INTO @DocUPDTArchiveBatch(BCSDocUpdateFileHistoryID, BCSClientConfigID, ClientName)
	  SELECT BCSDocUpdateFileHistoryID, BCSClientConfig.BCSClientConfigID, BCSClientConfig.ClientName
	  FROM @LatestDocUPDTFileDetails latestFileDetails
  	  INNER JOIN BCSClientConfig ON BCSClientConfig.BCSClientConfigID = latestFileDetails.BCSClientConfigID
	  INNER JOIN BCSClientIPFileConfig ON BCSClientConfig.ClientPrefix = BCSClientIPFileConfig.ClientPrefix
	  INNER JOIN BCSScheduleFrequency ON BCSClientIPFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
	  WHERE BCSClientConfig.SendIPDocUpdate = 1  AND BCSScheduleFrequency.FrequencyType = 'LastBusinessDayOfMonthToSendFrom'
	  AND  (
			Convert(Date, @ProcessedDate) = Convert(Date, dbo.FN_GetBusinessDayofMonthByDaysToCountFrom(BCSClientIPFileConfig.LastBusinessDayToCountFrom, BCSClientConfig.HolidayTypeId)) 
			AND
			Convert(Date, @ProcessedDate) <> Convert(Date, DateCreated)
			AND 
			@ProcessedDate > DateAdd(minute, 30, Convert(Date, @ProcessedDate) + DateCreated - Convert(Date, DateCreated)) 
			)
	 AND MissedNextFileSLAEmailSent IS NULL
  
    
   UPDATE BCSDocUpdateFileHistory 
   SET MissedNextFileSLAEmailSent = 'Y'
   FROM  BCSDocUpdateFileHistory
   INNER JOIN @DocUPDTArchiveBatch ArchiveBatch ON BCSDocUpdateFileHistory.BCSDocUpdateFileHistoryID  = ArchiveBatch.BCSDocUpdateFileHistoryID  

    
 SELECT * FROM @DocUPDTArchiveBatch
    
End  
Go	 