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

