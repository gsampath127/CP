/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteStaticResource]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE DeleteStaticResource
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteStaticResource] 
				 @StaticResourceId int,
				 @deletedBy int
				                 				 			
AS 
BEGIN 
		  DELETE StaticResource
		    WHERE StaticResourceId = @StaticResourceId
   
		  UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'StaticResource'
				AND	[Key] = @StaticResourceId
				AND [CUDType] = 'D' 

END