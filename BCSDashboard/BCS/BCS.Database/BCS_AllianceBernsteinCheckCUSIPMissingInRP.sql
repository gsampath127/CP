USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_AllianceBernsteinCheckCUSIPMissingInRP]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_AllianceBernsteinCheckCUSIPMissingInRP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_AllianceBernsteinCheckCUSIPMissingInRP]
GO

CREATE PROCEDURE [dbo].[BCS_AllianceBernsteinCheckCUSIPMissingInRP]
AS
BEGIN
	
	DECLARE @IsMissingCusip BIT
	SET @IsMissingCusip = 0
	
	IF EXISTS(
			SELECT Top 1 clmn5
			FROM BCSAllianceBernstein_FTP_RT1	
			LEFT JOIN ProsTicker ON ProsTicker.CUSIP like clmn5 + '%'      
			WHERE ProsTicker.CUSIP IS NULL)
	BEGIN
		SET @IsMissingCusip = 1
	END
	
	SELECT @IsMissingCusip
End
GO

