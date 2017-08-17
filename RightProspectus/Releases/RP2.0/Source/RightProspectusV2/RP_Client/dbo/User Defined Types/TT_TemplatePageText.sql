CREATE TYPE [dbo].[TT_TemplatePageText] AS TABLE
(
	PageId int,
	ResourceKey varchar(200),
	DefaultText nvarchar(max)
)
