

Alter Procedure [dbo].[BCS_GetSECDocumentsForProsIDandSummaryProspectus]
@ProsId int
as
Begin
 
		SET NOCOUNT ON
		
		Declare @DocumentDate datetime

		Declare @DocumentTypes Table 
		(
		  DocumentType varchar(250)
		)

		 
		INSERT INTO @DocumentTypes(DocumentType) values('Summary Prospectus - New')
		INSERT INTO @DocumentTypes(DocumentType) values('Summary Prospectus - Revised')
		
		 SELECT @DocumentDate = CASE WHEN isnull(RevisedSPDate,'1/1/1980') > SPDate THEN RevisedSPDate ELSE SPDate END 
			   FROM Prospectus WHERE ProsID = @ProsId   
			  
			  
		IF EXISTS
		(
			SELECT TOP 1 Acc#
			FROM Edgar 
			INNER JOIN EdgarFunds on EdgarFunds.EdgarID = Edgar.EdgarID 
			INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID			
			WHERE Edgar.DocumentType in (SELECT DocumentType FROM @DocumentTypes)
				AND DocumentDate >= @DocumentDate
				AND EdgarFunds.FundID = @ProsId 
				AND (EdgarFunds.Processed IN ('1','Y'))
			ORDER By DateFiled   
		)
		Begin
			  INSERT INTO @DocumentTypes(DocumentType) values('Summary Prospectus - Supplement') 			  
			  
			   

		
				SELECT Distinct Acc#,Edgar.EdgarID,EffectiveDate,DocumentType,FormType,DateFiled,DocumentDate 
					FROM Edgar 
						INNER JOIN EdgarFunds on EdgarFunds.EdgarID = Edgar.EdgarID 
						INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID			
						WHERE Edgar.DocumentType in (SELECT DocumentType FROM @DocumentTypes)
						   AND DocumentDate >= @DocumentDate
						   AND EdgarFunds.FundID = @ProsId 
						   AND (EdgarFunds.Processed IN ('1','Y'))
						ORDER By DateFiled  	
		End
		  Else
			Begin
				SELECT Distinct Acc#,Edgar.EdgarID,EffectiveDate,DocumentType,FormType,DateFiled,DocumentDate 
						FROM Edgar 
							INNER JOIN EdgarFunds on EdgarFunds.EdgarID = Edgar.EdgarID 
							INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID			
							WHERE 1 <> 1 
			End
			   
	  
	  
End		
