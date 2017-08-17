
PRINT N'Creating [dbo].[TT_ClientUsers]...';


GO
CREATE TYPE [dbo].[TT_ClientUsers] AS TABLE (
    [ClientId] INT NOT NULL,
    [UserId]   INT NOT NULL);


GO
PRINT N'Creating [dbo].[TT_CUDHistory]...';


GO
CREATE TYPE [dbo].[TT_CUDHistory] AS TABLE (
    [Id]        INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([Id] ASC),
    [TableName] NVARCHAR (128) NOT NULL,
    [Key]       INT            NOT NULL,
    [SecondKey] NVARCHAR (200) NULL,
    [ThirdKey]  NVARCHAR (200) NULL,
    [CUDType]   CHAR (1)       NOT NULL,
    [UserId]    INT            NULL);


GO
PRINT N'Creating [dbo].[TT_CUDHistoryData]...';


GO
CREATE TYPE [dbo].[TT_CUDHistoryData] AS TABLE (
    [ParentId]   INT            NOT NULL,
    [ColumnName] NVARCHAR (128) NOT NULL,
    [SqlDbType]  INT            NOT NULL,
    [OldValue]   NVARCHAR (MAX) NULL,
    [NewValue]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ParentId] ASC, [ColumnName] ASC));


GO
PRINT N'Creating [dbo].[TT_UserRoles]...';


GO
CREATE TYPE [dbo].[TT_UserRoles] AS TABLE (
    [UserId] INT NOT NULL,
    [RoleID] INT NOT NULL);


GO
PRINT N'Creating [dbo].[ClientDns]...';


GO
CREATE TABLE [dbo].[ClientDns] (
    [ClientDnsId]     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClientId]        INT           NOT NULL,
    [Dns]             VARCHAR (255) NOT NULL,
    [UtcModifiedDate] DATETIME      NOT NULL,
    [ModifiedBy]      INT           NOT NULL,
    CONSTRAINT [PK_ClientDns] PRIMARY KEY CLUSTERED ([ClientDnsId] ASC)
);


GO
PRINT N'Creating [dbo].[Clients]...';


GO
CREATE TABLE [dbo].[Clients] (
    [ClientId]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClientName]           NVARCHAR (200) NOT NULL,
    [ConnectionStringName] VARCHAR (200)  NOT NULL,
    [DatabaseName]         VARCHAR (200)  NOT NULL,
    [VerticalMarketId]     INT            NOT NULL,
    [ClientDescription]    NVARCHAR (400) NULL,
    [UtcModifiedDate]      DATETIME       NOT NULL,
    [ModifiedBy]           INT            NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([ClientId] ASC) ON [PRIMARY],
    CONSTRAINT [ClientName_Unique_Constraint] UNIQUE NONCLUSTERED ([ClientName] ASC)
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ClientSettings]...';


GO
CREATE TABLE [dbo].[ClientSettings] (
    [ClientId]        INT      NOT NULL,
    [DefaultSiteId]   INT      NOT NULL,
    [UtcModifiedDate] DATETIME NOT NULL,
    [ModifiedBy]      INT      NULL
);


GO
PRINT N'Creating [dbo].[ClientUsers]...';


GO
CREATE TABLE [dbo].[ClientUsers] (
    [ClientId]        INT      NOT NULL,
    [UserId]          INT      NOT NULL,
    [UtcModifiedDate] DATETIME NOT NULL,
    [ModifiedBy]      INT      NOT NULL,
    CONSTRAINT [PK_ClientUsers] PRIMARY KEY CLUSTERED ([ClientId] ASC, [UserId] ASC)
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[CUDHistory]...';


GO
CREATE TABLE [dbo].[CUDHistory] (
    [CUDHistoryId] INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TableName]    NVARCHAR (128)   NOT NULL,
    [Key]          INT              NOT NULL,
    [SecondKey]    NVARCHAR (200)   NULL,
    [ThirdKey]     NVARCHAR (200)   NULL,
    [CUDType]      CHAR (1)         NULL,
    [UtcCUDDate]   DATETIME         NOT NULL,
    [BatchId]      UNIQUEIDENTIFIER NOT NULL,
    [UserId]       INT              NULL,
    CONSTRAINT [PK_CUDHistory] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC)
);


GO
PRINT N'Creating [dbo].[CUDHistoryData]...';


GO
CREATE TABLE [dbo].[CUDHistoryData] (
    [CUDHistoryId] INT            NOT NULL,
    [ColumnName]   NVARCHAR (128) NOT NULL,
    [SqlDbType]    INT            NOT NULL,
    [OldValue]     NVARCHAR (MAX) NULL,
    [NewValue]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CUDHistoryData] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC, [ColumnName] ASC)
);


GO
PRINT N'Creating [dbo].[ErrorLog]...';


GO
CREATE TABLE [dbo].[ErrorLog] (
    [ErrorLogId]       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ErrorCode]        INT             NOT NULL,
    [ErrorUtcDate]     DATETIME        NOT NULL,
    [Priority]         INT             NULL,
    [Severity]         NVARCHAR (32)   NULL,
    [Title]            NVARCHAR (256)  NULL,
    [MachineName]      NVARCHAR (32)   NULL,
    [AppDomainName]    NVARCHAR (512)  NULL,
    [ProcessID]        NVARCHAR (256)  NULL,
    [ProcessName]      NVARCHAR (512)  NULL,
    [ThreadName]       NVARCHAR (512)  NULL,
    [Win32ThreadId]    NVARCHAR (128)  NULL,
    [EventId]          INT             NULL,
    [ClientId]         INT             NULL,
    [SiteActivityId]   INT             NULL,
    [Message]          NVARCHAR (1500) NULL,
    [FormattedMessage] NVARCHAR (MAX)  NULL,
    [URL]              NVARCHAR (500)  NULL,
    [AbsoluteURL]      NVARCHAR (500)  NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([ErrorLogId] ASC)
);


GO
PRINT N'Creating [dbo].[FrequencyType]...';


GO
CREATE TABLE [dbo].[FrequencyType] (
    [EnumKey]  INT            NOT NULL,
    [EnumText] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_FrequencyType] PRIMARY KEY CLUSTERED ([EnumKey] ASC, [EnumText] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Page]...';


GO
CREATE TABLE [dbo].[Page] (
    [PageId]      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([PageId] ASC)
);


GO
PRINT N'Creating [dbo].[Reports]...';


GO
CREATE TABLE [dbo].[Reports] (
    [ReportId]          INT            IDENTITY (1, 1) NOT NULL,
    [ReportName]        NVARCHAR (200) NOT NULL,
    [ReportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([ReportId] ASC)
);


GO
PRINT N'Creating [dbo].[ReportScheduleRecipients]...';


GO
CREATE TABLE [dbo].[ReportScheduleRecipients] (
    [ReportScheduleId] INT            NOT NULL,
    [Email]            NVARCHAR (256) NULL,
    [FTPServerIP]      NVARCHAR (100) NULL,
    [FTPFilePath]      NVARCHAR (256) NULL,
    [FTPName]          NVARCHAR (50)  NULL,
    [FTPUsername]      NVARCHAR (50)  NULL,
    [FTPPassword]      NVARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME       NOT NULL,
    [ModifiedBy]       INT            NULL,
    CONSTRAINT [PK_ReportScheduleRecipients_1] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC),
    CONSTRAINT [IX_ReportScheduleRecipients] UNIQUE NONCLUSTERED ([ReportScheduleId] ASC)
);


GO
PRINT N'Creating [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [RoleId]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [UtcModifiedDate] DATETIME       NULL,
    [ModifiedBy]      INT            NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[Template]...';


GO
CREATE TABLE [dbo].[Template] (
    [TemplateId]  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([TemplateId] ASC)
);


GO
PRINT N'Creating [dbo].[TemplateFeature]...';


GO
CREATE TABLE [dbo].[TemplateFeature] (
    [TemplateId]  INT            NOT NULL,
    [Key]         NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateFeature] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [Key] ASC)
);


GO
PRINT N'Creating [dbo].[TemplateNavigation]...';


GO
CREATE TABLE [dbo].[TemplateNavigation] (
    [TemplateId]           INT            NOT NULL,
    [NavigationKey]        VARCHAR (200)  NOT NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [XslTransform]         XML            NULL,
    [DefaultNavigationXml] XML            NULL,
    [Description]          NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateNavigation] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [NavigationKey] ASC)
);


GO
PRINT N'Creating [dbo].[TemplatePage]...';


GO
CREATE TABLE [dbo].[TemplatePage] (
    [TemplateId] INT NOT NULL,
    [PageId]     INT NOT NULL,
    CONSTRAINT [PK_TemplatePage] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC)
);


GO
PRINT N'Creating [dbo].[TemplatePageFeature]...';


GO
CREATE TABLE [dbo].[TemplatePageFeature] (
    [TemplateId]  INT            NOT NULL,
    [PageId]      INT            NOT NULL,
    [Key]         NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageFeature] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [Key] ASC)
);


GO
PRINT N'Creating [dbo].[TemplatePageNavigation]...';


GO
CREATE TABLE [dbo].[TemplatePageNavigation] (
    [TemplateId]           INT            NOT NULL,
    [PageId]               INT            NOT NULL,
    [NavigationKey]        VARCHAR (200)  NOT NULL,
    [Name]                 NVARCHAR (200) NOT NULL,
    [XslTransform]         XML            NULL,
    [DefaultNavigationXml] XML            NULL,
    [Description]          NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageNavigation] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [NavigationKey] ASC)
);


GO
PRINT N'Creating [dbo].[TemplatePageText]...';


GO
CREATE TABLE [dbo].[TemplatePageText] (
    [TemplateId]  INT            NOT NULL,
    [PageId]      INT            NOT NULL,
    [ResourceKey] VARCHAR (200)  NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [IsHtml]      BIT            NULL,
    [DefaultText] NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplatePageText] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [PageId] ASC, [ResourceKey] ASC)
);


GO
PRINT N'Creating [dbo].[TemplateText]...';


GO
CREATE TABLE [dbo].[TemplateText] (
    [TemplateId]  INT            NOT NULL,
    [ResourceKey] VARCHAR (200)  NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [IsHtml]      BIT            NULL,
    [DefaultText] NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (400) NULL,
    CONSTRAINT [PK_TemplateText] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [ResourceKey] ASC)
);


GO
PRINT N'Creating [dbo].[UserRoles]...';


GO
CREATE TABLE [dbo].[UserRoles] (
    [UserId]          INT      NOT NULL,
    [RoleId]          INT      NOT NULL,
    [UtcModifiedDate] DATETIME NULL,
    [ModifiedBy]      INT      NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [UserId]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Email]                NVARCHAR (256) NOT NULL,
    [EmailConfirmed]       BIT            NULL,
    [PasswordHash]         NVARCHAR (MAX) NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX) NOT NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NULL,
    [TwoFactorEnabled]     BIT            NULL,
    [LockOutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NULL,
    [AccessFailedCount]    INT            NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [FirstName]            NVARCHAR (100) NULL,
    [LastName]             NVARCHAR (100) NULL,
    [UtcModifiedDate]      DATETIME       NULL,
    [ModifiedBy]           INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC) ON [PRIMARY],
    UNIQUE NONCLUSTERED ([UserName] ASC) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


GO
PRINT N'Creating [dbo].[VerticalMarkets]...';


GO
CREATE TABLE [dbo].[VerticalMarkets] (
    [VerticalMarketId]     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MarketName]           NVARCHAR (200) NOT NULL,
    [ConnectionStringName] VARCHAR (200)  NOT NULL,
    [DatabaseName]         VARCHAR (200)  NOT NULL,
    [MarketDescription]    NVARCHAR (400) NULL,
    [UtcModifiedDate]      DATETIME       NOT NULL,
    [ModifiedBy]           INT            NOT NULL,
    CONSTRAINT [PK_VerticalMarkets] PRIMARY KEY CLUSTERED ([VerticalMarketId] ASC)
);


GO
PRINT N'Creating Default Constraint on [dbo].[ClientDns]....';


GO
ALTER TABLE [dbo].[ClientDns]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[Clients]....';


GO
ALTER TABLE [dbo].[Clients]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ClientSettings_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientSettings]
    ADD CONSTRAINT [DF_ClientSettings_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[ClientUsers]....';


GO
ALTER TABLE [dbo].[ClientUsers]
    ADD DEFAULT GETUTCDATE() FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_CUDHistory_UtcCUDDate...';


GO
ALTER TABLE [dbo].[CUDHistory]
    ADD CONSTRAINT [DF_CUDHistory_UtcCUDDate] DEFAULT (GETUTCDATE()) FOR [UtcCUDDate];


GO
PRINT N'Creating DF_ReportScheduleRecipients_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ReportScheduleRecipients]
    ADD CONSTRAINT [DF_ReportScheduleRecipients_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[Roles]....';


GO
ALTER TABLE [dbo].[Roles]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[UserRoles]....';


GO
ALTER TABLE [dbo].[UserRoles]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT ((0)) FOR [EmailConfirmed];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT ((0)) FOR [PhoneNumberConfirmed];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT ((0)) FOR [TwoFactorEnabled];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT ((0)) FOR [LockoutEnabled];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT ((0)) FOR [AccessFailedCount];


GO
PRINT N'Creating Default Constraint on [dbo].[Users]....';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[VerticalMarkets]....';


GO
ALTER TABLE [dbo].[VerticalMarkets]
    ADD DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating fk_Clients...';


GO
ALTER TABLE [dbo].[ClientDns] WITH NOCHECK
    ADD CONSTRAINT [fk_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]);


GO
PRINT N'Creating fk1_Clients...';


GO
ALTER TABLE [dbo].[Clients] WITH NOCHECK
    ADD CONSTRAINT [fk1_Clients] FOREIGN KEY ([VerticalMarketId]) REFERENCES [dbo].[VerticalMarkets] ([VerticalMarketId]);


GO
PRINT N'Creating fk1_ClientUsers...';


GO
ALTER TABLE [dbo].[ClientUsers] WITH NOCHECK
    ADD CONSTRAINT [fk1_ClientUsers] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]);


GO
PRINT N'Creating fk2_ClientUsers...';


GO
ALTER TABLE [dbo].[ClientUsers] WITH NOCHECK
    ADD CONSTRAINT [fk2_ClientUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]);


GO
PRINT N'Creating fk_CUDHistoryData...';


GO
ALTER TABLE [dbo].[CUDHistoryData] WITH NOCHECK
    ADD CONSTRAINT [fk_CUDHistoryData] FOREIGN KEY ([CUDHistoryId]) REFERENCES [dbo].[CUDHistory] ([CUDHistoryId]);


GO
PRINT N'Creating fk1_TemplateFeature...';


GO
ALTER TABLE [dbo].[TemplateFeature] WITH NOCHECK
    ADD CONSTRAINT [fk1_TemplateFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk_TemplateNavigation1...';


GO
ALTER TABLE [dbo].[TemplateNavigation] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplateNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk1_TemplatePage...';


GO
ALTER TABLE [dbo].[TemplatePage] WITH NOCHECK
    ADD CONSTRAINT [fk1_TemplatePage] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk2_TemplatePage...';


GO
ALTER TABLE [dbo].[TemplatePage] WITH NOCHECK
    ADD CONSTRAINT [fk2_TemplatePage] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId]);


GO
PRINT N'Creating fk1_TemplatePageFeature...';


GO
ALTER TABLE [dbo].[TemplatePageFeature] WITH NOCHECK
    ADD CONSTRAINT [fk1_TemplatePageFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk2_TemplatePageFeature...';


GO
ALTER TABLE [dbo].[TemplatePageFeature] WITH NOCHECK
    ADD CONSTRAINT [fk2_TemplatePageFeature] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId]);


GO
PRINT N'Creating fk_TemplatePageNavigation1...';


GO
ALTER TABLE [dbo].[TemplatePageNavigation] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplatePageNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk_TemplatePageNavigation2...';


GO
ALTER TABLE [dbo].[TemplatePageNavigation] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplatePageNavigation2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId]);


GO
PRINT N'Creating fk_TemplatePageText1...';


GO
ALTER TABLE [dbo].[TemplatePageText] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplatePageText1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk_TemplatePageText2...';


GO
ALTER TABLE [dbo].[TemplatePageText] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplatePageText2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId]);


GO
PRINT N'Creating fk_TemplateText...';


GO
ALTER TABLE [dbo].[TemplateText] WITH NOCHECK
    ADD CONSTRAINT [fk_TemplateText] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId]);


GO
PRINT N'Creating fk_Roles...';


GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]);


GO
PRINT N'Creating fk_Users...';


GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]);


GO
PRINT N'Creating [dbo].[fnFrequencyDescription]...';


GO
CREATE FUNCTION dbo.fnFrequencyDescription
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS VARCHAR(200)
WITH SCHEMABINDING
AS
BEGIN
	RETURN
	(
		SELECT
			CASE @FrequencyType
				WHEN 1
					THEN 'Runs once.'
				WHEN 2
					THEN 'Runs every (' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 ELSE @FrequencyInterval END) + ') days.'
				WHEN 3
					THEN 'Runs Weekly on '
					+ CASE @FrequencyInterval
						WHEN 2 THEN 'Monday'
						WHEN 3 THEN 'Tuesday'
						WHEN 4 THEN 'Wednesday'
						WHEN 5 THEN 'Thursday'
						WHEN 6 THEN 'Friday'
						WHEN 7 THEN 'Saturday'
						ELSE 'Sunday'
					END + '.'
				WHEN 4
					THEN 'Runs Monthly on day ' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END) + '.'
				WHEN 5
					THEN 'Runs Quarterly.'
				WHEN 6
					THEN 'Runs Bi-Annually.'
				WHEN 7 
					THEN 'Runs Annually'
				ELSE 'Does not run.'
			END
	);
END;
GO
PRINT N'Creating [dbo].[fnGetDateFromParts]...';


GO
CREATE FUNCTION dbo.fnGetDateFromParts
(
	@Year INT,
	@Month INT,
	@Day INT,
	@Time VARCHAR(12)
)
RETURNS SMALLDATETIME
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @ReturnDate SMALLDATETIME;
	IF @Time IS NULL
	BEGIN
		SET @ReturnDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), @Month) + '/' + CONVERT(VARCHAR(10), @Day) + '/' + CONVERT(VARCHAR(10), @Year), 101);
	END
	ELSE
	BEGIN
		SET @ReturnDate = CONVERT(DATETIME, CONVERT(VARCHAR(23), @Year) + '-' + CONVERT(VARCHAR(23), @Month) + '-' + CONVERT(VARCHAR(23), @Day) + ' ' + CONVERT(VARCHAR(23), @Time), 121);
	END
	
	RETURN @ReturnDate;
END;
GO
PRINT N'Creating [dbo].[fnNextReportDateUtc]...';


GO
CREATE FUNCTION dbo.fnNextReportDateUtc
(
	@UtcFirstRunDate DATETIME,
	@UtcLastRunDate DATETIME,
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS SMALLDATETIME
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @NextDate SMALLDATETIME = NULL;
	IF @FrequencyType = 1 --One time only
	BEGIN
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NOT NULL
					THEN NULL
					ELSE @UtcFirstRunDate
			END;
	END
	ELSE IF @FrequencyType = 2 --Daily
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NULL 
					THEN @UtcFirstRunDate
					ELSE DATEADD(DAY, @FrequencyInterval, @UtcLastRunDate)
			END;
	END
	ELSE IF @FrequencyType = 3 --Weekly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval BETWEEN 2 AND 7 THEN @FrequencyInterval ELSE 1 END;
		--Can't use DATEPART with WEEKDAY or DW as this is non deterministic. So we pick a sunday date way in the past and do a DATEDIFF.
		DECLARE @SundayDate DATETIME = CONVERT(DATETIME, '18991231', 112);
		DECLARE @DW INT = DATEDIFF(DAY, @SundayDate, @NextDate) % 7 + 1;
		IF (@DW < @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, @FrequencyInterval - @DW, @NextDate);
		END
		ELSE IF (@DW > @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, 7 - (@DW - @FrequencyInterval), @NextDate);
		END;
	END
	ELSE IF @FrequencyType = 4 --Monthly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		DECLARE @TimePart VARCHAR(12) = CONVERT(VARCHAR(12), @NextDate, 114)
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END;
		DECLARE @DM INT = DAY(@NextDate);
		DECLARE @FixedFrequency INT = @FrequencyInterval;
		DECLARE @MaxDay INT = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), 1, NULL))));
		IF @FixedFrequency > @MaxDay
		BEGIN
			SET	@FixedFrequency = @MaxDay;
		END;
		
		IF @DM < @FixedFrequency
		BEGIN
			SET @NextDate = dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), @FixedFrequency, @TimePart);
		END
		ELSE IF @DM > @FixedFrequency
		BEGIN
			SET @NextDate = DATEADD(MONTH, 1, dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), 1, @TimePart));
			SET @MaxDay = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, @NextDate)));
			SET @FixedFrequency =
				CASE
					WHEN @FrequencyInterval > @MaxDay
						THEN @MaxDay
						ELSE @FrequencyInterval
				END;
			SET @NextDate = dbo.fnGetDateFromParts(YEAR(@NextDate), MONTH(@NextDate), @FixedFrequency, @TimePart);
		END;
	END
	ELSE IF @FrequencyType = 5 --Quarterly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(QUARTER, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 6 --Bi-Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(MONTH, 6, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 7 --Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(YEAR, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END;
	
	RETURN @NextDate;
END;
GO
PRINT N'Creating [dbo].[fnReportScheduleTypeAndIntervalCheck]...';


GO
CREATE FUNCTION dbo.fnReportScheduleTypeAndIntervalCheck
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS BIT
WITH SCHEMABINDING
AS
BEGIN
	RETURN
		CASE @FrequencyType
			WHEN 1 THEN -- Once
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 2 THEN -- Daily
				CASE
					WHEN @FrequencyInterval < 1 THEN 0
					ELSE 1
				END
			WHEN 3 THEN -- Weekly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 7 THEN 1
					ELSE 0
				END
			WHEN 4 THEN -- Monthly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 31 THEN 1
					ELSE 0
				END
			WHEN 5 THEN -- Quarterly
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 6 THEN -- Bi-Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 7 THEN -- Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			ELSE 0
		END;
END;
GO
PRINT N'Creating [dbo].[ReportSchedule]...';


GO
CREATE TABLE [dbo].[ReportSchedule] (
    [ReportScheduleId]         INT            IDENTITY (1, 1) NOT NULL,
    [ReportId]                 INT            NOT NULL,
    [ClientId]                 INT            NOT NULL,
    [IsEnabled]                BIT            NOT NULL,
    [Status]                   INT            NULL,
    [FrequencyType]            INT            NOT NULL,
    [FrequencyInterval]        INT            NOT NULL,
    [UtcFirstScheduledRunDate] SMALLDATETIME  NOT NULL,
    [UtcLastScheduledRunDate]  SMALLDATETIME  NULL,
    [UtcLastActualRunDate]     DATETIME       NULL,
    [ServiceName]              NVARCHAR (100) NULL,
    [ExecutionCount]           INT            NULL,
    [UtcModifiedDate]          DATETIME       NOT NULL,
    [ModifiedBy]               INT            NULL,
    [UtcNextScheduledRunDate]  AS             ([dbo].[fnNextReportDateUtc]([UtcFirstScheduledRunDate], [UtcLastScheduledRunDate], [FrequencyType], [FrequencyInterval])) PERSISTED,
    [FrequencyDescription]     AS             ([dbo].[fnFrequencyDescription]([FrequencyType], [FrequencyInterval])) PERSISTED,
    CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC)
);


GO
PRINT N'Creating DF_ReportSchedule_IsEnabled...';


GO
ALTER TABLE [dbo].[ReportSchedule]
    ADD CONSTRAINT [DF_ReportSchedule_IsEnabled] DEFAULT ((1)) FOR [IsEnabled];


GO
PRINT N'Creating DF_ReportSchedule_ExecutionCount...';


GO
ALTER TABLE [dbo].[ReportSchedule]
    ADD CONSTRAINT [DF_ReportSchedule_ExecutionCount] DEFAULT ((0)) FOR [ExecutionCount];


GO
PRINT N'Creating DF_ReportSchedule_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ReportSchedule]
    ADD CONSTRAINT [DF_ReportSchedule_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating FK_ReportSchedule_Clients...';


GO
ALTER TABLE [dbo].[ReportSchedule] WITH NOCHECK
    ADD CONSTRAINT [FK_ReportSchedule_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]);


GO
PRINT N'Creating FK_ReportSchedule_Reports...';


GO
ALTER TABLE [dbo].[ReportSchedule] WITH NOCHECK
    ADD CONSTRAINT [FK_ReportSchedule_Reports] FOREIGN KEY ([ReportId]) REFERENCES [dbo].[Reports] ([ReportId]);


GO
PRINT N'Creating FK_ReportScheduleRecipients_ReportSchedule...';


GO
ALTER TABLE [dbo].[ReportScheduleRecipients] WITH NOCHECK
    ADD CONSTRAINT [FK_ReportScheduleRecipients_ReportSchedule] FOREIGN KEY ([ReportScheduleId]) REFERENCES [dbo].[ReportSchedule] ([ReportScheduleId]);


GO
PRINT N'Creating CK_ReportSchedule_FrequencyType_FrequencyInterval...';


GO
ALTER TABLE [dbo].[ReportSchedule] WITH NOCHECK
    ADD CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval] CHECK ([dbo].[fnReportScheduleTypeAndIntervalCheck]([FrequencyType],[FrequencyInterval])=(1));


GO
PRINT N'Creating [dbo].[CUDHistory_Insert]...';


GO
-- Stored procedure for CUDHistory and Data inserts. Called from Triggers.
CREATE PROCEDURE [dbo].CUDHistory_Insert
(
	@CUDHistory dbo.TT_CUDHistory READONLY,
	@CUDHistoryData dbo.TT_CUDHistoryData READONLY
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdMapTable TABLE
	(
	CUDHistoryId INT NOT NULL PRIMARY KEY CLUSTERED,
	TT_Id INT NULL,
	TableName NVARCHAR(128) NOT NULL,
	[Key] INT NOT NULL,
	SecondKey NVARCHAR(200) NULL,
	ThirdKey NVARCHAR(200) NULL
	);
	
	DECLARE @BatchId UNIQUEIDENTIFIER;
	
	SET @BatchId = NEWID();
	
	INSERT	INTO CUDHistory (TableName, [Key], SecondKey, ThirdKey, CUDType, BatchId, UserId)
	OUTPUT	inserted.CUDHistoryId, inserted.TableName, inserted.[Key], inserted.SecondKey, inserted.ThirdKey
	INTO	@IdMapTable (CUDHistoryId, TableName, [Key], SecondKey, ThirdKey)
	SELECT	c.TableName, c.[Key], c.SecondKey, c.ThirdKey, c.CUDType, @BatchId, c.UserId
	FROM	@CUDHistory c;
	
	UPDATE	t1
	SET	t1.TT_Id = c.Id
	FROM	@IdMapTable t1
	INNER JOIN	@CUDHistory c
	ON	t1.TableName = c.TableName
	AND	t1.[Key] = c.[Key]
	AND (t1.SecondKey = c.SecondKey OR (t1.SecondKey IS NULL AND c.SecondKey IS NULL))
	AND (t1.ThirdKey = c.ThirdKey OR (t1.ThirdKey IS NULL AND c.ThirdKey IS NULL));
	
	INSERT INTO CUDHistoryData (CUDHistoryId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	t1.CUDHistoryId, cd.ColumnName, cd.SqlDbType, cd.OldValue, cd.NewValue
	FROM	@CUDHistoryData cd
	INNER JOIN @IdMapTable t1
	ON	cd.ParentId = t1.TT_Id;
END;
GO
PRINT N'Creating [dbo].[RPV2Hosted_SaveErrorLog]...';


GO
CREATE PROCEDURE [dbo].RPV2Hosted_SaveErrorLog
@ErrorCode int,
@Priority int,
@Severity nvarchar(32),
@Title nvarchar(256),
@MachineName nvarchar(32),
@AppDomainName nvarchar(512),
@ProcessID nvarchar(256),
@ProcessName nvarchar(512),
@ThreadName nvarchar(512),
@Win32ThreadId nvarchar(128),
@EventId int,
@SiteActivityId int,
@Message nvarchar(1500),
@FormattedMessage nvarchar(max),
@URL nvarchar(500),
@AbsoluteURL nvarchar(500)
AS
BEGIN

  INSERT INTO ErrorLog
     (ErrorCode,
      ErrorUtcDate,
      [Priority],
      Severity,
      Title,
      MachineName,
      AppDomainName,
      ProcessID,
      ProcessName,
      ThreadName,
      Win32ThreadId,
      EventId,
      SiteActivityId,
      [Message],
      FormattedMessage,
	  URL,
	  AbsoluteURL
      )
   VALUES
     (@ErrorCode,
      GETUTCDATE(),
      @Priority,
      @Severity,
      @Title,
      @MachineName,
      @AppDomainName,
      @ProcessID,
      @ProcessName,
      @ThreadName,
      @Win32ThreadId,
      @EventId,
      @SiteActivityId,
      @Message,
      @FormattedMessage,
	  @URL,
	  @AbsoluteURL
      ) 
     
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteClient]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteClient]
	Added By: Ashok
	Updated By: Noel Dsouza
	Date: 09/07/2015
	Reason : To DELETE THE Client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClient] 
				 @clientId int,
				 @DeletedBy int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_DeleteClient]
AS 
BEGIN 


	DELETE FROM ClientUsers
	WHERE ClientId = @clientId 
	
	DELETE  [Clients]				
	WHERE   ClientId=@clientId	

				   
	UPDATE	CUDHistory				 
  		SET	UserId = @deletedBy
	WHERE	TableName = N'ClientUsers'
		AND	[Key] = @clientId
		AND [CUDType] = 'D';

    UPDATE	CUDHistory
  	 SET	UserId = @DeletedBy
	WHERE	TableName = N'Clients'
		AND	[Key] = @clientId AND [CUDType] = 'D';
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteReportSchedule]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteReportSchedule]
@ReportScheduleID int,  
@DeletedBy int  
AS  
BEGIN  
  
	DELETE ReportScheduleRecipients
	WHERE ReportScheduleId = @ReportScheduleID;  
  
	DELETE ReportSchedule  
	WHERE ReportScheduleID = @ReportScheduleID;  
  
  
  
	UPDATE CUDHistory   
	SET  UserId = @DeletedBy    
	WHERE TableName = N'ReportSchedule'    
	AND [Key] = @ReportScheduleID AND CUDType = 'D';    
  
  
	UPDATE CUDHistory    
	SET  UserId = @DeletedBy    
	WHERE TableName = N'ReportScheduleRecipients'    
	AND [Key] = @ReportScheduleID AND CUDType = 'D';
    
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteRoles]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteRoles]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE Roles
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteRoles] 
				 @rolesId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE Roles
		   WHERE RoleId = @rolesId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'Roles'
				AND	[Key] = @rolesId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteUser]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteUser]
	Added By: Ashok
	Date: 09/07/2015
	Updated : 09/19/2015 BY Noel
	Reason : To DELETE THE USER
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteUser] 
				 @userId int,
				 @deletedBy int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_DeleteUser]
AS 
BEGIN 
				
				DELETE FROM ClientUsers
				 WHERE UserId = @userId 
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'ClientUsers'
					AND	[SecondKey] = @userId
					AND [CUDType] = 'D';
				   
				   
				DELETE FROM UserRoles
				 WHERE UserId = @userId 
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'UserRoles'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 

				DELETE  USERS				
				WHERE UserId=@userId	
				
				UPDATE	CUDHistory				 
  				 SET	UserId = @deletedBy
				WHERE	TableName = N'USERS'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 
				
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllClients]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015
-- Updated date : 22nd-Sept-2015 By Noel
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClients]
AS
BEGIN
  SELECT 
	Clients.ClientId,
    Clients.ClientName,
    VerticalMarkets.MarketName AS VerticalMarketName,
    Clients.ClientDescription,
    Clients.ConnectionStringName AS ClientConnectionStringName,    
    Clients.DatabaseName AS ClientDatabaseName,
	VerticalMarkets.DatabaseName AS VerticalMarketDatabaseName,
	VerticalMarkets.ConnectionStringName AS VerticalMarketConnectionStringName,
    Clients.VerticalMarketId,
    ClientDns.ClientDnsId,
    ClientDns.Dns,
    ClientUsers.UserId,
    Clients.ModifiedBy,
    Clients.UtcModifiedDate AS UtcLastModified
  FROM Clients
   INNER JOIN VerticalMarkets ON Clients.VerticalMarketId = VerticalMarkets.VerticalMarketId
   LEFT OUTER JOIN ClientDNS ON Clients.ClientId = ClientDNS.ClientId  
   LEFT OUTER JOIN ClientUsers ON Clients.ClientId = ClientUsers.ClientId
  ORDER BY Clients.ClientId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllReports]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReports]
AS
BEGIN
  SELECT 
	ReportId,
	ReportName,
	ReportDescription,
	ModifiedBy=0,
	UtcLastModified = GetUTCDATE()
  FROM Reports
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllReportSchedule]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReportSchedule]
@ClientID int
AS
BEGIN

  SELECT DISTINCT	
	Reports.ReportName,	
	ReportSchedule.IsEnabled,	
	ReportSchedule.FrequencyType,
	ReportSchedule.FrequencyInterval,
	ReportSchedule.UtcFirstScheduledRunDate,
	ReportSchedule.UtcLastScheduledRunDate,
	ReportSchedule.UtcLastActualRunDate,
	ReportSchedule.ModifiedBy,  
	ReportSchedule.UtcModifiedDate AS UtcLastModified
  FROM ReportSchedule
  INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
  INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
  Where ReportSchedule.ClientId = @ClientID
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllRoles]...';


GO
 /*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetAllRoles]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To GET ALL Roles
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllRoles]
AS
BEGIN
  SELECT 
	RoleId,
	Name,
    ModifiedBy,
    UtcModifiedDate as UtcLastModified
  FROM Roles
   
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllRolesByUserId]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllRolesByUserId]
@UserId int
AS

BEGIN

   Select DISTINCT
          UserId ,
          UserRoles.RoleId,
          Name ,
		  UserRoles.UtcModifiedDate as UtcLastModified,
		  UserRoles.ModifiedBy as ModifiedBy
          FROM UserRoles
          INNER JOIN Roles on UserRoles.RoleId = Roles.RoleId
          WHERE UserRoles.UserId = @UserId


END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplateFeature]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplateFeature]
AS
BEGIN

   SELECT  TemplateFeature.TemplateId,
		Template.[Name] AS TemplateName,
		TemplateFeature.[Key] as FeatureKey,
		TemplateFeature.[Description] as FeatureDescription
    FROM Template
	INNER JOIN TemplateFeature ON Template.TemplateID = TemplateFeature.TemplateID
    ORDER BY TemplateFeature.TemplateId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplatePage]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplatePage
AS
BEGIN
	SELECT  TemplatePage.TemplateId,
		Template.[Name] AS TemplateName,
		Page.PageID,
		Page.Name as PageName,
		Page.[Description] as PageDescription  
	FROM Template
	INNER JOIN TemplatePage ON Template.TemplateID = TemplatePage.TemplateID
	INNER JOIN Page ON TemplatePage.PageId =  Page.PageID
	ORDER BY TemplatePage.TemplateId,Page.PageID
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplatePageFeature]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplatePageFeature]

AS

BEGIN



   SELECT  TemplatePageFeature.TemplateId,

		Template.[Name] AS TemplateName,
		TemplatePageFeature.PageId,


		TemplatePageFeature.[Key] as FeatureKey,

		TemplatePageFeature.[Description] as FeatureDescription

    FROM Template

	INNER JOIN TemplatePageFeature ON Template.TemplateID = TemplatePageFeature.TemplateID

    ORDER BY TemplatePageFeature.TemplateId

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplatePageNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplatePageNavigation]

AS

BEGIN

SELECT  TemplateId,
		PageId,
		NavigationKey,
		Name,
		XslTransform,
		DefaultNavigationXml,
		[Description]		
     	FROM TemplatePageNavigation

End
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplatePageText]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplatePageText
AS
BEGIN
  SELECT TemplateId,
		PageId,
		ResourceKey,
		[Name],
		IsHtml,
		DefaultText,
		[Description]		
	FROM TemplatePageText
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplateSiteNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTemplateSiteNavigation]

AS

BEGIN

SELECT  TemplateId,
		NavigationKey,
		Name,
		XslTransform,
		DefaultNavigationXml,
		[Description]		
     	FROM TemplateNavigation

End
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTemplateText]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTemplateText
AS
BEGIN
  SELECT TemplateId,
		ResourceKey,
		[Name],
		IsHtml,
		DefaultText,
		[Description]		
	FROM TemplateText
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllUsers]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllUsers]
AS
BEGIN
	SELECT 
		   [Users].[UserId]
		  ,[Email]
		  ,[EmailConfirmed]
		  ,[PasswordHash]
		  ,[SecurityStamp]
		  ,[PhoneNumber]
		  ,[PhoneNumberConfirmed]
		  ,[TwoFactorEnabled]
		  ,[LockOutEndDateUtc]
		  ,[LockoutEnabled]
		  ,[AccessFailedCount]
		  ,[UserName]
		  ,[FirstName]
		  ,[LastName]
		  ,[Users].[UtcModifiedDate] as LastModified
		  ,[Users].[ModifiedBy]
		  ,[UserRoles].RoleId
		  ,[Roles].Name 
		  ,[ClientUsers].ClientId
	  FROM [dbo].[Users]
		  LEFT OUTER JOIN [UserRoles] ON [Users].[UserId] = [UserRoles].[UserId]
		  LEFT OUTER JOIN [Roles] ON [Roles].[RoleId] = [UserRoles].[RoleId]
		  LEFT OUTER JOIN [ClientUsers] ON [Users].[UserId] = [ClientUsers].[UserId]
	  ORDER BY [Users].[UserId]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllVerticalMarkets]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllVerticalMarkets]
AS
BEGIN
	SELECT  VerticalMarkets.VerticalMarketId,
		VerticalMarkets.MarketName,
		VerticalMarkets.[MarketDescription],
		VerticalMarkets.UtcModifiedDate as UtcLastModified,
		VerticalMarkets.ModifiedBy as ModifiedBy	   
	FROM VerticalMarkets
	ORDER BY VerticalMarketId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetClientById]...';


GO


/*

	Procedure Name:[dbo].[RPV2HostedAdmin_GetClientById]

	Added By: Ashok

	Date: 09/07/2015

	Reason : To get the the Client details 

*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetClientById] 

				 @clientId int                				 			

				

 --DROP PROCEDURE [RPV2SystemHostedAdmin_GetClientById]

AS BEGIN 



			SELECT 

				CLIENTS.clientName, 

				CLIENTS.ConnectionStringName,

				CLIENTS.DatabaseName,

				CLIENTS.VerticalMarketId,

				CLIENTS.ClientDescription,

				CLIENTS.UtcModifiedDate,

				CLIENTS.ModifiedBy,

				CLIENTDNS.Dns

				FROM CLIENTS,CLIENTDNS

				WHERE CLIENTS.ClientId=CLIENTDNS.ClientId

				AND CLIENTS.ClientId=@clientId



			

	END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetClients]...';


GO

/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetClients]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To List all the Clients for Paging
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetClients]
	@clientId INT=NULL,
	@clientName NVARCHAR(400)=NULL,
	@connectionStringName VARCHAR(200)=NULL,
	@databaseName VARCHAR (200)=NULL,
	@verticalMarketId int=NULL,
	@clientDescription NVARCHAR(800)=NULL,
	@utcModifiedDate DATETIME,
	@isActive BIT,
	@modifiedBy INT,
	@pageIndex int,
	@pageSize int,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

	
	--exec [RPV2SystemHostedAdmin_GetClients] null,null,null,null,null,null,'',null,0,1,10,'desc','databaseName',0	
	--DROP PROCEDURE [RPV2System_GetClients]
AS
BEGIN
	
			SELECT DISTINCT
						RowRank,
						ClientId,
						ClientName,
						ConnectionStringName,
						DatabaseName,
						VerticalMarketId,
						ClientDescription,
						UtcModifiedDate,
					    ModifiedBy
								
			FROM
						(
					 SELECT 
						Clients.ClientId,
					 	Clients.ClientName,
						Clients.ConnectionStringName,
						Clients.DatabaseName,
						Clients.VerticalMarketId,
						Clients.ClientDescription,	
						Clients.UtcModifiedDate,
					    Clients.ModifiedBy,
							CASE  										   
								   WHEN @sortColumn = 'ClientId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Clients.ClientId Asc) 									  
								   WHEN @sortColumn = 'ClientId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Clients.ClientId Desc) 									  
								   WHEN @sortColumn = 'ClientName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Clients.ClientName Asc) 
								   WHEN @sortColumn = 'ClientName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Clients.ClientName Desc) 
								   WHEN @sortColumn = 'DatabaseName' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.DatabaseName Asc)
								   WHEN @sortColumn = 'DatabaseName' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.DatabaseName Desc)
								   WHEN @sortColumn = 'VerticalMarketId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.VerticalMarketId Asc)
								   WHEN @sortColumn = 'VerticalMarketId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.VerticalMarketId Desc)
							 End 										          
							   AS RowRank
					 FROM
					  Clients				 
					 WHERE
					 	 (Clients.ClientId = @clientId OR @clientId IS NULL)
				   AND(Clients.ClientName = @clientName OR @clientName IS NULL)	 
				   AND(Clients.DatabaseName = @databaseName OR @databaseName IS NULL)	 
				   AND(Clients.VerticalMarketId = @verticalMarketId OR @verticalMarketId IS NULL)	 
					) AS ClientsTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
	END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetDefaultPageNameById]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetDefaultPageNameById]
	Added By: Arshdeep
	Date: 11/09/2015
	Reason : To get the the PageID
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetDefaultPageNameById]
				 @pageId int               				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetDefaultPageNameById]
 --exec [RPV2HostedAdmin_GetDefaultPageNameById] 1
AS BEGIN 

			SELECT 
				P.PageId,
				P.Name,
				P.[Description]	
				From Page P
				WHERE P.PageId = @pageId
			
	END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetReportScheduleByClientId]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetReportScheduleByClientId]	
	@ReportName NVARCHAR(100) = NULL,
	@FrequencyType INT = NULL,
	@FrequencyInterval INT = NULL,
	@UtcFirstScheduledRunDate DATETIME = NULL,
	@UtcLastScheduledRunDate DATETIME = NULL,
	@IsEnabled BIT = NULL,
	@PageSize INT,
	@PageIndex INT,
    @SortDirection NVARCHAR(10),
	@SortColumn NVARCHAR(100),
	@ClientID int	
AS
BEGIN
	SELECT DISTINCT		
				RowRank,		
				ReportScheduleId,
				ReportId,
				ReportName,
				ClientId,
				IsEnabled,
				FrequencyType,
				FrequencyInterval,
				UtcFirstScheduledRunDate,
				UtcNextScheduledRunDate,
				UtcLastScheduledRunDate,
				UtcLastActualRunDate,
				ModifiedBy,
				UtcLastModified
			FROM 
			(
				SELECT
       			ReportSchedule.ReportScheduleId,
				ReportSchedule.ReportId,
				Reports.ReportName,
				ReportSchedule.ClientId,
				ReportSchedule.IsEnabled,
				ReportSchedule.FrequencyType,
				ReportSchedule.FrequencyInterval,
				ReportSchedule.UtcFirstScheduledRunDate,
				ReportSchedule.UtcNextScheduledRunDate,
				ReportSchedule.UtcLastScheduledRunDate,
				ReportSchedule.UtcLastActualRunDate,
				ReportSchedule.ModifiedBy,  
				ReportSchedule.UtcModifiedDate AS UtcLastModified,
				CASE	WHEN @SortColumn = 'ReportName' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY Reports.ReportName Asc)
						WHEN @SortColumn = 'ReportName' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY Reports.ReportName Desc)
						WHEN @SortColumn = 'FrequencyType' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  FrequencyType.EnumText Asc)
						WHEN @SortColumn = 'FrequencyType' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  FrequencyType.EnumText Desc)
						WHEN @SortColumn = 'FrequencyInterval' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyInterval Asc)
						WHEN @SortColumn = 'FrequencyInterval' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.FrequencyInterval Desc)
						WHEN @SortColumn = 'UtcFirstScheduledRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcFirstScheduledRunDate Asc)
						WHEN @SortColumn = 'UtcFirstScheduledRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcFirstScheduledRunDate Desc)
						WHEN @SortColumn = 'UtcLastActualRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcLastActualRunDate Asc)
						WHEN @SortColumn = 'UtcLastActualRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcLastActualRunDate Desc)
						WHEN @SortColumn = 'UtcNextScheduledRunDate' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcNextScheduledRunDate Asc)
						WHEN @SortColumn = 'UtcNextScheduledRunDate' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.UtcNextScheduledRunDate Desc)
						WHEN @SortColumn = 'IsEnabled' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.IsEnabled Asc)
						WHEN @SortColumn = 'IsEnabled' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  ReportSchedule.IsEnabled Desc)
 				End AS RowRank
				FROM ReportSchedule
				INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
				INNER JOIN FrequencyType ON ReportSchedule.FrequencyType = FrequencyType.EnumKey
				INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
				WHERE	ReportSchedule.ClientId = @ClientID AND
							(Reports.ReportName = @ReportName OR @ReportName IS NULL)
							AND(ReportSchedule.FrequencyType = @FrequencyType OR @FrequencyType IS NULL)
							AND(ReportSchedule.FrequencyInterval = @FrequencyInterval OR @FrequencyInterval IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcFirstScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcFirstScheduledRunDate,102) OR @UtcFirstScheduledRunDate IS NULL)
							AND (CONVERT(Varchar(50), ReportSchedule.UtcLastScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcLastScheduledRunDate,102) OR @UtcLastScheduledRunDate IS NULL)
							AND(ReportSchedule.IsEnabled = @IsEnabled OR @IsEnabled IS NULL)
							)AS ReportScheduleDetails 
			WHERE RowRank BETWEEN @PageIndex*@PageSize-@PageSize + 1  AND @PageSize*@PageIndex
			ORDER BY RowRank

			SELECT count(*) 
			FROM ReportSchedule
			INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId
			INNER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId
			WHERE	ReportSchedule.ClientId = @ClientID AND
					(Reports.ReportName = @ReportName OR @ReportName IS NULL)
					AND (ReportSchedule.FrequencyType = @FrequencyType OR @FrequencyType IS NULL)
					AND (ReportSchedule.FrequencyInterval = @FrequencyInterval OR @FrequencyInterval IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcFirstScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcFirstScheduledRunDate,102) OR @UtcFirstScheduledRunDate IS NULL)
					AND (CONVERT(Varchar(50), ReportSchedule.UtcLastScheduledRunDate,102) =  CONVERT(VARCHAR(50), @UtcLastScheduledRunDate,102) OR @UtcLastScheduledRunDate IS NULL)
					AND (ReportSchedule.IsEnabled = @IsEnabled OR @IsEnabled IS NULL)
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetReportScheduleById]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetReportScheduleById]
@ReportScheduleId int
AS
BEGIN  

	SELECT DISTINCT
		ReportSchedule.ReportScheduleId,  
		ReportSchedule.ReportId,  
		Reports.ReportName,  
		ReportSchedule.ClientId,  
		ReportSchedule.IsEnabled,  
		ReportSchedule.[Status],  
		ReportSchedule.ServiceName,  
		ReportSchedule.FrequencyType,  
		ReportSchedule.FrequencyInterval,  
		ReportSchedule.UtcFirstScheduledRunDate,  
		ReportSchedule.UtcNextScheduledRunDate,  
		ReportSchedule.UtcLastScheduledRunDate,  
		ReportSchedule.UtcLastActualRunDate,  
		ReportSchedule.FrequencyDescription,  
		ReportSchedule.ModifiedBy,  
		ReportSchedule.UtcModifiedDate AS UtcLastModified,  
		ReportScheduleRecipients.Email,  
		ReportScheduleRecipients.FTPServerIP,  
		ReportScheduleRecipients.FTPFilePath,  
		ReportScheduleRecipients.FTPUsername,  
		ReportScheduleRecipients.FTPPassword  
  
	FROM ReportSchedule  
	INNER JOIN Reports ON ReportSchedule.ReportId = Reports.ReportId  
	LEFT OUTER JOIN ReportScheduleRecipients ON ReportScheduleRecipients.ReportScheduleId = ReportSchedule.ReportScheduleId  
	WHERE ReportSchedule.ReportScheduleId = @ReportScheduleId
   
    
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetTemplateNameById]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetTemplateNameById]
	Added By: Arshdeep
	Date: 11/09/2015
	Reason : To get the the TemplateID
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetTemplateNameById]
				 @templateId int               				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetTemplateNameById]
 --exec [RPV2HostedAdmin_GetTemplateNameById] 1
AS BEGIN 

			SELECT 
				T.TemplateId,
				T.Name,
				T.[Description]	
				From Template T
				WHERE T.TemplateId=@templateId
			
	END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetUserById]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetUserById]
	Added By: Ashok
	Date: 09/07/2015
	Reason : To get the the User details 
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetUserById]
				 @userId int                				 			
				
 --DROP PROCEDURE [RPV2HostedAdmin_GetUserById]
 --exec RPV2System_GetUserById 2
AS BEGIN 

			SELECT 
				USERS.UserId, 				
				USERS.Email,
				USERS.EmailConfirmed,
				USERS.PasswordHash,
				USERS.SecurityStamp,
				USERS.PhoneNumber,
				USERS.PhoneNumberConfirmed,
				USERS.TwoFactorEnabled,
				USERS.LockOutEndDateUtc,
				USERS.LockoutEnabled,
				USERS.AccessFailedCount,
				USERS.UserName,
				USERS.FirstName,
				USERS.LastName,					
				USERS.UtcModifiedDate,
				USERS.ModifiedBy
				FROM USERS
				WHERE USERS.UserId=@userId
			
	END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetUsers]...';


GO

/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetUsers]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To List all the Users with paging
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetUsers]
	@userId INT=NULL,
	@fName NVARCHAR(100)=NULL,
	@lName NVARCHAR(100)=NULL,
	@eMail NVARCHAR (256)=NULL,
	@pageSize int,
	@pageIndex int,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

	
	--exec [RPV2SystemHostedAdmin__GetUsers] null,NULL,NULL,NULL,10,1,'ASC','UserId',0	
	--DROP PROCEDURE [RPV2SystemHostedAdmin__GetUsers]
AS
BEGIN
	
			SELECT DISTINCT
					    RowRank,
					 	UserId,
					 	FirstName,
					 	LastName,
					 	Email
					 FROM 
					  (
				  SELECT
					Users.UserId,
					Users.FirstName,
					Users.LastName,
					Users.Email,
					 CASE  
							WHEN @sortColumn = 'UserId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Users.UserId Asc) 									  
							WHEN @sortColumn = 'UserId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Users.UserId Desc) 									  
							WHEN @sortColumn = 'FirstName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Users.FirstName Asc) 
							WHEN @sortColumn = 'FirstName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Users.FirstName Desc) 
							WHEN @sortColumn = 'LastName' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Users.LastName Asc)
							WHEN @sortColumn = 'LastName' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Users.LastName Desc)
							WHEN @sortColumn = 'Email' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Users.Email Asc)
							WHEN @sortColumn = 'Email' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Users.Email Desc)
					 	 End 										          
							   AS RowRank
					FROM
					 Users     					
				   WHERE 
					  (Users.UserId = @userId OR @userId IS NULL)
				   AND(Users.FirstName = @fName OR @fName IS NULL)	 
				   AND(Users.LastName = @lName OR @lName IS NULL)	 
				   AND(Users.Email = @eMail OR @eMail IS NULL)	 
					) AS UsersTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ProcessReportSchedule]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ProcessReportSchedule]

(

	@ServiceName AS NVARCHAR(100)

)

AS

BEGIN

	SET NOCOUNT ON;	

	DECLARE @ProcExecutionId AS UNIQUEIDENTIFIER, @ReportToProcess AS INT, @ClientToProcess AS INT;

	

	--Populate table variable with all ReportSchedules that need to be processed now

	DECLARE @ReportScheduleVar TABLE(ReportScheduleId INT, ReportId INT, ClientId INT, UtcLastActualRunDate DateTime)

	INSERT INTO @ReportScheduleVar(ReportScheduleId, ReportId, ClientId, UtcLastActualRunDate)

	SELECT ReportScheduleId, ReportId, ClientId, UtcLastActualRunDate

	FROM ReportSchedule

	WHERE IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)

	

	--- START: Get the next entry that should be Processed (by app/client priority) ---



	--- GET NEXT REPORT ---

	SELECT TOP 1 @ReportToProcess = [ReportId]

	FROM @ReportScheduleVar

	WHERE [ReportId] > 

			(SELECT TOP 1 [ReportId]  

				FROM ReportSchedule

				ORDER BY [UtcLastActualRunDate] DESC);	



	--- IF NO REPORT Id IS FOUND GREATER THAN LAST PROCESSED ONE, GO BACK TO THE START OF THE REPORT LIST ---

	IF (@ReportToProcess IS NULL)	

	BEGIN

		SELECT TOP 1 @ReportToProcess = [ReportId]

		FROM [ReportSchedule] 

		WHERE IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)

		ORDER BY [UtcLastActualRunDate] DESC;

	END;



	IF (@ReportToProcess IS NULL)

	BEGIN

		RETURN;

	END;

	

	--- GET NEXT CLIENT FOR THAT REPORT ---

	SELECT TOP 1 @ClientToProcess = [ClientId]

	FROM @ReportScheduleVar 

	WHERE [ReportId] = @ReportToProcess AND [ClientId] > 

			(SELECT TOP 1 [ClientId]  

				FROM ReportSchedule

				ORDER BY [UtcLastActualRunDate] DESC);



	--- IF NO REPORT CLIENT Id IS FOUND GREATER THAN LAST PROCESSED ONE, GO BACK TO THE START OF THE CLIENT LIST ---

	IF (@ClientToProcess IS NULL)	

	BEGIN

		SELECT TOP 1 @ClientToProcess = [ClientId]

		FROM [ReportSchedule] 

		WHERE [ReportId] = @ReportToProcess AND IsEnabled = 1 AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 AND (Status <> 1 OR Status is NULL)

		ORDER BY [UtcLastActualRunDate] DESC;

	END;



	----Checking just in case we have bad data

	IF (@ClientToProcess IS NULL)

	BEGIN

		RETURN;

	END;

	--- END: Get the next entry that should be Processed (by app/client priority) ---

	

	DECLARE @ReportScheduleId AS INT;



	SELECT TOP 1 @ReportScheduleId = [ReportScheduleId]

	FROM [ReportSchedule]

	WHERE [ReportId] = @ReportToProcess

		AND [ClientId] = @ClientToProcess

		AND [IsEnabled] = 1

		AND (UtcNextScheduledRunDate - GetUtcDate()) <= 0 

		AND (Status <> 1 OR Status is NULL)

	ORDER BY [UtcLastActualRunDate];

	

	UPDATE [ReportSchedule]

	SET [Status] = 1, [UtcLastActualRunDate] = GETUTCDATE(), 

	[ExecutionCount] = [ReportSchedule].[ExecutionCount] + 1, [ServiceName] = @ServiceName

	WHERE [ReportSchedule].[ReportScheduleId] = @ReportScheduleId;

		

	--- END: Assign a unique process execution Id to the entry ---



	--- Get the entry being Processed ---

	SELECT 

		[ReportSchedule].[ReportScheduleId], 

		[Reports].[ReportName], 

		[Clients].[ClientId],

		[Clients].[ClientName],

		[Reports].[ReportDescription], 

		[ReportSchedule].[Status], 

		[ReportSchedule].[ServiceName], 

		[ReportSchedule].[FrequencyType],

		[ReportSchedule].[FrequencyInterval],

		[ReportSchedule].[UtcNextScheduledRunDate],

		[ReportSchedule].[ExecutionCount],

		[ReportScheduleRecipients].[Email], 

		[ReportScheduleRecipients].[FTPServerIP],

		[ReportScheduleRecipients].[FTPFilePath],

		[ReportScheduleRecipients].[FTPUsername],

		[ReportScheduleRecipients].[FTPPassword],

		[ReportSchedule].[FrequencyDescription]

	FROM [ReportSchedule]

		INNER JOIN [Reports] ON [ReportSchedule].[ReportId] = [Reports].[ReportId]

		INNER JOIN [Clients] ON [Clients].[ClientId] = [ReportSchedule].[ClientId]

		LEFT OUTER JOIN [ReportScheduleRecipients] ON [ReportScheduleRecipients].[ReportScheduleId] = [ReportSchedule].[ReportScheduleId]

	WHERE [ReportSchedule].[ReportScheduleId] = @ReportScheduleId;

	

END;
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ProcessResetReportSchedule]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ProcessResetReportSchedule]
	@ReportScheduleId INT,
	@MaxProcessAttempts INT = 2
AS

IF EXISTS(SELECT 1 FROM [ReportSchedule] WHERE [Status] = 1 AND ExecutionCount >= @MaxProcessAttempts AND ReportScheduleId = @ReportScheduleId)
BEGIN
	
	-- Mark the schedule as failed ---
	UPDATE ReportSchedule
	SET [Status] = 3
	WHERE ReportScheduleId = @ReportScheduleId
END
ELSE
BEGIN
	-- Reset any remaining records and increment ProcessAttempts by 1
	UPDATE ReportSchedule
	SET [Status] = 0, ServiceName = NULL, ExecutionCount = ExecutionCount + 1
	WHERE ReportScheduleId = @ReportScheduleId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ReportScheduleData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ReportScheduleData_CacheDependencyCheck]
AS
BEGIN
	SELECT	ReportScheduleId, COUNT_BIG(*) AS Total
	FROM	dbo.ReportSchedule
	GROUP BY ReportScheduleId;

	SELECT	ReportId, COUNT_BIG(*) AS Total
	FROM	dbo.Reports
	GROUP BY ReportId;
	
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveClient]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetClients]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To add and update the client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClient]	
	@clientId int,
	@clientName NVARCHAR(400),
	@clientconnectionStringName VARCHAR(200),
	@clientdatabaseName VARCHAR (200),
	@verticalMarketId int,
	@Description NVARCHAR(800),
	@modifiedBy int,
	@ClientUsers TT_ClientUsers READONLY	
	--exec [RPV2HostedAdmin_SaveClient] 'Wipro','wiproConnect1','wiproConnect1wiproConnect1',2,'Added for Test',0,sysdate,0,1
	--DROP PROCEDURE [RPV2HostedAdmin_SaveClient]
AS
BEGIN
	
	IF(@clientId=0) 
		BEGIN
		 DECLARE @identityClientId int
			INSERT INTO Clients(
				clientName,
				connectionStringName,
				databaseName,
				verticalMarketId,
				clientDescription,
				utcModifiedDate,
				ModifiedBy) 
			VALUES (
				@clientName,
				@clientconnectionStringName,
				@clientdatabaseName,
				@verticalMarketId,
				@Description,
				GETUTCDATE(),
				@modifiedBy)


			SELECT @identityClientId=SCOPE_IDENTITY()	
				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT @identityClientId,UserId,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers

		END
	ELSE
		BEGIN
			UPDATE Clients 
				SET clientName=@clientName,
				connectionStringName=@clientconnectionStringName,
				databaseName=@clientdatabaseName,
				verticalMarketId=@verticalMarketId,
				clientDescription=@Description,
				ModifiedBy=@modifiedBy,
				utcModifiedDate=GETUTCDATE()
			WHERE ClientId = @clientId
			
			DECLARE @DeletedClientUsers TT_ClientUsers				
				
			 DELETE FROM ClientUsers
			   OUTPUT deleted.ClientId,
					 deleted.UserId
			   INTO @DeletedClientUsers						
			 WHERE ClientId = @clientId 
			   AND UserId NOT IN 
			   (SELECT UserId FROM @ClientUsers)
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'ClientUsers'
					AND	[KEY] = @clientId
					AND [CUDType] = 'D' 
					AND [SecondKey]  IN (SELECT UserId from @DeletedClientUsers)
					AND UserId IS NULL;


			INSERT INTO CLIENTUSERS
			(
				CLIENTID,
				USERID,
				UtcModifiedDate,
				ModifiedBy
			)
			SELECT CLIENTID,UserID,GETUTCDATE(),@modifiedBy 
			FROM @ClientUsers WHERE UserId NOT IN
			(SELECT UserId FROM ClientUsers WHERE ClientId = @clientId)
				

		END


		
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveReportSchedule]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportSchedule]	

	@reportscheduleid int,
	@reportid int,
	@clientid int,
	@isenabled bit,
	@frequencyType int,
	@frequencyInterval int,
	@UtcfirstScheduledRunDate DateTime,
	@UtcLastScheduledRunDate DateTime,
	@email nvarchar(256),
	@ftpServerIP nvarchar(100),
	@ftpFilePath nvarchar(256),
	@ftpUsername nvarchar(50),
	@ftpPassword nvarchar(50),
	@modifiedBy int
AS
BEGIN
DECLARE @identityReportScheduleId int
	
	IF(@reportscheduleid=0) 
		BEGIN
			INSERT
			     INTO
			 ReportSchedule
			               (
							reportId,
							ClientId,
							IsEnabled,
							FrequencyType,
							FrequencyInterval,
							UtcFirstScheduledRunDate,
							UtcLastScheduledRunDate,
							UtcModifiedDate,
							ModifiedBy) 

					VALUES (
							@reportid,
							@clientid,
							@isenabled,
							@frequencyType,
							@frequencyInterval,
							@UtcfirstScheduledRunDate,
							@UtcLastScheduledRunDate,
							GetUtcDate(),
							@modifiedBy)

				SET @identityReportScheduleId = SCOPE_IDENTITY()

				INSERT INTO ReportScheduleRecipients
				(ReportScheduleId,
				Email,
				FTPServerIP,
				FTPFilePath,
			    FTPUsername,
				FTPPassword,
				UtcModifiedDate,
				ModifiedBy
				)
				VALUES(
				@identityReportScheduleId,
				@email,
				@ftpServerIP,
				@ftpFilePath,
				@ftpUsername,
				@ftpPassword,
	            GetUtcDate(),
				@modifiedBy
				)
		END
	ELSE
		BEGIN
			UPDATE ReportSchedule
			
			 SET 
					
					reportId = @reportid,
					ClientId = @clientid,
					IsEnabled = @isenabled,
					FrequencyType = @frequencyType,
					FrequencyInterval = @frequencyInterval,
					UtcFirstScheduledRunDate = @UtcfirstScheduledRunDate,
					UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
					UtcModifiedDate = GetUtcDate(),
					ModifiedBy= @modifiedBy

			WHERE 
			        reportScheduleId = @reportscheduleid

		    UPDATE ReportScheduleRecipients
			
			 SET
			    Email=@email,
				FTPServerIP=@ftpServerIP,
				FTPFilePath=@ftpFilePath,
			    FTPUsername=@ftpUsername,
				FTPPassword=@ftpPassword,
				UtcModifiedDate=GetUtcDate(),
				ModifiedBy = @modifiedBy
				WHERE 
			        ReportScheduleId = @reportscheduleid

		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveReportScheduleEntry]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportScheduleEntry]
(
	@ReportScheduleId INT,
	@Status INT,
	@MaxProcessAttempts INT = 2,
	@UtcLastScheduledRunDate DATETIME
)
AS
BEGIN
	IF(@Status = 2)
	BEGIN
		UPDATE ReportSchedule
		SET 
			Status = @Status,
			UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
			ServiceName = '',
			ExecutionCount = 0
		WHERE ReportScheduleId = @ReportScheduleId
	END
	ELSE IF(@Status = 3)
	BEGIN
		IF EXISTS(SELECT * FROM [ReportSchedule] WHERE [Status] = 1 AND ExecutionCount >= @MaxProcessAttempts AND ReportScheduleId = @ReportScheduleId)
		BEGIN
			-- Mark the schedule as failed ---
			UPDATE ReportSchedule
			SET 
				[Status] = 3,
				UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
				ServiceName = '',
				ExecutionCount = 0
			WHERE ReportScheduleId = @ReportScheduleId
		END
		ELSE
		BEGIN
			-- Reset any remaining records and increment ProcessAttempts by 1
			UPDATE ReportSchedule
			SET [Status] = 0, ServiceName = NULL
			WHERE ReportScheduleId = @ReportScheduleId
		END
	END
	ELSE
	BEGIN
		UPDATE ReportSchedule
		SET 
			Status = @Status,
			UtcLastScheduledRunDate = @UtcLastScheduledRunDate,
			ServiceName = ''		
		WHERE ReportScheduleId = @ReportScheduleId
	END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveRoles]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveRoles]
	Added By: Noel
	Date: 09/19/2015
	Reason : To add and update the role
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveRoles]	
	@RoleId int,
	@Name NVARCHAR(256),
	@modifiedBy int
AS
BEGIN
	
	IF(@RoleId=0) 
	BEGIN
	  INSERT INTO Roles(Name,
						UtcModifiedDate,
						ModifiedBy)
				VALUES(@Name,
						GETUTCDATE(),
						@modifiedBy)
	END
	ELSE
	BEGIN
	  UPDATE Roles
	   SET Name = @Name,
	   UtcModifiedDate = GETUTCDATE(),
	   ModifiedBy = @modifiedBy	    
	END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveUser]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveUser]
	Added By: Ashok
	Date: 09/07/2015
	Updated : 09/19/2015 By Noel Dsouza
	Reason : To add and update the client
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUser] 
				 @userId int,
                 @email nVARCHAR(512),
				 @emailConfirmed bit,
                 @passwordHash nvarchar,                
				 @securityStamp nvarchar,
                 @PhoneNumber nvarchar, 
				 @phoneNumberConfirmed bit, 
				 @twoFactorEnabled bit,
				 @userName nvarchar(512),
				 @firstName nvarchar(200),
				 @lastName nvarchar(200),
				 @modifiedBy int,
				 @UserRoles TT_UserRoles READONLY,
				 @ClientUsers TT_ClientUsers READONLY	
--DROP PROCEDURE [RPV2HostedAdmin_SaveUser]
AS
BEGIN 
	IF @userId = 0 
	BEGIN
	   DECLARE @identityUserId int
					INSERT INTO USERS(					
										Email,
										EmailConfirmed,
										PasswordHash,
										SecurityStamp,
										PhoneNumber,
										PhoneNumberConfirmed,
										TwoFactorEnabled,
										UserName,
										FirstName,
										LastName,					
										UtcModifiedDate,
										ModifiedBy									
										) 
								VALUES (
										@email ,
										@emailConfirmed,
										@passwordHash,
										@securityStamp,
										@PhoneNumber,
										@phoneNumberConfirmed,
										@twoFactorEnabled,
										@userName,
										@firstName,
										@lastName,
										GETUTCDATE(),
										@modifiedBy									
										)
					
				SELECT @identityUserId=SCOPE_IDENTITY()			
			

				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT CLIENTID,@identityUserId,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers
				
				
				INSERT INTO UserRoles
				(
					USERID,				
					RoleId,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT @identityUserId,RoleID,GETUTCDATE(),@modifiedBy
				 FROM @UserRoles


	END 
	ELSE
		BEGIN
			UPDATE USERS
				SET 														
					Email=@email ,
					EmailConfirmed=@emailConfirmed,
					PasswordHash=@passwordHash,
					SecurityStamp=@securityStamp,
					PhoneNumber=@PhoneNumber,
					PhoneNumberConfirmed=@phoneNumberConfirmed,
					TwoFactorEnabled=@twoFactorEnabled,
					UserName=@userName,
					FirstName=@firstName,
					LastName=@lastName,
					UtcModifiedDate=GETUTCDATE(),
					ModifiedBy=@modifiedBy
				WHERE UserId=@userId
				
				DECLARE @DeletedClientUsers TT_ClientUsers				
				
				DELETE FROM ClientUsers
				  OUTPUT deleted.ClientId,
						 deleted.UserId
				  INTO @DeletedClientUsers						
				 WHERE UserId = @userId 
				   AND ClientId NOT IN 
				   (SELECT ClientId FROM @ClientUsers)
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'ClientUsers'
					AND	[SecondKey] = @userId
					AND [CUDType] = 'D' 
					AND [Key]  in (SELECT ClientID from @DeletedClientUsers)
					AND UserId IS NULL;
				   
				   
				DECLARE @DeletedUserRoles TT_UserRoles
				
				DELETE FROM UserRoles
				  OUTPUT deleted.UserId,
						 deleted.RoleId
				  INTO @DeletedUserRoles						
				 WHERE UserId = @userId 
				   AND RoleId NOT IN 
				   (SELECT RoleID FROM @UserRoles)		
				   
				UPDATE	CUDHistory				 
  				 SET	UserId = @modifiedBy
				WHERE	TableName = N'UserRoles'
					AND	[Key] = @userId
					AND [CUDType] = 'D' 
					AND [SecondKey]  in (SELECT RoleID from @DeletedUserRoles)
					AND UserId IS NULL;
				   		   
				 
				
				INSERT INTO CLIENTUSERS
				(
					CLIENTID,
					USERID,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT CLIENTID,UserID,GETUTCDATE(),@modifiedBy 
				FROM @ClientUsers WHERE ClientID NOT IN
				(SELECT CLIENTID FROM ClientUsers WHERE UserId = @userId)
				
				
				INSERT INTO UserRoles
				(
					USERID,				
					RoleId,
					UtcModifiedDate,
					ModifiedBy
				)
				SELECT UserID,RoleID,GETUTCDATE(),@modifiedBy
				 FROM @UserRoles WHERE RoleID NOT IN
				(SELECT RoleID FROM UserRoles  WHERE UserId = @userId)


	END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveUserRoles]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUserRoles]
@UserId int,
@RoleId int,
@ModifiedBy int
AS
BEGIN
  IF EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @UserId )
		 BEGIN
			UPDATE UserRoles
			SET 
				RoleId = @RoleId,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE UserId = @UserId	
		 END
   ELSE
          BEGIN
		       INSERT INTO UserRoles(
			USerId,
			RoleId,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@UserId,
			@RoleId,
		    GETUTCDATE(),
			@ModifiedBy
			)
		  END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplateFeature_CacheDependencyCheck]...';


GO

CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_StaticTemplateFeature_CacheDependencyCheck]
AS
BEGIN
    SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
	
	SELECT TemplateId,[Key],COUNT_BIG(*) AS Total
	FROM dbo.TemplateFeature
	GROUP BY TemplateId,[Key]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplatePage_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_StaticTemplatePage_CacheDependencyCheck
AS
BEGIN
 	SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
   	SELECT	PageID, COUNT_BIG(*) AS Total
	FROM	dbo.Page
	GROUP BY PageID;
	
	SELECT TemplateId,PageId,COUNT_BIG(*) AS Total
	FROM dbo.TemplatePage
	GROUP BY TemplateId,PageId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplatePageFeature_CacheDependencyCheck]...';


GO


CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_StaticTemplatePageFeature_CacheDependencyCheck]
AS
BEGIN
    SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
	
	SELECT TemplateId,[Key],COUNT_BIG(*) AS Total
	FROM dbo.TemplatePageFeature
	GROUP BY TemplateId,[Key]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplatePageNavigation_CacheDependencyCheck]...';


GO
create PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplatePageNavigation_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageNavigation
	GROUP BY TemplateID,NavigationKey;

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplatePageText_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_StaticTemplatePageText_CacheDependencyCheck
AS
BEGIN
 	SELECT	TemplateID,PageId,ResourceKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageText
	GROUP BY TemplateID,PageId,ResourceKey;

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplateSiteNavigation_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplateSiteNavigation_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplateNavigation
	GROUP BY TemplateID,NavigationKey;

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticTemplateText_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticTemplateText_CacheDependencyCheck]
AS
BEGIN
 	SELECT	TemplateID,ResourceKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplateText
	GROUP BY TemplateID,ResourceKey;

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetClientsData]...';


GO
CREATE Procedure [dbo].RPV2HostedSites_GetClientsData
AS
BEGIN

	SELECT Clients.ClientID,
			ClientName,
			DNS as ClientDNS,
			Clients.ConnectionStringName AS ClientConnectionStringName,
			Clients.DatabaseName AS ClientDatabaseName,
			VerticalMarkets.ConnectionStringName as VerticalMarketConnectionStringName,
			VerticalMarkets.DatabaseName as VerticalMarketsDatabaseName
	FROM Clients
		INNER JOIN VerticalMarkets ON Clients.VerticalMarketId = VerticalMarkets.VerticalMarketId
		LEFT OUTER JOIN ClientDns on Clients.ClientId = ClientDns.ClientId

	SELECT  TemplatePage.TemplateId,
		Template.[Name] AS TemplateName,
		Page.PageID,
		Page.Name as PageName  
	FROM Template
	INNER JOIN TemplatePage ON Template.TemplateID = TemplatePage.TemplateID
	INNER JOIN Page ON TemplatePage.PageId =  Page.PageID
	ORDER BY TemplatePage.TemplateId,Page.PageID

	
	SELECT  TemplateId, NavigationKey, XslTransform, DefaultNavigationXml
	FROM TemplateNavigation


	SELECT  TemplateId, PageId, NavigationKey, XslTransform, DefaultNavigationXml
	FROM TemplatePageNavigation

	
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_StaticClientsData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticClientsData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	ClientID, COUNT_BIG(*) AS Total
	FROM	dbo.ClientDns
	GROUP BY ClientID;
	
   	SELECT	ClientID, COUNT_BIG(*) AS Total
	FROM	dbo.Clients
	GROUP BY ClientID;
	
	SELECT	VerticalMarketId, COUNT_BIG(*) AS Total
	FROM	dbo.VerticalMarkets
	GROUP BY VerticalMarketId;

	SELECT	TemplateID, COUNT_BIG(*) AS Total
	FROM	dbo.Template
	GROUP BY TemplateID;
	
   	SELECT	PageID, COUNT_BIG(*) AS Total
	FROM	dbo.Page
	GROUP BY PageID;

	SELECT	ClientId, COUNT_BIG(*) AS Total
	FROM	dbo.ClientUsers
	GROUP BY ClientId;

	SELECT	TemplateId, NavigationKey,  COUNT_BIG(*) AS Total
	FROM	dbo.TemplateNavigation
	GROUP BY TemplateId, NavigationKey;

	SELECT	TemplateId, PageId, NavigationKey, COUNT_BIG(*) AS Total
	FROM	dbo.TemplatePageNavigation
	GROUP BY TemplateId, PageId, NavigationKey;
	
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_StaticRolesData_CacheDependencyCheck]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedSites_StaticRolesData_CacheDependencyCheck]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : Cache Dependency Roles
*/

CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticRolesData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	RoleId, COUNT_BIG(*) AS Total
	FROM	dbo.Roles
	GROUP BY RoleId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_StaticUsersData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel
-- Create date: 18-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticUsersData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	UserId, COUNT_BIG(*) AS Total
	FROM	dbo.[Users]
	GROUP BY UserId;

	SELECT	ClientId, COUNT_BIG(*) AS Total
	FROM	dbo.ClientUsers
	GROUP BY ClientId;

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_StaticVerticalMarketsData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_StaticVerticalMarketsData_CacheDependencyCheck]
AS
BEGIN
  
	SELECT	VerticalMarketId, COUNT_BIG(*) AS Total
	FROM	dbo.VerticalMarkets
	GROUP BY VerticalMarketId;

END
GO
PRINT N'Creating [dbo].[T_ClientDns_D]...';


GO
-- Delete trigger for ClientDns
CREATE TRIGGER T_ClientDns_D
ON dbo.ClientDns
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDnsId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDnsId,
			CONVERT(NVARCHAR(MAX), d.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), d.Dns) AS Dns
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientId' THEN d.ClientId
		WHEN 'Dns' THEN d.Dns
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDnsId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDns_I]...';


GO
-- Insert trigger for ClientDns
CREATE TRIGGER T_ClientDns_I
ON dbo.ClientDns
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDnsId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDnsId,
			CONVERT(NVARCHAR(MAX), i.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), i.Dns) AS Dns
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientId' THEN i.ClientId
		WHEN 'Dns' THEN i.Dns
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDns_U]...';


GO
-- Update trigger for ClientDns
CREATE TRIGGER T_ClientDns_U
ON dbo.ClientDns
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDns';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDnsId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDnsId,
			CONVERT(NVARCHAR(MAX), i.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), i.Dns) AS Dns
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDnsId,
			CONVERT(NVARCHAR(MAX), d.ClientId) AS ClientId,
			CONVERT(NVARCHAR(MAX), d.Dns) AS Dns
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientId' THEN d.ClientId
			WHEN 'Dns' THEN d.Dns
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ClientId' THEN i.ClientId
			WHEN 'Dns' THEN i.Dns
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDnsId = d.ClientDnsId
			CROSS JOIN (VALUES('ClientId'), ('Dns')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDnsId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Client_U]...';


GO
-- Update trigger for Client
CREATE TRIGGER T_Client_U
ON dbo.Clients
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
			CONVERT(NVARCHAR(MAX), i.ClientName) AS ClientName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.VerticalMarketId) as VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.ClientDescription) as ClientDescription
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientId,
			CONVERT(NVARCHAR(MAX), d.ClientName) AS ClientName,
			CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), d.VerticalMarketId) as VerticalMarketId,
			CONVERT(NVARCHAR(MAX), d.ClientDescription) as ClientDescription
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientName' THEN d.ClientName
			WHEN 'ConnectionStringName' THEN d.ConnectionStringName
			WHEN 'DatabaseName' THEN d.DatabaseName
			WHEN 'VerticalMarketId' THEN d.VerticalMarketId
			WHEN 'ClientDescription' THEN d.ClientDescription		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ClientName' THEN i.ClientName
			WHEN 'ConnectionStringName' THEN i.ConnectionStringName
			WHEN 'DatabaseName' THEN i.DatabaseName
			WHEN 'VerticalMarketId' THEN i.VerticalMarketId
			WHEN 'ClientDescription' THEN i.ClientDescription		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientId = d.ClientId
			CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Client_D]...';


GO
-- Delete trigger for Client
CREATE TRIGGER T_Client_D
ON dbo.Clients
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientId,
		CONVERT(NVARCHAR(MAX), d.ClientName) AS ClientName,
		CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
		CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
		CONVERT(NVARCHAR(MAX), d.VerticalMarketId) as VerticalMarketId,
		CONVERT(NVARCHAR(MAX), d.ClientDescription) as ClientDescription
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientName' THEN d.ClientName
		WHEN 'ConnectionStringName' THEN d.ConnectionStringName
		WHEN 'DatabaseName' THEN d.DatabaseName
		WHEN 'VerticalMarketId' THEN d.VerticalMarketId
		WHEN 'ClientDescription' THEN d.ClientDescription		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Client_I]...';


GO
-- Insert trigger for Client
CREATE TRIGGER T_Client_I
ON dbo.Clients
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Clients';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
			CONVERT(NVARCHAR(MAX), i.ClientName) AS ClientName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.VerticalMarketId) as VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.ClientDescription) as ClientDescription
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientName' THEN i.ClientName
		WHEN 'ConnectionStringName' THEN i.ConnectionStringName
		WHEN 'DatabaseName' THEN i.DatabaseName
		WHEN 'VerticalMarketId' THEN i.VerticalMarketId
		WHEN 'ClientDescription' THEN i.ClientDescription		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('ClientName'), ('ConnectionStringName'), ('DatabaseName'), ('VerticalMarketId'), ('ClientDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientUsers_D]...';


GO
-- Delete trigger for ClientUsers
CREATE TRIGGER T_ClientUsers_D
ON dbo.ClientUsers
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientUsers';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.ClientId, d.UserId, 'D'
	FROM	deleted d;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientUsers_U]...';


GO
-- Update trigger for ClientUsers
CREATE TRIGGER T_ClientUsers_U
ON dbo.ClientUsers
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientUsers';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientId, i.UserId, 'U', i.ModifiedBy
	FROM	inserted i;
	
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientUsers_I]...';


GO
-- Insert trigger for ClientUsers
CREATE TRIGGER T_ClientUsers_I
ON dbo.ClientUsers
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientUsers';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientId, i.UserId, 'I', i.ModifiedBy
	FROM	inserted i;
		
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Roles_U]...';


GO
-- Update trigger for Roles
CREATE TRIGGER T_Roles_U
ON dbo.Roles
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Roles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.RoleId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.RoleId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.RoleId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.RoleId = d.RoleId
			CROSS JOIN (VALUES('Name')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.RoleId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Roles_D]...';


GO
-- Delete trigger for Roles
CREATE TRIGGER T_Roles_D
ON dbo.Roles
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Roles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.RoleId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.RoleId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.RoleId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Roles_I]...';


GO
-- Insert trigger for Roles
CREATE TRIGGER T_Roles_I
ON dbo.Roles
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Roles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.RoleId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.RoleId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.RoleId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UserRoles_D]...';


GO
-- Delete trigger for UserRoles
CREATE TRIGGER T_UserRoles_D
ON dbo.UserRoles
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.UserId,d.RoleId, 'D'
	FROM	deleted d;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UserRoles_U]...';


GO
-- Update trigger for UserRoles
CREATE TRIGGER T_UserRoles_U
ON dbo.UserRoles
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.UserId,i.RoleId, 'U', i.ModifiedBy
	FROM	inserted i;
	
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UserRoles_I]...';


GO
-- Insert trigger for UserRoles
CREATE TRIGGER T_UserRoles_I
ON dbo.UserRoles
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UserRoles';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.UserId,i.RoleId, 'I', i.ModifiedBy
	FROM	inserted i;
	

			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Users_D]...';


GO
-- Delete trigger for Users
CREATE TRIGGER T_Users_D
ON dbo.Users
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.UserId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.UserId,
			CONVERT(NVARCHAR(MAX), d.Email) AS Email,
			CONVERT(NVARCHAR(MAX), d.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), d.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), d.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), d.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), d.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), d.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), d.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), d.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), d.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), d.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), d.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), d.LastName) AS LastName			
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Email' THEN d.Email
			WHEN 'EmailConfirmed' THEN d.EmailConfirmed
			WHEN 'PasswordHash' THEN d.PasswordHash
			WHEN 'SecurityStamp' THEN d.SecurityStamp
			WHEN 'PhoneNumber' THEN d.PhoneNumber
			WHEN 'PhoneNumberConfirmed' THEN d.PhoneNumberConfirmed
			WHEN 'TwoFactorEnabled' THEN d.TwoFactorEnabled
			WHEN 'LockOutEndDateUtc' THEN d.LockOutEndDateUtc
			WHEN 'LockoutEnabled' THEN d.LockoutEnabled
			WHEN 'AccessFailedCount' THEN d.AccessFailedCount
			WHEN 'UserName' THEN d.UserName
			WHEN 'FirstName' THEN d.FirstName
			WHEN 'LastName' THEN d.LastName
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.UserId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Users_U]...';


GO
-- Update trigger for Users
CREATE TRIGGER T_Users_U
ON dbo.Users
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UserId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UserId,
			CONVERT(NVARCHAR(MAX), i.Email) AS Email,
			CONVERT(NVARCHAR(MAX), i.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), i.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), i.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), i.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), i.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), i.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), i.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), i.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), i.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), i.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), i.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), i.LastName) AS LastName			
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.UserId,
			CONVERT(NVARCHAR(MAX), d.Email) AS Email,
			CONVERT(NVARCHAR(MAX), d.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), d.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), d.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), d.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), d.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), d.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), d.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), d.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), d.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), d.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), d.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), d.LastName) AS LastName			
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Email' THEN d.Email
			WHEN 'EmailConfirmed' THEN d.EmailConfirmed
			WHEN 'PasswordHash' THEN d.PasswordHash
			WHEN 'SecurityStamp' THEN d.SecurityStamp
			WHEN 'PhoneNumber' THEN d.PhoneNumber
			WHEN 'PhoneNumberConfirmed' THEN d.PhoneNumberConfirmed
			WHEN 'TwoFactorEnabled' THEN d.TwoFactorEnabled
			WHEN 'LockOutEndDateUtc' THEN d.LockOutEndDateUtc
			WHEN 'LockoutEnabled' THEN d.LockoutEnabled
			WHEN 'AccessFailedCount' THEN d.AccessFailedCount
			WHEN 'UserName' THEN d.UserName
			WHEN 'FirstName' THEN d.FirstName
			WHEN 'LastName' THEN d.LastName
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Email' THEN i.Email
			WHEN 'EmailConfirmed' THEN i.EmailConfirmed
			WHEN 'PasswordHash' THEN i.PasswordHash
			WHEN 'SecurityStamp' THEN i.SecurityStamp
			WHEN 'PhoneNumber' THEN i.PhoneNumber
			WHEN 'PhoneNumberConfirmed' THEN i.PhoneNumberConfirmed
			WHEN 'TwoFactorEnabled' THEN i.TwoFactorEnabled
			WHEN 'LockOutEndDateUtc' THEN i.LockOutEndDateUtc
			WHEN 'LockoutEnabled' THEN i.LockoutEnabled
			WHEN 'AccessFailedCount' THEN i.AccessFailedCount
			WHEN 'UserName' THEN i.UserName
			WHEN 'FirstName' THEN i.FirstName
			WHEN 'LastName' THEN i.LastName
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.UserId = d.UserId
					CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.UserId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Users_I]...';


GO
-- Insert trigger for Users
CREATE TRIGGER T_Users_I
ON dbo.Users
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UserId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UserId,
			CONVERT(NVARCHAR(MAX), i.Email) AS Email,
			CONVERT(NVARCHAR(MAX), i.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), i.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), i.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), i.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), i.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), i.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), i.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), i.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), i.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), i.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), i.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), i.LastName) AS LastName			
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Email' THEN i.Email
		WHEN 'EmailConfirmed' THEN i.EmailConfirmed
		WHEN 'PasswordHash' THEN i.PasswordHash
		WHEN 'SecurityStamp' THEN i.SecurityStamp
		WHEN 'PhoneNumber' THEN i.PhoneNumber
		WHEN 'PhoneNumberConfirmed' THEN i.PhoneNumberConfirmed
		WHEN 'TwoFactorEnabled' THEN i.TwoFactorEnabled
		WHEN 'LockOutEndDateUtc' THEN i.LockOutEndDateUtc
		WHEN 'LockoutEnabled' THEN i.LockoutEnabled
		WHEN 'AccessFailedCount' THEN i.AccessFailedCount
		WHEN 'UserName' THEN i.UserName
		WHEN 'FirstName' THEN i.FirstName
		WHEN 'LastName' THEN i.LastName
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.UserId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_VerticalMarkets_D]...';


GO
-- Delete trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_D
ON dbo.VerticalMarkets
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.VerticalMarketId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.VerticalMarketId,
		CONVERT(NVARCHAR(MAX), d.MarketName) AS MarketName,
		CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
		CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
		CONVERT(NVARCHAR(MAX), d.MarketDescription) as MarketDescription
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MarketName' THEN d.MarketName
		WHEN 'ConnectionStringName' THEN d.ConnectionStringName
		WHEN 'DatabaseName' THEN d.DatabaseName
		WHEN 'MarketDescription' THEN d.MarketDescription		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'),  ('MarketDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.VerticalMarketId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_VerticalMarkets_U]...';


GO
-- Update trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_U
ON dbo.VerticalMarkets
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.VerticalMarketId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.MarketDescription) as MarketDescription
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), d.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), d.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), d.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), d.MarketDescription) as MarketDescription
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'MarketName' THEN d.MarketName
			WHEN 'ConnectionStringName' THEN d.ConnectionStringName
			WHEN 'DatabaseName' THEN d.DatabaseName
			WHEN 'MarketDescription' THEN d.MarketDescription		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'MarketName' THEN i.MarketName
			WHEN 'ConnectionStringName' THEN i.ConnectionStringName
			WHEN 'DatabaseName' THEN i.DatabaseName
			WHEN 'MarketDescription' THEN i.MarketDescription		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.VerticalMarketId = d.VerticalMarketId
			CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'), ('MarketDescription')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.VerticalMarketId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_VerticalMarkets_I]...';


GO
-- Insert trigger for VerticalMarkets
CREATE TRIGGER T_VerticalMarkets_I
ON dbo.VerticalMarkets
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'VerticalMarkets';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.VerticalMarketId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.VerticalMarketId,
			CONVERT(NVARCHAR(MAX), i.MarketName) AS MarketName,
			CONVERT(NVARCHAR(MAX), i.ConnectionStringName) AS ConnectionStringName,
			CONVERT(NVARCHAR(MAX), i.DatabaseName) AS DatabaseName,
			CONVERT(NVARCHAR(MAX), i.MarketDescription) as MarketDescription
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MarketName' THEN i.MarketName
		WHEN 'ConnectionStringName' THEN i.ConnectionStringName
		WHEN 'DatabaseName' THEN i.DatabaseName
		WHEN 'MarketDescription' THEN i.MarketDescription		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('MarketName'), ('ConnectionStringName'), ('DatabaseName'), ('MarketDescription')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.VerticalMarketId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a6be8ca2-879c-49fd-9a3c-7d0feabe5b27')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a6be8ca2-879c-49fd-9a3c-7d0feabe5b27')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '6b6891fb-4b74-443e-8ffb-c372e977c8b5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('6b6891fb-4b74-443e-8ffb-c372e977c8b5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9c81b667-8f59-47d2-a1d1-59d96379212c')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9c81b667-8f59-47d2-a1d1-59d96379212c')

GO


PRINT N'Checking existing data against newly created constraints';
GO

ALTER TABLE [dbo].[ClientDns] WITH CHECK CHECK CONSTRAINT [fk_Clients];

ALTER TABLE [dbo].[Clients] WITH CHECK CHECK CONSTRAINT [fk1_Clients];

ALTER TABLE [dbo].[ClientUsers] WITH CHECK CHECK CONSTRAINT [fk1_ClientUsers];

ALTER TABLE [dbo].[ClientUsers] WITH CHECK CHECK CONSTRAINT [fk2_ClientUsers];

ALTER TABLE [dbo].[CUDHistoryData] WITH CHECK CHECK CONSTRAINT [fk_CUDHistoryData];

ALTER TABLE [dbo].[TemplateFeature] WITH CHECK CHECK CONSTRAINT [fk1_TemplateFeature];

ALTER TABLE [dbo].[TemplateNavigation] WITH CHECK CHECK CONSTRAINT [fk_TemplateNavigation1];

ALTER TABLE [dbo].[TemplatePage] WITH CHECK CHECK CONSTRAINT [fk1_TemplatePage];

ALTER TABLE [dbo].[TemplatePage] WITH CHECK CHECK CONSTRAINT [fk2_TemplatePage];

ALTER TABLE [dbo].[TemplatePageFeature] WITH CHECK CHECK CONSTRAINT [fk1_TemplatePageFeature];

ALTER TABLE [dbo].[TemplatePageFeature] WITH CHECK CHECK CONSTRAINT [fk2_TemplatePageFeature];

ALTER TABLE [dbo].[TemplatePageNavigation] WITH CHECK CHECK CONSTRAINT [fk_TemplatePageNavigation1];

ALTER TABLE [dbo].[TemplatePageNavigation] WITH CHECK CHECK CONSTRAINT [fk_TemplatePageNavigation2];

ALTER TABLE [dbo].[TemplatePageText] WITH CHECK CHECK CONSTRAINT [fk_TemplatePageText1];

ALTER TABLE [dbo].[TemplatePageText] WITH CHECK CHECK CONSTRAINT [fk_TemplatePageText2];

ALTER TABLE [dbo].[TemplateText] WITH CHECK CHECK CONSTRAINT [fk_TemplateText];

ALTER TABLE [dbo].[UserRoles] WITH CHECK CHECK CONSTRAINT [fk_Roles];

ALTER TABLE [dbo].[UserRoles] WITH CHECK CHECK CONSTRAINT [fk_Users];

ALTER TABLE [dbo].[ReportSchedule] WITH CHECK CHECK CONSTRAINT [FK_ReportSchedule_Clients];

ALTER TABLE [dbo].[ReportSchedule] WITH CHECK CHECK CONSTRAINT [FK_ReportSchedule_Reports];

ALTER TABLE [dbo].[ReportScheduleRecipients] WITH CHECK CHECK CONSTRAINT [FK_ReportScheduleRecipients_ReportSchedule];

ALTER TABLE [dbo].[ReportSchedule] WITH CHECK CHECK CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval];


GO
PRINT N'Update complete.';


GO
