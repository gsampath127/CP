ALTER Procedure [dbo].[BCS_SaveDocUpdateSLinkAndSECDetailsWithoutEdgarID]    
@SECDetails SECDetailsType READONLY,    
@RRDPDFURL nvarchar(512),    
@PDFName nvarchar(55),    
@CUSIP nvarchar(10),    
@TickerID int,    
@ProsID int,    
@FundName nvarchar(255),    
@ProsDocID int,  
@FilingAddedDate datetime,    
@ProcessedDate datetime,
@FromProcess int,    
@DocumentType varchar(3),
@BCSDocUpdateID int output,    
@IsReprocessed bit output    
as    
Begin    
   Declare @CurrentDocumentType nvarchar(4)    
       
   Declare @CurrentEffectiveDate datetime    
       
   Declare @CurrentDocumentDate datetime    
       
   Declare @CurrentDateFiled datetime    
       
   Declare @CurrentAcc# nvarchar(250)    
       
   Declare @CurrentEdgarID int    
       
   set @IsReprocessed = 0    
   
   -- @FromProcess 1 -> From CUSIPNewlyAdded 2-> SP in FL Mode add P
       
       
        
       
   SELECT top 1 @CurrentEdgarID=EdgarID,@CurrentDocumentType=SECDetails.DocumentType,    
     @CurrentEffectiveDate=ISNULL(SECDetails.EffectiveDate,SECDetails.DocumentDate),    
     @CurrentDocumentDate=SECDetails.DocumentDate,    
     @CurrentDateFiled=SECDetails.DateFiled,    
     @CurrentAcc#=Acc#     
  FROM  @SECDetails SECDetails           
  ORDER BY SECDetails.DocumentDate desc,SECDetails.DateFiled desc  
  
  		 Declare @AllDocumenTypes Table
		 (
		   DocumentType nvarchar(4)
		 ) 
  
  			  IF @DocumentType = 'SP' -- DocumentType is actually ProsDocTypeID
			  Begin
			  		INSERT INTO @AllDocumenTypes values('P')
					INSERT INTO @AllDocumenTypes values('PS')
					INSERT INTO @AllDocumenTypes values('RP')			    
			  End
			  Else 
			    IF @DocumentType = 'P'
			    Begin
		   			    INSERT INTO @AllDocumenTypes values('SP')
					    INSERT INTO @AllDocumenTypes values('SPS')
					    INSERT INTO @AllDocumenTypes values('RSP')
			    End 
			    
			    Declare @PageCount int
				Declare @PageSizeHeight [decimal](5, 2)
				Declare @PageSizeWidth [decimal](5, 2) 
				
				SELECT @PageCount=[PageCount],
					   @PageSizeHeight = PageSizeHeight,
					   @PageSizeWidth = PageSizeWidth
				  FROM ProsDocs
				 WHERE ProsDocId = @ProsDocID
      
        
  IF @FromProcess = 1 
		AND NOT EXISTS(
						SELECT BCSDocUpdateId 
							FROM BCSDocUpdate 
							INNER JOIN @AllDocumenTypes AllDocumenTypes 
									ON BCSDocUpdate.DocumentType = AllDocumenTypes.DocumentType
							WHERE BCSDocUpdate.CUSIP = @CUSIP AND IsRemoved = 0
					  )
  Begin   

     
			SELECT @BCSDocUpdateID = BCSDocUpdateID  -- check if it was processed before with same slinkname    
			  FROM BCSDocUpdate   
			WHERE BCSDocUpdate.CUSIP = @CUSIP   
				AND BCSDocUpdate.ProsID = @ProsID           
				AND BCSDocUpdate.IsRemoved = 0            
		          
		          
			IF @BCSDocUpdateID IS NOT NULL 
			Begin    
			  SET @IsReprocessed = 1    
			  RETURN    
			End        
				Else   -- IF DocUpdateID is null,it means the CUSIP is new or we are adding a duplicate record          
					 Begin   
						 UPDATE BCSDocUpdate  -- We are doing  this to handle merger scenarios where Prosid for a CUSIP could change since CUSIP was moved to another prospectus
						 SET IsRemoved = 1  
						 WHERE CUSIP = @CUSIP 
						 AND IsRemoved = 0 
						 
						 
					       
						 INSERT INTO BCSDocUpdate(EdgarID,Acc#,    
								RRDPDFURL,PDFName,    
								DocumentType,CUSIP,    
								TickerID,EffectiveDate,    
								DocumentDate,ProsID,    
								IsFiled,FiledDate,    
								FundName,ProsDocID,FilingStatusAddedDate,    
								IsProcessed,ProcessedDate 
								,[PageCount],PageSizeHeight
								,PageSizeWidth   
								)    
						  Values(@CurrentEdgarID,@CurrentAcc#,    
						   @RRDPDFURL,@PDFName,    
						   @CurrentDocumentType ,@CUSIP,    
						   @TickerID,@CurrentEffectiveDate,    
						   @CurrentDocumentDate,@ProsID,    
						   1,@CurrentDateFiled,    
						   @FundName,@ProsDocID,@FilingAddedDate,1,@ProcessedDate,
						    @PageCount,@PageSizeHeight,
							 @PageSizeWidth     
						  )     
					        
						SELECT @BCSDocUpdateID = @@IDENTITY  
						
						INSERT INTO BCSDocUpdateSECDetails(DocUpdateID,Acc#,    
						  DateFiled,DocumentDate,EffectiveDate,    
						  FormType,DocumentType)    
						SELECT Distinct @BCSDocUpdateID,Acc#,    
						 DateFiled,DocumentDate,EffectiveDate,    
						 FormType,DocumentType    
						FROM  @SECDetails SECDetails    
						Order by DocumentDate      
					 End    
			 
	END
	ELSE
	  IF @FromProcess = 2 -- We will always have SP in FL in the table.
	  Begin
	    SELECT @BCSDocUpdateID=BCSDocUpdateID 
					FROM BCSDocUpdate 					
					WHERE CUSIP = @CUSIP AND IsRemoved=0 and IsProcessed = 0 
					AND DocumentDate <= DATEADD(day,-1,GETDATE())  AND DocumentType IN ('SP', 'SPS', 'RSP')
					
	    IF @BCSDocUpdateID is not null
			AND NOT EXISTS( --- Making sure P does have a filing pending to be processed
						SELECT  DISTINCT FundID FROM Edgar 
						INNER JOIN EdgarFunds ON Edgar.EdgarID = EdgarFunds.EdgarID
						WHERE EdgarFunds.Processed = '0' AND Edgar.DateUpdated IS NULL
						AND Edgar.DocumentType LIKE 'Prospectus%' AND EdgarFunds.FundID = @ProsID
					)
					-- Check if SP is still in FL with current Document Date <= getdate() - 1
		Begin
					UPDATE BCSDocUpdate
					   Set EdgarID=@CurrentEdgarID,Acc#=@CurrentAcc#,
							RRDPDFURL=@RRDPDFURL,PDFName=@PDFName,
							DocumentType=@CurrentDocumentType,TickerID=@TickerID,
							EffectiveDate=@CurrentEffectiveDate,DocumentDate=@CurrentDocumentDate,
							ProsID=@ProsID,IsFiled=1,FiledDate=@CurrentDateFiled,
							FilingStatusAddedDate=@FilingAddedDate,
							FundName=@FundName,ProsDocID=@ProsDocID,
							IsProcessed=1,ProcessedDate=@ProcessedDate,
							[PageCount]=@PageCount,PageSizeHeight=@PageSizeHeight,
							 PageSizeWidth=@PageSizeWidth	
						WHERE BCSDocUpdateId=@BCSDocUpdateID
						
					  DELETE BCSDocUpdateSECDetails  
					   WHERE BCSDocUpdateSECDetails.DocUpdateID = @BCSDocUpdateID 
					   
					   INSERT INTO BCSDocUpdateSECDetails(DocUpdateID,Acc#,    
						  DateFiled,DocumentDate,EffectiveDate,    
						  FormType,DocumentType)    
					    SELECT Distinct @BCSDocUpdateID,Acc#,    
						 DateFiled,DocumentDate,EffectiveDate,    
						 FormType,DocumentType    
						FROM  @SECDetails SECDetails    
						Order by DocumentDate    	
		End
		Else
		 Begin
			SET @IsReprocessed = 1
		 End
	  END	
	  ELSE
	   BEGIN
			SET @IsReprocessed = 1
	   ENd	      
           
  End         
	       
	           
           