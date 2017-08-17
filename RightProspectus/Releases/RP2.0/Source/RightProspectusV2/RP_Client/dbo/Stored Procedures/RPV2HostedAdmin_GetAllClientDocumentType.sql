-- =============================================
-- Author:		Arshdeep
-- Create date: 6th-Oct-2015
-- RPV2HostedAdmin_RPV2HostedAdmin_GetAllClientDocumentType
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentType]
AS
BEGIN
  SELECT DISTINCT 
    ClientDocumentTypeId,
	Name,
	[Description],
	UtcModifiedDate as UtcLastModified,
	ModifiedBy as ModifiedBy
	 FROM
     ClientDocumentType
     
END