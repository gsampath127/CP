CREATE FUNCTION [dbo].[fnFrequencyDescription]
(
	@FrequencyType INT,
	@FrequencyInterval INT,
	@WeekDays VARCHAR(20)
)
RETURNS VARCHAR(200)
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @FrequencyDescription VARCHAR(200) = NULL;
	IF @FrequencyType = 3 AND @WeekDays IS NOT NULL 
	BEGIN        
			DECLARE @tblCount INT = 0, @count INT = 1	
			DECLARE @WeekConf Table(DayID INT, WD BIT, RowNum INT);
			INSERT INTO @WeekConf(DayID, WD, RowNum)
			SELECT ID, Element, ROW_NUMBER() OVER(ORDER BY Id) FROM [dbo].Split(@WeekDays,',') WHERE Element=1
			SET @FrequencyDescription = 'Runs Weekly on';
			SET @tblCount = (select count(*) from @WeekConf);
			WHILE(@count <= @tblCount)
			BEGIN
			SET @FrequencyDescription = @FrequencyDescription + 
				CASE (SELECT DayID from @WeekConf WHERE RowNum = @count)
					WHEN 2 THEN ' Monday,'
					WHEN 3 THEN ' Tuesday,'
					WHEN 4 THEN ' Wednesday,'
					WHEN 5 THEN ' Thursday,'
					WHEN 6 THEN ' Friday,'
					WHEN 7 THEN ' Saturday,'
					ELSE ' Sunday,'
				END
			SET @count = @count + 1;
			END
			SET @FrequencyDescription = Substring(@FrequencyDescription, 1 , Len(@FrequencyDescription)-1) + '.';
	END
	ELSE
	BEGIN
			SET @FrequencyDescription = 
			CASE @FrequencyType
				WHEN 1
					THEN 'Runs once.'
				WHEN 2
					THEN 'Runs every (' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 ELSE @FrequencyInterval END) + ') days.'
				WHEN 3
					THEN 'Runs Weekly on ' +
					CASE @FrequencyInterval
						WHEN 2 THEN 'Monday.'
						WHEN 3 THEN 'Tuesday.'
						WHEN 4 THEN 'Wednesday.'
						WHEN 5 THEN 'Thursday.'
						WHEN 6 THEN 'Friday.'
						WHEN 7 THEN 'Saturday.'
						ELSE 'Sunday.'
					END
				WHEN 4
					THEN 'Runs Monthly on day ' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END) + '.'
				WHEN 5
					THEN 'Runs Quarterly.'
				WHEN 6
					THEN 'Runs Semi-Annually.'
				WHEN 7 
					THEN 'Runs Annually'
				WHEN 8
					THEN 'Runs every (' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 ELSE @FrequencyInterval END) + ') hours.'
				ELSE 'Does not run.'
			END;
	END
	RETURN @FrequencyDescription;
END;