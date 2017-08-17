CREATE FUNCTION dbo.fnFrequencyDescription
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS VARCHAR(200)
WITH SCHEMABINDING
AS
BEGIN
	RETURN
	(
		SELECT
			CASE @FrequencyType
				WHEN 1
					THEN 'Runs once.'
				WHEN 2
					THEN 'Runs every (' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 ELSE @FrequencyInterval END) + ') days.'
				WHEN 3
					THEN 'Runs Weekly on '
					+ CASE @FrequencyInterval
						WHEN 2 THEN 'Monday'
						WHEN 3 THEN 'Tuesday'
						WHEN 4 THEN 'Wednesday'
						WHEN 5 THEN 'Thursday'
						WHEN 6 THEN 'Friday'
						WHEN 7 THEN 'Saturday'
						ELSE 'Sunday'
					END + '.'
				WHEN 4
					THEN 'Runs Monthly on day ' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END) + '.'
				WHEN 5
					THEN 'Runs Quarterly.'
				WHEN 6
					THEN 'Runs Bi-Annually.'
				WHEN 7 
					THEN 'Runs Annually'
				ELSE 'Does not run.'
			END
	);
END;