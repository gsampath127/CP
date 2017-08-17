CREATE TABLE [dbo].[ReportSchedule] (
    [ReportScheduleId]        INT      IDENTITY (1, 1) NOT NULL,
    [ReportId]                INT      NOT NULL,
    [IsEnabled]               BIT      CONSTRAINT [DF_ReportSchedule_IsEnabled] DEFAULT ((1)) NOT NULL,
    [FrequencyType]           INT      NOT NULL,
    [FrequencyInterval]       INT      NOT NULL,
    [UtcFirstRunDate]         DATETIME NOT NULL,
    [UtcLastScheduledRunDate] DATETIME NULL,
    [UtcLastRunDate]          DATETIME NULL,
    [UtcModifiedDate]         DATETIME CONSTRAINT [DF_ReportSchedule_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]              INT      NULL,
    [UtcNextRunDate]          AS       ([dbo].[fnNextReportDateUtc]([UtcFirstRunDate],[UtcLastScheduledRunDate],[FrequencyType],[FrequencyInterval])) PERSISTED,
    [FrequencyDescription]    AS       ([dbo].[fnFrequencyDescription]([FrequencyType],[FrequencyInterval])) PERSISTED,
    CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC),
    CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval] CHECK ([dbo].[fnReportScheduleTypeAndIntervalCheck]([FrequencyType],[FrequencyInterval])=(1))
);

