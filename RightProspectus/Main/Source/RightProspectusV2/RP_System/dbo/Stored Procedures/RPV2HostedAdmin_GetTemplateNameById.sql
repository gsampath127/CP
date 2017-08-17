/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetTemplateNameById]
	Added By: Arshdeep
	Date: 11/09/2015
	Reason : To get the the TemplateID
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetTemplateNameById]
				 @templateId int               				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetTemplateNameById]
 --exec [RPV2HostedAdmin_GetTemplateNameById] 1
AS BEGIN 

			SELECT 
				T.TemplateId,
				T.Name,
				T.[Description]	
				From Template T
				WHERE T.TemplateId=@templateId
			
	END
