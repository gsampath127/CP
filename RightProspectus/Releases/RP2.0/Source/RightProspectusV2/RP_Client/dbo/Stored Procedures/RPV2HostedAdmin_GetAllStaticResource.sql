-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
-- RPV2HostedAdmin_GetAllStaticResource
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllStaticResource]
AS
BEGIN
	SELECT 
		   StaticResourceId,
		   [FileName],
		   Size,
		   MimeType,
		   Data,		   
		   [UtcModifiedDate] as UtcLastModified,
		   [ModifiedBy]
	  FROM [dbo].[StaticResource]
		  
END 