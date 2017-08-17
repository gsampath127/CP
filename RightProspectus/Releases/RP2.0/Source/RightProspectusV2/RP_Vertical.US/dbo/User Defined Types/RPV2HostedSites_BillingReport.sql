CREATE TYPE [dbo].[RPV2HostedSites_BillingReport] AS TABLE (
    [MarketID]             VARCHAR(50) NOT NULL,
    [IsNameOverrideProvided] BIT NOT NULL);
