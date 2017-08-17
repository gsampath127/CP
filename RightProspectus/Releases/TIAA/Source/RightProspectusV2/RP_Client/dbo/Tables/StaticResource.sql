
CREATE TABLE [dbo].[StaticResource](
	[StaticResourceId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](260) NOT NULL,
	[Size] [int] NOT NULL,
	[MimeType] [varchar](127) NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[UtcModifiedDate] [datetime] CONSTRAINT [DF_StaticResource_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_StaticResource] PRIMARY KEY CLUSTERED 
(
	[StaticResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]