

/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveClientDocumentType]
	Added By: Arshdeep
	Date: 10/07/2015
	Reason : To add and update the ClientDocumentType
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocumentType]
	@clientdocumenttypeid int,
	@name NVARCHAR(100),
	@description NVARCHAR(400),
	@hosteddocumentsdisplaycount int,
	@ftpName NVARCHAR(400),
	@ftpusername NVARCHAR(400),
	@ftppassword NVARCHAR(400),
	@issftp bit,
	@ModifiedBy int
AS
BEGIN

	IF(@clientdocumenttypeid = 0)
	BEGIN
		INSERT INTO ClientDocumentType(name,
						[description],
						utcModifiedDate,
						HostedDocumentsDisplayCount,
						FTPName,
						FTPUsername,
						FTPPassword,
						IsSFTP,
						ModifiedBy)
				VALUES (@name,
						@description,
						GETUTCDATE(),
						@hosteddocumentsdisplaycount,
						@ftpName,
						@ftpusername,
						@ftppassword,
						@issftp,
						@modifiedBy)
	END
	ELSE
	BEGIN
		UPDATE ClientDocumentType SET name=@name,
					[description]=@description,
					utcModifiedDate=GETUTCDATE(),
					HostedDocumentsDisplayCount=@hosteddocumentsdisplaycount,
					ModifiedBy=@modifiedBy
		WHERE ClientDocumentTypeId = @clientdocumenttypeid
	END
END
GO