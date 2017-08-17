CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllCUDHistory]
AS
BEGIN
	SELECT 
	TableName,
	UserId
	FROM [CUDHistory] ORDER BY TableName
END
