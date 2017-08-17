
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_SaveARSARFilingsPendingToBeProcessed]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_SaveARSARFilingsPendingToBeProcessed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_SaveARSARFilingsPendingToBeProcessed]
GO


CREATE PROCEDURE [dbo].[BCS_SaveARSARFilingsPendingToBeProcessed]
@EdgarID int,
@DocumentType nvarchar(4),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@FundName nvarchar(255),
@FilingAddedDate datetime
AS
BEGIN

	DECLARE @Acc# nvarchar(250)

	DECLARE @EffectiveDate datetime

	DECLARE @DocumentDate datetime

	DECLARE @FiledDate datetime

	DECLARE @FormType nvarchar(100)

	SELECT @Acc# = Acc#,
		@EffectiveDate = EffectiveDate,
		@DocumentDate = DocumentDate,
		@FiledDate = DateFiled,
		@FormType = FormType 
	FROM Edgar
	WHERE EdgarID = @EdgarID
	
	
	IF NOT EXISTS (SELECT TOP 1 BCSDocUpdateARSARID FROM BCSDocUpdateARSAR
			   WHERE EdgarID = @EdgarID AND CUSIP = @CUSIP)

	BEGIN
	
		INSERT INTO BCSDocUpdateARSAR
		(
			EdgarID,
			Acc#,		
			DocumentType,
			CUSIP,
			TickerID,
			EffectiveDate,
			DocumentDate,	 
			ProsID,
			FundName,			
			IsFiled,
			FiledDate,
			FilingStatusAddedDate,
			IsProcessed,
			FormType
		)
		VALUES
		(
			@EdgarID,
			@Acc#,
			@DocumentType,
			@CUSIP,
			@TickerID,
			@EffectiveDate,
			@DocumentDate,
			@ProsID,
			@FundName,			
			1,
			@FiledDate,
			@FilingAddedDate,
			0,		
			@FormType
		)
	
	END 

END