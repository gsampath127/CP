

/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveClientDocumentType]
	Added By: Arshdeep
	Date: 10/07/2015
	Reason : To add and update the ClientDocumentType
*/
Create PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocumentType]	
	@clientdocumenttypeid int,
	@name NVARCHAR(100),
	@description NVARCHAR(400),
	@ModifiedBy int
AS
BEGIN
	
	IF(@clientdocumenttypeid=0) 
		BEGIN
			INSERT INTO ClientDocumentType(name,
							[description],
							utcModifiedDate,
							ModifiedBy) 
					VALUES (@name,
							@description,
							GETUTCDATE(),
							@modifiedBy)
		END
	ELSE
		BEGIN
			UPDATE ClientDocumentType SET name=@name,
						[description]=@description,
						utcModifiedDate=GETUTCDATE(),
						ModifiedBy=@modifiedBy
			WHERE ClientDocumentTypeId = @clientdocumenttypeid
		END

END