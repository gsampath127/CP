CREATE FUNCTION [dbo].[fnDataEndDateUtc]
(
    @UtcFirstScheduledRunDate DATETIME,
	@UtcLastScheduledRunDate DATETIME,
	@UtcDataEndDate DATETIME,
	@UtcLastDataEndDate DATETIME,
	@FrequencyType INT,
	@FrequencyInterval INT,
	@WeekDays VARCHAR(20)	
)
RETURNS DATETIME
WITH SCHEMABINDING
AS
BEGIN
DECLARE @UtcNextDataEndDate DATETIME = NULL;
IF @UtcDataEndDate IS NULL --For records with DataEndDate not passed by User
BEGIN
	SET @UtcNextDataEndDate = [dbo].[fnNextReportDateUtc](@UtcFirstScheduledRunDate,@UtcLastScheduledRunDate,@FrequencyType,@FrequencyInterval,@WeekDays);
END

ELSE IF @UtcLastDataEndDate IS NULL --Come's across this scenario when entry created in ReportSchedule Table
BEGIN
	SET @UtcNextDataEndDate =@UtcDataEndDate;
END

ELSE
 BEGIN
 IF @FrequencyType = 1 --Run Once
	BEGIN
		SET @UtcNextDataEndDate =  @UtcDataEndDate;
	END

  IF @FrequencyType = 2 --Daily
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @UtcNextDataEndDate =   DATEADD(DAY, @FrequencyInterval, @UtcLastDataEndDate);		
			
	END
	ELSE IF @FrequencyType = 3 --Weekly
	BEGIN
	    DECLARE @DateConf Table(DayID INT, SendReport BIT);
		INSERT INTO @DateConf(DayID, SendReport)
		SELECT ID, Element FROM [dbo].Split(@WeekDays,',')

 		--Can't use DATEPART with WEEKDAY or DW as this is non deterministic. So we pick a sunday date way in the past and do a DATEDIFF.
		DECLARE @SundayDate DATETIME = CONVERT(DATETIME, '18991231', 112);
		DECLARE @dw INT = DATEDIFF(DAY, @SundayDate, @UtcLastDataEndDate) % 7 + 1;
		DECLARE @AddCount INT = 0;
 
		WHILE(@AddCount < 8)
		BEGIN
			SET @dw = @dw + 1;
			IF @dw > 7
				SET @dw = 1;
 
			SET @AddCount = @AddCount + 1;
			IF EXISTS(SELECT TOP 1 DayID FROM @DateConf WHERE DayID = @dw AND SendReport = 1)
			BEGIN        
				BREAK;
			END
		END; 
		IF(@AddCount < 8)
			SET @UtcNextDataEndDate = DateAdd(Day, @AddCount, @UtcLastDataEndDate);
		ELSE
			SET @UtcNextDataEndDate = @UtcLastDataEndDate;
	END
	ELSE IF @FrequencyType = 4 --Monthly
	BEGIN
		SET @UtcNextDataEndDate = DATEADD(DAY, 1, @UtcLastDataEndDate);
		DECLARE @TimePart VARCHAR(12) = CONVERT(VARCHAR(12), @UtcNextDataEndDate, 114)
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END;
		DECLARE @DM INT = DAY(@UtcNextDataEndDate);
		DECLARE @FixedFrequency INT = @FrequencyInterval;
		DECLARE @MaxDay INT = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@UtcNextDataEndDate), MONTH(@UtcNextDataEndDate), 1, NULL))));
		IF @FixedFrequency > @MaxDay
		BEGIN
			SET	@FixedFrequency = @MaxDay;
		END;
		
		IF @DM < @FixedFrequency
		BEGIN
			SET @UtcNextDataEndDate = dbo.fnGetDateFromParts(YEAR(@UtcNextDataEndDate), MONTH(@UtcNextDataEndDate), @FixedFrequency, @TimePart);
		END
		ELSE IF @DM > @FixedFrequency
		BEGIN
			SET @UtcNextDataEndDate = DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@UtcNextDataEndDate), MONTH(@UtcNextDataEndDate), 1, @TimePart));
			SET @MaxDay = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, @UtcNextDataEndDate)));
			SET @FixedFrequency =
				CASE
					WHEN @FrequencyInterval > @MaxDay
						THEN @MaxDay
						ELSE @FrequencyInterval
				END;
			SET @UtcNextDataEndDate = dbo.fnGetDateFromParts(YEAR(@UtcNextDataEndDate), MONTH(@UtcNextDataEndDate), @FixedFrequency, @TimePart);
		END;
	END
	ELSE IF @FrequencyType = 5 --Quarterly
	BEGIN
		SET @UtcNextDataEndDate = DATEADD(QUARTER, 1, @UtcLastDataEndDate);
	END
	ELSE IF @FrequencyType = 6 --Bi-Annually
	BEGIN
		SET @UtcNextDataEndDate = DATEADD(MONTH, 6, @UtcLastDataEndDate);
	END
	ELSE IF @FrequencyType = 7 --Annually
	BEGIN
		SET @UtcNextDataEndDate = DATEADD(YEAR, 1, @UtcLastDataEndDate);
	END
	ELSE IF @FrequencyType = 8 --Hourly
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @UtcNextDataEndDate =DATEADD(HOUR, @FrequencyInterval, @UtcLastDataEndDate);
			
	END

 END

 
	
	RETURN @UtcNextDataEndDate;
END;

GO