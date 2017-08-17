CREATE TABLE [dbo].[VerticalXmlImport] (
    [VerticalXmlImportId] INT            IDENTITY (1, 1) NOT NULL,
    [ImportTypes]         INT            NOT NULL,
    [ImportXml]           XML            NOT NULL,
    [ImportDate]          DATETIME       NOT NULL,
    [ImportedBy]          INT            NOT NULL,
    [ExportBackupId]      INT            NULL,
    [ImportDescription]   NVARCHAR (400) NULL,
    [Status]			  INT CONSTRAINT [DF_VerticalXmlImport_Status] DEFAULT ((0)) NOT NULL, 
    CONSTRAINT [PK_VerticalXmlImport] PRIMARY KEY CLUSTERED ([VerticalXmlImportId] ASC),
    CONSTRAINT [FK_VerticalXmlImport_VerticalXmlExport] FOREIGN KEY ([ExportBackupId]) REFERENCES [dbo].[VerticalXmlExport] ([VerticalXmlExportId])
);

