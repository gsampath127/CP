CREATE FUNCTION [dbo].[fnNextReportDateUtc]
(
	@UtcFirstRunDate DATETIME,
	@UtcLastRunDate DATETIME,
	@FrequencyType INT,
	@FrequencyInterval INT,
	@WeekDays VARCHAR(20)
)
RETURNS SMALLDATETIME
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @NextDate SMALLDATETIME = NULL;
	IF @FrequencyType = 1 --One time only
	BEGIN
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NOT NULL
					THEN NULL
					ELSE @UtcFirstRunDate
			END;
	END
	ELSE IF @FrequencyType = 2 --Daily
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NULL 
					THEN @UtcFirstRunDate
					ELSE DATEADD(DAY, @FrequencyInterval, @UtcLastRunDate)
			END;
	END
	ELSE IF @FrequencyType = 3 --Weekly
	BEGIN
		SET @NextDate = ISNULL(@UtcLastRunDate, @UtcFirstRunDate);
		IF @UtcLastRunDate IS NOT NULL
		BEGIN
		DECLARE @DateConf Table(DayID INT, SendReport BIT);
		INSERT INTO @DateConf(DayID, SendReport)
		SELECT ID, Element FROM [dbo].Split(@WeekDays,',')
 
		--Can't use DATEPART with WEEKDAY or DW as this is non deterministic. So we pick a sunday date way in the past and do a DATEDIFF.
		DECLARE @SundayDate DATETIME = CONVERT(DATETIME, '18991231', 112);
		DECLARE @dw INT = DATEDIFF(DAY, @SundayDate, @NextDate) % 7 + 1;
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
			SET @NextDate = DateAdd(Day, @AddCount, @NextDate);	 
		END	
	END
	ELSE IF @FrequencyType = 4 --Monthly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		DECLARE @TimePart VARCHAR(12) = CONVERT(VARCHAR(12), @NextDate, 114)
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END;
		DECLARE @DM INT = DAY(@NextDate);
		DECLARE @FixedFrequency INT = @FrequencyInterval;
		DECLARE @MaxDay INT = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), 1, NULL))));
		IF @FixedFrequency > @MaxDay
		BEGIN
			SET	@FixedFrequency = @MaxDay;
		END;
		
		IF @DM < @FixedFrequency
		BEGIN
			SET @NextDate = dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), @FixedFrequency, @TimePart);
		END
		ELSE IF @DM > @FixedFrequency
		BEGIN
			SET @NextDate = DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), 1, @TimePart));
			SET @MaxDay = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, @NextDate)));
			SET @FixedFrequency =
				CASE
					WHEN @FrequencyInterval > @MaxDay
						THEN @MaxDay
						ELSE @FrequencyInterval
				END;
			SET @NextDate = dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), @FixedFrequency, @TimePart);
		END;
	END
	ELSE IF @FrequencyType = 5 --Quarterly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(QUARTER, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 6 --Bi-Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(MONTH, 6, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 7 --Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(YEAR, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 8 --Hourly
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NULL 
					THEN @UtcFirstRunDate
					ELSE DATEADD(HOUR, @FrequencyInterval, @UtcLastRunDate)
			END;
	END
	
	RETURN @NextDate;
END

GO