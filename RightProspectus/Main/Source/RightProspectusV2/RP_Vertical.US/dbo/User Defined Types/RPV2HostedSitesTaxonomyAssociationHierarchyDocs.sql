
CREATE TYPE [dbo].[RPV2HostedSitesTaxonomyAssociationHierarchyDocs] AS TABLE(
	[TaxonomyID] [int] NOT NULL,
	[IsNameOverrideProvided] [bit] NOT NULL,
	[DocumentTypeID] [int] NOT NULL,
	[IsDocumentTypeNameOverrideProvided] [bit] NOT NULL,
	[IsParent] [bit] NOT NULL
)
GO
