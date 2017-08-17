CREATE TABLE [dbo].[SiteXmlExport] (
    [SiteXmlExportId]   INT            IDENTITY (1, 1) NOT NULL,
    [ExportTypes]       INT            NOT NULL,
    [ExportXml]         XML            NOT NULL,
    [ExportDate]        DATETIME       NOT NULL,
    [ExportedBy]        INT            NOT NULL,
    [ExportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_SiteXmlExport] PRIMARY KEY CLUSTERED ([SiteXmlExportId] ASC)
);

