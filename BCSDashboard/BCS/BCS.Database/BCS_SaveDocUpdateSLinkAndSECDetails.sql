ALTER Procedure [dbo].[BCS_SaveDocUpdateSLinkAndSECDetails]
@SECDetails SECDetailsType READONLY,
@RRDPDFURL nvarchar(512),
@PDFName nvarchar(55),
@CUSIP nvarchar(10),
@TickerID int,
@ProsID int,
@EdgarID int,
@FundName nvarchar(255),
@ProsDocID int,
@ProcessedDate datetime,
@BCSDocUpdateID int output,
@IsReprocessed bit output
as
Begin
		 Declare @CurrentDocumentType nvarchar(4)
		 
		 Declare @CurrentEffectiveDate datetime
		 
		 Declare @CurrentDocumentDate datetime
		 
		 Declare @CurrentDateFiled datetime
		 
		 Declare @CurrentAcc# nvarchar(250)
		 
		 Declare @FilingStatusAddedDate datetime
		 
		 Declare @AllDocumenTypes Table
		 (
		   DocumentType nvarchar(4)
		 )
		 
		 set @IsReprocessed = 0
		 
		 
		 
		 SELECT @CurrentDocumentType=
						REPLACE(
							REPLACE(
								REPLACE(
									REPLACE(
											REPLACE(
													REPLACE(DocumentType,'Summary Prospectus - New','SP')
													,'Summary Prospectus - Revised','RSP')
													,'Summary Prospectus - Supplement','SPS')
													,'Prospectus - New','P')
													,'Prospectus - Revised','RP')
													,'Prospectus - Supplement','PS') ,
					@CurrentEffectiveDate=ISNULL(EffectiveDate,DocumentDate),
					@CurrentDocumentDate=DocumentDate,
					@CurrentDateFiled=DateFiled,			
					@CurrentAcc#=Acc# 
		 FROM Edgar
		 WHERE EdgarID = @EdgarID
		 
		 
		
		
		IF @BCSDocUpdateID is null and @CurrentDocumentType in ('P','PS','RP')
			  Begin	
			    IF EXISTS (
				  SELECT BCSDocUpdateID -- Check if CUSIP is already using summary prospectus in doc update table,then don't replace with P,just return back
			      FROM BCSDocUpdate 
			       WHERE BCSDocUpdate.CUSIP = @CUSIP 
					AND BCSDocUpdate.IsRemoved = 0 
					AND BCSDocUpdate.DocumentType in ('SP','SPS','RSP')
					AND BCSDocUpdate.IsProcessed = 1
					)
					BEGIN
					  SET @IsReprocessed = 1
					  return
					END
			  End
		  
		  
		  SELECT @BCSDocUpdateID = BCSDocUpdateID,@FilingStatusAddedDate = FilingStatusAddedDate  -- check if it was processed before with same slinkname
					FROM BCSDocUpdate
  			   WHERE BCSDocUpdate.CUSIP = @CUSIP				  
				  AND BCSDocUpdate.Acc# = @CurrentAcc#
				  AND BCSDocUpdate.DocumentType = @CurrentDocumentType
				  AND BCSDocUpdate.DocumentDate = @CurrentDocumentDate
				  AND BCSDocUpdate.PDFName = @PDFName
				  AND BCSDocUpdate.IsRemoved = 0	  				  
				  AND IsFiled=1
				  
		  IF @BCSDocUpdateID is not null
		  Begin
		    set @IsReprocessed = 1
		    return
		  End		  
		  Else


			  IF @BCSDocUpdateID is null
			  Begin			    
				 SELECT @BCSDocUpdateID = BCSDocUpdateID,@FilingStatusAddedDate = FilingStatusAddedDate FROM BCSDocUpdate -- Check if a edgar exists that is in filing mode
				 WHERE BCSDocUpdate.CUSIP = @CUSIP
					  AND BCSDocUpdate.EdgarID = @EdgarID
					  AND BCSDocUpdate.Acc# = @CurrentAcc#	  
					  AND BCSDocUpdate.IsRemoved = 0
					  AND convert(datetime, convert(varchar(10), BCSDocUpdate.DocumentDate, 102))  
						   = convert(datetime, convert(varchar(10), @CurrentDocumentDate, 102))
					  AND IsProcessed=0 AND IsFiled=1    
			  End
			  
			  IF @BCSDocUpdateID is null and @CurrentDocumentType in ('SP','SPS','RSP')
			  Begin	
			    SELECT @BCSDocUpdateID = BCSDocUpdateID -- Check if CUSIP is using statutory prospectus in doc update table,then replace with SP
			      FROM BCSDocUpdate 
			       WHERE BCSDocUpdate.CUSIP = @CUSIP 
					AND BCSDocUpdate.IsRemoved = 0 
					AND BCSDocUpdate.DocumentType in ('P','PS','RP')
			  End
			  
			  IF @BCSDocUpdateID is null and @CurrentDocumentType in ('P','PS','RP')
			  Begin	
			    SELECT @BCSDocUpdateID = BCSDocUpdateID -- Check if CUSIP is in FL mode for Summary prospectus in doc update table ,then replace with P if doc date > a day
			      FROM BCSDocUpdate 
			       WHERE BCSDocUpdate.CUSIP = @CUSIP 
					AND BCSDocUpdate.IsRemoved = 0 
					AND BCSDocUpdate.DocumentType in ('SP','SPS','RSP') 
					AND BCSDocUpdate.IsProcessed = 0 
					AND BCSDocUpdate.DocumentDate <= DATEADD(D,-1,GETDATE())
			  End
			  
			  IF @BCSDocUpdateID is not null -- either SP in FL Mode Doc Update needs to replaced or P in Doc Update needs to be replaced.
			  Begin
			    SET @FilingStatusAddedDate=@ProcessedDate
			  End
			  
			  IF @CurrentDocumentType in ('SP','SPS','RSP')
			  Begin
			    INSERT INTO @AllDocumenTypes values('SP')
			    INSERT INTO @AllDocumenTypes values('SPS')
			    INSERT INTO @AllDocumenTypes values('RSP')
			  End
			  Else 
			    IF @CurrentDocumentType in ('P','PS','RP')
			    Begin
					INSERT INTO @AllDocumenTypes values('P')
					INSERT INTO @AllDocumenTypes values('PS')
					INSERT INTO @AllDocumenTypes values('RP')			    
			    End
			    


			  IF @BCSDocUpdateID is null
			  Begin			    
				 SELECT @BCSDocUpdateID = BCSDocUpdateID,@FilingStatusAddedDate = FilingStatusAddedDate 
				 FROM BCSDocUpdate -- Check if a older document date exists
				 INNER JOIN @AllDocumenTypes AD on BCSDocUpdate.DocumentType = AD.DocumentType
				 WHERE BCSDocUpdate.CUSIP = @CUSIP
					  AND BCSDocUpdate.EdgarID <> @EdgarID
					  --AND BCSDocUpdate.Acc# <> @CurrentAcc#	-- we commented this since there is a possibility some		  
					  AND BCSDocUpdate.IsRemoved = 0
					  AND convert(datetime, convert(varchar(10), BCSDocUpdate.DocumentDate, 102))  
						   < convert(datetime, convert(varchar(10), @CurrentDocumentDate, 102))
					  --AND IsProcessed=1     
			  End
			  
			  
			  IF @BCSDocUpdateID is null
			  Begin			    
				 SELECT @BCSDocUpdateID = BCSDocUpdateID,@FilingStatusAddedDate = FilingStatusAddedDate 
				 FROM BCSDocUpdate -- Check if a current document date with old filing date exists
				 INNER JOIN @AllDocumenTypes AD on BCSDocUpdate.DocumentType = AD.DocumentType
				 WHERE BCSDocUpdate.CUSIP = @CUSIP
					  AND BCSDocUpdate.EdgarID <> @EdgarID
					  --AND BCSDocUpdate.Acc# <> @CurrentAcc#	-- we commented this since there is a possibility some		  
					  AND BCSDocUpdate.IsRemoved = 0
					  AND convert(datetime, convert(varchar(10), BCSDocUpdate.DocumentDate, 102))  
						   = convert(datetime, convert(varchar(10), @CurrentDocumentDate, 102))
					  AND convert(datetime, convert(varchar(10), BCSDocUpdate.FiledDate, 102))  
						   < convert(datetime, convert(varchar(10), @CurrentDateFiled, 102))						   
					  --AND IsProcessed=1     
			  End
		  
			  	Declare @PageCount int
				Declare @PageSizeHeight [decimal](5, 2)
				Declare @PageSizeWidth [decimal](5, 2) 
				
				SELECT @PageCount=[PageCount],
					   @PageSizeHeight = PageSizeHeight,
					   @PageSizeWidth = PageSizeWidth
				  FROM ProsDocs
				 WHERE ProsDocId = @ProsDocID

		  
			  IF @BCSDocUpdateID is null -- IF DocUpdateID is null,it means the CUSIP is new or we are adding a duplicate record 
				  Begin
					 INSERT INTO BCSDocUpdate(EdgarID,Acc#,
											  RRDPDFURL,PDFName,
											  DocumentType,CUSIP,
											  TickerID,EffectiveDate,
											  DocumentDate,ProsID,
											  IsFiled,FiledDate,FilingStatusAddedDate,
											  FundName,ProsDocID,
											  IsProcessed,ProcessedDate
											  ,[PageCount],PageSizeHeight
											  ,PageSizeWidth
											  )
					  Values(@EdgarID,@CurrentAcc#,
							 @RRDPDFURL,@PDFName,
							 @CurrentDocumentType ,@CUSIP,
							 @TickerID,@CurrentEffectiveDate,
							 @CurrentDocumentDate,@ProsID,
							 1,@CurrentDateFiled,@ProcessedDate,
							 @FundName,@ProsDocID,1,@ProcessedDate,
							 @PageCount,@PageSizeHeight,
							 @PageSizeWidth	
							)	
				
					SELECT @BCSDocUpdateID = @@IDENTITY		
				  End
			  Else
				  Begin
					 IF @FilingStatusAddedDate < @CurrentDateFiled
					 Begin
					   SET @FilingStatusAddedDate = @ProcessedDate
					 End
					 
					 Update BCSDocUpdate
					   Set EdgarID=@EdgarID,Acc#=@CurrentAcc#,
							RRDPDFURL=@RRDPDFURL,PDFName=@PDFName,
							DocumentType=@CurrentDocumentType,TickerID=@TickerID,
							EffectiveDate=@CurrentEffectiveDate,DocumentDate=@CurrentDocumentDate,
							ProsID=@ProsID,IsFiled=1,FiledDate=@CurrentDateFiled,FilingStatusAddedDate=@FilingStatusAddedDate,
							FundName=@FundName,ProsDocID=@ProsDocID,
							IsProcessed=1,ProcessedDate=@ProcessedDate,
							[PageCount]=@PageCount,PageSizeHeight=@PageSizeHeight,
							 PageSizeWidth=@PageSizeWidth	
						WHERE BCSDocUpdateId=@BCSDocUpdateID
						
					  Delete BCSDocUpdateSECDetails  
					   Where BCSDocUpdateSECDetails.DocUpdateID = @BCSDocUpdateID 		

					  DELETE BCSDocUpdateGIMSlink
						WHERE DocUpdateID = @BCSDocUpdateID			

				   
				  End			  
			
				INSERT INTO BCSDocUpdateSECDetails(DocUpdateID,Acc#,
												   DateFiled,DocumentDate,EffectiveDate,
												   FormType,DocumentType)
				 SELECT Distinct @BCSDocUpdateID,Acc#,
						DateFiled,DocumentDate,EffectiveDate,
						FormType,DocumentType
				 FROM 	@SECDetails SECDetails
				 ORDER BY DocumentDate								   
					  
						  
End

