CREATE TABLE [dbo].[Reports] (
    [ReportId]          INT            IDENTITY (1, 1) NOT NULL,
    [ReportName]        NVARCHAR (200) NOT NULL,
    [ReportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([ReportId] ASC)
);

