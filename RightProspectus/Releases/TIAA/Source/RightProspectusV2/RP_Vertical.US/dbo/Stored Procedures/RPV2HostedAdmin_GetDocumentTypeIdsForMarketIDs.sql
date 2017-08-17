--Created By : Noel Dsouza
--Created Date : 10/19/2015
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetDocumentTypeIdsForMarketIDs]
	@DocumentTypeMarketIds TT_DocumentTypeMarketId READONLY	
AS
BEGIN
  SELECT DocumentTypeID,DocumentTypeMarketIds.MarketID
   FROM @DocumentTypeMarketIds DocumentTypeMarketIds
   INNER JOIN DocumentType ON DocumentTypeMarketIds.marketId = DocumentType.MarketID
END