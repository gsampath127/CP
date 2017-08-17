
CREATE TYPE [dbo].[TT_DocumentTypeAssociation] AS TABLE
(
	headerText nvarchar(100),
	linkText nvarchar(100),
	[Order] int,
	MarketId nvarchar(50),
	descriptionOverride nvarchar(400),
	cssClass varchar(50),
	[delete] bit,
	siteid int,
	taxonomymarketId nvarchar(50),
	taxonomylevel int,
	documenttypeid int
)
