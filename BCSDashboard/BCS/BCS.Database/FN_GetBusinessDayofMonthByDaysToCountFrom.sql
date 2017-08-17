
ALTER FUNCTION  FN_GetBusinessDayofMonthByDaysToCountFrom(@lastBusinessDaysToCountFrom int, @HolidayTypeId int = null)
RETURNS DATETIME  
WITH EXECUTE AS CALLER  
AS  
BEGIN       
    DECLARE @currentdate DATETIME = GETDATE()
            

    DECLARE @BusinessDay DATETIME  =  CONVERT(varchar(4),YEAR(@currentdate)) + '/' 
						+  CONVERT(varchar(2),Month(@currentdate)) + '/' 
						+ CONVERT(varchar(2),DATEDIFF(DAY, @currentdate, DATEADD(MONTH, 1, @currentdate)))
						+ ' ' + CONVERT(varchar(2),DATEPART(HOUR,  @currentdate)) + ':'
						+ CONVERT(varchar(2),DATEPART(minute,  @currentdate)) + ':'
						+ CONVERT(varchar(2),DATEPART(SECOND,  @currentdate)) 


	DECLARE @BusinessDaysCounter int = 0
             

    WHILE @BusinessDaysCounter < @lastBusinessDaysToCountFrom
    BEGIN
        IF @BusinessDaysCounter != 0
        BEGIN
            SET @BusinessDay = DATEADD(D,-1,@BusinessDay)
			
			IF EXISTS(SELECT HolidayID FROM Holidays WHERE Convert(Date, [Date]) = Convert(Date, @BusinessDay) and HolidayTypeId = @HolidayTypeId)
			BEGIN				
				IF DATEPART(weekday,  @BusinessDay) NOT IN (1 , 7)
				BEGIN
					SET @BusinessDay = DATEADD(D,-1, @BusinessDay)
				END				
			END
			 
        END


				
        IF DATEPART(weekday,  @BusinessDay) = 1
			BEGIN
				SET @BusinessDay = DATEADD(D,-2,@BusinessDay) 
			END
        ELSE
			IF DATEPART(weekday,  @BusinessDay) = 7
			BEGIN
				SET @BusinessDay = DATEADD(D,-1,@BusinessDay) 
			END
                
        SET @BusinessDaysCounter = @BusinessDaysCounter + 1
    END

            
        
    RETURN(@BusinessDay)
END