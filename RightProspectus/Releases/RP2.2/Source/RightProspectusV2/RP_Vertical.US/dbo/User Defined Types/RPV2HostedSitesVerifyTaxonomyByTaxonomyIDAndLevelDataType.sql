CREATE TYPE [dbo].[RPV2HostedSitesVerifyTaxonomyByTaxonomyIDAndLevelDataType] AS TABLE
(
	[TaxonomyId]           INT NULL,
	[Level]					 INT NULL,
    [IsNameOverrideProvided] BIT NULL
)
