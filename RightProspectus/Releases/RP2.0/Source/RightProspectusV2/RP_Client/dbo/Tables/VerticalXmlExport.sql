CREATE TABLE [dbo].[VerticalXmlExport] (
    [VerticalXmlExportId] INT            IDENTITY (1, 1) NOT NULL,
    [ExportTypes]         INT            NOT NULL,
    [ExportXml]           XML            NOT NULL,
    [ExportDate]          DATETIME       NOT NULL,
    [ExportedBy]          INT            NOT NULL,
    [ExportDescription]   NVARCHAR (400) NULL,
	[Status] INT CONSTRAINT [DF_VerticalXmlExport_Status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_VerticalXmlExport] PRIMARY KEY CLUSTERED ([VerticalXmlExportId] ASC)
);

