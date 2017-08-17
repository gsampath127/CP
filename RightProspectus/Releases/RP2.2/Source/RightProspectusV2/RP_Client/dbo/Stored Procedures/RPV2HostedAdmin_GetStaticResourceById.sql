create procedure dbo.RPV2HostedAdmin_GetStaticResourceById

@StaticResourceId int 
as
BEGIN
   Select StaticResourceId,
           [FileName],
			Size,
			MimeType,
			Data,
			UtcModifiedDate,
			ModifiedBy
     FROM
	      
		  StaticResource
     WHERE StaticResourceId=@StaticResourceId

           
END