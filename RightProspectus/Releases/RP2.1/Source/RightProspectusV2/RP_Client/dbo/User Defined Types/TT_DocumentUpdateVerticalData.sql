CREATE TYPE [dbo].[TT_DocumentUpdateVerticalData] AS TABLE
(
    MarketId NVARCHAR(100),
	DocumentTypeID INT,
	TaxonomyName NVARCHAR(500),
	DocumentDate DATETIME,
	DocumentUpdatedDate DATETIME
)
