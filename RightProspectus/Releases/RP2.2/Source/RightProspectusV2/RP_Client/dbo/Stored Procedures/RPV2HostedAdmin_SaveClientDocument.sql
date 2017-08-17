

/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveClientDocument]
	Added By: 
	Date: 
	Reason : To add and update the ClientDocument
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocument]	
	@ClientDocumentId int,
	@Name NVARCHAR(100),
	@Description NVARCHAR(400),
	@IsPrivate BIT,
	@ContentUri NVARCHAR(2083),
	@ClientDocumentTypeId INT,
	@MimeType NVARCHAR(127),
	@FileName NVARCHAR(260),
	@FileData VARBINARY(MAX),
	@ModifiedBy INT
AS
BEGIN
	
	IF(@ClientDocumentId=0) 
		BEGIN
			INSERT INTO [dbo].[ClientDocument]
							(
								  [ClientDocumentTypeId]
								  ,[FileName]
								  ,[MimeType]
								  ,[IsPrivate]
								  ,[ContentUri]
								  ,[Name]
								  ,[Description]
								  ,[UtcModifiedDate]
								  ,[ModifiedBy]
							) 
					VALUES (
									@ClientDocumentTypeId
									,@FileName
									,@MimeType
									,@IsPrivate
									,@ContentUri
									,@Name
									,@Description
									,GETUTCDATE()
									,@ModifiedBy
							)


					 INSERT INTO [dbo].[ClientDocumentData] 
							  (
									[ClientDocumentId]
									,[Data]
									,[UtcModifiedDate]
									,[ModifiedBy]
							   )
							   VALUES
							  (
									SCOPE_IDENTITY()
									,@FileData
									,GETUTCDATE()
									,@ModifiedBy
							  )
		END
	ELSE
		BEGIN
			UPDATE 
					[dbo].[ClientDocument]
			
			SET 
					[ClientDocumentTypeId]	 = @ClientDocumentTypeId
					,[FileName]				 = @FileName
					,[MimeType]				 = @MimeType
					,[IsPrivate]			 = @IsPrivate
					,[ContentUri]			 = @ContentUri
					,[Name]					 = @Name
					,[Description]			 = @Description
					,[UtcModifiedDate]		 = GETUTCDATE()
					,[ModifiedBy]			 = @ModifiedBy
			WHERE 
					ClientDocumentId = @ClientDocumentId

		
			UPDATE 
					[dbo].[ClientDocumentData] 
			
			SET 
					[Data]					 = @FileData
					,[UtcModifiedDate]		 = GETUTCDATE()
					,[ModifiedBy]   		 = @ModifiedBy
			WHERE 
					ClientDocumentId = @ClientDocumentId
			
		END

END