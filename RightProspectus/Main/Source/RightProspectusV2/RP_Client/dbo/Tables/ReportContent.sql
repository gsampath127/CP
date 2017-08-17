CREATE TABLE [dbo].[ReportContent] (
    [ReportContentId]  INT             IDENTITY (1, 1) NOT NULL,
    [ReportScheduleId] INT             NOT NULL,
    [ReportRunDate]    DATETIME        NOT NULL,
    [FileName]         NVARCHAR (260)  NOT NULL,
    [MimeType]         NVARCHAR (127)  NOT NULL,
    [IsPrivate]        BIT             NOT NULL,
    [ContentUri]       NVARCHAR (2083) NULL,
    [Name]             NVARCHAR (100)  NULL,
    [Description]      NVARCHAR (400)  NULL,
    [UtcModifiedDate]  DATETIME        CONSTRAINT [DF_ReportContent_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       INT             NULL,
    CONSTRAINT [PK_ReportContent] PRIMARY KEY CLUSTERED ([ReportContentId] ASC)
);




GO
CREATE TRIGGER [dbo].[trg_ReportContent_UtcModifiedDate]
ON dbo.ReportContent
AFTER UPDATE
AS
    UPDATE dbo.ReportContent
    SET UtcModifiedDate = GETUTCDATE()
    WHERE [ReportContentId] 
	   IN 
	      (SELECT DISTINCT [ReportContentId] FROM Inserted)