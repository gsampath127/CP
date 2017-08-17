USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[OnGoing_AddDocumentToProsdocs]    Script Date: 03/20/2015 13:58:40 ******/

ALTER Procedure [dbo].[OnGoing_AddDocumentToProsdocs] 
	@EdgarID int,
	@ProsIDs nvarchar(MAX),
	@ProsDocTypeID varchar(3),
	@ProsDocURL varchar(500),	
	@IsDocRevised bit,
	@ProsDocUsePDF bit,
	@ProsDocPDFInitial int,
	@DeleteSupplements bit,
	@Path nvarchar(max)=null,
	@FileName varchar(100)=null,
	@CompanyFolderName nvarchar(100)=null,
	@PageCount int=null,
	@PageSizeHeight decimal(5,2)=null,
	@PageSizeWidth decimal(5,2)=null	
	
as 
Begin
   BEGIN TRANSACTION
      
	SET NOCOUNT ON;


	
	DECLARE @RowData NVARCHAR(MAX)
    DECLARE @Delimeter NVARCHAR(1)
    DECLARE @Docdate datetime
    
	DECLARE @tblProsID TABLE 
	(
		ProsID INT
	) 
   
   
   
   	SET @RowData =@ProsIDs
	SET @Delimeter = ','

	DECLARE @Iterator INT
    SET @Iterator = 1

    DECLARE @FoundIndex INT
    SET @FoundIndex = CHARINDEX(@Delimeter,@RowData)
 
	

    WHILE (@FoundIndex>0)
    BEGIN

        INSERT INTO @tblProsID (ProsID)
        
        SELECT 
            ProsID = LTRIM(RTRIM(SUBSTRING(@RowData, 1, @FoundIndex - 1)))

	    SET @RowData = SUBSTRING(@RowData,
					@FoundIndex + DATALENGTH(@Delimeter) / 2,
					LEN(@RowData))


        SET @Iterator = @Iterator + 1
        SET @FoundIndex = CHARINDEX(@Delimeter, @RowData)

    END
  
    INSERT INTO @tblProsID (ProsID)
    SELECT Data = LTRIM(RTRIM(@RowData))  
    
    Declare @DocumentType varchar(250)
    
    SELECT @Docdate=DocumentDate, @DocumentType = DocumentType
     FROM Edgar
     WHERE EdgarID = @EdgarID
    
	IF @DocumentType not like '%supplement%'
	Begin
			--Update the Date of the Document based on ProsDocTypeid .if revised update the revised date. 
			 IF @ProsDocTypeID = 'P'
			 Begin
			  IF @IsDocRevised = 0
				UPDATE Prospectus
				  SET ProsDate = @DocDate,RevisedProsDate=null
				  WHERE ProsID in (SELECT ProsID from @tblProsID)
			  ELSE
				  UPDATE Prospectus
				  SET RevisedProsDate = @DocDate
				  WHERE ProsID in (SELECT ProsID from @tblProsID)
			  End
			 Else
			  IF @ProsDocTypeID = 'SP'
			  Begin
				  IF @IsDocRevised = 0
					UPDATE Prospectus
					  SET SPDate = @DocDate,RevisedSPDate=null
					WHERE ProsID in (SELECT ProsID from @tblProsID)
				  ELSE
					  UPDATE Prospectus
					   SET RevisedSPDate = @DocDate
					  WHERE ProsID in (SELECT ProsID from @tblProsID)      
			  End
			  Else
				IF @ProsDocTypeID = 'S'
				Begin
				  IF @IsDocRevised = 0
					 UPDATE Prospectus
					  SET SDate = @DocDate,RevisedSDate=null
					 WHERE ProsID in (SELECT ProsID from @tblProsID)
				  ELSE
					  UPDATE Prospectus
					   SET RevisedSDate = @DocDate
					  WHERE ProsID in (SELECT ProsID from @tblProsID)        
				End
				Else
					IF @ProsDocTypeID = 'AR'
					Begin
					  IF @IsDocRevised = 0
						 UPDATE Prospectus
						  SET ARDate = @DocDate,RevisedARDate=null
						 WHERE ProsID in (SELECT ProsID from @tblProsID)
					  ELSE
						  UPDATE Prospectus
						   SET RevisedARDate = @DocDate
						  WHERE ProsID in (SELECT ProsID from @tblProsID)       
					End
					Else
						IF @ProsDocTypeID = 'SAR'
						Begin
						  IF @IsDocRevised = 0
							 UPDATE Prospectus
							  SET SARDate = @DocDate,RevisedSARDate=null
							 WHERE ProsID in (SELECT ProsID from @tblProsID)
						  ELSE
							  UPDATE Prospectus
							   SET RevisedSARDate = @DocDate
							  WHERE ProsID in (SELECT ProsID from @tblProsID)       
						End
						Else
							IF @ProsDocTypeID = 'PVR'
							Begin
							  IF @IsDocRevised = 0
								 UPDATE Prospectus
								  SET PVRDate = @DocDate,RevisedPVRDate=null
								 WHERE ProsID in (SELECT ProsID from @tblProsID)
							  ELSE
								  UPDATE Prospectus
								   SET RevisedPVRDate = @DocDate
								  WHERE ProsID in (SELECT ProsID from @tblProsID)       
							End		
							Else
								IF @ProsDocTypeID = 'PH'
								Begin
								  IF @IsDocRevised = 0
									 UPDATE Prospectus
									  SET PHDate = @DocDate,RevisedPHDate=null
									 WHERE ProsID in (SELECT ProsID from @tblProsID)
								  ELSE
									  UPDATE Prospectus
									   SET RevisedPHDate = @DocDate
									  WHERE ProsID in (SELECT ProsID from @tblProsID)       				        
								End								
        
     End   
        
      
      --A URL has been assigned to the Fund so EdgarFunds IsURLAssigned will set to 1.
      UPDATE EdgarFunds
       SET isURLAssigned=1
      WHERE EdgarID = @EdgarID
      AND FundID in (SELECT PROSID from  @tblProsID) 
      
     Declare @ProsDocTypeSupplementID varchar(3)
     
     IF @DeleteSupplements = 1
     Begin
     
		 set @ProsDocTypeSupplementID = @ProsDocTypeID + 'S';
	     
		 DELETE ProsDocs
		   WHERE ProsId in (Select ProsId from @tblProsID)
		   and ProsDocTypeId = @ProsDocTypeSupplementID
       
     End
     
     --Update the ProsDocs table for the funds with the new URL 
     --if the document type for the fund already exists.
     
      UPDATE ProsDocs 
       SET ProsDocURL=@ProsDocURL,[PageCount]=@PageCount,
		   PageSizeHeight=@PageSizeHeight,PageSizeWidth=@PageSizeWidth
	  WHERE ProsId in (Select ProsId from @tblProsID)
	   AND ProsDocTypeId=@ProsDocTypeID
	  
	  --Remove the Updated ProsIds
	  DELETE @tblProsID
	  WHERE ProsID in
	  (select ProsID FROM ProsDocs WHERE ProsDocTypeId = @ProsDocTypeID) 
    
    --INSERT a record in prosdocs for the prosids and document type that does not exists in prosdocs table 
    INSERT INTO ProsDocs(ProsId,ProsDocTypeId,ProsDocAltURL,
							ProsDocUseAltURL,ProsDocOrder,ProsDocLevel,
							ProsDocURL,ProsDocUsePDF,ProsDocPDFInitial,[PageCount],
							PageSizeHeight,PageSizeWidth)
     SELECT ProsID,@ProsDocTypeID,'',
			0,1,1,
			@ProsDocURL,@ProsDocUsePDF,@ProsDocPDFInitial,@PageCount,
			@PageSizeHeight,@PageSizeWidth
     FROM @tblProsID
     
     
   IF @Path is not null and @FileName is not null and @CompanyFolderName is not null
   Begin
     Declare @ReplacePath nvarchar(max)
     Declare @IsIngestorDragDrop bit
     IF @Path like 'D:\HostedDocuments%'
       Begin
		set @ReplacePath = REPLACE(@Path,'D:\HostedDocuments\\','D:\HostedDocuments\')
		set @IsIngestorDragDrop = 1
	   End
	 Else
	   Begin
		set @ReplacePath = @Path
		set @IsIngestorDragDrop = 0
	   End
     
	  INSERT INTO hostedadmin.dbo.tblHostedUploadedPDF
			([Path],OriginalFileName,
			Company,isUploadedHostOne,
			isUploadedHostTwo,isUploadedHostThree,
			isSynchronized,[Date],IsDeletedFromTheList,IsIngestorDragAndDrop)
		VALUES(@ReplacePath,@FileName,
			  @CompanyFolderName,1,
			  0,0,
			  1,getdate(),0,@IsIngestorDragDrop)
	End      
	
   COMMIT TRANSACTION
   RETURN
   
	-- If an error occurs, rollback and exit
	E_General_Error:	
    
    ROLLBACK TRANSACTION
    RETURN
    
   
End