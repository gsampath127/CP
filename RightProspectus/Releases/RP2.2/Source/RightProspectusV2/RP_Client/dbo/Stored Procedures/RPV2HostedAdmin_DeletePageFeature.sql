

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeletePageFeature] 


@deletedBy int,
@PageId int,
@PageKey varchar(200),
@SiteId int
AS
BEGIN



      DELETE from  PageFeature
		     WHERE PageId = @PageId
		     AND
		     [Key] = @PageKey
		     AND 
		     SiteId = @SiteId


      UPDATE CUDHistory				 
			 SET	UserId = @deletedBy
			 WHERE	TableName = N'PageFeature'
					AND	[Key] = @PageId
				    AND [CUDType] = 'D' 


END