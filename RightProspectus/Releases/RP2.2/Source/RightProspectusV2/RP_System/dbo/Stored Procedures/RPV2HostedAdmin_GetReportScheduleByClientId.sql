CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetReportScheduleByClientId]	
	@ReportName NVARCHAR(100) = NULL,
	@FrequencyType INT = NULL,
	@FrequencyInterval INT = NULL,
	@UtcFirstScheduledRunDate DATETIME = NULL,
	@UtcLastScheduledRunDate DATETIME = NULL,
	@UtcNextScheduledRunDate DATETIME = NULL,
	@UtcScheduledEndDate DATETIME = NULL,
	@IsEnabled BIT = NULL,
	@PageSize INT,
	@PageIndex INT,
    @SortDirection NVARCHAR(10),
	@SortColumn NVARCHAR(100),
	@ClientID int	
AS
BEGIN
	SELECT DISTINCT		
				RowRank,		
				ReportScheduleId,
				ReportId,
				ReportName,
				FrequencyDescription,
				ClientId,
				IsEnabled,
				FrequencyType,
				FrequencyInterval,
				UtcFirstScheduledRunDate,
				UtcNextScheduledRunDate,
				UtcLastScheduledRunDate,
				UtcScheduledEndDate,
				UtcLastActualRunDate,
				ModifiedBy,
				UtcLastModified
			FROM 
			(
			SELECT
       			ReportSchedule.ReportScheduleId,
				ReportSchedule.ReportId,
				Reports.ReportName,
				ReportSchedule.ClientId,
				ReportSchedule.IsEnabled,
				ReportSchedule.FrequencyType,
				ReportSchedule.FrequencyDescription,
				ReportSchedule.FrequencyInterval,
				ReportSchedule.UtcFirstScheduledRunDate,
				ReportSchedule.UtcScheduledEndDate,
				ReportSchedule.UtcNextScheduledRunDate,
				ReportSchedule.UtcLastScheduledRunDate,
				ReportSchedule.UtcLastActualRunDate,
				ReportSchedule.ModifiedBy,  
				ReportSchedule.UtcModifiedDate AS UtcLastModified,
				CASE	WHEN @SortColumn = 'ReportName' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY Reports.ReportName Asc)
						WHEN @SortColumn = 'ReportName' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY Reports.ReportName Desc)
						WHEN @SortColumn = 'FrequencyType' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  FrequencyType.EnumText Asc)
						WHEN @SortColumn = 'FrequencyType' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  FrequencyType.EnumText Desc)
						WHEN @SortColumn = 'FrequencyInterval' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyInterval Asc)
						WHEN @SortColumn = 'FrequencyInterval' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyInterval Desc)
                        WHEN @SortColumn = 'FrequencyDescription' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyDescription Asc)
						WHEN @SortColumn = 'FrequencyDescription' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyDescription Desc)
						WHEN @SortColumn = 'UtcFirstScheduledRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcFirstScheduledRunDate Asc)
						WHEN @SortColumn = 'UtcFirstScheduledRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcFirstScheduledRunDate Desc)
						WHEN @SortColumn = 'UtcLastActualRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcLastActualRunDate Asc)
						WHEN @SortColumn = 'UtcLastActualRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcLastActualRunDate Desc)
						WHEN @SortColumn = 'UtcNextScheduledRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcNextScheduledRunDate Asc)
						WHEN @SortColumn = 'UtcNextScheduledRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcNextScheduledRunDate Desc)
					    WHEN @SortColumn = 'UtcScheduledEndDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcScheduledEndDate Asc)
						WHEN @SortColumn = 'UtcScheduledEndDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcScheduledEndDate Desc)
						WHEN @SortColumn = 'IsEnabled' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.IsEnabled Asc)
						WHEN @SortColumn = 'IsEnabled' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.IsEnabled Desc)
 				End AS RowRank
				FROM ReportSchedule
				INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
				INNER JOIN FrequencyType ON ReportSchedule.FrequencyType = FrequencyType.EnumKey
				INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
				WHERE	ReportSchedule.ClientId = @ClientID AND
							(Reports.ReportName = @ReportName OR @ReportName IS NULL)
							AND(ReportSchedule.FrequencyType = @FrequencyType OR @FrequencyType IS NULL)
    						AND(ReportSchedule.FrequencyInterval = @FrequencyInterval OR @FrequencyInterval IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcFirstScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcFirstScheduledRunDate,102) OR @UtcFirstScheduledRunDate IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcLastScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcLastScheduledRunDate,102) OR @UtcLastScheduledRunDate IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcNextScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcNextScheduledRunDate,102) OR @UtcNextScheduledRunDate IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcScheduledEndDate,102) =  CONVERT(VARCHAR(50), @UtcScheduledEndDate,102) OR @UtcScheduledEndDate IS NULL)
							AND(ReportSchedule.IsEnabled = @IsEnabled OR @IsEnabled IS NULL)
						)AS ReportScheduleDetails 
			WHERE RowRank BETWEEN @PageIndex*@PageSize-@PageSize + 1  AND @PageSize*@PageIndex
			ORDER BY RowRank
			SELECT count(*) 
			FROM ReportSchedule
			INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
			INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
			WHERE	ReportSchedule.ClientId = @ClientID AND
					(Reports.ReportName = @ReportName OR @ReportName IS NULL)
					AND (ReportSchedule.FrequencyType = @FrequencyType OR @FrequencyType IS NULL)
					AND (ReportSchedule.FrequencyInterval = @FrequencyInterval OR @FrequencyInterval IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcFirstScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcFirstScheduledRunDate,102) OR @UtcFirstScheduledRunDate IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcLastScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcLastScheduledRunDate,102) OR @UtcLastScheduledRunDate IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcNextScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcNextScheduledRunDate,102) OR @UtcNextScheduledRunDate IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcScheduledEndDate,102) =  CONVERT(VARCHAR(50), @UtcScheduledEndDate,102) OR @UtcScheduledEndDate IS NULL)
					AND (ReportSchedule.IsEnabled = @IsEnabled OR @IsEnabled IS NULL)
END

