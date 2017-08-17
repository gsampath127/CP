/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetDefaultPageNameById]
	Added By: Arshdeep
	Date: 11/09/2015
	Reason : To get the the PageID
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetDefaultPageNameById]
				 @pageId int               				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetDefaultPageNameById]
 --exec [RPV2HostedAdmin_GetDefaultPageNameById] 1
AS BEGIN 

			SELECT 
				P.PageId,
				P.Name,
				P.[Description]	
				From Page P
				WHERE P.PageId = @pageId
			
	END
