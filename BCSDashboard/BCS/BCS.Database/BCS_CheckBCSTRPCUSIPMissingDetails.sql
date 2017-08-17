USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_CheckBCSTRPCUSIPMissingDetails]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_CheckBCSTRPCUSIPMissingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_CheckBCSTRPCUSIPMissingDetails]
GO

CREATE PROCEDURE [dbo].[BCS_CheckBCSTRPCUSIPMissingDetails]
AS
BEGIN
	
	DECLARE @IsMissingCusip BIT
	SET @IsMissingCusip = 0
	
	IF EXISTS(
			SELECT Top 1 BCSTRPFLT.BCSTRPFLTID
			FROM BCSTRPFLT	
			LEFT JOIN ProsTicker ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
			WHERE ProsTicker.CUSIP IS NULL)
	BEGIN
		SET @IsMissingCusip = 1
	END
	
	SELECT @IsMissingCusip
End
GO

