CREATE TABLE [dbo].[BrowserVersion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	[Version] [INT] NOT NULL,
	[DownloadUrl] [NVARCHAR](300) NOT NULL,
	[ModifiedBy] [int] NOT NULL DEFAULT ((0)),
	[UtcModifiedDate] [date] NOT NULL DEFAULT (getdate())
)