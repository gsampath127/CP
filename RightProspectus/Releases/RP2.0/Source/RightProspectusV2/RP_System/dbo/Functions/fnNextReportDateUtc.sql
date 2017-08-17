CREATE FUNCTION dbo.fnNextReportDateUtc
(
	@UtcFirstRunDate DATETIME,
	@UtcLastRunDate DATETIME,
	@FrequencyType INT,
	@FrequencyInterval INT
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
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval BETWEEN 2 AND 7 THEN @FrequencyInterval ELSE 1 END;
		--Can't use DATEPART with WEEKDAY or DW as this is non deterministic. So we pick a sunday date way in the past and do a DATEDIFF.
		DECLARE @SundayDate DATETIME = CONVERT(DATETIME, '18991231', 112);
		DECLARE @DW INT = DATEDIFF(DAY, @SundayDate, @NextDate) % 7 + 1;
		IF (@DW < @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, @FrequencyInterval - @DW, @NextDate);
		END
		ELSE IF (@DW > @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, 7 - (@DW - @FrequencyInterval), @NextDate);
		END;
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
	END;
	
	RETURN @NextDate;
END;

GO