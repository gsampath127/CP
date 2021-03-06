

ALTER Procedure [dbo].[BCS_SaveDocUpdateFilingsPendingToBeProcessed]
@SECDetails SECDetailsType READONLY,
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@EdgarID int,
@FundName nvarchar(255),
@FilingAddedDate datetime,
@BCSDocUpdateID int output,
@IsProcessed bit output
as
Begin
		 Declare @CurrentDocumentType nvarchar(4)
		 
		 Declare @CurrentEffectiveDate datetime
		 
		 Declare @CurrentDocumentDate datetime
		 
		 Declare @CurrentDateFiled datetime
		 
		 Declare @CurrentAcc# nvarchar(250)
		 
		 set @IsProcessed = 0
		 
		 
		 SELECT		@CurrentDocumentType = DocumentType,
					@CurrentEffectiveDate=EffectiveDate,
					@CurrentDocumentDate=DocumentDate,
					@CurrentDateFiled=DateFiled,			
					@CurrentAcc#=Acc# 
		 FROM @SECDetails SECDetails
		 WHERE EdgarID = @EdgarID
		
		  
		  
				   SELECT @BCSDocUpdateID = BCSDocUpdateID  -- check if it was already processed or already added as a filing status record 												
					   FROM BCSDocUpdate			       --  by the time we picked this record.
					   WHERE BCSDocUpdate.CUSIP = @CUSIP				  
						  AND BCSDocUpdate.Acc# = @CurrentAcc#
						  AND BCSDocUpdate.EdgarID = @EdgarID
						  AND BCSDocUpdate.DocumentType = @CurrentDocumentType				  
						  AND BCSDocUpdate.IsRemoved = 0	  				  				  
						  
				   IF @BCSDocUpdateID is not null
					  BEGIN
						SET @IsProcessed = 1
						RETURN
					  END	
					  
					  
				   IF @CurrentDocumentType in ('P','PS','RP')
					  Begin	
						IF EXISTS (
						  SELECT BCSDocUpdateID -- Check if CUSIP is already using summary prospectus in doc update table,then don't replace with P,just return back
						  FROM BCSDocUpdate 
						   WHERE BCSDocUpdate.CUSIP = @CUSIP 
							AND BCSDocUpdate.IsRemoved = 0 
							AND BCSDocUpdate.DocumentType in ('SP','SPS','RSP')					
							)
							BEGIN
							  SET @IsProcessed = 1
							  return
							END
					   End		
					   
				   IF  @CurrentDocumentType in ('SP','SPS','RSP')
					  Begin		
					   --  IF @CurrentDocumentDate <= Convert(date, getdate()-1)
							 --Begin
								--IF Exists(SELECT BCSDocUpdateID FROM BCSDocUpdate
								--			WHERE BCSDocUpdate.CUSIP = @CUSIP
								--			AND BCSDocUpdate.DocumentType in ('P','PS','RP')
								--			AND IsProcessed=1
								--			AND IsRemoved=0) -- we don't want to replace a processed P with a SP in FL mode and document date older than a day
								--BEGIN
								--  SET @IsProcessed = 1
								--  return
								--END	
						  --    END						
					     					  					
							  SELECT @BCSDocUpdateID=BCSDocUpdateID -- Check if CUSIP is using prospectus in doc update table,then replace with Summary since Summmary Replaces Prospectus
							  FROM BCSDocUpdate 
							   WHERE BCSDocUpdate.CUSIP = @CUSIP 
								AND BCSDocUpdate.IsRemoved = 0 
								AND BCSDocUpdate.DocumentType in ('P','PS','RP')					
					      End						   			  	  
		  
				   IF @BCSDocUpdateID is null -- if not null then SP came in and P is in doc update,P needs to be replaced .don't do any further checks.
				   BEGIN
		  
						SELECT @BCSDocUpdateID = BCSDocUpdateID FROM BCSDocUpdate -- Check if a entry with a new document date exists.if so return as IsProcessed
						  WHERE BCSDocUpdate.CUSIP = @CUSIP					  
							  AND BCSDocUpdate.IsRemoved = 0
							  AND convert(datetime, convert(varchar(10), BCSDocUpdate.DocumentDate, 102))  
								   > convert(datetime, convert(varchar(10), @CurrentDocumentDate, 102))	
								   					  
						IF @BCSDocUpdateID is not null
						  BEGIN
							SET @IsProcessed = 1
							RETURN
						  END	
						  
						SELECT @BCSDocUpdateID = BCSDocUpdateID FROM BCSDocUpdate -- Check if a entry with a current document date exists with same or greater than filing date.if so return as IsProcessed
						  WHERE BCSDocUpdate.CUSIP = @CUSIP					  
							  AND BCSDocUpdate.IsRemoved = 0
							  AND convert(datetime, convert(varchar(10), BCSDocUpdate.DocumentDate, 102))  
								   = convert(datetime, convert(varchar(10), @CurrentDocumentDate, 102))	
							  AND convert(datetime, convert(varchar(10), BCSDocUpdate.FiledDate, 102))  
								   >= convert(datetime, convert(varchar(10), @CurrentDateFiled, 102))	
								   					  
						IF @BCSDocUpdateID is not null
						  BEGIN
							SET @IsProcessed = 1
							RETURN
						  END						  
						  
						
					  

						  IF @BCSDocUpdateID is null -- check if a existing document exists
							  BEGIN			    
								 SELECT @BCSDocUpdateID = BCSDocUpdateID FROM BCSDocUpdate 
								 WHERE BCSDocUpdate.CUSIP = @CUSIP					  
									  AND BCSDocUpdate.IsRemoved = 0								  					     
							  END

				END 
				  
					  IF @BCSDocUpdateID is null -- IF DocUpdateID is null,it means the CUSIP is new or we are adding a duplicate record 
						  BEGIN
							UPDATE BCSDocUpdate
							 SET IsRemoved=1
							WHERE CUSIP = @CUSIP AND IsRemoved=0
							
							 INSERT INTO BCSDocUpdate(EdgarID,Acc#,
													  RRDPDFURL,PDFName,
													  DocumentType,CUSIP,
													  TickerID,EffectiveDate,
													  DocumentDate,ProsID,
													  IsFiled,FiledDate,
													  FilingStatusAddedDate,
													  FundName,ProsDocID,
													  IsProcessed,ProcessedDate
													  )
							  Values(
									 @EdgarID,@CurrentAcc#,
									 null,null,
									 @CurrentDocumentType ,@CUSIP,
									 @TickerID,@CurrentEffectiveDate,
									 @CurrentDocumentDate,@ProsID,
									 1,@CurrentDateFiled,
									 @FilingAddedDate,
									 @FundName,null,0,null	
									)	
						
							SELECT @BCSDocUpdateID = @@IDENTITY		
						  END
					  Else
						  BEGIN
					    
							 UPDATE BCSDocUpdate
							   SET EdgarID=@EdgarID,Acc#=@CurrentAcc#,
									RRDPDFURL=null,PDFName=null,
									[PageCount]=null,PageSizeHeight=null,PageSizeWidth=null,
									DocumentType=@CurrentDocumentType,TickerID=@TickerID,
									EffectiveDate=@CurrentEffectiveDate,DocumentDate=@CurrentDocumentDate,
									ProsID=@ProsID,IsFiled=1,FiledDate=@CurrentDateFiled,
									FilingStatusAddedDate=@FilingAddedDate,
									FundName=@FundName,ProsDocID=null,
									IsProcessed=0,ProcessedDate=null
								WHERE BCSDocUpdateId=@BCSDocUpdateID
								
							  DELETE BCSDocUpdateSECDetails  
							   WHERE BCSDocUpdateSECDetails.DocUpdateID = @BCSDocUpdateID 		
						   
						  END			  
					
						INSERT INTO BCSDocUpdateSECDetails(DocUpdateID,Acc#,
														   DateFiled,DocumentDate,EffectiveDate,
														   FormType,DocumentType)
						 SELECT Distinct @BCSDocUpdateID,Acc#,
								DateFiled,DocumentDate,EffectiveDate,
								FormType,DocumentType
						 FROM 	@SECDetails SECDetails
						 ORDER BY DocumentDate								   
					  
						  
End
