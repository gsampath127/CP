CREATE TABLE [dbo].[ReportContentData] (
    [ReportContentId] INT             NOT NULL,
    [Data]            VARBINARY (MAX) NULL,
    [HasData]         AS              (case when [Data] IS NULL then (0) else (1) end) PERSISTED NOT NULL,
    [DataLength]      AS              (len([Data])) PERSISTED,
    [DataHash]		  AS (CONVERT([varbinary](20),case [Data] when NULL then NULL else [dbo].[fnHashBytesNVARCHARMAX]('SHA1',[Data]) end,0)) PERSISTED,
    [UtcModifiedDate] DATETIME        CONSTRAINT [DF_ReportContentData_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT             NULL,
    CONSTRAINT [PK_ReportContentData] PRIMARY KEY CLUSTERED ([ReportContentId] ASC),
    CONSTRAINT [FK_ReportContentData_ReportContent] FOREIGN KEY ([ReportContentId]) REFERENCES [dbo].[ReportContent] ([ReportContentId])
);








GO
CREATE TRIGGER trg_ReportContentData_UtcModifiedDate
ON dbo.ReportContentData
AFTER UPDATE
AS
    UPDATE dbo.ReportContentData
    SET UtcModifiedDate = GETUTCDATE()
    WHERE ReportContentId 
	   IN 
	      (SELECT DISTINCT ReportContentId FROM Inserted)