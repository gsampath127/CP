/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyLevelExternalId]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE TaxonomyLevelExternalId
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteTaxonomyLevelExternalId] 
				 @Level int,
				 @TaxonomyId int,
				 @ExternalId nvarchar(100),
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE TaxonomyLevelExternalId
		    WHERE [Level] = @Level
		    AND TaxonomyId = @TaxonomyId
		    AND ExternalId = @ExternalId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'TaxonomyLevelExternalId'
				AND	[Key] = @Level
				AND [SecondKey] = @TaxonomyId
				AND [ThirdKey] = @ExternalId
				AND [CUDType] = 'D' 

END