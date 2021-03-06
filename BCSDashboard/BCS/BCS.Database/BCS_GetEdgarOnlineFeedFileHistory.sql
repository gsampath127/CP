USE [db1029]
GO

CREATE Procedure [dbo].[BCS_GetEdgarOnlineFeedFileHistory] 
	@SelectedDate DATETIME
AS
BEGIN
	
	SELECT	DateReceived, 
			[FileName]
	FROM  [dbo].[EdgarOnlineFeedFileHistory] WHERE 
	Convert(varchar(50), DateReceived, 101) = Convert(varchar(50), @SelectedDate, 101)
END
