CREATE TABLE [dbo].[ReportSchedule] (
    [ReportScheduleId]         INT            IDENTITY (1, 1) NOT NULL,
    [ReportId]                 INT            NOT NULL,
    [ClientId]                 INT            NOT NULL,    
    [Status]                   INT            NULL,
    [FrequencyType]            INT            NOT NULL,
    [FrequencyInterval]        INT            NOT NULL,
    [UtcFirstScheduledRunDate] SMALLDATETIME  NOT NULL,
    [UtcLastScheduledRunDate]  SMALLDATETIME  NULL,
    [UtcLastActualRunDate]     DATETIME       NULL,
    [ServiceName]              NVARCHAR (100) NULL,
    [ExecutionCount]           INT            CONSTRAINT [DF_ReportSchedule_ExecutionCount] DEFAULT ((0)) NULL,
    [UtcModifiedDate]          DATETIME       CONSTRAINT [DF_ReportSchedule_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]               INT            NULL,    
	[UtcDataStartDate] [datetime] NULL,
	[UtcDataEndDate] [datetime] NULL,
	[UtcScheduledEndDate] [datetime] NULL,
	[WeekDays] [varchar](20) NULL,
	[UtcLastDataEndDate] [datetime] NULL,
	[FrequencyDescription]  AS ([dbo].[fnFrequencyDescription]([FrequencyType],[FrequencyInterval],[WeekDays])) PERSISTED,
	[UtcNextDataEndDate]  AS ([dbo].[fnDataEndDateUtc]([UtcFirstScheduledRunDate],[UtcLastScheduledRunDate],[UtcDataEndDate],[UtcLastDataEndDate],[FrequencyType],[FrequencyInterval],[WeekDays])) PERSISTED,
	[UtcNextScheduledRunDate]  AS ([dbo].[fnNextReportDateUtc]([UtcFirstScheduledRunDate],[UtcLastScheduledRunDate],[FrequencyType],[FrequencyInterval],[WeekDays])) PERSISTED,
	[IsEnabled]  AS ([dbo].[fnIsEnabled]([dbo].[fnNextReportDateUtc]([UtcFirstScheduledRunDate],[UtcLastScheduledRunDate],[FrequencyType],[FrequencyInterval],[WeekDays]),[UtcScheduledEndDate])) PERSISTED,
    CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC),
    CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval] CHECK ([dbo].[fnReportScheduleTypeAndIntervalCheck]([FrequencyType],[FrequencyInterval])=(1)),
    CONSTRAINT [FK_ReportSchedule_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]),
    CONSTRAINT [FK_ReportSchedule_Reports] FOREIGN KEY ([ReportId]) REFERENCES [dbo].[Reports] ([ReportId])
);



