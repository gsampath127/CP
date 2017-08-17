/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteFootnote]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE UrlRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteFootnote] 
				 @FootnoteId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE Footnote
		   WHERE FootnoteId = @FootnoteId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'Footnote'
				AND	[Key] = @FootnoteId
				AND [CUDType] = 'D' 

END