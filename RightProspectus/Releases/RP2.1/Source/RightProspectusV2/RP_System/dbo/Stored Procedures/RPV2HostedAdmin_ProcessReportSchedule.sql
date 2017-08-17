CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ProcessReportSchedule]
(
	@ServiceName AS NVARCHAR(100)
)
AS
BEGIN
	SET NOCOUNT ON;	
	DECLARE @ProcExecutionId AS UNIQUEIDENTIFIER, @ReportToProcess AS INT, @ClientToProcess AS INT;
	
	--Populate table variable with all ReportSchedules that need to be processed now
	DECLARE @ReportScheduleVar TABLE(ReportScheduleId INT, ReportId INT, ClientId INT, UtcLastActualRunDate DateTime)
	INSERT INTO @ReportScheduleVar(ReportScheduleId, ReportId, ClientId, UtcLastActualRunDate)
	SELECT ReportScheduleId, ReportId, ClientId, UtcLastActualRunDate
	FROM ReportSchedule
	WHERE IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)
	
	--- START: Get the next entry that should be Processed (by app/client priority) ---

	--- GET NEXT REPORT ---
	SELECT TOP 1 @ReportToProcess = [ReportId]
	FROM @ReportScheduleVar
	WHERE [ReportId] > 
			(SELECT TOP 1 [ReportId]  
				FROM ReportSchedule
				ORDER BY [UtcLastActualRunDate] DESC);	

	--- IF NO REPORT Id IS FOUND GREATER THAN LAST PROCESSED ONE, GO BACK TO THE START OF THE REPORT LIST ---
	IF (@ReportToProcess IS NULL)	
	BEGIN
		SELECT TOP 1 @ReportToProcess = [ReportId]
		FROM [ReportSchedule] 
		WHERE IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)
		ORDER BY [UtcLastActualRunDate] DESC;
	END;

	IF (@ReportToProcess IS NULL)
	BEGIN
		RETURN;
	END;
	
	--- GET NEXT CLIENT FOR THAT REPORT ---
	SELECT TOP 1 @ClientToProcess = [ClientId]
	FROM @ReportScheduleVar 
	WHERE [ReportId] = @ReportToProcess AND [ClientId] > 
			(SELECT TOP 1 [ClientId]  
				FROM ReportSchedule
				ORDER BY [UtcLastActualRunDate] DESC);

	--- IF NO REPORT CLIENT Id IS FOUND GREATER THAN LAST PROCESSED ONE, GO BACK TO THE START OF THE CLIENT LIST ---
	IF (@ClientToProcess IS NULL)	
	BEGIN
		SELECT TOP 1 @ClientToProcess = [ClientId]
		FROM [ReportSchedule] 
		WHERE [ReportId] = @ReportToProcess AND IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)
		ORDER BY [UtcLastActualRunDate] DESC;
	END;

	----Checking just in case we have bad data
	IF (@ClientToProcess IS NULL)
	BEGIN
		RETURN;
	END;
	--- END: Get the next entry that should be Processed (by app/client priority) ---
	
	DECLARE @ReportScheduleId AS INT;

	SELECT TOP 1 @ReportScheduleId = [ReportScheduleId]
	FROM [ReportSchedule]
	WHERE [ReportId] = @ReportToProcess
		AND [ClientId] = @ClientToProcess
		AND [IsEnabled] = 1
		AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 
		AND (Status <> 1 OR Status is NULL)
	ORDER BY [UtcLastActualRunDate];
	
	UPDATE [ReportSchedule]
	SET [Status] = 1, [UtcLastActualRunDate] = GETUTCDATE(), 
	[ExecutionCount] = [ReportSchedule].[ExecutionCount] + 1, [ServiceName] = @ServiceName
	WHERE [ReportSchedule].[ReportScheduleId] = @ReportScheduleId;
		
	--- END: Assign a unique process execution Id to the entry ---

	--- Get the entry being Processed ---
	SELECT 
		[ReportSchedule].[ReportScheduleId], 
		[Reports].[ReportName], 
		[Clients].[ClientId],
		[Clients].[ClientName],
		[Reports].[ReportDescription], 
		[ReportSchedule].[Status], 
		[ReportSchedule].[ServiceName], 
		[ReportSchedule].[FrequencyType],
		[ReportSchedule].[FrequencyInterval],
		[ReportSchedule].[UtcNextScheduledRunDate],
		[ReportSchedule].[ExecutionCount],
		[ReportScheduleRecipients].[Email], 
		[ReportScheduleRecipients].[FTPServerIP],
		[ReportScheduleRecipients].[FTPFilePath],
		[ReportScheduleRecipients].[FTPUsername],
		[ReportScheduleRecipients].[FTPPassword],
		[ReportSchedule].[FrequencyDescription],
		[ReportScheduleRecipients].IsSFTP
	FROM [ReportSchedule]
		INNER JOIN [Reports] ON [ReportSchedule].[ReportId] = [Reports].[ReportId]
		INNER JOIN [Clients] ON [Clients].[ClientId] = [ReportSchedule].[ClientId]
		LEFT OUTER JOIN [ReportScheduleRecipients] ON [ReportScheduleRecipients].[ReportScheduleId] = [ReportSchedule].[ReportScheduleId]
	WHERE [ReportSchedule].[ReportScheduleId] = @ReportScheduleId;
	
END
GO


