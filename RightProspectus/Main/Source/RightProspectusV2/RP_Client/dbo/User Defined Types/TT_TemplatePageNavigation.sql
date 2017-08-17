CREATE TYPE [dbo].[TT_TemplatePageNavigation] AS TABLE
(
	PageId int,
	NavigationKey varchar(200),
	DefaultNavigationXml xml
)
