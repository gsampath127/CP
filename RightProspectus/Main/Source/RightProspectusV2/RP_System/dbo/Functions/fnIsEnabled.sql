CREATE FUNCTION [dbo].[fnIsEnabled]
(
	@UtcNextScheduledRunDate DATETIME,
	@UtcScheduledEndDate DATETIME
	
)
RETURNS BIT
WITH SCHEMABINDING
AS
BEGIN
	  DECLARE @IsENabled BIT=1;
	    IF @UtcScheduledEndDate IS NULL
		    SET  @IsENabled =1;
		ELSE IF  @UtcNextScheduledRunDate IS NULL
		     SET @IsENabled =0;
	    ELSE IF @UtcNextScheduledRunDate > @UtcScheduledEndDate
		    SET  @IsENabled =0;
		

	RETURN @IsENabled
END;