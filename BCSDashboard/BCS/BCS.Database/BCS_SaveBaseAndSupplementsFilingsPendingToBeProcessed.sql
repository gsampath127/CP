
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_SaveBaseAndSupplementsFilingsPendingToBeProcessed]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_SaveBaseAndSupplementsFilingsPendingToBeProcessed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_SaveBaseAndSupplementsFilingsPendingToBeProcessed]
GO


CREATE PROCEDURE [dbo].[BCS_SaveBaseAndSupplementsFilingsPendingToBeProcessed]
@EdgarID int,
@DocumentType nvarchar(4),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@FundName nvarchar(255),
@FilingAddedDate datetime
AS
BEGIN

	--Return if CUSIp is not present in watchlist
	IF NOT EXISTS (SELECT CUSIP FROM BCSWatchListCUSIPView WHERE CUSIP = @CUSIP)
	BEGIN

		RETURN

	END


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
	
	
	
	--1) Check if CUSIP is already using summary prospectus in doc update table,then don't replace with P,just return back
	IF @DocumentType in ('P','PS','RP')
	BEGIN	
		IF EXISTS (
				  SELECT BCSDocUpdateSupplementsID 
				  FROM BCSDocUpdateSupplements 
				   WHERE BCSDocUpdateSupplements.CUSIP = @CUSIP 
					AND BCSDocUpdateSupplements.IsRemoved = 0 
					AND BCSDocUpdateSupplements.DocumentType in ('SP','SPS','RSP')					
			)
		BEGIN							  
		  RETURN
		END
	END
	
	--2) if P exists and SP is being inserted,update P records as Isremoved =1 for that cusip.
	IF  @DocumentType in ('SP','SPS','RSP')
	Begin						  				
	 					  					
		  IF EXISTS ( SELECT Top 1 BCSDocUpdateSupplementsID
					  FROM BCSDocUpdateSupplements 
					  WHERE BCSDocUpdateSupplements.CUSIP = @CUSIP AND BCSDocUpdateSupplements.IsRemoved = 0 
						AND BCSDocUpdateSupplements.DocumentType in ('P','PS','RP')
			)
			BEGIN
			
				UPDATE BCSDocUpdateSupplements
				SET IsRemoved = 1
				WHERE BCSDocUpdateSupplements.CUSIP = @CUSIP AND BCSDocUpdateSupplements.IsRemoved = 0 
						AND BCSDocUpdateSupplements.DocumentType in ('P','PS','RP')
			
			
			END
			
	  End
	
	
	IF NOT EXISTS (SELECT TOP 1 BCSDocUpdateSupplementsID FROM BCSDocUpdateSupplements
			   WHERE EdgarID = @EdgarID AND CUSIP = @CUSIP AND IsRemoved = 0)

	BEGIN
	
		INSERT INTO BCSDocUpdateSupplements
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
GO