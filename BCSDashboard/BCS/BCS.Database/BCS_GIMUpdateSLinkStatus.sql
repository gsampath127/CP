
Alter Procedure [dbo].[BCS_GIMUpdateSLinkStatus]
@ZipFileName nvarchar(36),
@CompletedDate datetime,
@PassedorFailed bit=0
as
Begin
  IF @PassedorFailed = 1
	  Begin
	  
		    
			UPDATE BCSDocUpdateGIMSlink
			SET IsAPF = 1, APFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF = 0 AND IsOPF = 0
	    
			UPDATE BCSDocUpdateSupplementsSlink
			SET IsAPF = 1, APFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF = 0 AND IsOPF = 0
			 
			UPDATE BCSDocUpdateARSARSlink
			SET IsAPF = 1, APFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF = 0 AND IsOPF = 0				  
				  
	    	     
	  End
   Else
     IF @PassedorFailed = 0
		Begin
			
			UPDATE BCSDocUpdateGIMSlink
			SET IsOPF = 1, OPFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF=0 AND IsOPF=0
		     
			UPDATE BCSDocUpdateSupplementsSlink
			SET IsOPF = 1, OPFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF=0 AND IsOPF=0
				  
			UPDATE BCSDocUpdateARSARSlink
			SET IsOPF = 1, OPFReceivedDate = @CompletedDate
			WHERE ZipFileName like '%' + @ZipFileName
			AND IsAPF=0 AND IsOPF=0
								  
		End  
End
GO