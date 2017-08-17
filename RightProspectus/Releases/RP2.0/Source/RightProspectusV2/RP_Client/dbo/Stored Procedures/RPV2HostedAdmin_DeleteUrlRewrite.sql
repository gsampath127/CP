/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteUrlRewrite]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE UrlRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteUrlRewrite] 
				 @UrlRewriteId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE UrlRewrite
		   WHERE UrlRewriteId = @UrlRewriteId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'UrlRewrite'
				AND	[Key] = @UrlRewriteId
				AND [CUDType] = 'D' 

END