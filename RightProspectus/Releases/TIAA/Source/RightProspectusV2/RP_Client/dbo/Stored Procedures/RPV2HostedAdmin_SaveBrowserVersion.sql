CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveBrowserVersion
@SelectedId int,
@Name varchar(50),  
@Version int,  
@DownloadURL varchar(100),
@ModifiedBy int
  
AS  
BEGIN 
	IF EXISTS (SELECT Top 1 Id FROM BrowserVersion WHERE Id = @SelectedId)  
	BEGIN  
		UPDATE BrowserVersion   
		SET Name = @Name,  
		    Version = @Version,
			DownloadUrl = @DownloadURL,
			UtcModifiedDate = GETUTCDATE(), 
			ModifiedBy = @ModifiedBy  

		WHERE Id = @SelectedId
	END	
	ELSE  
	BEGIN  
		INSERT INTO BrowserVersion(  
			Name,  
			Version,  
			DownloadUrl,
			ModifiedBy,
			UtcModifiedDate
			)  
		VALUES
			(@Name,        
			@Version,  
			@DownloadURL,
			@ModifiedBy ,
            GETUTCDATE()
			)  
	END
END



