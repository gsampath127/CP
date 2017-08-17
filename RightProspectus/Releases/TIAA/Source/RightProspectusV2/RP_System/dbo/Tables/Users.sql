
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[SecurityStamp] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockOutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[UtcModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [EmailConfirmed]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [LockoutEnabled]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [AccessFailedCount]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT (getutcdate()) FOR [UtcModifiedDate]
GO


GO


