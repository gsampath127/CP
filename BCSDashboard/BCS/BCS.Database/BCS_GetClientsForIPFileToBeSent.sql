ALTER Procedure [dbo].[BCS_GetClientsForIPFileToBeSent]  
AS  
BEGIN  
  
 SET NOCOUNT ON  
  
 SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
   
 Declare @ProcessedDate DATETIME = GETDATE()   
  
 Declare @ArchiveBatch Table  
 (  
  BCSClientIPFileConfigID int,
  ClientPrefix NVARCHAR(100),  
  NeedIPConfirmationFile BIT,  
  IPConfirmationFileDropFTPPath nvarchar(500),  
  IPConfirmationFileDropFTPUserName nvarchar(50),  
  IPConfirmationFileDropFTPPassword nvarchar (50),
  LastFileSentDate DATETIME  
 )  
 
 --- DAILY Schedule Checks   
	 INSERT INTO @ArchiveBatch(BCSClientIPFileConfigID,ClientPrefix,NeedIPConfirmationFile,
							IPConfirmationFileDropFTPPath,IPConfirmationFileDropFTPUserName,
							IPConfirmationFileDropFTPPassword,LastFileSentDate)
	 SELECT    BCSClientIPFileConfigID,BCSClientConfig.ClientPrefix,NeedIPConfirmationFile,
							IPConfirmationFileDropFTPPath,IPConfirmationFileDropFTPUserName,
							IPConfirmationFileDropFTPPassword,LastFileSentDate
	  FROM BCSClientIPFileConfig
	  INNER JOIN BCSClientConfig ON BCSClientConfig.ClientPrefix = BCSClientIPFileConfig.ClientPrefix
	  INNER JOIN BCSScheduleFrequency ON BCSClientIPFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
	  WHERE BCSClientConfig.SendIPDocUpdate = 1  AND BCSScheduleFrequency.FrequencyType = 'Daily'
	  AND DATEPART(weekday,  @ProcessedDate) BETWEEN BCSClientIPFileConfig.DailyDayOfWeekFrom AND BCSClientIPFileConfig.DailyDayOfWeekTo
	  AND  @ProcessedDate BETWEEN DATEADD(day, DATEDIFF(day, 0, @ProcessedDate), SendTimeFrom) 
		AND DATEADD(day, DATEDIFF(day, 0, @ProcessedDate), SendTimeTo)  
	  AND (LastFileSentDate IS NULL OR Convert(Date, LastFileSentDate) < Convert(Date, @ProcessedDate))  
  
  --LastBusinessDayOfMonthToSendFrom Checks--
  
    INSERT INTO @ArchiveBatch(BCSClientIPFileConfigID,ClientPrefix,NeedIPConfirmationFile,
						IPConfirmationFileDropFTPPath,IPConfirmationFileDropFTPUserName,
						IPConfirmationFileDropFTPPassword,LastFileSentDate)
	 SELECT    BCSClientIPFileConfigID,BCSClientConfig.ClientPrefix,NeedIPConfirmationFile,
							IPConfirmationFileDropFTPPath,IPConfirmationFileDropFTPUserName,
							IPConfirmationFileDropFTPPassword,LastFileSentDate
	  FROM BCSClientIPFileConfig
  	  INNER JOIN BCSClientConfig ON BCSClientConfig.ClientPrefix = BCSClientIPFileConfig.ClientPrefix
	  INNER JOIN BCSScheduleFrequency ON BCSClientIPFileConfig.BCSScheduleFrequencyID = BCSScheduleFrequency.BCSScheduleFrequencyID
	  WHERE BCSClientConfig.SendIPDocUpdate = 1  AND BCSScheduleFrequency.FrequencyType = 'LastBusinessDayOfMonthToSendFrom'
	  AND  dbo.FN_GetBusinessDayofMonthByDaysToCountFrom(BCSClientIPFileConfig.LastBusinessDayToCountFrom, BCSClientConfig.HolidayTypeId) 
			BETWEEN DATEADD(day, DATEDIFF(day, 0, @ProcessedDate), SendTimeFrom) 
			AND DATEADD(day, DATEDIFF(day, 0, @ProcessedDate), SendTimeTo)  
	 AND (LastFileSentDate IS NULL OR Convert(Date, LastFileSentDate) < Convert(Date, @ProcessedDate))
  
    
 UPDATE BCSClientIPFileConfig 
   SET LastFileSentDate = @ProcessedDate
   FROM  BCSClientIPFileConfig
   INNER JOIN @ArchiveBatch ArchiveBatch ON BCSClientIPFileConfig.BCSClientIPFileConfigID  = ArchiveBatch.BCSClientIPFileConfigID  

    
 SELECT *,@ProcessedDate as ProcessedDate FROM @ArchiveBatch  
    
End  
Go	 