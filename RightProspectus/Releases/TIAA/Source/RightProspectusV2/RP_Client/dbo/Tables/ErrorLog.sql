CREATE TABLE [dbo].[ErrorLog] (
    [ErrorLogId]       INT             IDENTITY (1, 1) NOT NULL,
    [ErrorCode]        INT             NOT NULL,
    [ErrorUtcDate]     DATETIME        NOT NULL,
    [Priority]         INT             NOT NULL,
    [Severity]         NVARCHAR (32)   NOT NULL,
    [Title]            NVARCHAR (256)  NOT NULL,
    [MachineName]      NVARCHAR (32)   NOT NULL,
    [AppDomainName]    NVARCHAR (512)  NOT NULL,
    [ProcessID]        NVARCHAR (256)  NOT NULL,
    [ProcessName]      NVARCHAR (512)  NOT NULL,
    [ThreadName]       NVARCHAR (512)  NULL,
    [Win32ThreadId]    NVARCHAR (128)  NULL,
    [EventId]          INT             NULL,
    [SiteActivityId]   INT             NULL,
    [Message]          NVARCHAR (1500) NULL,
    [FormattedMessage] NVARCHAR (MAX)  NULL,
    [URL] NVARCHAR(500) NULL, 
    [AbsoluteURL] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([ErrorLogId] ASC),
    CONSTRAINT [FK_ErrorLog_SiteActivity] FOREIGN KEY ([SiteActivityId]) REFERENCES [dbo].[SiteActivity] ([SiteActivityId])
);

