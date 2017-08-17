USE [db1029]
GO



Create Procedure [dbo].[BCS_GetSECDocumentsForProsIDandProspectus]
@ProsId int
as
Begin
 
		SET NOCOUNT ON
		
		Declare @DocumentDate datetime

		Declare @DocumentTypes Table 
		(
		  DocumentType varchar(250)
		)

		 
		INSERT INTO @DocumentTypes(DocumentType) values('Prospectus - New')
		INSERT INTO @DocumentTypes(DocumentType) values('Prospectus - Revised')
		
		 SELECT @DocumentDate = CASE 
									WHEN isnull(RevisedProsDate,'1/1/1980') > isnull(ProsDate,'1/1/1980') 
									THEN RevisedProsDate 
									ELSE ProsDate 
								END 
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
				AND (EdgarFunds.Processed IN ('1','Y')) AND Edgar.IsReplaced=0
			ORDER By DateFiled   
		)
		Begin
			    INSERT INTO @DocumentTypes(DocumentType) values('Prospectus - Supplement') 		  
		
				SELECT Distinct Acc#,Edgar.EdgarID,EffectiveDate,DocumentType,FormType,DateFiled,DocumentDate 
					FROM Edgar 
						INNER JOIN EdgarFunds on EdgarFunds.EdgarID = Edgar.EdgarID 
						INNER JOIN ProsTicker on EdgarFunds.TickerID = ProsTicker.TickerID			
						WHERE Edgar.DocumentType in (SELECT DocumentType FROM @DocumentTypes)
						   AND DocumentDate >= @DocumentDate
						   AND EdgarFunds.FundID = @ProsId 
						   AND (EdgarFunds.Processed IN ('1','Y')) AND Edgar.IsReplaced=0
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
