CREATE FUNCTION [dbo].[fnReportScheduleTypeAndIntervalCheck]
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS BIT
WITH SCHEMABINDING
AS
BEGIN
	RETURN
		CASE @FrequencyType
			WHEN 1 THEN -- Once
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 2 THEN -- Daily
				CASE
					WHEN @FrequencyInterval < 1 THEN 0
					ELSE 1
				END
			WHEN 3 THEN -- Weekly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 7 THEN 1
					ELSE 0
				END
			WHEN 4 THEN -- Monthly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 31 THEN 1
					ELSE 0
				END
			WHEN 5 THEN -- Quarterly
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 6 THEN -- Bi-Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 7 THEN -- Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 8 THEN -- Hourly
				CASE
					WHEN @FrequencyInterval < 1 THEN 0
					ELSE 1
				END
			ELSE 0
		END;
END;