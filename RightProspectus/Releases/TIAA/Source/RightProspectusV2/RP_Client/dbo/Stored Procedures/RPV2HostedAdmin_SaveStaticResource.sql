/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveStaticResource]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the StaticResource
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveStaticResource]	
	@StaticResourceId int,
	@FileName nvarchar(260),
	@Size int,
	@MimeType varchar(127),
	@Data varbinary(max),
	@modifiedBy int
AS
BEGIN

   IF @StaticResourceId = 0 
	   BEGIN
				INSERT INTO StaticResource
					(					
					[FileName],
					Size,
					MimeType,
					Data,
					utcModifiedDate,
					ModifiedBy) 
				VALUES (
					@FileName,
					@Size,
					@MimeType,
					@Data,
					GETUTCDATE(),
					@modifiedBy)
		END
	ELSE
	    BEGIN
			UPDATE StaticResource
			SET [FileName] = @FileName,
				Size = @Size,
				MimeType = @MimeType,
				Data = @Data,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @modifiedBy
			WHERE StaticResourceId = @StaticResourceId
	    END	
END