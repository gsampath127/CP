CREATE TYPE [dbo].[TT_TaxonomyAssociationFootnotes] AS TABLE
(
	taxonomyassociationsystemid INT,
	[level] int,
	taxonomymarketId nvarchar(50),
	languageCulture varchar(50),
	[text] nvarchar(max),
	[delete] bit
)
