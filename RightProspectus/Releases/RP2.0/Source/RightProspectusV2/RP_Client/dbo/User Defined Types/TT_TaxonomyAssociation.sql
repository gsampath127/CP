CREATE TYPE [dbo].[TT_TaxonomyAssociation] AS TABLE
(
	[Level] int,
	importId nvarchar(60),
	systemId int,
	nameOverride nvarchar(200),
	marketId nvarchar(50),
	descriptionOverride nvarchar(400),
	cssClass varchar(50),
	[delete] bit,
	siteid int,
	taxonomyId int
)
