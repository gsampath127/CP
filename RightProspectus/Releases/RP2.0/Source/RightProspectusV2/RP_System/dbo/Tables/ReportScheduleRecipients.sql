CREATE TABLE [dbo].[ReportScheduleRecipients] (
    [ReportScheduleId] INT            NOT NULL,
    [Email]            NVARCHAR (256) NULL,
    [FTPServerIP]      NVARCHAR (100) NULL,
    [FTPFilePath]      NVARCHAR (256) NULL,
    [FTPName]          NVARCHAR (50)  NULL,
    [FTPUsername]      NVARCHAR (50)  NULL,
    [FTPPassword]      NVARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME       CONSTRAINT [DF_ReportScheduleRecipients_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       INT            NULL,
    CONSTRAINT [PK_ReportScheduleRecipients_1] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC),
    CONSTRAINT [FK_ReportScheduleRecipients_ReportSchedule] FOREIGN KEY ([ReportScheduleId]) REFERENCES [dbo].[ReportSchedule] ([ReportScheduleId]),
    CONSTRAINT [IX_ReportScheduleRecipients] UNIQUE NONCLUSTERED ([ReportScheduleId] ASC)
);



