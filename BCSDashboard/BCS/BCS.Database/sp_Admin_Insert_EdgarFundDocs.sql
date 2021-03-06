USE [db1029]
GO


ALTER Procedure [dbo].[sp_Admin_Insert_EdgarFundDocs]
	@EdgarID int,
	@FundID int,
	@ClientID int=null,
	@CustomTypeID  int=null,
	@DocUrl nvarchar(500),
	@ProsDocID int,
	@DateInserted Datetime,
	@DocumentType nvarchar(255)='N/A',
	@InlineFlag bit=0
As
BEGIN
 IF @ClientID is null and @CustomTypeID is null
		BEGIN
			IF not exists(Select EdgarFundsDocId from EdgarFundsDocs 
					   WHERE EdgarID = @EdgarID 
					   AND FundID = @FundID 
					   AND ProsDocID = @ProsDocID and ClientID is null and CustomTypeID is null)
			BEGIN
				INSERT into EdgarFundsDocs(EdgarID,FundID,DocUrl,ProsDocID,DateInserted)
				Values(@EdgarID,@FundID,@DocUrl,@ProsDocID,@DateInserted)
			END
			
			IF @DocumentType like 'Summary%'  
				OR @DocumentType like 'Prospectus%'  
				OR (@DocumentType like '%Annual%' AND @InlineFlag =1)
				OR (@DocumentType like '%Quarterly%' AND @InlineFlag =1)
				and @DocUrl not like 'http://www.rightprospectus.com/documents/SECPDFs%'
			Begin
			  IF NOT (@DocumentType like  'Prospectus%' 
						AND EXISTS(SELECT  TOP 1 BCSDocUpdateId FROM BCSDocUpdate 
									WHERE ProsID = @FundID 
									AND DocumentType IN ('SP','SPS','RSP') AND IsRemoved=0 
							)
					 )					
			  Begin
				  IF not exists(SELECT BCSURLDownloadQueueID From BCSURLDownloadQueue
								WHERE EdgarID = @EdgarID
								AND ProsID=@FundID
								AND ProsDocID = @ProsDocID)
				   BEGIN			     
				     INSERT INTO BCSURLDownloadQueue(EdgarID,ProsID,ProsDocID,URLToDownload,ProcessedDate)
			         Values(@EdgarID,@FundID,@ProsDocID,@DocUrl,@DateInserted)
			       END				
			  End
			End
		END				
 ELSE
		BEGIN
			IF not exists(Select EdgarFundsDocId from EdgarFundsDocs 
					   WHERE EdgarID = @EdgarID 
					   AND FundID = @FundID 
					   AND ProsDocID = @ProsDocID and ClientID = @ClientID and CustomTypeID = @CustomTypeID)
			BEGIN
				INSERT into EdgarFundsDocs(EdgarID,FundID,ClientID,CustomTypeID,DocUrl,ProsDocID,DateInserted)
				Values(@EdgarID,@FundID,@ClientID,@CustomTypeID,@DocUrl,@ProsDocID,@DateInserted)
			END
		END				
End
