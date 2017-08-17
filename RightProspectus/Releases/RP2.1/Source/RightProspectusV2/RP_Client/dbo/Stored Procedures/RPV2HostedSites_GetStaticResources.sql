CREATE PROCEDURE [dbo].[RPV2HostedSites_GetStaticResources]
as
BEGIN
	SELECT [FileName], Size, MimeType, Data, UtcModifiedDate FROM StaticResource
END