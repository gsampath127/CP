CREATE TABLE [dbo].[SiteXmlImport] (
    [SiteXmlImportId]   INT            IDENTITY (1, 1) NOT NULL,
    [ImportTypes]       INT            NOT NULL,
    [ImportXml]         XML            NOT NULL,
    [ImportDate]        DATETIME       NOT NULL,
    [ImportedBy]        INT            NOT NULL,
    [ExportBackupId]    INT            NOT NULL,
    [ImportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_SiteXmlImport] PRIMARY KEY CLUSTERED ([SiteXmlImportId] ASC),
    CONSTRAINT [FK_SiteXmlImport_SiteXmlExport] FOREIGN KEY ([ExportBackupId]) REFERENCES [dbo].[SiteXmlExport] ([SiteXmlExportId])
);

