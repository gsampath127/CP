CREATE PROCEDURE [dbo].[RPV2HostedAdmin_VerticalImportGetAllApproveProofing]
@SiteId Int
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION;

	DECLARE @SiteId_X Int
	SET @SiteId_X = @SiteId -- Added to avoid parameter sniffing in sql server. Do Not Remove.

    exec [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_DocumentTypeAssociation] @SiteId_X    
    exec [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_TaxonomyAssociation] @SiteId_X
	exec [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_MarketLevelDocumentTypeAssociation] @SiteId_X
	exec [dbo].[RPV2HostedAdmin_VerticalImport_ApproveProofing_Footnote] @SiteId_X

   COMMIT TRANSACTION;
   SELECT 'Success' AS [Status]
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
    SELECT 'Fail' as [Status]   
END CATCH
END