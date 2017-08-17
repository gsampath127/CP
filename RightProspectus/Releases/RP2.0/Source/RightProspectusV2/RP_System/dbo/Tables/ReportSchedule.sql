CREATE TABLE [dbo].[ReportSchedule] (
    [ReportScheduleId]         INT            IDENTITY (1, 1) NOT NULL,
    [ReportId]                 INT            NOT NULL,
    [ClientId]                 INT            NOT NULL,
    [IsEnabled]                BIT            CONSTRAINT [DF_ReportSchedule_IsEnabled] DEFAULT ((1)) NOT NULL,
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
    [UtcNextScheduledRunDate]  AS             ([dbo].[fnNextReportDateUtc]([UtcFirstScheduledRunDate],[UtcLastScheduledRunDate],[FrequencyType],[FrequencyInterval])) PERSISTED,
    [FrequencyDescription]     AS             ([dbo].[fnFrequencyDescription]([FrequencyType],[FrequencyInterval])) PERSISTED,
    CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC),
    CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval] CHECK ([dbo].[fnReportScheduleTypeAndIntervalCheck]([FrequencyType],[FrequencyInterval])=(1)),
    CONSTRAINT [FK_ReportSchedule_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]),
    CONSTRAINT [FK_ReportSchedule_Reports] FOREIGN KEY ([ReportId]) REFERENCES [dbo].[Reports] ([ReportId])
);



