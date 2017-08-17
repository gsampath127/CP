
PRINT N'Creating [dbo].[TT_ClientDocumentGroupClientDocument]...';


GO
CREATE TYPE [dbo].[TT_ClientDocumentGroupClientDocument] AS TABLE (
    [ClientDocumentGroupId] INT NOT NULL,
    [ClientDocumentId]      INT NOT NULL,
    [Order]                 INT NULL);


GO
PRINT N'Creating [dbo].[TT_CUDHistory]...';


GO
CREATE TYPE [dbo].[TT_CUDHistory] AS TABLE (
    [Id]        INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([Id] ASC),
    [TableName] NVARCHAR (128) NOT NULL,
    [Key]       INT            NOT NULL,
    [SecondKey] NVARCHAR (200) NULL,
    [ThirdKey]  NVARCHAR (200) NULL,
    [CUDType]   CHAR (1)       NOT NULL,
    [UserId]    INT            NULL);


GO
PRINT N'Creating [dbo].[TT_CUDHistoryData]...';


GO
CREATE TYPE [dbo].[TT_CUDHistoryData] AS TABLE (
    [ParentId]   INT            NOT NULL,
    [ColumnName] NVARCHAR (128) NOT NULL,
    [SqlDbType]  INT            NOT NULL,
    [OldValue]   NVARCHAR (MAX) NULL,
    [NewValue]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ParentId] ASC, [ColumnName] ASC));


GO
PRINT N'Creating [dbo].[TT_DocumentTypeAssociation]...';


GO
CREATE TYPE [dbo].[TT_DocumentTypeAssociation] AS TABLE (
    [headerText]          NVARCHAR (100) NULL,
    [linkText]            NVARCHAR (100) NULL,
    [Order]               INT            NULL,
    [MarketId]            NVARCHAR (50)  NULL,
    [descriptionOverride] NVARCHAR (400) NULL,
    [cssClass]            VARCHAR (50)   NULL,
    [delete]              BIT            NULL,
    [siteid]              INT            NULL,
    [taxonomymarketId]    NVARCHAR (50)  NULL,
    [taxonomylevel]       INT            NULL,
    [documenttypeid]      INT            NULL);


GO
PRINT N'Creating [dbo].[TT_RequestMaterialProsDetail]...';


GO
CREATE TYPE [dbo].[TT_RequestMaterialProsDetail] AS TABLE (
    [TaxonomyAssociationId] INT NULL,
    [DocumentTypeId]        INT NULL,
    [Quantity]              INT NULL);


GO
PRINT N'Creating [dbo].[TT_TaxonomyAssociation]...';


GO
CREATE TYPE [dbo].[TT_TaxonomyAssociation] AS TABLE (
    [Level]               INT            NULL,
    [importId]            NVARCHAR (60)  NULL,
    [systemId]            INT            NULL,
    [nameOverride]        NVARCHAR (200) NULL,
    [marketId]            NVARCHAR (50)  NULL,
    [descriptionOverride] NVARCHAR (400) NULL,
    [cssClass]            VARCHAR (50)   NULL,
    [delete]              BIT            NULL,
    [siteid]              INT            NULL,
    [taxonomyId]          INT            NULL);


GO
PRINT N'Creating [dbo].[TT_TaxonomyAssociationFootnotes]...';


GO
CREATE TYPE [dbo].[TT_TaxonomyAssociationFootnotes] AS TABLE (
    [taxonomyassociationsystemid] INT            NULL,
    [level]                       INT            NULL,
    [taxonomymarketId]            NVARCHAR (50)  NULL,
    [languageCulture]             VARCHAR (50)   NULL,
    [text]                        NVARCHAR (MAX) NULL,
    [delete]                      BIT            NULL);


GO
PRINT N'Creating [dbo].[TT_TaxonomyAssociationHierarchy]...';


GO
CREATE TYPE [dbo].[TT_TaxonomyAssociationHierarchy] AS TABLE (
    [parenttaxonomyassociationsystemid] INT           NULL,
    [parentlevel]                       INT           NULL,
    [parenttaxonomymarketId]            NVARCHAR (50) NULL,
    [relationshipType]                  INT           NULL,
    [childimportid]                     NVARCHAR (60) NULL,
    [deleteparent]                      BIT           NULL,
    [deletechild]                       BIT           NULL);


GO
PRINT N'Creating [dbo].[TT_TemplateNavigation]...';


GO
CREATE TYPE [dbo].[TT_TemplateNavigation] AS TABLE (
    [NavigationKey]        VARCHAR (200) NULL,
    [DefaultNavigationXml] XML           NULL);


GO
PRINT N'Creating [dbo].[TT_TemplatePageNavigation]...';


GO
CREATE TYPE [dbo].[TT_TemplatePageNavigation] AS TABLE (
    [PageId]               INT           NULL,
    [NavigationKey]        VARCHAR (200) NULL,
    [DefaultNavigationXml] XML           NULL);


GO
PRINT N'Creating [dbo].[TT_TemplatePageText]...';


GO
CREATE TYPE [dbo].[TT_TemplatePageText] AS TABLE (
    [PageId]      INT            NULL,
    [ResourceKey] VARCHAR (200)  NULL,
    [DefaultText] NVARCHAR (MAX) NULL);


GO
PRINT N'Creating [dbo].[TT_TemplateText]...';


GO
CREATE TYPE [dbo].[TT_TemplateText] AS TABLE (
    [ResourceKey] VARCHAR (200)  NULL,
    [DefaultText] NVARCHAR (MAX) NULL);


GO

PRINT N'Creating [dbo].[TT_DocumentUpdateVerticalData]...';
GO
CREATE TYPE [dbo].[TT_DocumentUpdateVerticalData] AS TABLE
(
    MarketId NVARCHAR(100),
	DocumentTypeID INT,
	TaxonomyName NVARCHAR(500),
	DocumentDate DATETIME,
	DocumentUpdatedDate DATETIME
)
GO


PRINT N'Creating [dbo].[BadRequest]...';


GO
CREATE TABLE [dbo].[BadRequest] (
    [SiteActivityId] INT            NOT NULL,
    [RequestIssue]   INT            NOT NULL,
    [ParameterName]  NVARCHAR (200) NULL,
    [ParameterValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BadRequest] PRIMARY KEY CLUSTERED ([SiteActivityId] ASC)
);


GO
PRINT N'Creating [dbo].[ClientDocument]...';


GO
CREATE TABLE [dbo].[ClientDocument] (
    [ClientDocumentId]     INT             IDENTITY (1, 1) NOT NULL,
    [ClientDocumentTypeId] INT             NOT NULL,
    [FileName]             NVARCHAR (260)  NOT NULL,
    [MimeType]             NVARCHAR (127)  NOT NULL,
    [IsPrivate]            BIT             NOT NULL,
    [ContentUri]           NVARCHAR (2083) NULL,
    [Name]                 NVARCHAR (100)  NULL,
    [Description]          NVARCHAR (400)  NULL,
    [UtcModifiedDate]      DATETIME        NOT NULL,
    [ModifiedBy]           INT             NULL,
    CONSTRAINT [PK_ClientDocument] PRIMARY KEY CLUSTERED ([ClientDocumentId] ASC)
);


GO
PRINT N'Creating [dbo].[ClientDocumentGroup]...';


GO
CREATE TABLE [dbo].[ClientDocumentGroup] (
    [ClientDocumentGroupId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]                        NVARCHAR (100) NOT NULL,
    [Description]                 NVARCHAR (400) NULL,
    [ParentClientDocumentGroupId] INT            NULL,
    [CssClass]                    VARCHAR (50)   NULL,
    [UtcModifiedDate]             DATETIME       NOT NULL,
    [ModifiedBy]                  INT            NULL,
    CONSTRAINT [PK_ClientDocumentGroup] PRIMARY KEY CLUSTERED ([ClientDocumentGroupId] ASC)
);


GO
PRINT N'Creating [dbo].[ClientDocumentGroupClientDocument]...';


GO
CREATE TABLE [dbo].[ClientDocumentGroupClientDocument] (
    [ClientDocumentGroupId] INT      NOT NULL,
    [ClientDocumentId]      INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_ClientDocumentGroupClientDocument] PRIMARY KEY CLUSTERED ([ClientDocumentGroupId] ASC, [ClientDocumentId] ASC)
);


GO
PRINT N'Creating [dbo].[ClientDocumentType]...';


GO
CREATE TABLE [dbo].[ClientDocumentType] (
    [ClientDocumentTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (400) NULL,	
    [UtcModifiedDate]      DATETIME       NOT NULL,
    [ModifiedBy]           INT            NULL,
	[HostedDocumentsDisplayCount]	INT   NOT NULL,
	[FTPName]              NVARCHAR (200) NULL,
	[FTPUsername]          NVARCHAR (200) NULL,
	[FTPPassword]          NVARCHAR (200) NULL,
	[IsSFTP]               BIT            NULL,
    CONSTRAINT [PK_ClientDocumentType] PRIMARY KEY CLUSTERED ([ClientDocumentTypeId] ASC)
);


GO
PRINT N'Creating [dbo].[ClientSettings]...';


GO
CREATE TABLE [dbo].[ClientSettings] (
    [ClientId]        INT      NOT NULL,
    [DefaultSiteId]   INT      NOT NULL,
    [UtcModifiedDate] DATETIME NOT NULL,
    [ModifiedBy]      INT      NULL,
    CONSTRAINT [ClientID_Unique_Constraint] UNIQUE NONCLUSTERED ([ClientId] ASC)
);


GO
PRINT N'Creating [dbo].[CUDHistory]...';


GO
CREATE TABLE [dbo].[CUDHistory] (
    [CUDHistoryId] INT              IDENTITY (1, 1) NOT NULL,
    [TableName]    NVARCHAR (128)   NOT NULL,
    [Key]          INT              NOT NULL,
    [SecondKey]    NVARCHAR (200)   NULL,
    [ThirdKey]     NVARCHAR (200)   NULL,
    [CUDType]      CHAR (1)         NOT NULL,
    [UtcCUDDate]   DATETIME         NOT NULL,
    [BatchId]      UNIQUEIDENTIFIER NOT NULL,
    [UserId]       INT              NULL,
    CONSTRAINT [PK_CUDHistory] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC)
);


GO
PRINT N'Creating [dbo].[CUDHistoryData]...';


GO
CREATE TABLE [dbo].[CUDHistoryData] (
    [CUDHistoryId] INT            NOT NULL,
    [ColumnName]   NVARCHAR (128) NOT NULL,
    [SqlDbType]    INT            NOT NULL,
    [OldValue]     NVARCHAR (MAX) NULL,
    [NewValue]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CUDHistoryData] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC, [ColumnName] ASC)
);


GO
PRINT N'Creating [dbo].[DocumentTypeAssociation]...';


GO
CREATE TABLE [dbo].[DocumentTypeAssociation] (
    [DocumentTypeAssociationId] INT            IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]            INT            NOT NULL,
    [SiteId]                    INT            NULL,
    [TaxonomyAssociationId]     INT            NULL,
    [Order]                     INT            NULL,
    [HeaderText]                NVARCHAR (100) NULL,
    [LinkText]                  NVARCHAR (100) NULL,
    [DescriptionOverride]       NVARCHAR (400) NULL,
    [CssClass]                  VARCHAR (50)   NULL,
    [MarketId]                  NVARCHAR (50)  NULL,
    [UtcModifiedDate]           DATETIME       NOT NULL,
    [ModifiedBy]                INT            NULL,
    CONSTRAINT [PK_DocumentTypeAssociation] PRIMARY KEY CLUSTERED ([DocumentTypeAssociationId] ASC)
);


GO
PRINT N'Creating [dbo].[DocumentTypeExternalId]...';


GO
CREATE TABLE [dbo].[DocumentTypeExternalId] (
    [DocumentTypeId]  INT            NOT NULL,
    [ExternalId]      NVARCHAR (100) NOT NULL,
    [IsPrimary]       BIT            NOT NULL,
    [UtcModifiedDate] DATETIME       NOT NULL,
    [ModifiedBy]      INT            NULL,
    CONSTRAINT [PK_DocumentTypeExternalId] PRIMARY KEY CLUSTERED ([DocumentTypeId] ASC, [ExternalId] ASC),
    CONSTRAINT [IX_DocumentTypeExternalId] UNIQUE NONCLUSTERED ([ExternalId] ASC)
);


GO
PRINT N'Creating [dbo].[ErrorLog]...';


GO
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
    [URL]              NVARCHAR (500)  NULL,
    [AbsoluteURL]      NVARCHAR (500)  NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([ErrorLogId] ASC)
);


GO
PRINT N'Creating [dbo].[Footnote]...';


GO
CREATE TABLE [dbo].[Footnote] (
    [FootnoteId]                 INT            IDENTITY (1, 1) NOT NULL,
    [TaxonomyAssociationId]      INT            NULL,
    [TaxonomyAssociationGroupId] INT            NULL,
    [LanguageCulture]            VARCHAR (50)   NULL,
    [Text]                       NVARCHAR (MAX) NULL,
    [Order]                      INT            NOT NULL,
    [UtcModifiedDate]            DATETIME       NOT NULL,
    [ModifiedBy]                 INT            NULL,
    CONSTRAINT [PK_Footnote] PRIMARY KEY CLUSTERED ([FootnoteId] ASC)
);


GO
PRINT N'Creating [dbo].[PageFeature]...';


GO
CREATE TABLE [dbo].[PageFeature] (
    [SiteId]          INT           NOT NULL,
    [PageId]          INT           NOT NULL,
    [Key]             VARCHAR (200) NOT NULL,
    [FeatureMode]     INT           NOT NULL,
    [UtcModifiedDate] DATETIME      NOT NULL,
    [ModifiedBy]      INT           NOT NULL,
    CONSTRAINT [PK_PageFeature] PRIMARY KEY CLUSTERED ([SiteId] ASC, [PageId] ASC, [Key] ASC)
);


GO
PRINT N'Creating [dbo].[PageNavigation]...';


GO
CREATE TABLE [dbo].[PageNavigation] (
    [PageNavigationId] INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]           INT           NOT NULL,
    [PageId]           INT           NOT NULL,
    [NavigationKey]    VARCHAR (200) NOT NULL,
    [CurrentVersion]   INT           NOT NULL,
    [LanguageCulture]  VARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME      NOT NULL,
    [ModifiedBy]       INT           NULL,
    CONSTRAINT [PK_PageNavigation] PRIMARY KEY CLUSTERED ([PageNavigationId] ASC),
    CONSTRAINT [IX_PageNavigation] UNIQUE NONCLUSTERED ([SiteId] ASC, [PageId] ASC, [NavigationKey] ASC, [LanguageCulture] ASC)
);


GO
PRINT N'Creating [dbo].[PageNavigationVersion]...';


GO
CREATE TABLE [dbo].[PageNavigationVersion] (
    [PageNavigationId] INT      NOT NULL,
    [Version]          INT      NOT NULL,
    [NavigationXml]    XML      NOT NULL,
    [UtcCreateDate]    DATETIME NOT NULL,
    [CreatedBy]        INT      NULL,
    CONSTRAINT [PK_PageNavigationVersion] PRIMARY KEY CLUSTERED ([PageNavigationId] ASC, [Version] ASC)
);


GO
PRINT N'Creating [dbo].[PageText]...';


GO
CREATE TABLE [dbo].[PageText] (
    [PageTextId]      INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]          INT           NOT NULL,
    [PageId]          INT           NOT NULL,
    [ResourceKey]     VARCHAR (200) NOT NULL,
    [CurrentVersion]  INT           NOT NULL,
    [LanguageCulture] VARCHAR (50)  NULL,
    [UtcModifiedDate] DATETIME      NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_PageText] PRIMARY KEY CLUSTERED ([PageTextId] ASC),
    CONSTRAINT [IX_PageText] UNIQUE NONCLUSTERED ([SiteId] ASC, [PageId] ASC, [ResourceKey] ASC, [CurrentVersion] ASC)
);


GO
PRINT N'Creating [dbo].[PageTextVersion]...';


GO
CREATE TABLE [dbo].[PageTextVersion] (
    [PageTextId]    INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [Text]          NVARCHAR (MAX) NULL,
    [UtcCreateDate] DATETIME       NOT NULL,
    [CreatedBy]     INT            NULL,
    CONSTRAINT [PK_PageTextVersion] PRIMARY KEY CLUSTERED ([PageTextId] ASC, [Version] ASC)
);


GO
PRINT N'Creating [dbo].[ReportContent]...';


GO
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
    [UtcModifiedDate]  DATETIME        NOT NULL,
    [ModifiedBy]       INT             NULL,
    CONSTRAINT [PK_ReportContent] PRIMARY KEY CLUSTERED ([ReportContentId] ASC)
);


GO
PRINT N'Creating [dbo].[RequestMaterialEmailHistory]...';


GO
CREATE TABLE [dbo].[RequestMaterialEmailHistory] (
    [RequestMaterialEmailHistoryId] INT              IDENTITY (1, 1) NOT NULL,
    [SiteId]                        INT              NOT NULL,
    [RecipEmail]                    NVARCHAR (150)   NULL,
    [RequestDateUtc]                DATETIME         NOT NULL,
    [UniqueID]                      UNIQUEIDENTIFIER NOT NULL,
    [RequestBatchId]                UNIQUEIDENTIFIER NULL,
    [FClickDateUtc]                 DATETIME         NULL,
    [RequestUri]                    INT              NULL,
    [UserAgentId]                   INT              NULL,
    [IPAddress]                     VARCHAR (15)     NULL,
    [ReferrerUri]                   INT              NULL,
    [Sent]                          BIT              NULL,
    CONSTRAINT [PK_RequestMaterialEmailHistory] PRIMARY KEY CLUSTERED ([RequestMaterialEmailHistoryId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestMaterialEmailProsDetail]...';


GO
CREATE TABLE [dbo].[RequestMaterialEmailProsDetail] (
    [RequestMaterialEmailProsDetailId] INT      IDENTITY (1, 1) NOT NULL,
    [RequestMaterialEmailHistoryId]    INT      NOT NULL,
    [TaxonomyAssociationId]            INT      NOT NULL,
    [DocumentTypeId]                   INT      NOT NULL,
    [SClickDateUtc]                    DATETIME NULL,
    CONSTRAINT [PK_RequestMaterialEmailProsDetail] PRIMARY KEY CLUSTERED ([RequestMaterialEmailProsDetailId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestMaterialPrintHistory]...';


GO
CREATE TABLE [dbo].[RequestMaterialPrintHistory] (
    [RequestMaterialPrintHistoryId] INT              IDENTITY (1, 1) NOT NULL,
    [SiteId]                        INT              NOT NULL,
    [ClientCompanyName]             NVARCHAR (100)   NULL,
    [ClientFirstName]               NVARCHAR (100)   NULL,
    [ClientMiddleName]              NVARCHAR (100)   NULL,
    [ClientLastName]                NVARCHAR (100)   NULL,
    [ClientName]                    NVARCHAR (200)   NULL,
    [Address1]                      NVARCHAR (200)   NULL,
    [Address2]                      NVARCHAR (200)   NULL,
    [Address3]                      NVARCHAR (200)   NULL,
    [City]                          NVARCHAR (100)   NULL,
    [StateOrProvince]               NVARCHAR (100)   NULL,
    [PostalCode]                    NVARCHAR (20)    NULL,
    [Country]                       NVARCHAR (200)   NULL,
    [RequestDateUtc]                DATETIME         NOT NULL,
    [UniqueID]                      UNIQUEIDENTIFIER NOT NULL,
    [RequestBatchId]                UNIQUEIDENTIFIER NULL,
    [RequestUri]                    INT              NULL,
    [UserAgentId]                   INT              NULL,
    [IPAddress]                     VARCHAR (15)     NULL,
    [ReferrerUri]                   INT              NULL,
    CONSTRAINT [PK_RequestMaterialPrintHistory] PRIMARY KEY CLUSTERED ([RequestMaterialPrintHistoryId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestMaterialPrintProsDetail]...';


GO
CREATE TABLE [dbo].[RequestMaterialPrintProsDetail] (
    [RequestMaterialPrintProsDetailId] INT IDENTITY (1, 1) NOT NULL,
    [RequestMaterialPrintHistoryId]    INT NOT NULL,
    [TaxonomyAssociationId]            INT NOT NULL,
    [DocumentTypeId]                   INT NOT NULL,
    [Quantity]                         INT NOT NULL,
    CONSTRAINT [PK_RequestMaterialPrintProsDetail] PRIMARY KEY CLUSTERED ([RequestMaterialPrintProsDetailId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestMaterialSKUDetail]...';


GO
CREATE TABLE [dbo].[RequestMaterialSKUDetail] (
    [RequestMaterialSKUDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [TaxonomyAssociationId]      INT            NOT NULL,
    [DocumentTypeId]             INT            NOT NULL,
    [SKUName]                    NVARCHAR (200) NULL,
    CONSTRAINT [PK_RequestMaterialSKUDetail] PRIMARY KEY CLUSTERED ([RequestMaterialSKUDetailId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Site]...';


GO
CREATE TABLE [dbo].[Site] (
    [SiteId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [TemplateId]      INT            NOT NULL,
    [DefaultPageId]   INT            NOT NULL,
    [ParentSiteId]    INT            NULL,
    [Description]     NVARCHAR (400) NULL,
    [UtcModifiedDate] DATETIME       NOT NULL,
    [ModifiedBy]      INT            NULL,
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([SiteId] ASC),
    CONSTRAINT [IX_Site] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[SiteActivity]...';


GO
CREATE TABLE [dbo].[SiteActivity] (
    [SiteActivityId]             INT              IDENTITY (1, 1) NOT NULL,
    [SiteId]                     INT              NOT NULL,
    [ClientIPAddress]            VARCHAR (15)     NOT NULL,
    [UserAgentId]                INT              NOT NULL,
    [RequestUtcDate]             DATETIME         NOT NULL,
    [HttpMethod]                 VARCHAR (20)     NOT NULL,
    [RequestUri]                 INT              NOT NULL,
    [ParsedRequestUri]           INT              NOT NULL,
    [ServerName]                 VARCHAR (50)     NOT NULL,
    [ReferrerUri]                INT              NULL,
    [InitDoc]                    BIT              NOT NULL,
    [RequestBatchId]             UNIQUEIDENTIFIER NULL,
    [UserId]                     INT              NULL,
    [PageId]                     INT              NULL,
    [TaxonomyAssociationGroupId] INT              NULL,
    [TaxonomyAssociationId]      INT              NULL,
    [DocumentTypeId]             INT              NULL,
    [ClientDocumentGroupId]      INT              NULL,
    [ClientDocumentId]           INT              NULL,
    [XBRLDocumentName]           VARCHAR (100)    NULL,
    [XBRLItemType]               INT              NULL,
    CONSTRAINT [PK_SiteActivity] PRIMARY KEY CLUSTERED ([SiteActivityId] ASC)
);


GO
PRINT N'Creating [dbo].[SiteFeature]...';


GO
CREATE TABLE [dbo].[SiteFeature] (
    [SiteId]          INT           NOT NULL,
    [Key]             VARCHAR (200) NOT NULL,
    [FeatureMode]     INT           NOT NULL,
    [UtcModifiedDate] DATETIME      NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_SiteFeature] PRIMARY KEY CLUSTERED ([SiteId] ASC, [Key] ASC)
);


GO
PRINT N'Creating [dbo].[SiteNavigation]...';


GO
CREATE TABLE [dbo].[SiteNavigation] (
    [SiteNavigationId] INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]           INT           NOT NULL,
    [NavigationKey]    VARCHAR (200) NOT NULL,
    [PageId]           INT           NULL,
    [CurrentVersion]   INT           NOT NULL,
    [LanguageCulture]  VARCHAR (50)  NULL,
    [UtcModifiedDate]  DATETIME      NOT NULL,
    [ModifiedBy]       INT           NULL,
    CONSTRAINT [PK_SiteNavigation] PRIMARY KEY CLUSTERED ([SiteNavigationId] ASC)
);


GO
PRINT N'Creating [dbo].[SiteNavigationVersion]...';


GO
CREATE TABLE [dbo].[SiteNavigationVersion] (
    [SiteNavigationId] INT      NOT NULL,
    [Version]          INT      NOT NULL,
    [NavigationXml]    XML      NOT NULL,
    [UtcCreateDate]    DATETIME NOT NULL,
    [CreatedBy]        INT      NULL,
    CONSTRAINT [PK_SiteNavigationVersion] PRIMARY KEY CLUSTERED ([SiteNavigationId] ASC, [Version] ASC)
);


GO
PRINT N'Creating [dbo].[SiteText]...';


GO
CREATE TABLE [dbo].[SiteText] (
    [SiteTextId]      INT           IDENTITY (1, 1) NOT NULL,
    [SiteId]          INT           NOT NULL,
    [ResourceKey]     VARCHAR (200) NOT NULL,
    [CurrentVersion]  INT           NOT NULL,
    [LanguageCulture] VARCHAR (50)  NULL,
    [UtcModifiedDate] DATETIME      NOT NULL,
    [ModifiedBy]      INT           NULL,
    CONSTRAINT [PK_SiteText] PRIMARY KEY CLUSTERED ([SiteTextId] ASC),
    CONSTRAINT [IX_SiteText] UNIQUE NONCLUSTERED ([SiteId] ASC, [ResourceKey] ASC, [LanguageCulture] ASC)
);


GO
PRINT N'Creating [dbo].[SiteTextVersion]...';


GO
CREATE TABLE [dbo].[SiteTextVersion] (
    [SiteTextId]    INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [Text]          NVARCHAR (MAX) NULL,
    [UtcCreateDate] DATETIME       NOT NULL,
    [CreatedBy]     INT            NULL,
    CONSTRAINT [PK_SiteTextVersion] PRIMARY KEY CLUSTERED ([SiteTextId] ASC, [Version] ASC)
);


GO
PRINT N'Creating [dbo].[SiteXmlExport]...';


GO
CREATE TABLE [dbo].[SiteXmlExport] (
    [SiteXmlExportId]   INT            IDENTITY (1, 1) NOT NULL,
    [ExportTypes]       INT            NOT NULL,
    [ExportXml]         XML            NOT NULL,
    [ExportDate]        DATETIME       NOT NULL,
    [ExportedBy]        INT            NOT NULL,
    [ExportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_SiteXmlExport] PRIMARY KEY CLUSTERED ([SiteXmlExportId] ASC)
);


GO
PRINT N'Creating [dbo].[SiteXmlImport]...';


GO
CREATE TABLE [dbo].[SiteXmlImport] (
    [SiteXmlImportId]   INT            IDENTITY (1, 1) NOT NULL,
    [ImportTypes]       INT            NOT NULL,
    [ImportXml]         XML            NOT NULL,
    [ImportDate]        DATETIME       NOT NULL,
    [ImportedBy]        INT            NOT NULL,
    [ExportBackupId]    INT            NOT NULL,
    [ImportDescription] NVARCHAR (400) NULL,
    CONSTRAINT [PK_SiteXmlImport] PRIMARY KEY CLUSTERED ([SiteXmlImportId] ASC)
);


GO
PRINT N'Creating [dbo].[StaticResource]...';


GO
CREATE TABLE [dbo].[StaticResource] (
    [StaticResourceId] INT             IDENTITY (1, 1) NOT NULL,
    [FileName]         NVARCHAR (260)  NOT NULL,
    [Size]             INT             NOT NULL,
    [MimeType]         VARCHAR (127)   NOT NULL,
    [Data]             VARBINARY (MAX) NOT NULL,
    [UtcModifiedDate]  DATETIME        NOT NULL,
    [ModifiedBy]       INT             NOT NULL,
    CONSTRAINT [PK_StaticResource] PRIMARY KEY CLUSTERED ([StaticResourceId] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[TaxonomyAssociation]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociation] (
    [TaxonomyAssociationId]       INT            IDENTITY (1, 1) NOT NULL,
    [Level]                       INT            NOT NULL,
    [TaxonomyId]                  INT            NOT NULL,
    [SiteId]                      INT            NULL,
    [ParentTaxonomyAssociationId] INT            NULL,
    [NameOverride]                NVARCHAR (200) NULL,
    [DescriptionOverride]         NVARCHAR (400) NULL,
    [CssClass]                    VARCHAR (50)   NULL,
    [MarketId]                    NVARCHAR (50)  NULL,
    [UtcModifiedDate]             DATETIME       NOT NULL,
    [ModifiedBy]                  INT            NULL,
    CONSTRAINT [PK_TaxonomyAssociation] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationClientDocument]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationClientDocument] (
    [TaxonomyAssociationId] INT      NOT NULL,
    [ClientDocumentId]      INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationClientDocument] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC, [ClientDocumentId] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationClientDocumentGroup]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] (
    [TaxonomyAssociationId] INT      NOT NULL,
    [ClientDocumentGroupId] INT      NOT NULL,
    [Order]                 INT      NULL,
    [UtcModifiedDate]       DATETIME NOT NULL,
    [ModifiedBy]            INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationClientDocumentGroup] PRIMARY KEY CLUSTERED ([TaxonomyAssociationId] ASC, [ClientDocumentGroupId] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationGroup]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationGroup] (
    [TaxonomyAssociationGroupId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]                             NVARCHAR (100) NOT NULL,
    [Description]                      NVARCHAR (400) NULL,
    [SiteId]                           INT            NULL,
    [ParentTaxonomyAssociationId]      INT            NULL,
    [ParentTaxonomyAssociationGroupId] INT            NULL,
    [CssClass]                         VARCHAR (50)   NULL,
    [UtcModifiedDate]                  DATETIME       NOT NULL,
    [ModifiedBy]                       INT            NULL,
    CONSTRAINT [PK_TaxonomyAssociationGroup] PRIMARY KEY CLUSTERED ([TaxonomyAssociationGroupId] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationGroupTaxonomyAssociation]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] (
    [TaxonomyAssociationGroupId] INT      NOT NULL,
    [TaxonomyAssociationId]      INT      NOT NULL,
    [Order]                      INT      NULL,
    [UtcModifiedDate]            DATETIME NOT NULL,
    [ModifiedBy]                 INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationGroupTaxonomyAssociation] PRIMARY KEY CLUSTERED ([TaxonomyAssociationGroupId] ASC, [TaxonomyAssociationId] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationHierachy]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationHierachy] (
    [ParentTaxonomyAssociationId] INT      NOT NULL,
    [ChildTaxonomyAssociationId]  INT      NOT NULL,
    [RelationshipType]            INT      NOT NULL,
    [Order]                       INT      NULL,
    [UtcModifiedDate]             DATETIME NOT NULL,
    [ModifiedBy]                  INT      NULL,
    CONSTRAINT [PK_TaxonomyAssociationHierachy] PRIMARY KEY CLUSTERED ([ParentTaxonomyAssociationId] ASC, [ChildTaxonomyAssociationId] ASC, [RelationshipType] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyAssociationMetaData]...';


GO
CREATE TABLE [dbo].[TaxonomyAssociationMetaData] (
    [TaxonomyAssociationID] INT            NOT NULL,
    [Key]                   VARCHAR (20)   NOT NULL,
    [DataType]              INT            NOT NULL,
    [Order]                 INT            NULL,
    [IntegerValue]          INT            NULL,
    [BooleanValue]          BIT            NULL,
    [DateTimeValue]         DATETIME       NULL,
    [StringValue]           NVARCHAR (MAX) NULL,
    [UtcModifiedDate]       DATETIME       NOT NULL,
    [ModifiedBy]            INT            NULL,
    CONSTRAINT [PK_TaxonomyAssociationMetaData] PRIMARY KEY CLUSTERED ([TaxonomyAssociationID] ASC, [Key] ASC)
);


GO
PRINT N'Creating [dbo].[TaxonomyLevelExternalId]...';


GO
CREATE TABLE [dbo].[TaxonomyLevelExternalId] (
    [Level]           INT            NOT NULL,
    [TaxonomyId]      INT            NOT NULL,
    [ExternalId]      NVARCHAR (100) NOT NULL,
    [IsPrimary]       BIT            NOT NULL,
    [UtcModifiedDate] DATETIME       NOT NULL,
    [ModifiedBy]      INT            NULL,
	[SendDocumentUpdate] BIT	CONSTRAINT [DF_TaxonomyLevelExternalId_SendDocumentUpdate] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TaxonomyLevelExternalId] PRIMARY KEY CLUSTERED ([Level] ASC, [TaxonomyId] ASC, [ExternalId] ASC),
    CONSTRAINT [IX_TaxonomyLevelExternalId] UNIQUE NONCLUSTERED ([ExternalId] ASC)
);


GO
PRINT N'Creating [dbo].[Uri]...';


GO
CREATE TABLE [dbo].[Uri] (
    [UriId]     INT             IDENTITY (1, 1) NOT NULL,
    [UriString] NVARCHAR (2083) NOT NULL,
    [UriHash]   AS              (hashbytes('MD5', [UriString])) PERSISTED,
    [UriLength] AS              (len([UriString])) PERSISTED,
    CONSTRAINT [PK_Uri] PRIMARY KEY CLUSTERED ([UriId] ASC)
);


GO
PRINT N'Creating [dbo].[UrlRewrite]...';


GO
CREATE TABLE [dbo].[UrlRewrite] (
    [UrlRewriteId]    INT             IDENTITY (1, 1) NOT NULL,
    [MatchPattern]    NVARCHAR (2083) NOT NULL,
    [RewriteFormat]   NVARCHAR (2083) NOT NULL,
    [UtcModifiedDate] DATETIME        NOT NULL,
    [ModifiedBy]      INT             NULL,
    [PatternName]     NVARCHAR (100)  NULL,
    CONSTRAINT [PK_UrlRewrite] PRIMARY KEY CLUSTERED ([UrlRewriteId] ASC)
);


GO
PRINT N'Creating [dbo].[UserAgent]...';


GO
CREATE TABLE [dbo].[UserAgent] (
    [UserAgentId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserAgentString] NVARCHAR (MAX) NOT NULL,
    [UserAgentHash]   AS             (hashbytes('MD5', [UserAgentString])) PERSISTED,
    [UserAgentLength] AS             (len([UserAgentString])) PERSISTED,
    CONSTRAINT [PK_UserAgent] PRIMARY KEY CLUSTERED ([UserAgentId] ASC)
);


GO
PRINT N'Creating [dbo].[VerticalXmlExport]...';


GO
CREATE TABLE [dbo].[VerticalXmlExport] (
    [VerticalXmlExportId] INT            IDENTITY (1, 1) NOT NULL,
    [ExportTypes]         INT            NOT NULL,
    [ExportXml]           XML            NOT NULL,
    [ExportDate]          DATETIME       NOT NULL,
    [ExportedBy]          INT            NOT NULL,
    [ExportDescription]   NVARCHAR (400) NULL,
    [Status]              INT            NOT NULL,
    CONSTRAINT [PK_VerticalXmlExport] PRIMARY KEY CLUSTERED ([VerticalXmlExportId] ASC)
);


GO
PRINT N'Creating [dbo].[VerticalXmlImport]...';


GO
CREATE TABLE [dbo].[VerticalXmlImport] (
    [VerticalXmlImportId] INT            IDENTITY (1, 1) NOT NULL,
    [ImportTypes]         INT            NOT NULL,
    [ImportXml]           XML            NOT NULL,
    [ImportDate]          DATETIME       NOT NULL,
    [ImportedBy]          INT            NOT NULL,
    [ExportBackupId]      INT            NULL,
    [ImportDescription]   NVARCHAR (400) NULL,
    [Status]              INT            NOT NULL,
    CONSTRAINT [PK_VerticalXmlImport] PRIMARY KEY CLUSTERED ([VerticalXmlImportId] ASC)
);


GO

PRINT N'Creating [dbo].[TaxonomyLevelDocUpdate]...';
Go
CREATE TABLE [dbo].[TaxonomyLevelDocUpdate]
(
    [MarketId]				NVARCHAR(100)	NOT NULL,
	[DocumentTypeID]		INT				NOT NULL,
	[TaxonomyName]			NVARCHAR (1000)	NOT NULL,    
    [DocumentDate]			DATETIME		NULL,
	[DocumentUpdatedDate]	DATETIME		NULL,
	CONSTRAINT [PK_TaxonomyLevelDocUpdate] PRIMARY KEY CLUSTERED ([MarketId] ASC, [DocumentTypeID] ASC)   
);
GO



PRINT N'Creating DF_ClientDocument_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientDocument]
    ADD CONSTRAINT [DF_ClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ClientDocumentGroup_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientDocumentGroup]
    ADD CONSTRAINT [DF_ClientDocumentGroup_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ClientDocumentGroupClientDocument_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientDocumentGroupClientDocument]
    ADD CONSTRAINT [DF_ClientDocumentGroupClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ClientDocumentType_HostedDocumentsDisplayCount...';


GO
ALTER TABLE [dbo].[ClientDocumentType]
    ADD CONSTRAINT [DF_ClientDocumentType_HostedDocumentsDisplayCount] DEFAULT (0) FOR [HostedDocumentsDisplayCount];


GO
PRINT N'Creating DF_ClientDocumentType_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientDocumentType]
    ADD CONSTRAINT [DF_ClientDocumentType_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ClientSettings_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientSettings]
    ADD CONSTRAINT [DF_ClientSettings_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_CUDHistory_UtcCUDDate...';


GO
ALTER TABLE [dbo].[CUDHistory]
    ADD CONSTRAINT [DF_CUDHistory_UtcCUDDate] DEFAULT (GETUTCDATE()) FOR [UtcCUDDate];


GO
PRINT N'Creating DF_DocumentTypeAssociation_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[DocumentTypeAssociation]
    ADD CONSTRAINT [DF_DocumentTypeAssociation_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_DocumentTypeExternalId_IsPrimary...';


GO
ALTER TABLE [dbo].[DocumentTypeExternalId]
    ADD CONSTRAINT [DF_DocumentTypeExternalId_IsPrimary] DEFAULT ((0)) FOR [IsPrimary];


GO
PRINT N'Creating DF_DocumentTypeExternalId_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[DocumentTypeExternalId]
    ADD CONSTRAINT [DF_DocumentTypeExternalId_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_Footnote_Order...';


GO
ALTER TABLE [dbo].[Footnote]
    ADD CONSTRAINT [DF_Footnote_Order] DEFAULT (0) FOR [Order];


GO
PRINT N'Creating DF_Footnote_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[Footnote]
    ADD CONSTRAINT [DF_Footnote_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_PageFeature_FeatureMode...';


GO
ALTER TABLE [dbo].[PageFeature]
    ADD CONSTRAINT [DF_PageFeature_FeatureMode] DEFAULT ((0)) FOR [FeatureMode];


GO
PRINT N'Creating DF_PageFeature_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[PageFeature]
    ADD CONSTRAINT [DF_PageFeature_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_PageNavigation_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[PageNavigation]
    ADD CONSTRAINT [DF_PageNavigation_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_PageNavigationVersion_UtcCreateDate...';


GO
ALTER TABLE [dbo].[PageNavigationVersion]
    ADD CONSTRAINT [DF_PageNavigationVersion_UtcCreateDate] DEFAULT (getutcdate()) FOR [UtcCreateDate];


GO
PRINT N'Creating DF_PageText_CurrentVersion...';


GO
ALTER TABLE [dbo].[PageText]
    ADD CONSTRAINT [DF_PageText_CurrentVersion] DEFAULT ((0)) FOR [CurrentVersion];


GO
PRINT N'Creating DF_PageText_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[PageText]
    ADD CONSTRAINT [DF_PageText_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_PageTextVersion_UtcCreateDate...';


GO
ALTER TABLE [dbo].[PageTextVersion]
    ADD CONSTRAINT [DF_PageTextVersion_UtcCreateDate] DEFAULT (getutcdate()) FOR [UtcCreateDate];


GO
PRINT N'Creating DF_ReportContent_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ReportContent]
    ADD CONSTRAINT [DF_ReportContent_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_RequestMaterialEmailHistory_RequestDateUtc...';


GO
ALTER TABLE [dbo].[RequestMaterialEmailHistory]
    ADD CONSTRAINT [DF_RequestMaterialEmailHistory_RequestDateUtc] DEFAULT (GETUTCDATE()) FOR [RequestDateUtc];


GO
PRINT N'Creating DF_RequestMaterialPrintHistory_RequestDateUtc...';


GO
ALTER TABLE [dbo].[RequestMaterialPrintHistory]
    ADD CONSTRAINT [DF_RequestMaterialPrintHistory_RequestDateUtc] DEFAULT (GETUTCDATE()) FOR [RequestDateUtc];


GO
PRINT N'Creating DF_RequestMaterialPrintProsDetail_Quantity...';


GO
ALTER TABLE [dbo].[RequestMaterialPrintProsDetail]
    ADD CONSTRAINT [DF_RequestMaterialPrintProsDetail_Quantity] DEFAULT (1) FOR [Quantity];


GO
PRINT N'Creating DF_Site_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[Site]
    ADD CONSTRAINT [DF_Site_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating Default Constraint on [dbo].[SiteActivity]....';


GO
ALTER TABLE [dbo].[SiteActivity]
    ADD DEFAULT (0) FOR [InitDoc];


GO
PRINT N'Creating DF_SiteFeature_FeatureMode...';


GO
ALTER TABLE [dbo].[SiteFeature]
    ADD CONSTRAINT [DF_SiteFeature_FeatureMode] DEFAULT ((0)) FOR [FeatureMode];


GO
PRINT N'Creating DF_SiteFeature_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[SiteFeature]
    ADD CONSTRAINT [DF_SiteFeature_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_SiteNavigation_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[SiteNavigation]
    ADD CONSTRAINT [DF_SiteNavigation_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_SiteNavigationVersion_UtcCreateDate...';


GO
ALTER TABLE [dbo].[SiteNavigationVersion]
    ADD CONSTRAINT [DF_SiteNavigationVersion_UtcCreateDate] DEFAULT (getutcdate()) FOR [UtcCreateDate];


GO
PRINT N'Creating DF_SiteText_CurrentVersion...';


GO
ALTER TABLE [dbo].[SiteText]
    ADD CONSTRAINT [DF_SiteText_CurrentVersion] DEFAULT ((0)) FOR [CurrentVersion];


GO
PRINT N'Creating DF_SiteText_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[SiteText]
    ADD CONSTRAINT [DF_SiteText_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_SiteTextVersion_UtcCreateDate...';


GO
ALTER TABLE [dbo].[SiteTextVersion]
    ADD CONSTRAINT [DF_SiteTextVersion_UtcCreateDate] DEFAULT (getutcdate()) FOR [UtcCreateDate];


GO
PRINT N'Creating DF_StaticResource_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[StaticResource]
    ADD CONSTRAINT [DF_StaticResource_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociation_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociation]
    ADD CONSTRAINT [DF_TaxonomyAssociation_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationClientDocument_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocument]
    ADD CONSTRAINT [DF_TaxonomyAssociationClientDocument_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationClientDocumentGroup_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocumentGroup]
    ADD CONSTRAINT [DF_TaxonomyAssociationClientDocumentGroup_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationGroup_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroup]
    ADD CONSTRAINT [DF_TaxonomyAssociationGroup_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationGroupTaxonomyAssociation_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation]
    ADD CONSTRAINT [DF_TaxonomyAssociationGroupTaxonomyAssociation_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationHierachy_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationHierachy]
    ADD CONSTRAINT [DF_TaxonomyAssociationHierachy_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyAssociationMetaData_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationMetaData]
    ADD CONSTRAINT [DF_TaxonomyAssociationMetaData_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_TaxonomyLevelExternalId_IsPrimary...';


GO
ALTER TABLE [dbo].[TaxonomyLevelExternalId]
    ADD CONSTRAINT [DF_TaxonomyLevelExternalId_IsPrimary] DEFAULT ((0)) FOR [IsPrimary];


GO
PRINT N'Creating DF_TaxonomyLevelExternalId_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[TaxonomyLevelExternalId]
    ADD CONSTRAINT [DF_TaxonomyLevelExternalId_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_UrlRewrite_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[UrlRewrite]
    ADD CONSTRAINT [DF_UrlRewrite_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_VerticalXmlExport_Status...';


GO
ALTER TABLE [dbo].[VerticalXmlExport]
    ADD CONSTRAINT [DF_VerticalXmlExport_Status] DEFAULT ((0)) FOR [Status];


GO
PRINT N'Creating DF_VerticalXmlImport_Status...';


GO
ALTER TABLE [dbo].[VerticalXmlImport]
    ADD CONSTRAINT [DF_VerticalXmlImport_Status] DEFAULT ((0)) FOR [Status];


GO
PRINT N'Creating FK_BadRequest_SiteActivity...';


GO
ALTER TABLE [dbo].[BadRequest] WITH NOCHECK
    ADD CONSTRAINT [FK_BadRequest_SiteActivity] FOREIGN KEY ([SiteActivityId]) REFERENCES [dbo].[SiteActivity] ([SiteActivityId]);


GO
PRINT N'Creating FK_ClientDocument_ClientDocumentType...';


GO
ALTER TABLE [dbo].[ClientDocument] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDocument_ClientDocumentType] FOREIGN KEY ([ClientDocumentTypeId]) REFERENCES [dbo].[ClientDocumentType] ([ClientDocumentTypeId]);


GO
PRINT N'Creating FK_ClientDocumentGroup_ClientDocumentGroup...';


GO
ALTER TABLE [dbo].[ClientDocumentGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDocumentGroup_ClientDocumentGroup] FOREIGN KEY ([ParentClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId]);


GO
PRINT N'Creating FK_ClientDocumentGroupClientDocument_ClientDocument...';


GO
ALTER TABLE [dbo].[ClientDocumentGroupClientDocument] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId]);


GO
PRINT N'Creating FK_ClientDocumentGroupClientDocument_ClientDocumentGroup...';


GO
ALTER TABLE [dbo].[ClientDocumentGroupClientDocument] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocumentGroup] FOREIGN KEY ([ClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId]);


GO
PRINT N'Creating FK_ClientSettings_Site...';


GO
ALTER TABLE [dbo].[ClientSettings] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientSettings_Site] FOREIGN KEY ([DefaultSiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_CUDHistoryData_CUDHistory...';


GO
ALTER TABLE [dbo].[CUDHistoryData] WITH NOCHECK
    ADD CONSTRAINT [FK_CUDHistoryData_CUDHistory] FOREIGN KEY ([CUDHistoryId]) REFERENCES [dbo].[CUDHistory] ([CUDHistoryId]);


GO
PRINT N'Creating FK_DocumentTypeAssociation_Site...';


GO
ALTER TABLE [dbo].[DocumentTypeAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_DocumentTypeAssociation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_DocumentTypeAssociation_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[DocumentTypeAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_DocumentTypeAssociation_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_ErrorLog_SiteActivity...';


GO
ALTER TABLE [dbo].[ErrorLog] WITH NOCHECK
    ADD CONSTRAINT [FK_ErrorLog_SiteActivity] FOREIGN KEY ([SiteActivityId]) REFERENCES [dbo].[SiteActivity] ([SiteActivityId]);


GO
PRINT N'Creating FK_Footnote_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[Footnote] WITH NOCHECK
    ADD CONSTRAINT [FK_Footnote_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_Footnote_TaxonomyAssociationGroup...';


GO
ALTER TABLE [dbo].[Footnote] WITH NOCHECK
    ADD CONSTRAINT [FK_Footnote_TaxonomyAssociationGroup] FOREIGN KEY ([TaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId]);


GO
PRINT N'Creating FK_PageFeature_Site...';


GO
ALTER TABLE [dbo].[PageFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_PageFeature_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_PageNavigation_Site...';


GO
ALTER TABLE [dbo].[PageNavigation] WITH NOCHECK
    ADD CONSTRAINT [FK_PageNavigation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_PageNavigationVersion_PageNavigation...';


GO
ALTER TABLE [dbo].[PageNavigationVersion] WITH NOCHECK
    ADD CONSTRAINT [FK_PageNavigationVersion_PageNavigation] FOREIGN KEY ([PageNavigationId]) REFERENCES [dbo].[PageNavigation] ([PageNavigationId]);


GO
PRINT N'Creating FK_PageText_Site...';


GO
ALTER TABLE [dbo].[PageText] WITH NOCHECK
    ADD CONSTRAINT [FK_PageText_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_PageTextVersion_PageText...';


GO
ALTER TABLE [dbo].[PageTextVersion] WITH NOCHECK
    ADD CONSTRAINT [FK_PageTextVersion_PageText] FOREIGN KEY ([PageTextId]) REFERENCES [dbo].[PageText] ([PageTextId]);


GO
PRINT N'Creating FK_RequestMaterialEmailProsDetail_RequestMaterialEmailHistory...';


GO
ALTER TABLE [dbo].[RequestMaterialEmailProsDetail] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestMaterialEmailProsDetail_RequestMaterialEmailHistory] FOREIGN KEY ([RequestMaterialEmailHistoryId]) REFERENCES [dbo].[RequestMaterialEmailHistory] ([RequestMaterialEmailHistoryId]);


GO
PRINT N'Creating FK_RequestMaterialPrintProsDetail_RequestMaterialPrintHistory...';


GO
ALTER TABLE [dbo].[RequestMaterialPrintProsDetail] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestMaterialPrintProsDetail_RequestMaterialPrintHistory] FOREIGN KEY ([RequestMaterialPrintHistoryId]) REFERENCES [dbo].[RequestMaterialPrintHistory] ([RequestMaterialPrintHistoryId]);


GO
PRINT N'Creating FK_Site_Site...';


GO
ALTER TABLE [dbo].[Site] WITH NOCHECK
    ADD CONSTRAINT [FK_Site_Site] FOREIGN KEY ([ParentSiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_SiteActivity_Site...';


GO
ALTER TABLE [dbo].[SiteActivity] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteActivity_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_SiteActivity_Uri...';


GO
ALTER TABLE [dbo].[SiteActivity] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteActivity_Uri] FOREIGN KEY ([RequestUri]) REFERENCES [dbo].[Uri] ([UriId]);


GO
PRINT N'Creating FK_SiteActivity_Uri1...';


GO
ALTER TABLE [dbo].[SiteActivity] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteActivity_Uri1] FOREIGN KEY ([ParsedRequestUri]) REFERENCES [dbo].[Uri] ([UriId]);


GO
PRINT N'Creating FK_SiteActivity_Uri2...';


GO
ALTER TABLE [dbo].[SiteActivity] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteActivity_Uri2] FOREIGN KEY ([ReferrerUri]) REFERENCES [dbo].[Uri] ([UriId]);


GO
PRINT N'Creating FK_SiteActivity_UserAgent...';


GO
ALTER TABLE [dbo].[SiteActivity] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteActivity_UserAgent] FOREIGN KEY ([UserAgentId]) REFERENCES [dbo].[UserAgent] ([UserAgentId]);


GO
PRINT N'Creating FK_SiteFeature_Site...';


GO
ALTER TABLE [dbo].[SiteFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteFeature_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_SiteNavigation_Site...';


GO
ALTER TABLE [dbo].[SiteNavigation] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteNavigation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_SiteNavigationVersion_SiteNavigation...';


GO
ALTER TABLE [dbo].[SiteNavigationVersion] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteNavigationVersion_SiteNavigation] FOREIGN KEY ([SiteNavigationId]) REFERENCES [dbo].[SiteNavigation] ([SiteNavigationId]);


GO
PRINT N'Creating FK_SiteText_Site...';


GO
ALTER TABLE [dbo].[SiteText] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteText_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_SiteTextVersion_SiteText...';


GO
ALTER TABLE [dbo].[SiteTextVersion] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteTextVersion_SiteText] FOREIGN KEY ([SiteTextId]) REFERENCES [dbo].[SiteText] ([SiteTextId]);


GO
PRINT N'Creating FK_SiteXmlImport_SiteXmlExport...';


GO
ALTER TABLE [dbo].[SiteXmlImport] WITH NOCHECK
    ADD CONSTRAINT [FK_SiteXmlImport_SiteXmlExport] FOREIGN KEY ([ExportBackupId]) REFERENCES [dbo].[SiteXmlExport] ([SiteXmlExportId]);


GO
PRINT N'Creating FK_TaxonomyAssociation_Site...';


GO
ALTER TABLE [dbo].[TaxonomyAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociation_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_TaxonomyAssociation_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociation_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationClientDocument_ClientDocument...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocument] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationClientDocument_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId]);


GO
PRINT N'Creating FK_TaxonomyAssociationClientDocument_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocument] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationClientDocument_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationClientDocumentGroup_ClientDocumentGroup...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_ClientDocumentGroup] FOREIGN KEY ([ClientDocumentGroupId]) REFERENCES [dbo].[ClientDocumentGroup] ([ClientDocumentGroupId]);


GO
PRINT N'Creating FK_TaxonomyAssociationClientDocumentGroup_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationGroup_Site...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationGroup_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]);


GO
PRINT N'Creating FK_TaxonomyAssociationGroup_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationGroup_TaxonomyAssociationGroup...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociationGroup] FOREIGN KEY ([ParentTaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId]);


GO
PRINT N'Creating FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociationGroup...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociationGroup] FOREIGN KEY ([TaxonomyAssociationGroupId]) REFERENCES [dbo].[TaxonomyAssociationGroup] ([TaxonomyAssociationGroupId]);


GO
PRINT N'Creating FK_TaxonomyAssociationHierachy_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationHierachy] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation] FOREIGN KEY ([ParentTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationHierachy_TaxonomyAssociation1...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationHierachy] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation1] FOREIGN KEY ([ChildTaxonomyAssociationId]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_TaxonomyAssociationMetaData_TaxonomyAssociation...';


GO
ALTER TABLE [dbo].[TaxonomyAssociationMetaData] WITH NOCHECK
    ADD CONSTRAINT [FK_TaxonomyAssociationMetaData_TaxonomyAssociation] FOREIGN KEY ([TaxonomyAssociationID]) REFERENCES [dbo].[TaxonomyAssociation] ([TaxonomyAssociationId]);


GO
PRINT N'Creating FK_VerticalXmlImport_VerticalXmlExport...';


GO
ALTER TABLE [dbo].[VerticalXmlImport] WITH NOCHECK
    ADD CONSTRAINT [FK_VerticalXmlImport_VerticalXmlExport] FOREIGN KEY ([ExportBackupId]) REFERENCES [dbo].[VerticalXmlExport] ([VerticalXmlExportId]);


GO
PRINT N'Creating [dbo].[trg_ReportContent_UtcModifiedDate]...';


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
GO
PRINT N'Creating [dbo].[fnFrequencyDescription]...';


GO

CREATE FUNCTION dbo.fnFrequencyDescription
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS VARCHAR(200)
WITH SCHEMABINDING
AS
BEGIN
	RETURN
	(
		SELECT
			CASE @FrequencyType
				WHEN 1
					THEN 'Runs once.'
				WHEN 2
					THEN 'Runs every (' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 ELSE @FrequencyInterval END) + ') days.'
				WHEN 3
					THEN 'Runs Weekly on '
					+ CASE @FrequencyInterval
						WHEN 2 THEN 'Monday'
						WHEN 3 THEN 'Tuesday'
						WHEN 4 THEN 'Wednesday'
						WHEN 5 THEN 'Thursday'
						WHEN 6 THEN 'Friday'
						WHEN 7 THEN 'Saturday'
						ELSE 'Sunday'
					END + '.'
				WHEN 4
					THEN 'Runs Monthly on day ' + CONVERT(VARCHAR(200), CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END) + '.'
				WHEN 5
					THEN 'Runs Quarterly.'
				WHEN 6
					THEN 'Runs Bi-Annually.'
				WHEN 7 
					THEN 'Runs Annually'
				ELSE 'Does not run.'
			END
	);
END;
GO
PRINT N'Creating [dbo].[fnHashBytesNVARCHARMAX]...';


GO
CREATE FUNCTION [dbo].[fnHashBytesNVARCHARMAX]  
(  
 @Algorithm VARCHAR(10),  
 @Text NVARCHAR(MAX)  
)  
RETURNS VARBINARY(8000)  
WITH SCHEMABINDING  
AS  
BEGIN  
 DECLARE @NumHash INT;  
 DECLARE @HASH VARBINARY(8000);  
 SET @NumHash = CEILING(DATALENGTH(@Text) / (8000.0));  
 /* HashBytes only supports 8000 bytes so split the string if it is larger */  
 WHILE @NumHash > 1  
 BEGIN  
  -- # * 4000 character strings  
  WITH a AS  
  (SELECT 1 AS n UNION ALL SELECT 1), -- 2   
  b AS  
  (SELECT 1 AS n FROM a, a a1),       -- 4  
  c AS  
  (SELECT 1 AS n FROM b, b b1),       -- 16  
  d AS  
  (SELECT 1 AS n FROM c, c c1),       -- 256  
  e AS  
  (SELECT 1 AS n FROM d, d d1),       -- 65,536  
  f AS  
  (SELECT 1 AS n FROM e, e e1),       -- 4,294,967,296 = 17+ TRILLION characters  
  factored AS  
  (SELECT ROW_NUMBER() OVER (ORDER BY n) rn FROM f),  
  factors AS  
  (SELECT rn, (rn * 4000) + 1 factor FROM factored)  
  SELECT @Text = CAST  
   (  
    (  
     SELECT CONVERT(VARCHAR(MAX), HASHBYTES(@Algorithm, SUBSTRING(@Text, factor - 4000, 4000)), 1)  
     FROM factors  
     WHERE rn <= @NumHash  
     FOR XML PATH('')  
    ) AS NVARCHAR(MAX)  
   );  
    
  SET @NumHash = CEILING(DATALENGTH(@Text) / (8000.0));  
 END;  
 SET @HASH = CONVERT(VARBINARY(8000), HASHBYTES(@Algorithm, @Text));  
 RETURN @HASH;  
END;
GO
PRINT N'Creating [dbo].[fnNextReportDateUtc]...';


GO
CREATE FUNCTION dbo.fnNextReportDateUtc
(
	@UtcFirstRunDate DATETIME,
	@UtcLastRunDate DATETIME,
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS SMALLDATETIME
WITH SCHEMABINDING
AS
BEGIN
	IF (@UtcFirstRunDate IS NOT NULL)
	BEGIN
		SET @UtcFirstRunDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), @UtcFirstRunDate, 101), 101);
	END

	IF (@UtcLastRunDate IS NOT NULL)
	BEGIN
		SET @UtcLastRunDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), @UtcLastRunDate, 101), 101);
	END

	DECLARE @NextDate SMALLDATETIME = NULL;
	IF @FrequencyType = 1 --One time only
	BEGIN
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NOT NULL
					THEN NULL
					ELSE @UtcFirstRunDate
			END;
	END
	ELSE IF @FrequencyType = 2 --Daily
	BEGIN
		IF @FrequencyInterval < 0
		BEGIN
			SET @FrequencyInterval = 1;
		END
		SET @NextDate =
			CASE
				WHEN @UtcLastRunDate IS NULL 
					THEN @UtcFirstRunDate
					ELSE DATEADD(DAY, @FrequencyInterval, @UtcLastRunDate)
			END;
	END
	ELSE IF @FrequencyType = 3 --Weekly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval BETWEEN 2 AND 7 THEN @FrequencyInterval ELSE 1 END;
		--Can't use DATEPART with WEEKDAY or DW as this is non deterministic. So we pick a sunday date way in the past and do a DATEDIFF.
		DECLARE @SundayDate DATETIME = CONVERT(DATETIME, '18991231', 112);
		DECLARE @DW INT = DATEDIFF(DAY, @SundayDate, @NextDate) % 7 + 1;
		IF (@DW < @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, @FrequencyInterval - @DW, @NextDate);
		END
		ELSE IF (@DW > @FrequencyInterval)
		BEGIN
			SET @NextDate = DATEADD(DAY, 7 - (@DW - @FrequencyInterval), @NextDate);
		END;
	END
	ELSE IF @FrequencyType = 4 --Monthly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(DAY, 1, @UtcLastRunDate), @UtcFirstRunDate);
		SET @FrequencyInterval = CASE WHEN @FrequencyInterval < 1 THEN 1 WHEN @FrequencyInterval > 31 THEN 31 ELSE @FrequencyInterval END;
		DECLARE @DM INT = DAY(@NextDate);
		DECLARE @FixedFrequency INT = @FrequencyInterval;
		DECLARE @MaxDay INT = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, CONVERT(DATETIME, CONVERT(VARCHAR(10), MONTH(@NextDate)) + '/1/' + CONVERT(VARCHAR(10), YEAR(@NextDate)), 101))));
		IF @FixedFrequency > @MaxDay
		BEGIN
			SET	@FixedFrequency = @MaxDay;
		END;
		
		IF @DM < @FixedFrequency
		BEGIN
			SET @NextDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), MONTH(@NextDate)) + '/' + CONVERT(VARCHAR(10), @FixedFrequency) + '/' + CONVERT(VARCHAR(10), YEAR(@NextDate)), 101);
		END
		ELSE IF @DM > @FixedFrequency
		BEGIN
			SET @NextDate = DATEADD(MONTH, 1, CONVERT(DATETIME, CONVERT(VARCHAR(10), MONTH(@NextDate)) + '/1/' + CONVERT(VARCHAR(10), YEAR(@NextDate)), 101));
			SET @MaxDay = DAY(DATEADD(DAY, -1, DATEADD(MONTH, 1, @NextDate)));
			SET @FixedFrequency =
				CASE
					WHEN @FrequencyInterval > @MaxDay
						THEN @MaxDay
						ELSE @FrequencyInterval
				END;
				
			SET @NextDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), MONTH(@NextDate)) + '/' + CONVERT(VARCHAR(10), @FixedFrequency) + '/' + CONVERT(VARCHAR(10), YEAR(@NextDate)), 101);
		END;
	END
	ELSE IF @FrequencyType = 5 --Quarterly
	BEGIN
		SET @NextDate = ISNULL(DATEADD(QUARTER, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 6 --Bi-Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(MONTH, 6, @UtcLastRunDate), @UtcFirstRunDate);
	END
	ELSE IF @FrequencyType = 7 --Annually
	BEGIN
		SET @NextDate = ISNULL(DATEADD(YEAR, 1, @UtcLastRunDate), @UtcFirstRunDate);
	END;
	
	RETURN @NextDate;
END;
GO
PRINT N'Creating [dbo].[fnReportScheduleTypeAndIntervalCheck]...';


GO

CREATE FUNCTION dbo.fnReportScheduleTypeAndIntervalCheck
(
	@FrequencyType INT,
	@FrequencyInterval INT
)
RETURNS BIT
WITH SCHEMABINDING
AS
BEGIN
	RETURN
		CASE @FrequencyType
			WHEN 1 THEN -- Once
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 2 THEN -- Daily
				CASE
					WHEN @FrequencyInterval < 1 THEN 0
					ELSE 1
				END
			WHEN 3 THEN -- Weekly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 7 THEN 1
					ELSE 0
				END
			WHEN 4 THEN -- Monthly
				CASE
					WHEN @FrequencyInterval BETWEEN 1 AND 31 THEN 1
					ELSE 0
				END
			WHEN 5 THEN -- Quarterly
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 6 THEN -- Bi-Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			WHEN 7 THEN -- Annually
				CASE @FrequencyInterval
					WHEN 0 THEN 1
					ELSE 0
				END
			ELSE 0
		END;
END;
GO
PRINT N'Creating [dbo].[ClientDocumentData]...';


GO
CREATE TABLE [dbo].[ClientDocumentData] (
    [ClientDocumentId] INT             NOT NULL,
    [Data]             VARBINARY (MAX) NULL,
    [HasData]          AS              (CASE WHEN [Data] IS NULL THEN (0) ELSE (1) END) PERSISTED NOT NULL,
    [DataLength]       AS              (len([Data])) PERSISTED,
    [DataHash]         AS              (CONVERT (VARBINARY (20), CASE [Data] WHEN NULL THEN NULL ELSE [dbo].[fnHashBytesNVARCHARMAX]('SHA1', [Data]) END, 0)) PERSISTED,
    [UtcModifiedDate]  DATETIME        NOT NULL,
    [ModifiedBy]       INT             NULL,
    CONSTRAINT [PK_ClientDocumentData] PRIMARY KEY CLUSTERED ([ClientDocumentId] ASC)
);


GO
PRINT N'Creating [dbo].[ReportContentData]...';


GO
CREATE TABLE [dbo].[ReportContentData] (
    [ReportContentId] INT             NOT NULL,
    [Data]            VARBINARY (MAX) NULL,
    [HasData]         AS              (CASE WHEN [Data] IS NULL THEN (0) ELSE (1) END) PERSISTED NOT NULL,
    [DataLength]      AS              (len([Data])) PERSISTED,
    [DataHash]        AS              (CONVERT (VARBINARY (20), CASE [Data] WHEN NULL THEN NULL ELSE [dbo].[fnHashBytesNVARCHARMAX]('SHA1', [Data]) END, 0)) PERSISTED,
    [UtcModifiedDate] DATETIME        NOT NULL,
    [ModifiedBy]      INT             NULL,
    CONSTRAINT [PK_ReportContentData] PRIMARY KEY CLUSTERED ([ReportContentId] ASC)
);


GO
PRINT N'Creating [dbo].[ReportSchedule]...';


GO
CREATE TABLE [dbo].[ReportSchedule] (
    [ReportScheduleId]        INT      IDENTITY (1, 1) NOT NULL,
    [ReportId]                INT      NOT NULL,
    [IsEnabled]               BIT      NOT NULL,
    [FrequencyType]           INT      NOT NULL,
    [FrequencyInterval]       INT      NOT NULL,
    [UtcFirstRunDate]         DATETIME NOT NULL,
    [UtcLastScheduledRunDate] DATETIME NULL,
    [UtcLastRunDate]          DATETIME NULL,
    [UtcModifiedDate]         DATETIME NOT NULL,
    [ModifiedBy]              INT      NULL,
    [UtcNextRunDate]          AS       ([dbo].[fnNextReportDateUtc]([UtcFirstRunDate], [UtcLastScheduledRunDate], [FrequencyType], [FrequencyInterval])) PERSISTED,
    [FrequencyDescription]    AS       ([dbo].[fnFrequencyDescription]([FrequencyType], [FrequencyInterval])) PERSISTED,
    CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED ([ReportScheduleId] ASC)
);


GO
PRINT N'Creating [dbo].[BrowserVersion]...';

CREATE TABLE [dbo].[BrowserVersion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	[Version] [INT] NOT NULL,
	[DownloadUrl] [NVARCHAR](300) NOT NULL
)

GO
PRINT N'Creating FK_ClientDocumentData_ClientDocument...';


GO
ALTER TABLE [dbo].[ClientDocumentData] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDocumentData_ClientDocument] FOREIGN KEY ([ClientDocumentId]) REFERENCES [dbo].[ClientDocument] ([ClientDocumentId]);


GO
PRINT N'Creating FK_ReportContentData_ReportContent...';


GO
ALTER TABLE [dbo].[ReportContentData] WITH NOCHECK
    ADD CONSTRAINT [FK_ReportContentData_ReportContent] FOREIGN KEY ([ReportContentId]) REFERENCES [dbo].[ReportContent] ([ReportContentId]);


GO
PRINT N'Creating DF_ClientDocumentData_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ClientDocumentData]
    ADD CONSTRAINT [DF_ClientDocumentData_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ReportContentData_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ReportContentData]
    ADD CONSTRAINT [DF_ReportContentData_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating DF_ReportSchedule_IsEnabled...';


GO
ALTER TABLE [dbo].[ReportSchedule]
    ADD CONSTRAINT [DF_ReportSchedule_IsEnabled] DEFAULT ((1)) FOR [IsEnabled];


GO
PRINT N'Creating DF_ReportSchedule_UtcModifiedDate...';


GO
ALTER TABLE [dbo].[ReportSchedule]
    ADD CONSTRAINT [DF_ReportSchedule_UtcModifiedDate] DEFAULT (getutcdate()) FOR [UtcModifiedDate];


GO
PRINT N'Creating [dbo].[trg_ReportContentData_UtcModifiedDate]...';


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
GO
PRINT N'Creating CK_ReportSchedule_FrequencyType_FrequencyInterval...';


GO
ALTER TABLE [dbo].[ReportSchedule] WITH NOCHECK
    ADD CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval] CHECK ([dbo].[fnReportScheduleTypeAndIntervalCheck]([FrequencyType],[FrequencyInterval])=(1));


GO
PRINT N'Creating [dbo].[CUDHistory_Insert]...';


GO
-- Stored procedure for CUDHistory and Data inserts. Called from Triggers.
CREATE PROCEDURE [dbo].CUDHistory_Insert
(
	@CUDHistory dbo.TT_CUDHistory READONLY,
	@CUDHistoryData dbo.TT_CUDHistoryData READONLY
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdMapTable TABLE
	(
	CUDHistoryId INT NOT NULL PRIMARY KEY CLUSTERED,
	TT_Id INT NULL,
	TableName NVARCHAR(128) NOT NULL,
	[Key] INT NOT NULL,
	SecondKey NVARCHAR(200) NULL,
	ThirdKey NVARCHAR(200) NULL
	);
	
	DECLARE @BatchId UNIQUEIDENTIFIER;
	
	SET @BatchId = NEWID();
	
	INSERT	INTO CUDHistory (TableName, [Key], SecondKey, ThirdKey, CUDType, BatchId, UserId)
	OUTPUT	inserted.CUDHistoryId, inserted.TableName, inserted.[Key], inserted.SecondKey, inserted.ThirdKey
	INTO	@IdMapTable (CUDHistoryId, TableName, [Key], SecondKey, ThirdKey)
	SELECT	c.TableName, c.[Key], c.SecondKey, c.ThirdKey, c.CUDType, @BatchId, c.UserId
	FROM	@CUDHistory c;
	
	UPDATE	t1
	SET	t1.TT_Id = c.Id
	FROM	@IdMapTable t1
	INNER JOIN	@CUDHistory c
	ON	t1.TableName = c.TableName
	AND	t1.[Key] = c.[Key]
	AND (t1.SecondKey = c.SecondKey OR (t1.SecondKey IS NULL AND c.SecondKey IS NULL))
	AND (t1.ThirdKey = c.ThirdKey OR (t1.ThirdKey IS NULL AND c.ThirdKey IS NULL));
	
	INSERT INTO CUDHistoryData (CUDHistoryId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	t1.CUDHistoryId, cd.ColumnName, cd.SqlDbType, cd.OldValue, cd.NewValue
	FROM	@CUDHistoryData cd
	INNER JOIN @IdMapTable t1
	ON	cd.ParentId = t1.TT_Id;
END;
GO
PRINT N'Creating [dbo].[RPV2Hosted_SaveErrorLog]...';


GO
CREATE PROCEDURE [dbo].RPV2Hosted_SaveErrorLog
@ErrorCode int,
@Priority int,
@Severity nvarchar(32),
@Title nvarchar(256),
@MachineName nvarchar(32),
@AppDomainName nvarchar(512),
@ProcessID nvarchar(256),
@ProcessName nvarchar(512),
@ThreadName nvarchar(512),
@Win32ThreadId nvarchar(128),
@EventId int,
@SiteActivityId int,
@Message nvarchar(1500),
@FormattedMessage nvarchar(max),
@URL nvarchar(500),
@AbsoluteURL nvarchar(500)
AS
BEGIN

  INSERT INTO ErrorLog
     (ErrorCode,
      ErrorUtcDate,
      [Priority],
      Severity,
      Title,
      MachineName,
      AppDomainName,
      ProcessID,
      ProcessName,
      ThreadName,
      Win32ThreadId,
      EventId,
      SiteActivityId,
      [Message],
      FormattedMessage,
	  URL,
	  AbsoluteURL
      )
   VALUES
     (@ErrorCode,
      GETUTCDATE(),
      @Priority,
      @Severity,
      @Title,
      @MachineName,
      @AppDomainName,
      @ProcessID,
      @ProcessName,
      @ThreadName,
      @Win32ThreadId,
      @EventId,
      @SiteActivityId,
      @Message,
      @FormattedMessage,
	  @URL,
	  @AbsoluteURL
      ) 
     
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ClientDocumentData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		
-- Create date: 
-- RPV2HostedAdmin_ClientDocumentData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentData_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentID,COUNT_BIG(*)	 
   FROM
     ClientDocument
   GROUP BY ClientDocumentID        
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ClientDocumentGroup_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentGroup_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentGroupId,COUNT_BIG(*)	 
   FROM
     ClientDocumentGroup
   GROUP BY ClientDocumentGroupId        
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ClientDocumentGroupClientDocument_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentGroupClientDocument_CacheDependencyCheck]
AS
BEGIN
  SELECT DISTINCT
	 ClientDocumentGroupId,ClientDocumentId,COUNT_BIG(*)	 
   FROM
     ClientDocumentGroupClientDocument
   GROUP BY ClientDocumentGroupId , ClientDocumentId      
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ClientDocumentTypeData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Arshdeep
-- Create date: 6th-Oct-2015
-- RPV2HostedAdmin_ClientDocumentTypeData_CacheDependencyCheck
-- =============================================
Create PROCEDURE [dbo].[RPV2HostedAdmin_ClientDocumentTypeData_CacheDependencyCheck]
AS
BEGIN
 SELECT DISTINCT
	 ClientDocumentTypeID,COUNT_BIG(*)	 
   FROM
     ClientDocumentType
   GROUP BY ClientDocumentTypeID   
   
   Select Distinct  ClientDocumentTypeID,COUNT_BIG(*)	 
   FROM
     ClientDocument
   GROUP BY ClientDocumentTypeID 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_CUDHistory_CacheDependencyCheck]...';


GO
 CREATE PROCEDURE [dbo].[RPV2HostedAdmin_CUDHistory_CacheDependencyCheck]

AS

BEGIN

   	SELECT	CHD.CUDHistoryId, COUNT_BIG(*) AS Total

	FROM [CUDHistory] CH INNER JOIN [CUDHistoryData] CHD on
      
	 CH.CUDHistoryID=CHD.CUDHistoryID

	 GROUP BY CHD.CUDHistoryId,CHD.ColumnName

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteClientDocument]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocument]
@ClientDocumentID int,
@DeletedBy int
AS
BEGIN

	DELETE ClientDocumentData
      WHERE ClientDocumentId = @ClientDocumentID;

	DELETE ClientDocumentGroupClientDocument
	  WHERE ClientDocumentId = @ClientDocumentID;

     DELETE ClientDocument
      WHERE ClientDocumentId = @ClientDocumentID;
            
	UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocumentData'
		AND	[Key] = @ClientDocumentID AND CUDType = 'D';

    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocument'
		AND	[Key] = @ClientDocumentID AND CUDType = 'D';
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroup]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroup]
	@ClientDocumentGroupId INT, 
	@deletedBy INT
AS
BEGIN
	DELETE FROM ClientDocumentGroupClientDocument WHERE
		ClientDocumentGroupId = @ClientDocumentGroupId 
	DELETE FROM ClientDocumentGroup WHERE 
		ClientDocumentGroupId = @ClientDocumentGroupId

	UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	(TableName = N'ClientDocumentGroup' or TableName = N'ClientDocumentGroupClientDocument')
				AND	[Key] = @ClientDocumentGroupId
				AND [CUDType] = 'D' 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroupClientDocument]...';


GO
--create procedure [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]
--as
--begin
--	select 
--		ClientDocumentGroupId,
--		Name,
--		[Description],
--		ParentClientDocumentGroupId,
--		CssClass,
--		UtcModifiedDate,
--		ModifiedBy
--	from ClientDocumentGroup
--end

--sp_rename '[dbo].[RPV2HostedAdmin_GetAllUrlClientDocumentGroup]','[dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]'

--CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocument]
--AS
--BEGIN
--	SELECT 
--		ClientDocumentId,
--		ClientDocumentTypeId,
--		[FileName],
--		MimeType,
--		IsPrivate,
--		ContentUri,
--		Name,
--		[Description],
--		UtcModifiedDate,
--		ModifiedBy
--	FROM ClientDocument
--END

--exec [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]

--CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroupClientDocument]
--AS
--BEGIN
--	SELECT 
--		ClientDocumentGroupId,
--		ClientDocumentId,
--		[Order],
--		UtcModifiedDate,
--		ModifiedBy
--	FROM ClientDocumentGroupClientDocument
--END

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentGroupClientDocument]
	@ClientDocumentGroupId INT,
	@ClientDocumentId INT,
	@deletedBy INT
AS
BEGIN
	DELETE FROM ClientDocumentGroupClientDocument WHERE
		ClientDocumentGroupId = @ClientDocumentGroupId and
		ClientDocumentId = @ClientDocumentId

		UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'ClientDocumentGroupClientDocument'
				AND	[Key] = @ClientDocumentGroupId
				AND [CUDType] = 'D' 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteClientDocumentType]...';


GO
Create PROCEDURE [dbo].[RPV2HostedAdmin_DeleteClientDocumentType]
@ClientDocumentTypeID int,
@DeletedBy int
AS
BEGIN

     DELETE ClientDocumentType
      WHERE ClientDocumentTypeId = @ClientDocumentTypeID;
            
    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ClientDocumentType'
		AND	[Key] = @ClientDocumentTypeID AND CUDType = 'D';
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteDocumentTypeAssociation]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteDocumentTypeAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteDocumentTypeAssociation
@DocumentTypeAssociationId int,
@deletedBy int
AS
BEGIN
  DELETE DocumentTypeAssociation
  WHERE DocumentTypeAssociationId = @DocumentTypeAssociationId

    UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'DocumentTypeAssociation'
				AND	[Key] = @DocumentTypeAssociationId
				AND [CUDType] = 'D' 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteDocumentTypeExternalId]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteDocumentTypeExternalId
@DocumentTypeId int,
@ExternalId nvarchar(100),
@DeletedBy int
AS
BEGIN


     DELETE DocumentTypeExternalId
      WHERE DocumentTypeId = @DocumentTypeId
       AND ExternalId = @ExternalId      
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'DocumentTypeExternalId'
		AND	[Key] = @DocumentTypeId
		AND [SecondKey] = @ExternalId
		AND [CUDType] = 'D'; 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteFootnote]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteFootnote]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE UrlRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteFootnote] 
				 @FootnoteId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE Footnote
		   WHERE FootnoteId = @FootnoteId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'Footnote'
				AND	[Key] = @FootnoteId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeletePageFeature]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeletePageFeature] 


@deletedBy int,
@PageId int,
@PageKey varchar(200),
@SiteId int
AS
BEGIN



      DELETE from  PageFeature
		     WHERE PageId = @PageId
		     AND
		     [Key] = @PageKey
		     AND 
		     SiteId = @SiteId


      UPDATE CUDHistory				 
			 SET	UserId = @deletedBy
			 WHERE	TableName = N'PageFeature'
					AND	[Key] = @PageId
				    AND [CUDType] = 'D' 


END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeletePageNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeletePageNavigation]
@PageNavigationID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE PageNavigationVersion
      WHERE PageNavigationId = @PageNavigationID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE PageNavigationVersion
      WHERE PageNavigationId = @PageNavigationID      
     DELETE PageNavigation
       WHERE PageNavigationId = @PageNavigationID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'PageNavigation'
		AND	[Key] = @PageNavigationID AND CUDTYPE = 'D' ;
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeletePageText]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeletePageText
@PageTextID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE PageTextVersion
      WHERE PageTextId = @PageTextID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE PageTextVersion
      WHERE PageTextId = @PageTextID      
     DELETE PageText
       WHERE PageTextId = @PageTextID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'PageText'
		AND	[Key] = @PageTextID AND CUDTYPE = 'D' ;
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteReportContent]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteReportContent]
	Added By: Nimmy Rose Antony
	Date: 15 Oct 2015
	Reason : To delete the Report Content
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteReportContent]
@ReportContentId int,
@DeletedBy int
AS
BEGIN

	DELETE [dbo].[ReportContent]
      WHERE ReportContentId = @ReportContentId;

	DELETE [dbo].[ReportContentData]
	  WHERE [ReportContentId] = @ReportContentId;
            
	UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ReportContent'
		AND	[Key] = @ReportContentId AND CUDType = 'D';

    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'ReportContentData'
		AND	[Key] = @ReportContentId AND CUDType = 'D';
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteSite]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteSite
@SiteID int,
@DeletedBy int
AS
BEGIN

     DELETE Site
      WHERE SiteId = @SiteID;
            
    UPDATE	CUDHistory
  	 SET		UserId = @DeletedBy
	 WHERE	TableName = N'Site'
		AND	[Key] = @SiteID AND CUDType = 'D';
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteSiteFeature]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteSiteFeature] 
@SiteId int,
@SiteKey varchar(200),
@deletedBy int
AS

BEGIN
      DELETE SiteFeature
		   WHERE SiteId = @SiteId AND [Key] = @SiteKey

      UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'SiteFeature'
				AND	[Key] = @SiteId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteSiteNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteSiteNavigation]
@SiteNavigationId int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE SiteNavigationVersion
      WHERE SiteNavigationId = @SiteNavigationId
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE SiteNavigationVersion
      WHERE SiteNavigationId = @SiteNavigationId      
     DELETE SiteNavigation
       WHERE SiteNavigationId = @SiteNavigationId
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'SiteNavigation'
		AND	[Key] = @SiteNavigationId AND CUDTYPE = 'D' ;
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteSiteText]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteSiteText
@SiteTextID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE SiteTextVersion
      WHERE SiteTextId = @SiteTextID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE SiteTextVersion
      WHERE SiteTextId = @SiteTextID      
     DELETE SiteText
       WHERE SiteTextId = @SiteTextID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'SiteText'
		AND	[Key] = @SiteTextID AND CUDType = 'D';
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteStaticResource]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteStaticResource]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE DeleteStaticResource
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteStaticResource] 
				 @StaticResourceId int,
				 @deletedBy int
				                 				 			
AS 
BEGIN 
		  DELETE StaticResource
		    WHERE StaticResourceId = @StaticResourceId
   
		  UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'StaticResource'
				AND	[Key] = @StaticResourceId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociation]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete TaxonomyAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteTaxonomyAssociation
@TaxonomyAssociationId int,
@deletedBy int
AS
BEGIN
  DELETE TaxonomyAssociation
  WHERE TaxonomyAssociationId = @TaxonomyAssociationId

   UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'TaxonomyAssociation'
				AND	[Key] = @TaxonomyAssociationId
				AND [CUDType] = 'D' 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To delete DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy
@ParentTaxonomyAssociationId int,
@ChildTaxonomyAssociationId int,
@RelationshipType int,
@deletedBy int
AS
BEGIN
  DELETE TaxonomyAssociationHierachy
  WHERE ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId
	AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
	AND RelationshipType = @RelationshipType

    UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
		  WHERE	TableName = N'TaxonomyAssociationHierachy'
				AND	[Key] = @ParentTaxonomyAssociationId
				AND SecondKey = @ChildTaxonomyAssociationId
				AND ThirdKey = @RelationshipType
				AND [CUDType] = 'D' 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteTaxonomyLevelExternalId]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteTaxonomyLevelExternalId]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE TaxonomyLevelExternalId
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteTaxonomyLevelExternalId] 
				 @Level int,
				 @TaxonomyId int,
				 @ExternalId nvarchar(100),
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE TaxonomyLevelExternalId
		    WHERE [Level] = @Level
		    AND TaxonomyId = @TaxonomyId
		    AND ExternalId = @ExternalId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'TaxonomyLevelExternalId'
				AND	[Key] = @Level
				AND [SecondKey] = @TaxonomyId
				AND [ThirdKey] = @ExternalId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteUrlRewrite]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DeleteUrlRewrite]
	Added By: Noel
	Date: 09/19/2015
	Reason : To DELETE THE UrlRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteUrlRewrite] 
				 @UrlRewriteId int,
				 @deletedBy int                				 			
AS 
BEGIN 
		  DELETE UrlRewrite
		   WHERE UrlRewriteId = @UrlRewriteId
   
			UPDATE	CUDHistory				 
			 SET	UserId = @deletedBy
			WHERE	TableName = N'UrlRewrite'
				AND	[Key] = @UrlRewriteId
				AND [CUDType] = 'D' 

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DequeueVerticalXmlExport]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DequeueVerticalXmlExport]
	Added By: Noel Dsouza
	Date: 10/13/2015	
	Reason : To Dequeue VerticalXMLExport
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DequeueVerticalXmlExport
AS
BEGIN
SET NOCOUNT ON

DECLARE @BATCHSIZE INT
SET @BATCHSIZE = 1

UPDATE TOP(@BATCHSIZE) VerticalXmlExport WITH (UPDLOCK, READPAST)
 SET STATUS = 1
 OUTPUT INSERTED.VerticalXmlExportId,
		 INSERTED.ExportedBy, 
		 INSERTED.ExportDate, 
		 INSERTED.ExportTypes,
		 INSERTED.ExportDescription, 
		 INSERTED.[Status]
 WHERE STATUS = 0
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DequeueVerticalXmlImport]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_DequeueVerticalXmlImport]
	Added By: Noel Dsouza
	Date: 10/22/2015	
	Reason : To Dequeue VerticalXMLImport
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_DequeueVerticalXmlImport
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @DequeueVerticalXmlImport TABLE
	(
		VerticalXmlImportId int,
		ImportedBy int,
		ImportDate datetime,
		ImportTypes int,
		ImportXml xml,
		ImportExportXML xml,
		[Status] int
	)

	DECLARE @BATCHSIZE INT
	SET @BATCHSIZE = 1

	UPDATE TOP(@BATCHSIZE) VerticalXmlImport WITH (UPDLOCK, READPAST)
	 SET STATUS = 1
	 OUTPUT INSERTED.VerticalXmlImportId,
			 INSERTED.ImportedBy, 
			 INSERTED.ImportDate, 
			 INSERTED.ImportTypes,
			 INSERTED.ImportXml,
			 INSERTED.ImportXml AS ImportExportXML, 
			 INSERTED.[Status] INTO @DequeueVerticalXmlImport
	 WHERE STATUS = 0
 
	 IF @@ROWCOUNT = 0
	 BEGIN
		 UPDATE TOP(@BATCHSIZE) VerticalXmlImport WITH (UPDLOCK, READPAST)
		 SET STATUS = 5
		 OUTPUT INSERTED.VerticalXmlImportId,
				 INSERTED.ImportedBy, 
				 INSERTED.ImportDate, 
				 INSERTED.ImportTypes,
				 INSERTED.ImportXml,
				 VerticalXmlExport.ExportXml AS ImportExportXML,				 
				 INSERTED.[Status]  INTO @DequeueVerticalXmlImport
		 FROM VerticalXmlImport 
		 INNER JOIN VerticalXmlExport ON VerticalXmlImport.ExportBackupId = VerticalXmlExport.VerticalXmlExportId
		 WHERE VerticalXmlImport.[Status] = 4
	 END
	 
	 SELECT * FROM @DequeueVerticalXmlImport
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DocumentTypeData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 22nd-Sep-2015
-- RPV2HostedAdmin_DocumentTypeData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_DocumentTypeData_CacheDependencyCheck
AS
BEGIN
  SELECT DISTINCT
	 DocumentTypeID,COUNT_BIG(*)	 
   FROM
     DocumentTypeAssociation
   GROUP BY DocumentTypeID        
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_DocumentTypeExternalIdData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DocumentTypeExternalIdData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	DocumentTypeId,ExternalId, COUNT_BIG(*) AS Total
	FROM	dbo.DocumentTypeExternalId
	GROUP BY DocumentTypeId,ExternalId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllClientDocument]...';


GO
-- =============================================
-- Author:		
-- Create date: 
-- EXEC RPV2HostedAdmin_GetAllClientDocument
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocument]
AS
BEGIN

SELECT  DISTINCT

	CD.ClientDocumentId
      ,CD.ClientDocumentTypeId
	  ,CDT.Name AS ClientDocumentTypeName
      ,CD.FileName
      ,CD.MimeType
      ,CD.IsPrivate
      ,CD.ContentUri
      ,CD.Name
      ,CD.Description
      ,CD.UtcModifiedDate AS UtcLastModified
      ,CD.ModifiedBy
  FROM 
		[dbo].[ClientDocument] CD
		INNER JOIN ClientDocumentType CDT
		ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentGroup]
AS
BEGIN
	SELECT 
		[ClientDocumentGroup].ClientDocumentGroupId,
		[ClientDocumentGroup].[Name],
		[ClientDocumentGroup].[Description],
		ParentClientDocumentGroupId,
		CssClass,
		[ClientDocumentGroup].UtcModifiedDate as UtcLastModified,
		[ClientDocumentGroup].ModifiedBy,
		[ClientDocumentGroupClientDocument].ClientDocumentId,
		[Order],
		[FileName],
		[MimeType]
	FROM [dbo].[ClientDocumentGroup] 
	LEFT JOIN [dbo].[ClientDocumentGroupClientDocument]
	ON [ClientDocumentGroup].ClientDocumentGroupId = [ClientDocumentGroupClientDocument].ClientDocumentGroupId
	LEFT JOIN [dbo].[ClientDocument]
	ON [ClientDocumentGroupClientDocument].ClientDocumentId = [ClientDocument].ClientDocumentId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllClientDocumentType]...';


GO
-- =============================================
-- Author:		Arshdeep
-- Create date: 6th-Oct-2015
-- RPV2HostedAdmin_RPV2HostedAdmin_GetAllClientDocumentType
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllClientDocumentType]
AS
BEGIN

   SELECT DISTINCT
		ClientDocumentTypeId,
		Name,
		[Description],
		UtcModifiedDate as UtcLastModified,
		ModifiedBy as ModifiedBy,
		HostedDocumentsDisplayCount,
		FTPName,
		FTPUsername,
		FTPPassword,
		IsSFTP
	FROM ClientDocumentType     

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllCUDHistory]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllCUDHistory]
AS
BEGIN
	SELECT 
	TableName,
	UserId
	FROM [CUDHistory] ORDER BY TableName
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllDocumentType]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 22nd-Sep-2015
-- RPV2HostedAdmin_GetAllDocumentType
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllDocumentType
AS
BEGIN
  SELECT DISTINCT
	 DocumentTypeID,
	 HeaderText as DocumentTypeName
   FROM
     DocumentTypeAssociation
      INNER JOIN ClientSettings on DocumentTypeAssociation.SiteId = ClientSettings.DefaultSiteId   
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllDocumentTypeAssociation]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllDocumentTypeAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllDocumentTypeAssociation
AS
BEGIN
 SELECT DocumentTypeAssociationId
      ,DocumentTypeId
      ,SiteId
      ,TaxonomyAssociationId
      ,[Order]
      ,HeaderText
      ,LinkText
      ,DescriptionOverride
      ,CssClass
      ,MarketId
      ,UtcModifiedDate AS UtcLastModified
      ,ModifiedBy
  FROM dbo.DocumentTypeAssociation

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllDocumentTypeExternalId]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllDocumentTypeExternalId]
AS
BEGIN
SELECT DTE.DocumentTypeId,
	   DTE.ExternalId,
	   DTE.UtcModifiedDate as UtcLastModified,
	   ISNULL(DTE.ModifiedBy,0) as ModifiedBy,
	   DTE.IsPrimary
FROM DocumentTypeExternalId DTE
WHERE DTE.DocumentTypeId <> -1
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllFootnote]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 13th-Oct-2015
-- RPV2HostedAdmin_GetAllFootnote
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllFootnote
AS
BEGIN
  SELECT FootnoteId
      ,TaxonomyAssociationId
      ,TaxonomyAssociationGroupId
      ,LanguageCulture
      ,[Text]
      ,[Order]
      ,UtcModifiedDate as UtcLastModified
      ,ModifiedBy
  FROM dbo.Footnote
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllPageFeature]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllPageFeature]
AS
BEGIN
    SELECT DISTINCT 
	         SiteId,
			 PageId,
			 [Key] as PageKey,
			 FeatureMode,
			 UtcModifiedDate as UtcLastModified,
		     ModifiedBy as ModifiedBy
             FROM
			 dbo.[PageFeature]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllPageNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllPageNavigation]
AS
BEGIN

 SELECT DISTINCT PageNavigation.PageNavigationId, 
		PageNavigationVersion.[Version],
		PageNavigation.PageId,		
		[Site].TemplateId,				
		[Site].SITEID,
		[Site].Name as SiteName,
		NavigationKey,		 
		CAST(PageNavigationVersion.[NavigationXml] AS  NVARCHAR(MAX) ) NavigationXML,	 
		 CONVERT(bit,Case When CurrentVersion =PageNavigationVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForPageNavigationID,
		 LanguageCulture,
		 PageNavigation.UtcModifiedDate as UtcLastModified,
		 PageNavigation.ModifiedBy as ModifiedBy
         FROM PageNavigation
         INNER JOIN [Site] on PageNavigation.SiteId = [Site].SiteId         
         LEFT OUTER JOIN 
           PageNavigationVersion ON [PageNavigation].PageNavigationId = PageNavigationVersion.PageNavigationId 
			AND PageNavigationVersion.[Version] >= PageNavigation.CurrentVersion 
         LEFT OUTER JOIN 
           PageNavigationVersion Proofing ON [PageNavigation].PageNavigationId = Proofing.PageNavigationId 
			AND Proofing.[Version] > PageNavigation.CurrentVersion 			
	ORDER BY PageNavigation.PageNavigationId

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllPageText]...';


GO

CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllPageText
AS
BEGIN

  SELECT DISTINCT PageText.PageTextId, 
		PageTextVersion.[Version],
		PageText.PageId,		
		[Site].TemplateId,				
		[Site].SITEID,
		[Site].Name as SiteName,
		 ResourceKey,		 
		 PageTextVersion.[Text],		 
		 CONVERT(bit,Case When CurrentVersion =PageTextVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForPageTextID,
		 LanguageCulture,
		 PageText.UtcModifiedDate as UtcLastModified,
		 PageText.ModifiedBy as ModifiedBy
         FROM PageText
         INNER JOIN [Site] on PageText.SiteId = [Site].SiteId         
         LEFT OUTER JOIN 
           PageTextVersion ON [PageText].PageTextId = PageTextVersion.PageTextId 
			AND PageTextVersion.[Version] >= PageText.CurrentVersion 
         LEFT OUTER JOIN 
           PageTextVersion Proofing ON [PageText].PageTextId = Proofing.PageTextId 
			AND Proofing.[Version] > PageText.CurrentVersion 			
	ORDER BY PageText.PageTextId
	



END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllReportContent]...';


GO
-- =============================================
-- Author:	Nimmy Rose Antony	
-- Create date: 15 Oct 2015
-- EXEC RPV2HostedAdmin_GetAllReportContent
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllReportContent]
AS
BEGIN

SELECT  DISTINCT

	 ReportContentId
	, ReportScheduleId
	, MimeType
	, IsPrivate
	, ContentUri
	, Name
	, ReportRunDate
	, UtcModifiedDate as ModifiedDate
	, ModifiedBy
	, FileName
	, Description
	
FROM 
		ReportContent
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllSite]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllSite
AS
BEGIN
  SELECT DISTINCT SiteID,
		NAME,
		TemplateId,
		DefaultPageID,
		ParentSiteId,
		[Description],
		CONVERT(bit,Case When ClientSettings.DefaultSiteId IS NOT NULL Then 1 Else 0 End) AS 'IsDefaultSite',			
		[SITE].UtcModifiedDate as UtcLastModified,
		[SITE].ModifiedBy as ModifiedBy   
    FROM [SITE]
	LEFT JOIN ClientSettings ON [SITE].SiteId = ClientSettings.DefaultSiteId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllSiteFeature]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllSiteFeature]
AS
BEGIN
    SELECT DISTINCT 
	         SiteId,
			 [Key] as SiteKey,
			 FeatureMode,
			 UtcModifiedDate as UtcLastModified,
		     ModifiedBy as ModifiedBy
             FROM
			 dbo.[SiteFeature]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllSiteNavigation]...';


GO
CREATE PROCEDURE   [dbo].[RPV2HostedAdmin_GetAllSiteNavigation]
AS
BEGIN
	SELECT DISTINCT SiteNavigation.SiteNavigationId, 
		SiteNavigationVersion.[Version],
		[Site].SITEID,
		[Site].Name as SiteName,
		NavigationKey,		 
		SiteNavigation.PageId,
		 CAST(SiteNavigationVersion.[NavigationXml] AS  NVARCHAR(MAX) ) NavigationXML,		 
		 CONVERT(bit,Case When CurrentVersion = SiteNavigationVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForSiteNavigationId,
		 LanguageCulture,
		 SiteNavigation.UtcModifiedDate as UtcLastModified,
		 SiteNavigation.ModifiedBy as ModifiedBy
         FROM SiteNavigation
         INNER JOIN [Site] on SiteNavigation.SiteId = [Site].SiteId
         LEFT OUTER JOIN 
           SiteNavigationVersion ON [SiteNavigation].SiteNavigationId = SiteNavigationVersion.SiteNavigationId 
			AND SiteNavigationVersion.[Version] >= SiteNavigation.CurrentVersion 
         LEFT OUTER JOIN 
           SiteNavigationVersion Proofing ON [SiteNavigation].SiteNavigationId = Proofing.SiteNavigationId 
			AND Proofing.[Version] > SiteNavigation.CurrentVersion 			
	ORDER BY SiteNavigation.SiteNavigationId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllSiteText]...';


GO

CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllSiteText
AS
BEGIN

  SELECT DISTINCT SiteText.SiteTextId, 
		SiteTextVersion.[Version],
		[Site].SITEID,
		[Site].Name as SiteName,
		 ResourceKey,		 
		 SiteTextVersion.[Text],		 
		 CONVERT(bit,Case When CurrentVersion =SiteTextVersion.[Version] Then 0 Else 1 End) as IsProofing,
		 CONVERT(bit,Case When Proofing.[Version] IS not null Then 1 Else 0 End) as IsProofingAvailableForSiteTextID,
		 LanguageCulture,
		 SiteText.UtcModifiedDate as UtcLastModified,
		 SiteText.ModifiedBy as ModifiedBy
         FROM SiteText
         INNER JOIN [Site] on SiteText.SiteId = [Site].SiteId
         LEFT OUTER JOIN 
           SiteTextVersion ON [SiteText].SiteTextId = SiteTextVersion.SiteTextId 
			AND SiteTextVersion.[Version] >= SiteText.CurrentVersion 
         LEFT OUTER JOIN 
           SiteTextVersion Proofing ON [SiteText].SiteTextId = Proofing.SiteTextId 
			AND Proofing.[Version] > SiteText.CurrentVersion 			
	ORDER BY SiteText.SiteTextId
	



END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllStaticResource]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
-- RPV2HostedAdmin_GetAllStaticResource
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllStaticResource]
AS
BEGIN
	SELECT 
		   StaticResourceId,
		   [FileName],
		   Size,
		   MimeType,
		   Data,		   
		   [UtcModifiedDate] as UtcLastModified,
		   [ModifiedBy]
	  FROM [dbo].[StaticResource]
		  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTaxonomy]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 2nd-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTaxonomy
AS
BEGIN
  SELECT DISTINCT 		
		[LEVEL],		
		TaxonomyId,
		NameOverride AS TaxonomyName
	FROM TaxonomyAssociation
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTaxonomyAssociation]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomyAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllTaxonomyAssociation
AS
BEGIN
 SELECT TaxonomyAssociationId
      ,[Level]
      ,TaxonomyId
      ,SiteId
      ,ParentTaxonomyAssociationId
      ,NameOverride
      ,DescriptionOverride
      ,CssClass
      ,MarketId
      ,UtcModifiedDate AS UtcLastModified
      ,ModifiedBy
  FROM dbo.TaxonomyAssociation

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy
AS
BEGIN
 SELECT [ParentTaxonomyAssociationId]
      ,[ChildTaxonomyAssociationId]
      ,[RelationshipType]
      ,[Order]
      ,[UtcModifiedDate] AS UtcLastModified
      ,[ModifiedBy]
  FROM [dbo].[TaxonomyAssociationHierachy]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTaxonomyLevelExternalId]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
-- RPV2HostedAdmin_GetAllTaxonomyLevelExternalId
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTaxonomyLevelExternalId]
AS
BEGIN
	SELECT DISTINCT 
		   TLE.[Level],
		   TLE.TaxonomyId,
		   TA.NameOverride AS TaxonomyName,
		   TLE.ExternalId,
		   TLE.IsPrimary,
		   TLE.[UtcModifiedDate] as UtcLastModified,
		   TLE.[ModifiedBy]
	  FROM [TaxonomyLevelExternalId] TLE
	   INNER JOIN [TaxonomyAssociation]	TA ON TLE.[Level] = TA.[Level]
			AND TLE.TaxonomyId = TA.TaxonomyId
	  ORDER BY TLE.TaxonomyId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllUrlRewrite]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 19th-Sep-2015
--RPV2HostedAdmin_GetAllUrlRewrite
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllUrlRewrite]
AS
BEGIN
	SELECT 
		   UrlRewriteId,
		   MatchPattern,
		   RewriteFormat,
		   PatternName,		
		   UtcModifiedDate as UtcLastModified,
		   ModifiedBy as ModifiedBy
	  FROM [dbo].[UrlRewrite]
		  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllUrlRewriteText]...';


GO

CREATE procedure [dbo].[RPV2HostedAdmin_GetAllUrlRewriteText]

@PatternName nvarchar(100) = NULL,
@pageSize int,
@pageIndex int,
@sortDirection NVARCHAR(10),
@sortColumn NVARCHAR(100),
@count int out

AS

BEGIN

      SELECT DISTINCT
					    RowRank,
					 	UrlRewriteId,
						MatchPattern,
						RewriteFormat,
						UtcModifiedDate,
						ModifiedBy,
						PatternName
						FROM 
					  (
				  SELECT
				        UrlReWrite.UrlRewriteId,
						UrlReWrite.MatchPattern,
						UrlReWrite.RewriteFormat,
						UrlRewrite.UtcModifiedDate,
						UrlRewrite.ModifiedBy,
						UrlRewrite.PatternName,
						CASE  
							WHEN @sortColumn = 'PatternName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY UrlRewrite.PatternName Asc) 									  
							WHEN @sortColumn = 'PatternName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY UrlRewrite.PatternName Desc) 									  
							
						End 										          
							   AS RowRank

							   FROM
					             UrlReWrite
					
					 WHERE 
					  
					  UrlRewrite.PatternName LIKE '%' + ISNULL(@PatternName ,UrlRewrite.PatternName ) + '%'
				  
					) AS UrlRewriteTable  

					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
				       

   

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllVerticalXmlExport]...';


GO
-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllVerticalXmlExport
AS
BEGIN
  SELECT VerticalXmlExportId,
		ExportTypes,
		ExportDate,
		ExportedBy,
		ExportDescription,
		[Status]
    FROM VerticalXmlExport
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllVerticalXmlImport]...';


GO
-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetAllVerticalXmlImport
AS
BEGIN
  SELECT VerticalXmlImportId,
		ImportTypes,
		ImportDate,
		ImportedBy,
		ExportBackupId,
		ImportDescription,
		[Status]
    FROM VerticalXmlImport
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetCUDHistory]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetCUDHistory]
	@CUDHistoryId INT=NULL,
	@TableName NVARCHAR(100)=NULL,
	@CUDType NVARCHAR(100)=NULL,
	@UTCFromCUDDate DATETIME =NULL,
	@UTCToCUDDate DATETIME =NULL,
	@PageSize int,
	@PageIndex int,
    @SortDirection NVARCHAR(10),
	@SortColumn NVARCHAR(100),
	@UserID int=NULL
AS
BEGIN
	SELECT DISTINCT
				RowRank,
				CUDHistoryId,
				TableName,
				CUDType,
				UtcCUDDate,
				UserId
			FROM 
			(
			SELECT
       		cudhistory.CUDHistoryId,
			cudhistory.TableName,
			cudhistory.CUDType,
			cudhistory.UtcCUDDate,
            cudhistory.UserId,
			CASE	WHEN @SortColumn = 'CUDHistoryId' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDHistoryId Asc)
					WHEN @SortColumn = 'CUDHistoryId' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDHistoryId Desc)
					WHEN @SortColumn = 'TableName' AND @SortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY  cudhistory.TableName Asc)
					WHEN @SortColumn = 'TableName' AND @SortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY  cudhistory.TableName Desc)
					WHEN @SortColumn = 'CUDType' AND @SortDirection = 'Ascending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDType Asc)
					WHEN @SortColumn = 'CUDType' AND @SortDirection = 'Descending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.CUDType Desc)
					WHEN @SortColumn = 'UtcCUDDate' AND @SortDirection = 'Ascending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.UtcCUDDate Asc)
					WHEN @SortColumn = 'UtcCUDDate' AND @SortDirection = 'Descending'  THEN ROW_NUMBER() OVER(ORDER BY cudhistory.UtcCUDDate Desc)
 		End AS RowRank
	FROM cudhistory
    WHERE	(cudhistory.CUDHistoryId = @CUDHistoryId OR @CUDHistoryId IS NULL)
				AND(cudhistory.TableName = @TableName OR @TableName IS NULL)
				AND(cudhistory.CUDType = @CUDType OR @CUDType IS NULL)
				AND ((CONVERT(Varchar(50), cudhistory.UtcCUDDate,102) BETWEEN  CONVERT(VARCHAR(50), @UTCFromCUDDate,102) AND CONVERT(VARCHAR(50),@UTCToCUDDate, 102)) OR @UTCFromCUDDate IS NULL)
				AND(CUDHistory.UserId = @UserID OR @UserID IS NULL)
				)AS cudhistoryTable 
	WHERE RowRank BETWEEN @PageIndex*@PageSize-@PageSize+1  AND @PageSize*@PageIndex
	ORDER BY RowRank

	SELECT count(*) 
	FROM CUDHistory 
	WHERE  (cudhistory.CUDHistoryId = @CUDHistoryId OR @CUDHistoryId IS NULL)
	AND(cudhistory.TableName = @TableName OR @TableName IS NULL)
	AND(cudhistory.CUDType = @CUDType OR @CUDType IS NULL)	
	AND ((CONVERT(Varchar(50), cudhistory.UtcCUDDate,102) BETWEEN  CONVERT(VARCHAR(50), @UTCFromCUDDate,102) AND CONVERT(VARCHAR(50),@UTCToCUDDate, 102)) OR @UTCFromCUDDate IS NULL)
	AND(CUDHistory.UserId = @UserID OR @UserID IS NULL)
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetCUDHistoryDatabyId]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetCUDHistoryDatabyId]
@CUDHistoryId INT = NULL,
@PageSize int,
@PageIndex int,
@SortDirection NVARCHAR(10),
@SortColumn NVARCHAR(100)
AS
BEGIN

	SELECT DISTINCT
		RowRank,
		CUDHistoryId,
		ColumnName,
		SqlDbType,
		OldValue,
		NewValue,
		NewValueBinary,
		OldValueBinary
	FROM
	(
		SELECT
		cudhistorydata.CUDHistoryId,
		cudhistorydata.ColumnName,
		cudhistorydata.SqlDbType,
		ISNULL(cudhistorydata.OldValue, '') AS 'OldValue',
		ISNULL(cudhistorydata.NewValue, '') AS 'NewValue',
		Case When SqlDbType <> 165 THEN NULL ELSE CONVERT(varbinary(MAX), NewValue) END AS 'NewValueBinary',
		Case When SqlDbType <> 165 THEN NULL ELSE CONVERT(varbinary(MAX), OldValue) END AS 'OldValueBinary',
		CASE
		WHEN @sortColumn = 'ColumnName' AND @sortDirection = 'Ascending' THEN ROW_NUMBER() OVER(ORDER BY cudhistorydata.columnname Asc)
		WHEN @sortColumn = 'ColumnName' AND @sortDirection = 'Descending' THEN ROW_NUMBER() OVER(ORDER BY cudhistorydata.columnname Desc)
		END AS RowRank
		FROM
		cudhistorydata
		WHERE CUDHistoryData.CUDHistoryId = @CUDHistoryId)
	AS CUDHistoryDataTable
	WHERE RowRank BETWEEN @pageIndex * @pageSize - @pageSize + 1  AND @pageSize * @pageIndex
	ORDER BY RowRank

	SELECT count(*) FROM CUDHistoryData WHERE CUDHistoryData.CUDHistoryId = @CUDHistoryId

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetErrorActivityReport]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetErrorActivityReport]
@FromDate Datetime,
@ToDate Datetime
AS
BEGIN
    Select distinct 
	       BadRequest.SiteActivityId,
		   [Site].Name as SiteName,
           RequestIssue as BadRequestIssue,
           ParameterName as BadRequestParameterName,
           ParameterValue as BadRequestParameterValue,
		   URI.UriString,
		   RequestUtcDate,
		   ClientIPAddress,
		   UserAgentString
		   

		 From BadRequest

		INNER JOIN SiteActivity on BadRequest.SiteActivityId = SiteActivity.SiteActivityId
		INNER JOIN URI  on SiteActivity.RequestURI=URI.UriId
		INNER JOIN UserAgent on UserAgent.UserAgentId = SiteActivity.UserAgentId
		INNER JOIN [SITE]  on SiteActivity.siteId=[Site].siteId
		    
		WHERE cast(SiteActivity.RequestUtcDate as datetime) between @FromDate and @ToDate 
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetErrorLog]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetErrorLog]
@ErrorCode int = NULL,
@FromErrorDate DateTime = NULL,
@ToErrorDate DateTime = NULL,
@Title nvarchar(512) = NULL,
@ProcessName nvarchar(1024) = NULL,
@EventId int = NULL
AS
BEGIN
  
  IF @FromErrorDate IS NULL
  BEGIN
	SET @FromErrorDate = '1900-01-01'
  END

  IF @ToErrorDate IS NULL
  BEGIN
	SET @ToErrorDate = GETDATE()+1
  END


  SELECT * 
  FROM ErrorLog 
  WHERE ErrorCode = ISNULL(@ErrorCode, ErrorCode) 
  AND ErrorUtcDate BETWEEN @FromErrorDate AND @ToErrorDate
  AND Title = ISNULL(@Title,Title)
  AND ProcessName = ISNULL(@ProcessName, ProcessName)
  AND EventId = ISNULL(@EventId, EventID)
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetSearchComboDocType]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetSearchComboDocType
AS
BEGIN
SELECT 
	   DISTINCT DTE.DocumentTypeId,
	   DT.Name as DocumentTypeName
	FROM 
	   DocumentTypeExternalId DTE
	INNER JOIN 
	   RPV2USDB.dbo.DocumentType DT
	        ON DTE.DocumentTypeId = DT.DocumentTypeID
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetSiteActivityReport]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSiteActivityReport]  
 @FromDate datetime,  
 @ToDate datetime  
AS  
BEGIN  
   SELECT DISTINCT  
      SA.SiteActivityId,       
      SA.SiteId,    
      site.Name as SiteName,     
      SA.RequestBatchId,  
      UR.UriString,       
      SA.TaxonomyAssociationId,  
      TA.NameOverride,  
      SA.RequestUtcDate,  
      SA.ClientIPAddress,  
      SA.TaxonomyAssociationGroupId,  
      SA.DocumentTypeId,  
      DTA.HeaderText,  
      DTA.MarketId,  
      O.Click,  
      SA.InitDoc  
   FROM  
     
   (SELECT  
      SA.RequestBatchId,SA.DocumentTypeId,        
      Count(SA.RequestBatchId) as Click  
      FROM  
      SiteActivity SA  
      group by  
      SA.RequestBatchId,SA.DocumentTypeId  
    ) O  
      INNER JOIN SiteActivity SA on SA.RequestBatchId= O.RequestBatchId  
      INNER JOIN TaxonomyAssociation TA on SA.TaxonomyAssociationId=TA.TaxonomyAssociationId  
      INNER JOIN DocumentTypeAssociation DTA on SA.SiteId=DTA.SiteId AND SA.DocumentTypeId=DTA.DocumentTypeId   
      INNER JOIN URI UR on SA.RequestURI=UR.UriId  
      INNER JOIN SITE site on SA.siteId=site.siteId  
      AND SA.SiteActivityId NOT IN(SELECT SiteActivityId FROM BADREQUEST)  
   WHERE   
    cast(RequestUtcDate as datetime) between @FromDate and @ToDate       
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetSiteById]...';


GO
-- =============================================
-- Author:		Arshdeep Kaur
-- Create date: 9-sept-2015
-- Description:	Getting site data by siteid
-- =============================================

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSiteById] 

				 @siteId int 
-- exec [dbo].[RPV2HostedAdmin_GetSiteById]	1	     
--DROP PROCEDURE [RPV2ClientDb1_GetSiteById]

AS BEGIN 

			SELECT 

				Site.Name,
				Site.TemplateId,
				Site.DefaultPageId,
				Site.ParentSiteId,
				Site.[Description],
				Site.UtcModifiedDate,
				Site.ModifiedBy

				FROM Site
				WHERE  Site.SiteId = @siteId
	
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetSites]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSites]
	@siteId INT=NULL,
	@name NVARCHAR(100)=NULL,
	@templateId INT=NULL,
	@defaultPageId INT=NULL,
	@parentSiteId INT=NULL,
	@description NVARCHAR(400)=NULL,
	@utcModifiedDate DATETIME,
	@modifiedBy INT,
	@pageIndex INT,
	@pageSize INT,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

  -- exec [RPV2HostedAdmin__GetSites] null,null,null,null,null,null,'',0,1,10,'asc','Name',0
  -- DROP PROCEDURE [RPV2HostedAdmin__GetSites]
AS
BEGIN

  
				SELECT DISTINCT
					RowRank,
					 SiteId,
					 Name,
					 TemplateId,
					 DefaultPageId,
					 ParentSiteId,
					 Description,
					 UtcModifiedDate,
					 ModifiedBy					 
				FROM
				  (
				  SELECT
					 Site.SiteId,
					Site.Name,
					Site.TemplateId,
					Site.defaultPageId,
					Site.ParentSiteId,
					Site.[Description],
					Site.UtcModifiedDate,
					Site.ModifiedBy,
					  		 CASE  										   
								   WHEN @sortColumn = 'SiteId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Site.SiteId Asc) 									  
								   WHEN @sortColumn = 'SiteId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Site.SiteId Desc) 									  
								   WHEN @sortColumn = 'Name' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Site.Name Asc) 
								   WHEN @sortColumn = 'Name' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Site.Name Desc) 
								   WHEN @sortColumn = 'TemplateId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Site.TemplateId Asc)
								   WHEN @sortColumn = 'TemplateId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Site.TemplateId Desc)
								   WHEN @sortColumn = 'defaultPageId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Site.defaultPageId Asc)
								   WHEN @sortColumn = 'defaultPageId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Site.defaultPageId Desc)
							 End 										          
							   AS RowRank
				   FROM
					 Site     					
				   WHERE 
					 (Site.SiteId = @siteId OR @siteId IS NULL)
				   AND(Site.Name = @name OR @name IS NULL)	 
				   AND(Site.TemplateId = @templateId OR @templateId IS NULL)	 
				   AND(Site.DefaultPageId = @defaultPageId OR @defaultPageId IS NULL)	 
					) AS SiteTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
					
					

		

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetStaticResourceById]...';


GO
create procedure dbo.RPV2HostedAdmin_GetStaticResourceById

@StaticResourceId int 
as
BEGIN
   Select StaticResourceId,
           [FileName],
			Size,
			MimeType,
			Data,
			UtcModifiedDate,
			ModifiedBy
     FROM
	      
		  StaticResource
     WHERE StaticResourceId=@StaticResourceId

           
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetUrlRewriteText]...';


GO

CREATE procedure [dbo].[RPV2HostedAdmin_GetUrlRewriteText]

@UrlRewriteId int
as
BEGIN
   Select 
		UrlRewriteId 
		, MatchPattern 
		, RewriteFormat 
		, UtcModifiedDate 
		, ModifiedBy
		,PatternName 
   from 
		UrlRewrite
   where 
		UrlRewriteId = @UrlRewriteId

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetVerticalXmlExportByID]...';


GO
-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_GetVerticalXmlExportByID
@VerticalXmlExportId int
AS
BEGIN
  SELECT VerticalXmlExportId,
		ExportTypes,
		ExportDate,
		ExportXml,
		ExportedBy,
		ExportDescription,
		[Status]
    FROM VerticalXmlExport
  WHERE VerticalXmlExportId = @VerticalXmlExportId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_GetVerticalXmlImportByID]...';


GO
-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE dbo.[RPV2HostedAdmin_GetVerticalXmlImportByID]
@VerticalXmlImportId int
AS
BEGIN
  SELECT VerticalXmlImportId,
		ImportTypes,
		ImportDate,
		ImportXml,
		ImportedBy,
		ImportDescription,
		[Status],
		ExportBackupId
    FROM VerticalXmlImport
  WHERE VerticalXmlImportId = @VerticalXmlImportId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_PageFeature_CacheDependencyCheck]...';


GO

CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_PageFeature_CacheDependencyCheck]
AS
BEGIN
  SELECT PageId, SiteId , [Key] ,  COUNT_BIG(*) AS Total
  FROM dbo.pageFeature
  GROUP BY  PageId , SiteId, [Key]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_PageNavigationData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_PageNavigationData_CacheDependencyCheck]
AS
BEGIN
	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigation
	GROUP BY PageNavigationId;
	
   	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigationVersion
	GROUP BY PageNavigationId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_PageTextData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_PageTextData_CacheDependencyCheck]
AS
BEGIN
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageText
	GROUP BY PageTextId;
	
   	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageTextVersion
	GROUP BY PageTextId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_ReportContent_CacheDependencyCheck]...';


GO

-- =============================================
-- Author:		Nimmy Rose Antony
-- Create date: 14th-Oct-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_ReportContent_CacheDependencyCheck]
AS
BEGIN
   	SELECT	ReportContentId, COUNT_BIG(*) AS Total
	FROM	dbo.ReportContent
	GROUP BY ReportContentId;

	SELECT   ReportContentId, COUNT_BIG(*) AS Total
	FROM     dbo.ReportContentData
	GROUP BY ReportContentId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveClientDocument]...';


GO


/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveClientDocument]
	Added By: 
	Date: 
	Reason : To add and update the ClientDocument
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocument]	
	@ClientDocumentId int,
	@Name NVARCHAR(100),
	@Description NVARCHAR(400),
	@IsPrivate BIT,
	@ContentUri NVARCHAR(2083),
	@ClientDocumentTypeId INT,
	@MimeType NVARCHAR(127),
	@FileName NVARCHAR(260),
	@FileData VARBINARY(MAX),
	@ModifiedBy INT
AS
BEGIN
	
	IF(@ClientDocumentId=0) 
		BEGIN
			INSERT INTO [dbo].[ClientDocument]
							(
								  [ClientDocumentTypeId]
								  ,[FileName]
								  ,[MimeType]
								  ,[IsPrivate]
								  ,[ContentUri]
								  ,[Name]
								  ,[Description]
								  ,[UtcModifiedDate]
								  ,[ModifiedBy]
							) 
					VALUES (
									@ClientDocumentTypeId
									,@FileName
									,@MimeType
									,@IsPrivate
									,@ContentUri
									,@Name
									,@Description
									,GETUTCDATE()
									,@ModifiedBy
							)


					 INSERT INTO [dbo].[ClientDocumentData] 
							  (
									[ClientDocumentId]
									,[Data]
									,[UtcModifiedDate]
									,[ModifiedBy]
							   )
							   VALUES
							  (
									SCOPE_IDENTITY()
									,@FileData
									,GETUTCDATE()
									,@ModifiedBy
							  )
		END
	ELSE
		BEGIN
			UPDATE 
					[dbo].[ClientDocument]
			
			SET 
					[ClientDocumentTypeId]	 = @ClientDocumentTypeId
					,[FileName]				 = @FileName
					,[MimeType]				 = @MimeType
					,[IsPrivate]			 = @IsPrivate
					,[ContentUri]			 = @ContentUri
					,[Name]					 = @Name
					,[Description]			 = @Description
					,[UtcModifiedDate]		 = GETUTCDATE()
					,[ModifiedBy]			 = @ModifiedBy
			WHERE 
					ClientDocumentId = @ClientDocumentId

		
			UPDATE 
					[dbo].[ClientDocumentData] 
			
			SET 
					[Data]					 = @FileData
					,[UtcModifiedDate]		 = GETUTCDATE()
					,[ModifiedBy]   		 = @ModifiedBy
			WHERE 
					ClientDocumentId = @ClientDocumentId
			
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveClientDocumentGroup]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocumentGroup]
	@ClientDocumentGroupId INT, 
	@Name NVARCHAR(100), 
	@Description NVARCHAR(100),
	@ParentClientDocumentGroupId INT,
	@CssClass VARCHAR(50),
	@ModifiedBy INT,
	@ClientDocumentGroupClientDocument TT_ClientDocumentGroupClientDocument READONLY
AS
BEGIN
	IF @ClientDocumentGroupId = 0
		BEGIN
			DECLARE @identityClientDocumentGroupId INT
			INSERT INTO ClientDocumentGroup
					(
						Name,
						[Description],
						ParentClientDocumentGroupId,
						CssClass,
						UtcModifiedDate,
						ModifiedBy
					)
				VALUES
					(
						@Name,
						@Description,
						@ParentClientDocumentGroupId,
						@CssClass,
						GETUTCDATE(),
						@ModifiedBy
					)
			SELECT @identityClientDocumentGroupId = SCOPE_IDENTITY()

			INSERT INTO ClientDocumentGroupClientDocument 
					(
						ClientDocumentGroupId,
						ClientDocumentId,
						[Order],
						UtcModifiedDate,
						ModifiedBy
					)
					SELECT 
						@identityClientDocumentGroupId,
						ClientDocumentId,
						[Order],
						GETUTCDATE(),
						@ModifiedBy
					FROM @ClientDocumentGroupClientDocument
		END
	ELSE
		BEGIN
			UPDATE ClientDocumentGroup SET
				Name = @Name,
				[Description] = @Description,
				ParentClientDocumentGroupId = @ParentClientDocumentGroupId,
				CssClass = @CssClass,
				UtcModifiedDate = GETUTCDate(),
				ModifiedBy = @ModifiedBy
			WHERE ClientDocumentGroupId = @ClientDocumentGroupId

			DECLARE @deleteClientDocumentGroupClientDocument TT_ClientDocumentGroupClientDocument

			DELETE FROM ClientDocumentGroupClientDocument 
				OUTPUT deleted.ClientDocumentGroupId, deleted.ClientDocumentId , deleted.[Order]
				INTO @deleteClientDocumentGroupClientDocument
			WHERE ClientDocumentGroupId = @ClientDocumentGroupId AND
				ClientDocumentId NOT IN 
				(SELECT ClientDocumentId FROM @ClientDocumentGroupClientDocument)

			UPDATE	CUDHistory				 
  				 SET	UserId = @ModifiedBy
				WHERE	TableName = N'ClientDocumentGroupClientDocument'
					AND	[Key] = @ClientDocumentGroupId
					AND [CUDType] = 'D' 
					AND [SecondKey]  in (SELECT ClientDocumentId from @deleteClientDocumentGroupClientDocument);

			INSERT INTO ClientDocumentGroupClientDocument
				(
					ClientDocumentGroupId,
					ClientDocumentId,
					[Order],
					UtcModifiedDate,
					ModifiedBy
				)
			SELECT 
				ClientDocumentGroupId,
				ClientDocumentId,
				[Order],
				GETUTCDATE(),
				@ModifiedBy
			FROM @ClientDocumentGroupClientDocument 
			WHERE ClientDocumentId NOT IN 
				(
					SELECT ClientDocumentId FROM ClientDocumentGroupClientDocument 
					WHERE ClientDocumentGroupId = @ClientDocumentGroupId
				)
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveClientDocumentType]...';


GO



CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocumentType]
	@clientdocumenttypeid int,
	@name NVARCHAR(100),
	@description NVARCHAR(400),
	@hosteddocumentsdisplaycount int,
	@ftpName NVARCHAR(400),
	@ftpusername NVARCHAR(400),
	@ftppassword NVARCHAR(400),
	@issftp bit,
	@ModifiedBy int
AS
BEGIN

	IF(@clientdocumenttypeid = 0)
	BEGIN
		INSERT INTO ClientDocumentType(name,
						[description],
						utcModifiedDate,
						HostedDocumentsDisplayCount,
						FTPName,
						FTPUsername,
						FTPPassword,
						IsSFTP,
						ModifiedBy)
				VALUES (@name,
						@description,
						GETUTCDATE(),
						@hosteddocumentsdisplaycount,
						@ftpName,
						@ftpusername,
						@ftppassword,
						@issftp,
						@modifiedBy)
	END
	ELSE
	BEGIN
		UPDATE ClientDocumentType SET name=@name,
					[description]=@description,
					utcModifiedDate=GETUTCDATE(),
					HostedDocumentsDisplayCount=@hosteddocumentsdisplaycount,
					ModifiedBy=@modifiedBy
		WHERE ClientDocumentTypeId = @clientdocumenttypeid
	END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveDocumentTypeAssociation]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveDocumentTypeAssociation]
	Added By: Noel Dsouza
	Date: 10/12/2015	
	Reason : To add DocumentTypeAssociation
*/

CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveDocumentTypeAssociation
@DocumentTypeAssociationId int,
@DocumentTypeId int,
@SiteId int,
@TaxonomyAssociationId int,
@Order int,
@HeaderText nvarchar(100),
@LinkText nvarchar(100),
@DescriptionOverride nvarchar(400),
@CssClass varchar(50),
@MarketId nvarchar(50),
@ModifiedBy int
AS
BEGIN
  IF @DocumentTypeAssociationId = 0
	  BEGIN
	    INSERT INTO DocumentTypeAssociation
				(
					DocumentTypeId,
					SiteId,
					TaxonomyAssociationId,
					[Order],
					HeaderText,
					LinkText,
					DescriptionOverride,
					CssClass,
					MarketId,
					UtcModifiedDate,
					ModifiedBy				
				)
		VALUES
			 (
				@DocumentTypeId,
				@SiteId,
				@TaxonomyAssociationId,
				@Order,
				@HeaderText,
				@LinkText,
				@DescriptionOverride,
				@CssClass,
				@MarketId,
				GETUTCDATE(),
				@ModifiedBy
			 )
	  END
  ELSE
      BEGIN
		UPDATE DocumentTypeAssociation
			SET DocumentTypeId = @DocumentTypeId,
				SiteId = @SiteId,
				TaxonomyAssociationId = @TaxonomyAssociationId,
				[Order] = @Order,
				HeaderText = @HeaderText,
				LinkText = @LinkText,
				DescriptionOverride = @DescriptionOverride,
				CssClass = @CssClass,
				MarketId = @MarketId,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @ModifiedBy
		WHERE DocumentTypeAssociationId = @DocumentTypeAssociationId
		
      END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveDocumentTypeExternalId]...';


GO
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveDocumentTypeExternalId  
@DocumentTypeId int,  
@ExternalId nvarchar(100),  
@IsPrimary bit,  
@ModifiedBy int  
  
AS  
BEGIN  
  
	IF @IsPrimary = 1
	BEGIN
		UPDATE DocumentTypeExternalId   
		SET IsPrimary = 0,  
			UtcModifiedDate = GETUTCDATE(),  
			ModifiedBy = @ModifiedBy  
		WHERE DocumentTypeId = @DocumentTypeId AND IsPrimary = 1 AND ExternalId != @ExternalId	
	END
  
	IF EXISTS (SELECT Top 1 DocumentTypeId FROM DocumentTypeExternalId WHERE DocumentTypeId = @DocumentTypeId AND ExternalId = @ExternalId)  
	BEGIN  
		UPDATE DocumentTypeExternalId   
		SET IsPrimary = @IsPrimary,  
			UtcModifiedDate = GETUTCDATE(),  
			ModifiedBy = @ModifiedBy  
		WHERE DocumentTypeId = @DocumentTypeId  AND ExternalId = @ExternalId  
	END	
	ELSE  
	BEGIN  
		INSERT INTO DocumentTypeExternalId(  
			DocumentTypeId,  
			ExternalId,  
			IsPrimary,  
			UtcModifiedDate,        
			ModifiedBy)  
		VALUES
			(@DocumentTypeId,        
			@ExternalId,  
			@IsPrimary,  
			GETUTCDATE(),     
			@ModifiedBy)  
	END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveFootnote]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 13th-Oct-2015
-- RPV2HostedAdmin_SaveFootnote
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_SaveFootnote
@FootnoteId int,
@TaxonomyAssociationId int,
@TaxonomyAssociationGroupId int,
@LanguageCulture varchar(50),
@Text nvarchar(max),
@Order int,
@ModifiedBy int
AS
BEGIN
	IF @FootnoteId = 0
		BEGIN
		  INSERT INTO Footnote(
							 FootnoteId
							  ,TaxonomyAssociationId
							  ,TaxonomyAssociationGroupId
							  ,LanguageCulture
							  ,[Text]
							  ,[Order]
							  ,UtcModifiedDate
							  ,ModifiedBy
						)
			VALUES(
					@FootnoteId,
					@TaxonomyAssociationId,
					@TaxonomyAssociationGroupId,
					@LanguageCulture,
					@Text,
					@Order,
					GETUTCDATE(),
					@ModifiedBy
				  )
		END
	ELSE
		BEGIN
			UPDATE Footnote
				 SET TaxonomyAssociationId = @TaxonomyAssociationId,
					 TaxonomyAssociationGroupId = @TaxonomyAssociationGroupId,
					 LanguageCulture = @LanguageCulture,
					 [Text] = @Text,
					 [Order] = @Order,
					 UtcModifiedDate = GETUTCDATE(),
					 ModifiedBy = @ModifiedBy
				WHERE FootnoteId = @FootnoteId			
		END  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SavePageFeature]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageFeature]
	@SiteId int,
	@PageId int,
	@PageKey  varchar(200),
	@FeatureMode int,
	@ModifiedBy int
AS	
BEGIN 
     IF EXISTS (SELECT 1 FROM PageFeature WHERE PageId = @PageId AND [Key] = @PageKey AND SiteId = @SiteId)
		 BEGIN
			UPDATE PageFeature
			SET FeatureMode = @FeatureMode,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE PageId = @PageId AND [Key] = @PageKey AND SiteId = @SiteId

		 END

	 ELSE
		BEGIN
		    INSERT INTO PageFeature(
			SiteId,
			PageId,
			[Key],
			FeatureMode,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@SiteId,
			@PageId,
			@PageKey,
			@FeatureMode,
			GETUTCDATE(),
			@ModifiedBy
			)
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SavePageNavigation]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageNavigation]
@PageNavigationId int,
@SiteId int,
@PageID int,
@NavigationKey varchar(200),
@IsProofing bit,
@ModifiedBy int,
@NavigationXml xml,
@Version int
as
BEGIN 
  
  DECLARE @CurrentPageNavigationId INT
  
  IF @PageNavigationId = 0 AND @Version = 0
  Begin
    INSERT INTO PageNavigation( 
	SiteId,
	PageID ,
	NavigationKey ,
	CurrentVersion,
	UtcModifiedDate,
	ModifiedBy)
		  VALUES(@SiteId,
		  @PageID,
		  @NavigationKey,
		  1,
		  GETUTCDATE(),
		  @ModifiedBy)
    
    SELECT @CurrentPageNavigationId = SCOPE_IDENTITY()
    
    INSERT INTO PageNavigationVersion(
						PageNavigationId,
						[Version],	
						NavigationXml,	
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageNavigationId,
						1,
						@NavigationXml,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE PageNavigationVersion
			  SET [NavigationXml] = @NavigationXml			 
			  WHERE PageNavigationId = @PageNavigationId AND [Version] = @Version 
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO PageNavigationVersion
	      (
			PageNavigationId,
			[Version],
			[NavigationXml],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @PageNavigationId,
	      @Version+1,
	      @NavigationXml,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SavePageText]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageText]
@PageTextId int,
@Version int,
@PageID int,
@SiteId int,
@ResourceKey varchar(200),
@Text nvarchar(max),
@IsProofing bit,
@ModifiedBy int
as
BEGIN

  
  
  DECLARE @CurrentPageTextId INT
  
  IF @PageTextId = 0 AND @Version = 0
  Begin
    INSERT INTO PageText(
						SiteId,
						PageId,
						ResourceKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				VALUES(@SiteId,
						@PageID,
						@ResourceKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy)
    
    SELECT @CurrentPageTextId = SCOPE_IDENTITY()
    
    INSERT INTO PageTextVersion(
						PageTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageTextId,
						1,
						@Text,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE PageTextVersion
			  SET [Text] = @Text			 
			  WHERE PageTextId = @PageTextId AND [Version] = @Version        
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO PageTextVersion
	      (
			PageTextId,
			[Version],
			[Text],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @PageTextId,
	      @Version+1,
	      @Text,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveProofingApproval]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveProofingApproval]
@modifiedBy int
AS
BEGIN

	--Approve Site Text changes

	DECLARE @SiteTextProofingVersion TABLE
	( 
		 SiteTextId int,
		 ProofingVersion int
	)
	INSERT INTO @SiteTextProofingVersion
	SELECT SiteTextId, MAX([Version]) AS ProofingVersion
	FROM SiteTextVersion 
	GROUP BY SiteTextId

	UPDATE SiteText 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM SiteText ST
	INNER JOIN	@SiteTextProofingVersion versions ON ST.SiteTextId = versions.SiteTextId

	--Approve Page Text changes

	DECLARE @PageTextProofingVersion TABLE
	( 
		 PageTextId int,
		 ProofingVersion int
	)
	INSERT INTO @PageTextProofingVersion
	SELECT PageTextId, MAX([Version]) AS ProofingVersion
	FROM PageTextVersion  
	GROUP BY PageTextId

	UPDATE PageText 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM PageText PT
	INNER JOIN	@PageTextProofingVersion versions ON PT.PageTextId = versions.PageTextId


	--Approve Site Navigation changes

	DECLARE @SiteNavigationProofingVersion TABLE
	( 
		 SiteNavigationId int,
		 ProofingVersion int
	)
	INSERT INTO @SiteNavigationProofingVersion
	SELECT SiteNavigationId, MAX([Version]) AS ProofingVersion
	FROM SiteNavigationVersion  
	GROUP BY SiteNavigationId

	UPDATE SiteNavigation 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM SiteNavigation SN
	INNER JOIN	@SiteNavigationProofingVersion versions ON SN.SiteNavigationId = versions.SiteNavigationId


	--Approve Page Navigation changes

	DECLARE @PageNavigationProofingVersion TABLE
	( 
		 PageNavigationId int,
		 ProofingVersion int
	)
	INSERT INTO @PageNavigationProofingVersion
	SELECT PageNavigationId, MAX([Version]) AS ProofingVersion
	FROM PageNavigationVersion  
	GROUP BY PageNavigationId

	UPDATE PageNavigation 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM PageNavigation PN
	INNER JOIN	@PageNavigationProofingVersion versions ON PN.PageNavigationId = versions.PageNavigationId


END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveReportContent]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveReportContent]
	Added By: Nimmy Rose Antony
    Date: 15 Oct 2015
	Reason : To add and update the Report Content
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveReportContent]	
	@ReportScheduleId int,
	@MimeType NVARCHAR(127),
	@IsPrivate BIT,
	@ContentUri NVARCHAR(2083),
	@Name NVARCHAR(100),
	@ReportRunDate DATETIME ,
	@ModifiedBy INT,
	@FileName NVARCHAR(260),
	@Description NVARCHAR(400),
    @Data VARBINARY(MAX)
AS
BEGIN
	DECLARE @identityReportContentId int
	
		
			INSERT INTO [dbo].[ReportContent]
							(
							       [ReportScheduleId]
							      ,[MimeType]
								  ,[IsPrivate]
								  ,[ContentUri]
								  ,[Name]
								  ,[ReportRunDate]
								  ,[FileName]
								  ,[Description]
								  ,[ModifiedBy]								  								 
								  ,[UtcModifiedDate]
							) 
					VALUES (
									 @ReportScheduleId
									,@MimeType
									,@IsPrivate
									,@ContentUri
									,@Name
									,@ReportRunDate
									,@FileName
									,@Description
									,@ModifiedBy
									,GETUTCDATE()
							)

     SET @identityReportContentId = SCOPE_IDENTITY()
					 INSERT INTO [dbo].[ReportContentData]
							  (
									[ReportContentId]
									,[Data]
									,[UtcModifiedDate]
									,[ModifiedBy]
							   )
							   VALUES
							  (
									@identityReportContentId
									,@Data
									,GETUTCDATE()
									,@ModifiedBy
							  )
	
	
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveSite]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveSite]
	Added By: Noel Dsouza
	Date: 09/15/2015
	Reason : To add and update the Site
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSite]	
	@siteId int,
	@ClientId int,
	@name NVARCHAR(100),
	@templateId int,
	@defaultPageId int,	
	@IsDefaultSite bit,
	@parentSiteId int,
	@description NVARCHAR(400),
	@ModifiedBy int,
	@TemplateTextData TT_TemplateText readonly,
	@TemplatePageTextData TT_TemplatePageText readonly,
	@TemplateNavigationData TT_TemplateNavigation readonly,
	@TemplatePageNavigationData TT_TemplatePageNavigation readonly
AS
BEGIN
	
	IF @siteId = 0 
		BEGIN
			INSERT INTO Site(name,
							templateId,
							defaultPageId,
							parentSiteId,
							[description],
							utcModifiedDate,
							ModifiedBy) 
					VALUES (@name,
							@templateId,
							@defaultPageId,
							@parentSiteId,
							@description,
							GETUTCDATE(),
							@modifiedBy)

			SET @siteId = SCOPE_IDENTITY()

			IF @IsDefaultSite = 1
			BEGIN
				IF EXISTS(SELECT DefaultSiteId from ClientSettings)  
				BEGIN  
					UPDATE ClientSettings  
					SET DefaultSiteId = @siteId ,   
						[UtcModifiedDate] = GETUTCDATE(),  
						[ModifiedBy] = @modifiedBy					
				END  
				ELSE  
				BEGIN  
					INSERT INTO [ClientSettings]  
					([ClientId]  
					,[DefaultSiteId]  
					,[UtcModifiedDate]  
					,[ModifiedBy])  
					VALUES  
					(@ClientId  
					,@siteId  
					,GETUTCDATE(),  
					@modifiedBy)
				END			
			END

			
			--INSERT Default Site Setting - START

			--INSERT Default Site Text for newly added sites

			INSERT INTO SiteText(
						SiteId,
						ResourceKey,
						CurrentVersion,
						UtcModifiedDate,						
						ModifiedBy)
					SELECT @SiteId,						
						ResourceKey,
						1,		
						GETUTCDATE(),				
						@ModifiedBy
					FROM @TemplateTextData


			INSERT INTO SiteTextVersion(
						SiteTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
					SELECT SiteText.SiteTextId,
						1,
						tt.DefaultText,
						GETUTCDATE(),						
						@ModifiedBy
					FROM @TemplateTextData TT
					INNER JOIN SiteText on SiteText.SiteId = @siteId AND SiteText.ResourceKey = TT.ResourceKey


			--INSERT Default Page Text for newly added sites


			INSERT INTO PageText(
						SiteId,
						PageId,
						ResourceKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				SELECT  @SiteId,
						PageID,
						ResourceKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy
				FROM @TemplatePageTextData
    
			INSERT INTO PageTextVersion(
								PageTextId,
								[Version],
								[Text],		
								UtcCreateDate,				
								CreatedBy)
					SELECT	PageText.PageTextId,
								1,
								tpt.DefaultText,		
								GETUTCDATE(),				
								@ModifiedBy
					FROM @TemplatePageTextData TPT
					INNER JOIN PageText ON PageText.SiteId = @SiteId AND PageText.PageId = TPT.PageId  AND PageText.ResourceKey = TPT.ResourceKey


			--INSERT Default Site Navigation for newly added sites

			INSERT INTO SiteNavigation(
						SiteId,
						PageId,
						NavigationKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				SELECT  @SiteId,
						NULL,
						NavigationKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy
				FROM @TemplateNavigationData
    
    
			INSERT INTO SiteNavigationVersion(
								SiteNavigationId,
								[Version],
								[NavigationXML],		
								UtcCreateDate,				
								CreatedBy)
					SELECT 	SiteNavigation.SiteNavigationId,
								1,
								DefaultNavigationXml,		
								GETUTCDATE(),				
								@ModifiedBy
					FROM @TemplateNavigationData TN
					INNER JOIN SiteNavigation ON SiteNavigation.SiteId = @SiteId AND SiteNavigation.NavigationKey = TN.NavigationKey


			--INSERT Default Page Navigation for newly added sites

			INSERT INTO PageNavigation( 
								SiteId,
								PageID ,
								NavigationKey ,
								CurrentVersion,
								UtcModifiedDate,
								ModifiedBy)
					SELECT  @SiteId,
							PageID,
							NavigationKey,
							1,
							GETUTCDATE(),
							@ModifiedBy
					FROM @TemplatePageNavigationData
    
			INSERT INTO PageNavigationVersion(
								PageNavigationId,
								[Version],	
								NavigationXml,	
								UtcCreateDate,				
								CreatedBy)
						SELECT PageNavigation.PageNavigationId,
								1,
								DefaultNavigationXml,		
								GETUTCDATE(),				
								@ModifiedBy
						FROM @TemplatePageNavigationData TPN
						INNER JOIN PageNavigation ON PageNavigation.SiteId = @siteId AND PageNavigation.PageId = TPN.PageId AND PageNavigation.NavigationKey = TPN.NavigationKey

			--INSERT Default Site Setting END


		END
	ELSE
		BEGIN
			UPDATE Site 
			SET name=@name,
				templateId=@templateId,
				defaultPageId=@defaultPageId,
				parentSiteId=@parentSiteId,
				[description]=@description,
				utcModifiedDate=GETUTCDATE(),
				ModifiedBy=@modifiedBy
			WHERE SiteId = @siteId

			IF @IsDefaultSite = 1
			BEGIN
				UPDATE ClientSettings
				SET DefaultSiteId = @siteId,
				utcModifiedDate = GETUTCDATE(),
				ModifiedBy = @modifiedBy		
			END
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveSiteFeature]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSiteFeature]
	@SiteId int,
	@SiteKey  varchar(200),
	@FeatureMode int,
	@ModifiedBy int
AS	
BEGIN 
     IF EXISTS (SELECT 1 FROM SiteFeature WHERE SiteId = @SiteId AND [Key] =@SiteKey)
		 BEGIN
			UPDATE SiteFeature
			SET 
				FeatureMode = @FeatureMode,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE SiteId = @SiteId	 AND [Key] = @SiteKey
		 END

	 ELSE
		BEGIN
		    INSERT INTO SiteFeature(
			SiteId,
			[Key],
			FeatureMode,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@SiteId,
			@SiteKey,
			@FeatureMode,
			GETUTCDATE(),
			@ModifiedBy
			)
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveSiteNavigation]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSiteNavigation]
@SiteNavigationId int,
@Version int,
@PageId int,
@SiteId int,
@NavigationKey varchar(200),
@NavigationXML XML,
@IsProofing bit,
@ModifiedBy int

as
BEGIN

  DECLARE @CurrentPageTextId INT
  
  IF @SiteNavigationId = 0 AND @Version = 0
  Begin
    INSERT INTO SiteNavigation(
						SiteId,
						PageId,
						NavigationKey,
						CurrentVersion,	
						UtcModifiedDate,					
						ModifiedBy) 
				VALUES(@SiteId,
						@PageId,
						@NavigationKey,
						1,				
						GETUTCDATE(),		
						@ModifiedBy)
    
    SELECT @CurrentPageTextId = SCOPE_IDENTITY()
    
    INSERT INTO SiteNavigationVersion(
						SiteNavigationId,
						[Version],
						[NavigationXML],		
						UtcCreateDate,				
						CreatedBy)
			VALUES( 
						@CurrentPageTextId,
						1,
						@NavigationXML,		
						GETUTCDATE(),				
						@ModifiedBy
				  )
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE SiteNavigationVersion
			  SET [NavigationXML] = @NavigationXML			 
			  WHERE SiteNavigationId = @SiteNavigationId  AND [Version] = @Version     
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO SiteNavigationVersion
	      (
			SiteNavigationId,
			[Version],
			[NavigationXML],
			UtcCreateDate,
			CreatedBy
			)
	      VALUES
	      (
	      @SiteNavigationId,
	      @Version+1,
	      @NavigationXML,
	      GETUTCDATE(),
	      @ModifiedBy
	      )	        
	    END      
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveSiteText]...';


GO

CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveSiteText
@SiteTextId int,
@Version int,
@SiteId int,
@ResourceKey varchar(200),
@Text nvarchar(max),
@IsProofing bit,
@ModifiedBy int
as
BEGIN

  DECLARE @CurrentSiteTextId int
  
  IF @SiteTextId = 0 AND @Version = 0
  Begin
    INSERT INTO SiteText(
						SiteId,
						ResourceKey,
						CurrentVersion,
						UtcModifiedDate,						
						ModifiedBy)
				VALUES(@SiteId,						
						@ResourceKey,
						1,		
						GETUTCDATE(),				
						@ModifiedBy)
    
    SET @CurrentSiteTextId = SCOPE_IDENTITY()
    
    INSERT INTO SiteTextVersion(
						SiteTextId,
						[Version],
						[Text],		
						UtcCreateDate,				
						CreatedBy)
				VALUES(
						@CurrentSiteTextId,
						1,
						@Text,
						GETUTCDATE(),						
						@ModifiedBy)
  END
  ELSE
  BEGIN
      IF @IsProofing = 1
		  BEGIN
			 UPDATE SiteTextVersion
			  SET [Text] = @Text			 
			  WHERE SiteTextId = @SiteTextId  AND [Version] = @Version        
		  END 
	  ELSE
	    BEGIN
	      INSERT INTO SiteTextVersion(SiteTextId,[Version],[Text],UtcCreateDate, CreatedBy)
	      VALUES(@SiteTextId,@Version+1,@Text,GETUTCDATE(),@ModifiedBy)	        
	    END      
  END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveStaticResource]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveStaticResource]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the StaticResource
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveStaticResource]	
	@StaticResourceId int,
	@FileName nvarchar(260),
	@Size int,
	@MimeType varchar(127),
	@Data varbinary(max),
	@modifiedBy int
AS
BEGIN

   IF @StaticResourceId = 0 
	   BEGIN
				INSERT INTO StaticResource
					(					
					[FileName],
					Size,
					MimeType,
					Data,
					utcModifiedDate,
					ModifiedBy) 
				VALUES (
					@FileName,
					@Size,
					@MimeType,
					@Data,
					GETUTCDATE(),
					@modifiedBy)
		END
	ELSE
	    BEGIN
			UPDATE StaticResource
			SET [FileName] = @FileName,
				Size = @Size,
				MimeType = @MimeType,
				Data = @Data,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @modifiedBy
			WHERE StaticResourceId = @StaticResourceId
	    END	
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociation]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_SaveTaxonomyAssociation
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_SaveTaxonomyAssociation
@TaxonomyAssociationId int,
@Level int,
@TaxonomyId int,
@SiteId int,
@ParentTaxonomyAssociationId int,
@NameOverride nvarchar(200),
@DescriptionOverride nvarchar(40),
@CssClass varchar(50),
@MarketId nvarchar(50),
@ModifiedBy int
AS
BEGIN
 IF @TaxonomyAssociationId = 0
	  BEGIN
	    INSERT INTO TaxonomyAssociation
				(
					[Level],
					TaxonomyId,
					SiteId,
					ParentTaxonomyAssociationId,
					NameOverride,
					DescriptionOverride,					
					CssClass,
					MarketId,
					UtcModifiedDate,
					ModifiedBy				
				)
		VALUES
			 (
				@Level,
				@TaxonomyId,
				@SiteId,
				@ParentTaxonomyAssociationId,
				@NameOverride,
				@DescriptionOverride,
				@CssClass,
				@MarketId,
				GETUTCDATE(),
				@ModifiedBy
			 )
	  END
  ELSE
      BEGIN
		UPDATE TaxonomyAssociation
			SET [Level] = @Level,
				TaxonomyId = @TaxonomyId,
				SiteId = @SiteId,
				ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId,
				NameOverride = @NameOverride,
				DescriptionOverride = @DescriptionOverride,
				CssClass = @CssClass,
				MarketId = @MarketId,
				UtcModifiedDate = GETUTCDATE(),
				ModifiedBy = @ModifiedBy
		WHERE TaxonomyAssociationId = @TaxonomyAssociationId
		
      END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 12-Oct-2015
-- RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy
@ParentTaxonomyAssociationId int,
@ChildTaxonomyAssociationId int,
@RelationshipType int,
@Order int,
@ModifiedBy int
AS
BEGIN
  IF NOT EXISTS(SELECT ParentTaxonomyAssociationId
				 FROM TaxonomyAssociationHierachy
				WHERE ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId
				AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
				AND RelationshipType = @RelationshipType
				)
	  BEGIN
	    INSERT INTO TaxonomyAssociationHierachy
					(
					  ParentTaxonomyAssociationId,
					  ChildTaxonomyAssociationId,
					  RelationshipType,
					  [Order]
					 )
		VALUES(
				@ParentTaxonomyAssociationId,
				@ChildTaxonomyAssociationId,
				@RelationshipType,
				@Order
			  )
	  END
  ELSE
     BEGIN
			UPDATE TaxonomyAssociationHierachy
			SET 
				[Order] = @Order
			WHERE 		
				ParentTaxonomyAssociationId = @ParentTaxonomyAssociationId 
				AND ChildTaxonomyAssociationId = @ChildTaxonomyAssociationId
				AND RelationshipType = @RelationshipType			
     END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveTaxonomyLevelExternalId]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveTaxonomyLevelExternalId]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the TaxonomyLevelExternalId
*/ 
  
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveTaxonomyLevelExternalId]  
 @Level int,
 @TaxonomyID int,
 @ExternalId NVARCHAR(100),  
 @modifiedBy int,  
 @IsPrimary bit  
AS  
BEGIN

	IF @IsPrimary = 1
	BEGIN
		UPDATE TaxonomyLevelExternalId  
		SET   
			IsPrimary = 0,  
			ModifiedBy = @ModifiedBy,  
			UtcModifiedDate = GETUTCDATE()  
		WHERE  [Level] = @Level AND TaxonomyId = @TaxonomyID AND IsPrimary = 1 AND ExternalId != @ExternalId
	
	END

	IF EXISTS (SELECT Top 1 TaxonomyId  FROM TaxonomyLevelExternalId WHERE [Level] = @Level AND TaxonomyId = @TaxonomyID AND ExternalId = @ExternalId)  
	BEGIN	
		UPDATE TaxonomyLevelExternalId  
		SET   
			IsPrimary = @IsPrimary,  
			ModifiedBy = @ModifiedBy,  
			UtcModifiedDate = GETUTCDATE()  
		WHERE  [Level] = @Level AND TaxonomyId =@TaxonomyID AND ExternalId=@ExternalId  
	END
	ELSE  
	BEGIN
	
		INSERT INTO TaxonomyLevelExternalId(
			[Level],
			TaxonomyId, 
			ExternalId,
			utcModifiedDate,  
			ModifiedBy,
			IsPrimary) 
		VALUES(
			@Level,
			@TaxonomyID,
			@ExternalId,
			GETUTCDATE(), 
			@modifiedBy,
			@IsPrimary)  
	END  
  
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveUrlRewite]...';


GO

CREATE Procedure [dbo].[RPV2HostedAdmin_SaveUrlRewite]

@UrlRewriteId int,
@MatchPattern nvarchar(2083),
@RewriteFormat nvarchar(2083),
@UtcModifiedDate datetime,
@ModifiedBy int ,
@PatternName nvarchar(100)

as

BEGIN
     IF EXISTS (SELECT 1   FROM   UrlRewrite    WHERE  UrlRewriteId = @UrlRewriteId)
			UPDATE UrlRewrite
			Set MatchPattern = @MatchPattern ,
				RewriteFormat = @RewriteFormat,
				UtcModifiedDate = @UtcModifiedDate ,
				ModifiedBy = @ModifiedBy ,
				PatternName = @PatternName
			WHERE UrlRewriteId = @UrlRewriteId

	 ELSE
	     
		 INSERT INTO UrlRewrite
		    VALUES( 
                   @MatchPattern ,
                    @RewriteFormat ,
                    @UtcModifiedDate ,
                    @ModifiedBy ,
					@PatternName
					)


END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveUrlRewrite]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveUrlRewrite]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the URLRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUrlRewrite]	
	@UrlRewriteId int,
	@MatchPattern NVARCHAR(2083),
	@RewriteFormat NVARCHAR(2083),
	@PatternName NVARCHAR(100),
	@modifiedBy int
AS
BEGIN
	
	IF(@UrlRewriteId=0) 
		BEGIN
			INSERT INTO UrlRewrite(
				MatchPattern,
				RewriteFormat,
				PatternName,
				utcModifiedDate,
				ModifiedBy) 
			VALUES (
				@MatchPattern,
				@RewriteFormat,
				@PatternName,
				GETUTCDATE(),
				@modifiedBy)
		END
	ELSE
		BEGIN
			UPDATE UrlRewrite 
				SET MatchPattern=@MatchPattern,
				RewriteFormat=@RewriteFormat,
				PatternName=@PatternName,
				ModifiedBy=@modifiedBy,
				utcModifiedDate=GETUTCDATE()
			WHERE UrlRewriteId = @UrlRewriteId
		END

END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveVerticalDataFromImportXmlData]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveVerticalDataFromImportXmlData]
	Added By: Noel Dsouza
	Start Date: 21st Oct 2015	
	Final Updates : 21st Oct 2015
	Reason : To Save Vertical Data From ImportXml Data
	
	Details of the SP. Please do not change the sequence of insert,updates and deletes.
	
		-- 1. Temporary Table for TaxonomyAssociationIds that will be deleted and so child can be deleted

	    -- 2. Temporary Table Declaration for updating TaxonomyAssociationIDs in TaxonomyAssociationHierarchy

		-- 3. TaxonomyAssociation UPDATE Records existing records

		-- 4. TaxonomyAssociation INSERT New Records

		-- 5. INSERT New Records in the table TaxonomyLevelExternalId for TaxonomyAssociation marketids

		-- 6. Update DocumentTypeAssociation ------------

		-- 7. Insert New DocumentTypeAssociation ------------

		-- 8. Insert into DocumentTypeExternalID table For DocumentTypeAssociation MarketIds ----------------

		-- 9. Insert New Footnotes  

		-- 10. TaxonomyAssociationHierarchy INSERT  

		-- 11. TaxonomyAssociationHierarchy delete 

		-- 12. TaxonomyAssociation Footnotes Delete 

		-- 13. DocumentTypeAssociation Delete 

		-- 14. DocumentTypeExternalId Delete where documenttypeid does not exists in DocumentTypeAssociaiton Table.
				delete only when Restored from Backup

		-- 15. TaxonomyAssociation DELETE

		-- 16. TaxonomyLevelExternalId Delete Where TaxonomyID and Level have been deleted from TaxonomyAssociation. 
				Delete only when restored from backup XML
*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveVerticalDataFromImportXmlData]	
@TT_DocumentTypeAssociation TT_DocumentTypeAssociation readonly,
@TT_TaxonomyAssociation TT_TaxonomyAssociation readonly,
@TT_TaxonomyAssociationFootnotes TT_TaxonomyAssociationFootnotes readonly,
@TT_TaxonomyAssociationHierarchy TT_TaxonomyAssociationHierarchy readonly,
@ImportedBy int,
@IsBackup bit
AS
Begin

  DECLARE @ModifiedDate datetime
  SET @ModifiedDate = GETUTCDATE()

  DECLARE @SiteID int

  SELECT top 1 @SiteID = SiteID FROM 
  @TT_TaxonomyAssociation 
  WHERE SiteID is not null

  
  -- 1. Temporary Table for TaxonomyAssociationIds that will be deleted and so child can be deleted
  
  DECLARE @TempDeleteTaxonomyAssociationIds Table
  (
	TaxonomyAssociationId int,
	MarketId nvarchar(50),
	[Level] int,
	TaxonomyID int
  )

  -- 2. Temporary Table Declaration for updating TaxonomyAssociationIDs in TaxonomyAssociationHierarchy
  DECLARE @TempTaxonomyAssociationHierarchy Table
  (
    ParentTaxonomyAssociationId int,
    ParentTaxonomyLevel int,
    ParentTaxonomyMarketID nvarchar(50),    
    RelationshipType int,
    childimportid nvarchar(60),
    ChildTaxonomyAssociationId int,
    deleteparent bit,
    Deletechild bit     
  )
    INSERT INTO @TempTaxonomyAssociationHierarchy(ParentTaxonomyLevel,ParentTaxonomyMarketID,RelationshipType,childimportid,deleteparent,Deletechild)
		SELECT parentlevel,parenttaxonomymarketId,relationshipType,childimportid,deleteparent,deletechild
		FROM @TT_TaxonomyAssociationHierarchy
		--WHERE deleteparent = 0 AND Deletechild = 0
  
  --3. TaxonomyAssociation UPDATE Records existing records
  UPDATE TaxonomyAssociation
    SET NameOverride = TTTA.nameOverride, DescriptionOverride = TTTA.descriptionOverride, CssClass = TTTA.cssClass,
		TaxonomyId = TTTA.taxonomyId, UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM TaxonomyAssociation TA
    INNER JOIN @TT_TaxonomyAssociation TTTA 
    	ON TA.MarketId = TTTA.marketId 	AND TA.[Level] = TTTA.[Level] AND isnull(TA.SiteId,0) = isnull(TTTA.siteid,0)
   WHERE TTTA.taxonomyId IS NOT NULL AND TTTA.[delete] =0  

  -- 4. TaxonomyAssociation INSERT New Records
  INSERT INTO TaxonomyAssociation([Level],TaxonomyId,SiteId,NameOverride,DescriptionOverride,CssClass,MarketId,UtcModifiedDate,ModifiedBy)				
  SELECT DISTINCT TTTA.[Level],TTTA.taxonomyId,TTTA.siteid,TTTA.nameOverride,TTTA.descriptionOverride,TTTA.cssClass,TTTA.marketId,@ModifiedDate,@ImportedBy
		FROM @TT_TaxonomyAssociation TTTA
  LEFT OUTER JOIN TaxonomyAssociation TA 
		ON TTTA.[Level] = TA.[Level] AND TTTA.marketId = TA.MarketId AND isnull(TA.SiteId,0) = isnull(TTTA.siteid,0)
  WHERE TTTA.taxonomyId IS NOT NULL AND TTTA.[delete] =0 AND TA.TaxonomyAssociationId is null

  
  -- 5. INSERT New Records in the table TaxonomyLevelExternalId for TaxonomyAssociation marketids
  INSERT INTO TaxonomyLevelExternalId(TaxonomyId,[Level],ExternalId,UtcModifiedDate,ModifiedBy)
   SELECT DISTINCT TTTA.TaxonomyId,TTTA.[Level],marketId,GETUTCDATE(),@ImportedBy FROM @TT_TaxonomyAssociation TTTA
	LEFT OUTER JOIN TaxonomyLevelExternalId TLE ON TLE.TaxonomyId = TTTA.taxonomyId AND TLE.[Level] = TTTA.[Level] 
		AND TLE.ExternalId = TTTA.marketId
	WHERE TLE.TaxonomyId IS NULL AND TTTA.taxonomyId IS NOT NULL AND TTTA.[delete]=0
  
  -- 6. Update DocumentTypeAssociation ------------
    UPDATE DocumentTypeAssociation -- Update SiteId Level DTA
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
   	INNER JOIN [Site] ON DTA.SiteId = [Site].SiteId
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  AND TTDTA.siteid IS NOT NULL
		  AND DTA.SiteId IS NOT NULL AND TTDTA.siteid = DTA.SiteId
   WHERE TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0
		  
    UPDATE DocumentTypeAssociation -- Update Taxonomy Level DTA And SiteID at Taxonomy Level is not null
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  
    INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
				AND TTDTA.[taxonomylevel] = TA.[Level]
				AND DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
	WHERE TTDTA.siteid IS NULL AND DTA.SiteId IS NULL AND  TA.SiteId = @SiteID
		AND TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0


    UPDATE DocumentTypeAssociation -- Update Taxonomy Level DTA And SiteID at Taxonomy Level is null
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  
    INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
				AND TTDTA.[taxonomylevel] = TA.[Level]
				AND DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
	WHERE TTDTA.siteid IS NULL AND DTA.SiteId IS NULL AND  TA.SiteId IS NULL   
		AND TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0
   	
    	
  -- 7. Insert New DocumentTypeAssociation ------------
  
					-- INSERT DocumentTypes only at Site level
  INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, SiteId, 
									  DocumentTypeId, MarketId, UtcModifiedDate, ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText, TTDTA.[Order], TTDTA.descriptionOverride, TTDTA.cssClass,
				  TTDTA.siteid, TTDTA.documenttypeid, TTDTA.MarketId, @ModifiedDate, @ImportedBy
  FROM @TT_DocumentTypeAssociation TTDTA
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TTDTA.siteid = DTA.SiteId AND TTDTA.documenttypeid = DTA.DocumentTypeId AND DTA.SiteId = @SiteID
  WHERE TTDTA.siteid IS NOT NULL  AND TTDTA.taxonomylevel IS NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL  AND TTDTA.[delete]=0
	  
					-- INSERT DocumentTypes at Taxonomy level and MarketId	and SiteID under TaxonomyID				
  INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, TaxonomyAssociationId, DocumentTypeId,
									  MarketId, UtcModifiedDate,  ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText,TTDTA.[Order],TTDTA.descriptionOverride,TTDTA.cssClass,TA.TaxonomyAssociationId,TTDTA.documenttypeid,
				  TTDTA.MarketId,@ModifiedDate,@ImportedBy		 
  FROM @TT_DocumentTypeAssociation TTDTA
	INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
			AND TTDTA.[taxonomylevel] = TA.[Level] 
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TA.TaxonomyAssociationId = DTA.TaxonomyAssociationId 
			AND TTDTA.documenttypeid = DTA.DocumentTypeId
  WHERE TTDTA.siteid IS NULL AND TTDTA.taxonomylevel IS NOT NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL AND TTDTA.[delete]=0
	  AND TA.[SiteId] = @SiteID

	     -- INSERT DocumentTypes at Taxonomy level and MarketId	and SiteID is null under TaxonomyID
	    INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, TaxonomyAssociationId, DocumentTypeId,
									  MarketId, UtcModifiedDate,  ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText,TTDTA.[Order],TTDTA.descriptionOverride,TTDTA.cssClass,TA.TaxonomyAssociationId,TTDTA.documenttypeid,
				  TTDTA.MarketId,@ModifiedDate,@ImportedBy		 
  FROM @TT_DocumentTypeAssociation TTDTA
	INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
			AND TTDTA.[taxonomylevel] = TA.[Level]
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TA.TaxonomyAssociationId = DTA.TaxonomyAssociationId 
			AND TTDTA.documenttypeid = DTA.DocumentTypeId
  WHERE TTDTA.siteid IS NULL AND TTDTA.taxonomylevel IS NOT NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL AND TTDTA.[delete]=0
	  AND TA.[SiteId] is null
	  
  
	  
  --8. Insert into DocumentTypeExternalID table For DocumentTypeAssociation MarketIds ----------------
  INSERT INTO DocumentTypeExternalId(DocumentTypeId,ExternalId,ModifiedBy,UtcModifiedDate)
  SELECT DISTINCT TTDTA.DocumentTypeId,TTDTA.MarketId,@ImportedBy,@ModifiedDate 
  FROM @TT_DocumentTypeAssociation TTDTA
  LEFT OUTER JOIN DocumentTypeExternalId DTE ON TTDTA.documenttypeid = DTE.DocumentTypeId
		AND TTDTA.MarketId = DTE.ExternalId		 
  WHERE TTDTA.documenttypeid IS NOT NULL AND DTE.DocumentTypeId IS NULL AND TTDTA.[delete] =0
  
  
  --9. Insert New Footnotes  
  INSERT INTO Footnote(TaxonomyAssociationId, LanguageCulture,	[Text],	UtcModifiedDate, ModifiedBy)
  SELECT DISTINCT TA.TaxonomyAssociationId,  TTFN.languageCulture,  TTFN.[text],  @ModifiedDate, @ImportedBy
	FROM TaxonomyAssociation TA
	INNER JOIN @TT_TaxonomyAssociationFootnotes  TTFN ON TA.MarketId = TTFN.taxonomymarketId
				AND TA.[Level] = TTFN.[level] 
	LEFT OUTER JOIN Footnote FN ON TA.TaxonomyAssociationID = FN.TaxonomyAssociationID
					AND TTFN.[text] = FN.[Text]
	WHERE FN.FootnoteId IS NULL AND TTFN.[delete]=0
	
 --10. TaxonomyAssociationHierarchy INSERT  
  UPDATE @TempTaxonomyAssociationHierarchy
    SET ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
    FROM @TempTaxonomyAssociationHierarchy TTAH
    INNER JOIN TaxonomyAssociation TA ON TTAH.ParentTaxonomyLevel = TA.[Level]
				AND TTAH.ParentTaxonomyMarketID = TA.MarketId	
    WHERE TA.SiteId = @SiteID


	UPDATE @TempTaxonomyAssociationHierarchy
    SET ChildTaxonomyAssociationId = TA.TaxonomyAssociationId
    FROM @TempTaxonomyAssociationHierarchy TTAH
    INNER JOIN @TT_TaxonomyAssociation  TTTA ON TTAH.childimportid = TTTA.importId 
					AND ISNULL(TTAH.childimportid,'') != '' 
					AND ISNULL(TTTA.importId,'') != '' 
    INNER JOIN TaxonomyAssociation TA ON TTTA.[Level] = TA.[Level]
				AND TTTA.marketId = TA.MarketId  
	WHERE TA.SiteId IS NULL AND  TTTA.SiteID IS NULL 
				
  INSERT INTO TaxonomyAssociationHierachy(ParentTaxonomyAssociationId,ChildTaxonomyAssociationId,RelationshipType,
			UtcModifiedDate,ModifiedBy)
  SELECT DISTINCT TTAH.ParentTaxonomyAssociationId,TTAH.ChildTaxonomyAssociationId,TTAH.RelationshipType,
			@ModifiedDate,@ImportedBy
	 FROM @TempTaxonomyAssociationHierarchy TTAH 
	 LEFT OUTER JOIN TaxonomyAssociationHierachy TAH ON TTAH.ParentTaxonomyAssociationId = TAH.ParentTaxonomyAssociationId
				AND TTAH.ChildTaxonomyAssociationId = TAH.ChildTaxonomyAssociationId
				AND TTAH.RelationshipType = TAH.RelationshipType
	WHERE TAH.ParentTaxonomyAssociationId is null
	AND TTAH.ParentTaxonomyAssociationId is not null 
	AND TTAH.ChildTaxonomyAssociationId is not null
	AND TTAH.deleteparent = 0 and TTAH.Deletechild = 0
	
  
  	
  --11. TaxonomyAssociationHierarchy delete 
  
  INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- Add TA marked to be deleted in XML
  SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
   FROM TaxonomyAssociation TA   
  INNER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
   WHERE TTTA.[delete] = 1	
   
   INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- If Backup Add items not in XML for delete
   SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
	 FROM TaxonomyAssociation TA   
	LEFT OUTER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
	WHERE TTTA.marketId IS NULL AND @IsBackup=1 AND TA.SiteId = @SiteID

  INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- If Backup Add items not in XML for delete
   SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
	 FROM TaxonomyAssociation TA   
	LEFT OUTER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
	WHERE TTTA.marketId IS NULL AND @IsBackup=1 AND TA.SiteId IS NULL
	
  DECLARE @TaxonomyAssociationHierachy_Delete TABLE 
  (
    ParentTaxonomyAssociationId int,
    ChildTaxonomyAssociationId int,
    RelationshipType int
  )
  
  DELETE TaxonomyAssociationHierachy -- Delete TaxonomyHierarchy for which TaxonomyHierarchy is marked to be deleted in the XML
   OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
   FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempTaxonomyAssociationHierarchy TTAH ON TAH.ParentTaxonomyAssociationId = TTAH.ParentTaxonomyAssociationId
				AND TAH.ChildTaxonomyAssociationId = TTAH.ChildTaxonomyAssociationId
   WHERE TTAH.deleteparent =1 OR TTAH.Deletechild = 1
  
  DELETE TaxonomyAssociationHierachy -- Delete Parent TaxonomyHierarchy for which TaxonomyAssociation is marked to be deleted in the XML
    OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
    FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
   
  DELETE TaxonomyAssociationHierachy -- Delete Child TaxonomyHierarchy for which TaxonomyAssociation is marked to be deleted in the XML
   OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	  
  FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON TAH.ChildTaxonomyAssociationId = TA.TaxonomyAssociationId
   
  DELETE TaxonomyAssociationHierachy -- Mirror TaxonomyHierarchy with the XML TaxonomyHierarchy When Backup XML is being restored
     OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
   FROM TaxonomyAssociationHierachy TAH
  LEFT OUTER JOIN @TempTaxonomyAssociationHierarchy TTAH ON TAH.ParentTaxonomyAssociationId = TTAH.ParentTaxonomyAssociationId
				AND TAH.ChildTaxonomyAssociationId = TTAH.ChildTaxonomyAssociationId
   WHERE TTAH.ParentTaxonomyAssociationId IS NULL AND @IsBackup=1
   
   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
    SET UserId = @ImportedBy
   FROM CUDHistory 
   INNER JOIN @TaxonomyAssociationHierachy_Delete TAHD ON CUDHistory.[Key] = TAHD.ParentTaxonomyAssociationId
			AND CUDHistory.SecondKey = TAHD.ChildTaxonomyAssociationId
			AND CUDHistory.ThirdKey = TAHD.RelationshipType
			AND CUDHistory.TableName = 'TaxonomyAssociationHierachy'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
	
  --12. TaxonomyAssociation Footnotes Delete 
  
  DECLARE @Foootnote_Delete table
  (
	FootnoteId int
  )
  
  DELETE Footnote -- Delete where footnote is marked to be deleted
   OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	
   FROM Footnote FN 
    INNER JOIN TaxonomyAssociation TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId
	INNER JOIN @TT_TaxonomyAssociationFootnotes TTFN 
					ON TA.MarketId = TTFN.taxonomymarketId
					 AND TA.[Level] = TTFN.[level]
					 AND TTFN.[text] = FN.[Text]
   WHERE TTFN.[delete] = 1
   
  DELETE Footnote -- Delete footnote for whihch there is not a matching text in the xml and IsBackup is true
     OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	
   FROM Footnote FN 
    INNER JOIN TaxonomyAssociation TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId
	LEFT OUTER JOIN @TT_TaxonomyAssociationFootnotes TTFN 
					ON TA.MarketId = TTFN.taxonomymarketId
					 AND TA.[Level] = TTFN.[level]
					 AND TTFN.[text] = FN.[Text]
					 WHERE TTFN.taxonomymarketId is null AND @IsBackup=1
   
  DELETE Footnote -- Delete Footnotes where TA is also going to be deleted
   OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	  
   FROM Footnote FN 
    INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId  
   
    UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
      SET UserId = @ImportedBy
   FROM CUDHistory 
	 INNER JOIN @Foootnote_Delete FND ON CUDHistory.[Key] = FND.FootnoteId			
			AND CUDHistory.TableName = 'Footnote'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
   
  --13. DocumentTypeAssociation Delete 
  
   	  DECLARE @DocumentTypeAssociation_Delete TABLE 
	  (
		DocumentTypeAssociationId int
	  )

  
	  DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at taxonomy level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]
						 AND TTDTA.documenttypeid = DTA.DocumentTypeId
						 AND DTA.SiteId is null
	   WHERE TTDTA.[delete] = 1 AND TTDTA.siteid IS NULL AND TTDTA.documenttypeid IS NOT NULL AND TA.SiteId IS NOT NULL AND TA.SiteId = @SiteID 


	   	DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at taxonomy level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]
						 AND TTDTA.documenttypeid = DTA.DocumentTypeId
						 AND DTA.SiteId is null
	   WHERE TTDTA.[delete] = 1 AND TTDTA.siteid IS NULL AND TTDTA.documenttypeid IS NOT NULL AND TA.SiteId IS NULL
   
	  DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at site level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN [Site] ON DTA.SiteId = [Site].SiteId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TTDTA.documenttypeid = DTA.DocumentTypeId 
						AND DTA.SiteId IS NOT NULL
						AND TTDTA.siteid IS NOT NULL 
						AND TTDTA.siteid = DTA.SiteId						
	   WHERE TTDTA.[delete] = 1 AND TTDTA.documenttypeid IS NOT NULL 
	   
	  
	  DELETE DocumentTypeAssociation -- Delete DocumentTypeAssociation where Taxonomy Association is also going to be deleted
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	  
	   FROM DocumentTypeAssociation DTA 
			INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId  
	   AND TA.taxonomyId IS NOT NULL
   
      --Delete for Mirroring when restored from Backup XML   
     
	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at taxonomy level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]		
						 AND DTA.DocumentTypeId = 	TTDTA.documenttypeid					  					 
	   WHERE DTA.SiteId IS NULL  AND TTDTA.documenttypeid IS NULL 
		AND TA.SiteId is NOT NULL AND TA.SiteId = @SiteID  AND @IsBackup=1


	        
	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at taxonomy level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]	
						 AND DTA.DocumentTypeId = 	TTDTA.documenttypeid						  					 
	   WHERE DTA.SiteId IS NULL  AND TTDTA.documenttypeid IS NULL 
	   AND TA.SiteId is NULL AND @IsBackup=1
	   
	   

	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at site level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
	   	INNER JOIN [Site] S on DTA.SiteId = S.SiteId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TTDTA.documenttypeid = DTA.DocumentTypeId 						
						AND TTDTA.siteid = DTA.SiteId
	   WHERE TTDTA.documenttypeid IS NULL AND DTA.SiteId IS NOT NULL AND DTA.SiteId = @SiteID AND @IsBackup=1
	   
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @DocumentTypeAssociation_Delete DTAD 
				ON CUDHistory.[Key] = DTAD.DocumentTypeAssociationId			
			AND CUDHistory.TableName = 'DocumentTypeAssociation'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
   
  --14. DocumentTypeExternalId Delete where documenttypeid does not exists in DocumentTypeAssociaiton Table .delete only when Restored from Backup
  
	  DECLARE @DocumentTypeExternalId_Delete TABLE
	  (
		DocumentTypeId int,
		ExternalId nvarchar(100)
	  )
	    
	  DELETE DocumentTypeExternalId 
		OUTPUT deleted.DocumentTypeId,
				deleted.ExternalId
				INTO @DocumentTypeExternalId_Delete
		FROM DocumentTypeExternalId DTE
		LEFT OUTER JOIN  DocumentTypeAssociation DTA on DTE.DocumentTypeId = DTA.documenttypeid		
		WHERE DTA.DocumentTypeAssociationId IS NULL	AND @IsBackup=1
		
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @DocumentTypeExternalId_Delete DTED 
				ON CUDHistory.[Key] = DTED.DocumentTypeId
				 AND CUDHistory.SecondKey = DTED.ExternalId			
			AND CUDHistory.TableName = 'DocumentTypeExternalId'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL

   
   
   --15. TaxonomyAssociation DELETE
	   DELETE TaxonomyAssociation
		  FROM TaxonomyAssociation TA 
		INNER JOIN @TempDeleteTaxonomyAssociationIds TTTA 
			ON TA.TaxonomyAssociationId = TTTA.TaxonomyAssociationId 
			
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @TempDeleteTaxonomyAssociationIds TDTA 
				ON CUDHistory.[Key] = TDTA.TaxonomyAssociationId			
			AND CUDHistory.TableName = 'TaxonomyAssociation'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
			
								
  --16. TaxonomyLevelExternalId Delete Where TaxonomyID and Level have been deleted from TaxonomyAssociation. Delete only when restored from backup XML
	DECLARE @TaxonomyLevelExternalId_Delete TABLE
	  (
		[Level] int,
		TaxonomyId int,
		ExternalId nvarchar(100)
	  )
	  
	  DELETE TaxonomyLevelExternalId 
	  		OUTPUT deleted.[Level],
				deleted.TaxonomyId,
				deleted.ExternalId
				INTO @TaxonomyLevelExternalId_Delete
		FROM TaxonomyLevelExternalId TLE
		LEFT OUTER JOIN  TaxonomyAssociation TA on TLE.TaxonomyId = TA.taxonomyId
			AND TLE.[Level] = TA.[Level]			
		WHERE TA.TaxonomyAssociationId is null	AND @IsBackup=1							

	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @TaxonomyLevelExternalId_Delete TLED 
				ON CUDHistory.[Key] = TLED.[Level]
				 AND CUDHistory.SecondKey = TLED.TaxonomyId
				 AND CUDHistory.ThirdKey = TLED.ExternalId			
			AND CUDHistory.TableName = 'TaxonomyLevelExternalId'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
								
End
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveVerticalXmlBackupToExportTableAndUpdateStatus]...';


GO
--Created BY : Noel Dsouza
--Created Date : 10/22/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlBackupToExportTableAndUpdateStatus
@VerticalXmlImportId int,
@ExportXml XML,
@ExportedBy int
AS
BEGIN
  DECLARE @VerticalXmlExportId INT
  
  INSERT INTO VerticalXmlExport(ExportTypes,ExportXml,ExportDate,ExportedBy,ExportDescription,[Status])
  VALUES(1,@ExportXml,GETUTCDATE(),@ExportedBy,'Backup of XML Before Import',2)
  
  SET @VerticalXmlExportId = @@IDENTITY
  
  UPDATE VerticalXmlImport
    SET ExportBackupId = @VerticalXmlExportId,
		[Status] = 2
	WHERE VerticalXmlImportId = @VerticalXmlImportId    
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveVerticalXmlExport]...';


GO
--Author : Noel Dsouza
--Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlExport
@VerticalXmlExportId int,
@ExportTypes int,
@ExportXml XML,
@ExportedBy int,
@ExportDescription nvarchar(400),
@Status int
AS
BEGIN
  IF @VerticalXmlExportId = 0
	  BEGIN
	    INSERT INTO VerticalXmlExport
			(
			 ExportTypes,
			 ExportXml,
			 ExportDate,
			 ExportedBy,
			 ExportDescription,
			 [Status]
			)
		VALUES
			(
			 @ExportTypes,
			 @ExportXml,
			 GETUTCDATE(),
			 @ExportedBy,
			 @ExportDescription,
			 @Status			
			)
	  END
  ELSE
      BEGIN
		UPDATE VerticalXmlExport
		SET ExportTypes = @ExportTypes,
			ExportXml = @ExportXml,
			[Status] = @Status	
		WHERE VerticalXmlExportId = @VerticalXmlExportId	
      END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveVerticalXmlImport]...';


GO
--Author : Krishnan KV
--Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_SaveVerticalXmlImport
@VerticalXmlImportId int,
@ImportTypes int,
@ImportXml XML,
@ImportedBy int,
@ImportDescription nvarchar(400),
@Status int
AS
BEGIN
  IF @VerticalXmlImportId = 0
	  BEGIN
	    INSERT INTO VerticalXmlImport
			(
			 ImportTypes,
			 ImportXml,
			 ImportDate,
			 ImportedBy,
			 ImportDescription,
			 [Status]
			)
		VALUES
			(
			 @ImportTypes,
			 @ImportXml,
			 GETUTCDATE(),
			 @ImportedBy,
			 @ImportDescription,
			 @Status			
			)
	  END
  ELSE
      BEGIN
		UPDATE VerticalXmlImport
		SET [Status] = @Status	
		WHERE VerticalXmlImportId = @VerticalXmlImportId	
      END
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SiteData_CacheDependencyCheck]...';


GO

-- =============================================
-- Author:		Arshdeep
-- Create date: 16-Sep-2015

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteId, COUNT_BIG(*) AS Total
	FROM	dbo.[Site]
	GROUP BY SiteId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SiteFeature_CacheDependencyCheck]...';


GO
CREATE PROCEDURE  [dbo].[RPV2HostedAdmin_SiteFeature_CacheDependencyCheck]
AS
BEGIN
  SELECT SiteId , [Key] ,  COUNT_BIG(*) AS Total
  FROM dbo.SiteFeature
  GROUP BY  SiteId , [Key]
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SiteNavigationData_CacheDependencyCheck]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteNavigationData_CacheDependencyCheck]
AS
BEGIN
	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigation
	GROUP BY SiteNavigationId;
	
   	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigationVersion
	GROUP BY SiteNavigationId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_SiteTextData_CacheDependencyCheck]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SiteTextData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteText
	GROUP BY SiteTextId;
	
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteTextVersion
	GROUP BY SiteTextId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_StaticResourceData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel
-- Create date: 19-Sep-2015
-- RPV2HostedAdmin_StaticResourceData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_StaticResourceData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	StaticResourceId, COUNT_BIG(*) AS Total
	FROM	dbo.StaticResource
	GROUP BY StaticResourceId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 2nd-Oct-2015
-- RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck
-- =============================================
CREATE PROCEDURE [dbo].RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck
AS
BEGIN
  SELECT TaxonomyId,COUNT_BIG(*) AS Total
   FROM dbo.TaxonomyAssociation
  GROUP BY TaxonomyId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_TaxonomyLevelExternalIdData_CacheDependencyCheck]...';


GO
-- =============================================
-- Author:		Noel Dsouza
-- Create date: 18th-Sep-2015
-- RPV2HostedAdmin_TaxonomyLevelExternalIdData_CacheDependencyCheck

-- =============================================
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_TaxonomyLevelExternalIdData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	[Level],TaxonomyId,ExternalId, COUNT_BIG(*) AS Total
	FROM	dbo.TaxonomyLevelExternalId
	GROUP BY [Level],TaxonomyId,ExternalId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_UrlRewriteData_CacheDependencyCheck]...';


GO
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_UrlRewriteData_CacheDependencyCheck]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : Cache Dependency UrlRewriteId
*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_UrlRewriteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	UrlRewriteId, COUNT_BIG(*) AS Total
	FROM	dbo.UrlRewrite
	GROUP BY UrlRewriteId;
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_VerticalXmlExportData_CacheDependencyCheck]...';


GO
-- Created By : Noel Dsouza
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_VerticalXmlExportData_CacheDependencyCheck
AS
BEGIN
  SELECT VerticalXmlExportId,COUNT_BIG(*) AS Total
    FROM VerticalXmlExport
  GROUP BY VerticalXmlExportId
END
GO
PRINT N'Creating [dbo].[RPV2HostedAdmin_VerticalXmlImportData_CacheDependencyCheck]...';


GO
-- Created By : Krishnan KV
-- Created Date : 10/08/2015
CREATE PROCEDURE [dbo].RPV2HostedAdmin_VerticalXmlImportData_CacheDependencyCheck
AS
BEGIN
  SELECT VerticalXmlImportId,COUNT_BIG(*) AS Total
    FROM VerticalXmlImport
  GROUP BY VerticalXmlImportId
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_ClientsSiteData_CacheDependencyCheck]...';


GO

Create PROCEDURE [dbo].[RPV2HostedSites_ClientsSiteData_CacheDependencyCheck]
AS
BEGIN
   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteText
	GROUP BY SiteTextId;
	
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageText
	GROUP BY PageTextId;

   	SELECT	SiteTextId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteTextVersion
	GROUP BY SiteTextId;
	
	SELECT	PageTextId, COUNT_BIG(*) AS Total
	FROM	dbo.PageTextVersion
	GROUP BY PageTextId;
	
	SELECT	SiteID, COUNT_BIG(*) AS Total
	FROM	dbo.Site
	GROUP BY SiteID;
	
	SELECT StaticResourceId, COUNT_BIG(*) AS Total 
	FROM dbo.StaticResource 
	GROUP BY StaticResourceId

	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigation
	GROUP BY SiteNavigationId;

	SELECT	SiteNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.SiteNavigationVersion
	GROUP BY SiteNavigationId;

	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigation
	GROUP BY PageNavigationId;

	SELECT	PageNavigationId, COUNT_BIG(*) AS Total
	FROM	dbo.PageNavigationVersion
	GROUP BY PageNavigationId;

	SELECT	SiteId, [Key], COUNT_BIG(*) AS Total
	FROM	dbo.SiteFeature
	GROUP BY SiteId, [Key]

	SELECT	SiteId, PageId, [Key], COUNT_BIG(*) AS Total
	FROM	dbo.PageFeature
	GROUP BY SiteId, PageId, [Key]

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetAllSiteTextAndPageText]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetAllSiteTextAndPageText]
as
BEGIN

	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteId,
			-1 as PageID,
			'S' as Entity
	  FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteId,
			-1 as PageID,
			'S' as Entity
	  FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
	   AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	UNION
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId,
			'P' as Entity	
	  FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId,
			'P' as Entity
	  FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	 Order by 6,2,1  
 END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetClientsSiteData]...';


GO


CREATE PROCEDURE [dbo].[RPV2HostedSites_GetClientsSiteData]
AS
BEGIN
  	
	 
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId
	FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]	
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId
	FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  
	 
	 
	SELECT 1 AS IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			[PageText].SiteId,						
			PageId
	FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			[PageText].SiteId,									
			PageId
	FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  
	
	SELECT SiteID,
		Name as SiteName,
		DefaultPageId,
		ParentSiteID,
		TemplateId,		
		CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite			
	FROM 
	 Site 
	LEFT OUTER JOIN ClientSettings on Site.[SiteId] = ClientSettings.[DefaultSiteId]

	SELECT 
		[FileName], 
		Size, 
		MimeType, 
		Data, 
		UtcModifiedDate 
	FROM StaticResource
	
	
	
	SELECT 1 AS IsCurrentProductionVersion,
			SiteID,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM SiteNavigation
	  INNER JOIN SiteNavigationVersion on SiteNavigation.SiteNavigationId = SiteNavigationVersion.SiteNavigationId
			AND SiteNavigation.CurrentVersion = SiteNavigationVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			SiteID,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM SiteNavigation
	  INNER JOIN 
	   (SELECT SiteNavigationID,MAX([version]) as ProofVersion FROM SiteNavigationVersion
		GROUP by SiteNavigationID) as ProofingVersion on SiteNavigation.SiteNavigationId = ProofingVersion.SiteNavigationId  
	  INNER JOIN SiteNavigationVersion on SiteNavigation.SiteNavigationId = SiteNavigationVersion.SiteNavigationId
	   AND SiteNavigationVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1  



	SELECT 1 AS IsCurrentProductionVersion,
			SiteID,
			PageId,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM PageNavigation
	  INNER JOIN PageNavigationVersion on PageNavigation.PageNavigationId = PageNavigationVersion.PageNavigationId
			AND PageNavigation.CurrentVersion = PageNavigationVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			SiteID,
			PageId,
			NavigationKey,
			Convert(varchar(max), NavigationXml) As 'NavigationXml'
	FROM PageNavigation
	  INNER JOIN 
	   (SELECT PageNavigationID,MAX([version]) as ProofVersion FROM PageNavigationVersion
		GROUP by PageNavigationID) as ProofingVersion on PageNavigation.PageNavigationId = ProofingVersion.PageNavigationId  
	  INNER JOIN PageNavigationVersion on PageNavigation.PageNavigationId = PageNavigationVersion.PageNavigationId
	   AND PageNavigationVersion.[Version] = ProofingVersion.ProofVersion
	ORDER BY 2,1


	--SiteFeature

	SELECT 	SiteID,			
			[Key],
			FeatureMode
	FROM SiteFeature

	--PageFeature

	SELECT 	SiteID,
			PageId,
			[Key],
			FeatureMode
	FROM PageFeature
	
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetPageText]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetPageText]
as
BEGIN
	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId
	  FROM PageText
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
			AND PageText.CurrentVersion = PageTextVersion.[Version]		
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			PageTextVersion.[Text],
			SiteId,
			PageId
	  FROM PageText
	  INNER JOIN 
	   (SELECT PageTextID,MAX([version]) as ProofVersion FROM PageTextVersion
		GROUP by PageTextID) as ProofingVersion on PageText.PageTextId = ProofingVersion.PageTextId  
	  INNER JOIN PageTextVersion on PageText.PageTextId = PageTextVersion.PageTextId
	   AND PageTextVersion.[Version] = ProofingVersion.ProofVersion
	 Order by 2,1  
 END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetRequestMaterialPrintRequestData]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetRequestMaterialPrintRequestData]
@ReportFromDate DateTime,
@ReportToDate DateTime
AS
BEGIN
	SELECT  DISTINCT UniqueID,
		RMPH.RequestMaterialPrintHistoryID,
		RMPH.RequestDateUtc,
		RMPH.ClientCompanyName,
		RMPH.ClientFirstName,
		RMPH.ClientLastName, 
		RMPH.Address1,
		RMPH.Address2,
		RMPH.City,
		RMPH.StateOrProvince,
		RMPH.PostalCode,
		RMPPD.Quantity,
		TA.TaxonomyAssociationID,
		TA.TaxonomyID,
		TA.NameOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,
		RMSKU.SKUName
	FROM RequestMaterialPrintHistory RMPH
	INNER JOIN RequestMaterialPrintProsDetail RMPPD on RMPH.RequestMaterialPrintHistoryID = RMPPD.RequestMaterialPrintHistoryID
	INNER JOIN [dbo].[TaxonomyAssociation] TA ON RMPPD.TaxonomyAssociationId = TA.TaxonomyAssociationId
	LEFT JOIN [dbo].RequestMaterialSKUDetail RMSKU on TA.TaxonomyAssociationId = RMSKU.TaxonomyAssociationId AND RMPPD.DocumentTypeId = RMSKU.DocumentTypeId
	RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.DocumentTypeId = RMPPD.DocumentTypeId AND SiteLevelDTA.SiteId IS NOT NULL
	LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
	WHERE TA.TaxonomyAssociationId IS NOT NULL AND (RequestDateUtc BETWEEN  @ReportFromDate AND @ReportToDate)
	ORDER BY RMPH.RequestMaterialPrintHistoryID      

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetSiteText]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetSiteText]
as
BEGIN

	SELECT 1 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId,
			[Site].Name,
			CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite			
	  FROM SiteText
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteText.CurrentVersion = SiteTextVersion.[Version]	
	  INNER JOIN [Site]	ON SiteText.SiteId = [Site].SiteId
	  LEFT OUTER JOIN [ClientSettings] ON [ClientSettings].DefaultSiteId = [Site].SiteId
	UNION
	SELECT 0 as IsCurrentProductionVersion,
			ResourceKey,
			SiteTextVersion.[Text],
			SiteText.SiteId,
			[Site].Name,
			CASE WHEN DefaultSiteId IS NULL THEN 0 ELSE 1 END AS IsDefaultSite	
	  FROM SiteText
	  INNER JOIN 
	   (SELECT SiteTextID,MAX([version]) as ProofVersion FROM SiteTextVersion
		GROUP by SiteTextID) as ProofingVersion on SiteText.SiteTextId = ProofingVersion.SiteTextId  
	  INNER JOIN SiteTextVersion on SiteText.SiteTextId = SiteTextVersion.SiteTextId
			AND SiteTextVersion.[Version] = ProofingVersion.ProofVersion
	  INNER JOIN [Site]	ON SiteText.SiteId = [Site].SiteId
	  LEFT OUTER JOIN [ClientSettings] ON [ClientSettings].DefaultSiteId = [Site].SiteId
	 Order by 2,1  
 END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetStaticResources]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetStaticResources]
as
BEGIN
	SELECT [FileName], Size, MimeType, Data, UtcModifiedDate FROM StaticResource
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyAssociationDocuments]...';


GO

CREATE PROCEDURE [dbo].RPV2HostedSites_GetTaxonomyAssociationDocuments
@SiteName nvarchar(100)=null
as
Begin
	DECLARE @SiteID int
	
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END

	
	SELECT DISTINCT 
		TA.TaxonomyAssociationID,
		TA.TaxonomyID,
		TA.NameOverride,
		TA.DescriptionOverride,		
		TA.NameOverride AS TaxonomyNameOverRide,
		TA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		TA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,            
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		isnull(Footnote.[Order],0) FootnoteOrder
	FROM [dbo].[TaxonomyAssociation] TA 
		--LEFT OUTER JOIN [dbo].[TaxonomyAssociationHierachy] TAH on TA.TaxonomyAssociationId = TAH.ParentTaxonomyAssociationId
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
	WHERE TA.TaxonomyAssociationId IS NOT NULL 
	--and TAH.ParentTaxonomyAssociationId is null 
	and TA.SiteId = @SiteID
	ORDER BY 1,12        

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyAssociationHierarchy]...';


GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationHierarchy]
	@InternalPTAID INT=null,
	@ExternalID NVARCHAR(100)=null,
	@SiteName nvarchar(100)=null
AS
BEGIN
	DECLARE @SiteID int
	
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END

	Declare @InternalPTAIDToQueryOn INT
	
	IF @InternalPTAID is not null
		BEGIN
		  SET @InternalPTAIDToQueryOn= @InternalPTAID
		END
	ELSE IF @ExternalID IS NOT NULL
		BEGIN
			SELECT TOP 1 @InternalPTAIDToQueryOn = TAH.ParentTaxonomyAssociationId
			FROM TaxonomyAssociationHierachy TAH 
			  INNER JOIN TaxonomyAssociation TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
			  INNER JOIN TaxonomyLevelExternalId TLE on TA.TaxonomyId = TLE.TaxonomyId AND 	TA.[Level] = TLE.[Level]		  
			WHERE TLE.ExternalId = @ExternalID AND TA.SiteId = @SiteID
		END
	ELSE
		BEGIN
			SELECT TOP 1 @InternalPTAIDToQueryOn = TAH.ParentTaxonomyAssociationId
			FROM TaxonomyAssociationHierachy TAH 
			  INNER JOIN TaxonomyAssociation TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
			  INNER JOIN TaxonomyLevelExternalId TLE on TA.TaxonomyId = TLE.TaxonomyId AND 	TA.[Level] = TLE.[Level]		  
			WHERE TA.SiteId = @SiteID
		END

	SELECT DISTINCT
		PTA.TaxonomyAssociationId,   
		PTA.TaxonomyID,
		PTA.NameOverride,
		PTA.DescriptionOverride,
		1 AS IsParent,		
		PTA.NameOverride AS TaxonomyNameOverRide,
		PTA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		PTA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,         
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) as DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder
	FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] PTA ON TAH.ParentTaxonomyAssociationId = PTA.TaxonomyAssociationID AND TAH.ParentTaxonomyAssociationID = @InternalPTAIDToQueryOn 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = PTA.TaxonomyAssociationID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON PTA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON PTA.TaxonomyId  = TALE.TaxonomyId AND PTA.[Level] = TALE.[Level]
		
	WHERE PTA.TaxonomyAssociationId IS NOT NULL
	
	UNION
	
	SELECT DISTINCT
		CTA.TaxonomyAssociationID,
		CTA.TaxonomyID,
		CTA.NameOverride,
		CTA.DescriptionOverride,
		0 AS IsParent,
		CTA.NameOverride AS TaxonomyNameOverRide,
		CTA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		CTA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,            
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeNameLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder
	FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID AND TAH.ParentTaxonomyAssociationID = @InternalPTAIDToQueryOn 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = CTA.TaxonomyAssociationID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON CTA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON CTA.TaxonomyId  = TALE.TaxonomyId AND CTA.[Level] = TALE.[Level]
	WHERE CTA.TaxonomyAssociationId IS NOT NULL
	ORDER BY 5,1,13        
        
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyAssociationLinks]...';


GO

Create Procedure [dbo].RPV2HostedSites_GetTaxonomyAssociationLinks
@SiteName nvarchar(100)=null
as
Begin
DECLARE @SiteID int
IF @SiteName is null
	BEGIN
		SELECT @SiteID=DefaultSiteId FROM ClientSettings 
    END
ELSE
  	BEGIN
  	    SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  	END

  SELECT DISTINCT 
	  TAH.ParentTaxonomyAssociationId,
	  TA.TaxonomyID,
	  NameOverride,
	  TA.DescriptionOverride
  FROM TaxonomyAssociationHierachy TAH
	INNER JOIN TaxonomyAssociation TA on TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationID 	
    INNER JOIN [Site] on TA.SiteID = [Site].SiteID AND [Site].SiteId = @SiteID
    LEFT OUTER JOIN TaxonomyLevelExternalId TALE ON TA.TaxonomyId = TALE.TaxonomyId
						AND TA.[Level] = TALE.[Level]
End
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyIDByTaxonomyAssociationIDOrExternalID]...';


GO
CREATE PROCEDURE dbo.RPV2HostedSites_GetTaxonomyIDByTaxonomyAssociationIDOrExternalID
@Level int=NULL,
@ExternalId nvarchar(100)=NULL,
@TAID INT=NULL
AS
BEGIN
  	 
  	 IF @ExternalId IS NOT NULL
  		 BEGIN
	  	   SELECT TOP 1 TA.TaxonomyID,TA.NameOverride
	  	     FROM TaxonomyLevelExternalId TLE
	  	     INNER JOIN TaxonomyAssociation TA ON TLE.TaxonomyId = TA.TaxonomyId AND TLE.[Level] = TA.[Level] 
	  	   WHERE TA.[Level] = @Level
	  	     AND ExternalId = @ExternalId
  		 END
	 ELSE
	    IF @TAID IS NOT NULL
	     BEGIN
	       SELECT TOP 1 TaxonomyID,NameOverride 
	  	     FROM TaxonomyAssociation
	  	   WHERE TaxonomyAssociationId = @TAID
	     
	     END  	
  	
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomySpecificDocumentFrame]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomySpecificDocumentFrame]
@ExternalId nvarchar(100)=null,
@TAID INT = NULL,
@SiteName nvarchar(100)=null
AS
BEGIN
	DECLARE @SiteID int
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  	BEGIN
  	    SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  	END


	SELECT DISTINCT
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			TA.NameOverride as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.CssClass as TaxonomyCssClass,
  	 				CASE 
  	 					WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText 
  	 					ELSE TALevelDTA.HeaderText 
  	 				END  AS DocumentTypeHeaderText,            
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText 
						ELSE TALevelDTA.LinkText
					END	AS DocumentTypeNameOverride,
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride 
						ELSE TALevelDTA.DescriptionOverride
					END	AS DocumentTypeDescriptionOverride,         
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass 
						ELSE TALevelDTA.CssClass
					END AS DocumentTypeCssClass,
					CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId 
						ELSE TALevelDTA.DocumentTypeId 
					END AS DocumentTypeId,         			 
					CASE 
						WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] 
						ELSE TALevelDTA.[Order]
					END AS DocumentTypeOrder,
					ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) AS DocumentTypeExternalID
	FROM 
		TaxonomyAssociation TA 
			LEFT OUTER JOIN TaxonomyLevelExternalID TLE  ON TA.TaxonomyID = TLE.TaxonomyID AND TA.[Level] = TLE.[Level] 
			RIGHT OUTER JOIN DocumentTypeAssociation SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
			LEFT OUTER JOIN DocumentTypeAssociation TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			LEFT OUTER JOIN DocumentTypeExternalID SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
			LEFT OUTER JOIN DocumentTypeExternalID TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
					AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)
						   
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_SaveRequestMaterialEmailHistory]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_SaveRequestMaterialEmailHistory]  
@SiteName nvarchar(100),
@RecipEmail nvarchar(300),  
@UniqueID uniqueidentifier,
@RequestBatchId uniqueidentifier,  
@RequestUriString nvarchar(500), 
@UserAgent nvarchar(500),  
@IPAddress nvarchar(500),  
@Referer nvarchar(500),  
@Sent bit,  
@RequestMaterialEmailProsDetail TT_RequestMaterialProsDetail READONLY  
AS  
BEGIN 

	DECLARE @SiteID int
	SELECT @SiteID = DefaultSiteId from ClientSettings

	IF EXISTS(SELECT SiteId FROM [Site] WHERE Name = @SiteName)
	BEGIN
		SELECT @SiteID = SiteId FROM [Site] WHERE Name = @SiteName
	END

	DECLARE @UserAgentID int, @RequestUri int, @ReferrerUri int
  
	SELECT @UserAgentID = UserAgentId 
	FROM UserAgent 
	WHERE UserAgentString = @UserAgent
  
	IF  @UserAgentID IS NULL
	BEGIN
		INSERT INTO UserAgent(UserAgentString)
		VALUES(@UserAgent)	
			
		SELECT @UserAgentID = SCOPE_IDENTITY()
	END

	SELECT @RequestUri = UriId
	FROM Uri
	WHERE UriString = @RequestUriString
  
	IF @RequestUri is NULL
	BEGIN
	INSERT INTO Uri(UriString)
	VALUES(@RequestUriString)
	SELECT @RequestUri = SCOPE_IDENTITY()     
	END

	SELECT @ReferrerUri = UriId
	FROM Uri
	WHERE UriString = @Referer
  
	IF @ReferrerUri is NULL
	BEGIN
		INSERT INTO Uri(UriString)
		VALUES(@Referer)

		SELECT @ReferrerUri = SCOPE_IDENTITY()
	END
	 
	DECLARE @identityRequestMaterialEmailHistoryId int  
	INSERT INTO  RequestMaterialEmailHistory(
		SiteId,  
		RecipEmail,  
		UniqueID,
		RequestBatchId,
		RequestUri,  
		UserAgentId,  
		IPAddress,  
		ReferrerUri,  
		[Sent]  
	)  
	VALUES(  
		@SiteID,
		@RecipEmail,  
		@UniqueID,
		@RequestBatchId,
		@RequestUri, 
		@UserAgentID,  
		@IPAddress,  
		@ReferrerUri,  
		@Sent  
	)  
  
	SET @identityRequestMaterialEmailHistoryId=SCOPE_IDENTITY()  
  
	INSERT INTO RequestMaterialEmailProsDetail(
		RequestMaterialEmailHistoryId,  
		TaxonomyAssociationId,  
		DocumentTypeId
	)
		Select @identityRequestMaterialEmailHistoryId,
			   TaxonomyAssociationId,  
			   DocumentTypeId 
		From @RequestMaterialEmailProsDetail dt		
        
END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_SaveRequestMaterialPrintHistory]...';


GO
CREATE  PROCEDURE [dbo].[RPV2HostedSites_SaveRequestMaterialPrintHistory]
@SiteName nvarchar(100),
@ClientCompanyName nvarchar(100) ,   
@ClientFirstName nvarchar(100) ,    
@ClientMiddleName nvarchar(100) ,    
@ClientLastName nvarchar(100) ,    
@ClientFullName nvarchar(200) ,    
@Address1 nvarchar(100) ,    
@Address2 nvarchar(100) ,    
@City nvarchar(100) ,    
@StateOrProvince nvarchar(100) ,    
@PostalCode nvarchar(20) ,    
@UniqueID uniqueidentifier , 
@RequestBatchId uniqueidentifier, 
@RequestUriString nvarchar(500),    
@UserAgent nvarchar(250) ,    
@IPAddress nvarchar(250) ,    
@Referer nvarchar(max) ,    
@RequestMaterialPrintProsDetail TT_RequestMaterialProsDetail READONLY  
AS
BEGIN  
  
	DECLARE @SiteID int
	SELECT @SiteID = DefaultSiteId from ClientSettings

	IF EXISTS(SELECT SiteId FROM [Site] WHERE Name = @SiteName)
	BEGIN
		SELECT @SiteID = SiteId FROM [Site] WHERE Name = @SiteName
	END

	DECLARE @UserAgentID int, @RequestUri int, @ReferrerUri int
  
	SELECT @UserAgentID = UserAgentId 
	FROM UserAgent 
	WHERE UserAgentString = @UserAgent
  
	IF  @UserAgentID IS NULL
	BEGIN
		INSERT INTO UserAgent(UserAgentString)
		VALUES(@UserAgent)	
			
		SELECT @UserAgentID = SCOPE_IDENTITY()
	END

	SELECT @RequestUri = UriId
	FROM Uri
	WHERE UriString = @RequestUriString
  
	IF @RequestUri is NULL
	BEGIN
	INSERT INTO Uri(UriString)
	VALUES(@RequestUriString)
	SELECT @RequestUri = SCOPE_IDENTITY()     
	END

	SELECT @ReferrerUri = UriId
	FROM Uri
	WHERE UriString = @Referer
  
	IF @ReferrerUri is NULL
	BEGIN
		INSERT INTO Uri(UriString)
		VALUES(@Referer)

		SELECT @ReferrerUri = SCOPE_IDENTITY()
	END    
  
	DECLARE  @identityRequestMaterialPrintHistoryID int  
  
  
  
	INSERT INTO RequestMaterialPrintHistory(  
		SiteId,
		ClientCompanyName,  
		ClientFirstName,
		ClientMiddleName,   
		ClientLastName,    
		ClientName,    
		Address1,    
		Address2,    
		City,    
		StateOrProvince,    
		PostalCode,    
		UniqueID,  
		RequestBatchId,
		RequestUri,    
		UserAgentId,
		IPAddress,    
		ReferrerUri
	)
	VALUES( 
		@SiteID,
		@ClientCompanyName ,     
		@ClientFirstName ,     
		@ClientMiddleName ,     
		@ClientLastName ,    
		@ClientFullName ,    
		@Address1 ,    
		@Address2 ,    
		@City ,    
		@StateOrProvince ,    
		@PostalCode ,    
		@UniqueID ,
		@RequestBatchId, 
		@RequestUri,   
		@UserAgentID,    
		@IPAddress,    
		@ReferrerUri
	)  


	SET @identityRequestMaterialPrintHistoryID = SCOPE_IDENTITY() 
  
	INSERT INTO RequestMaterialPrintProsDetail(  
		RequestMaterialPrintHistoryID ,		
		TaxonomyAssociationId ,    
		DocumentTypeId ,    
		Quantity    
	)
		Select @identityRequestMaterialPrintHistoryID , TaxonomyAssociationId, DocumentTypeId, Quantity
		From @RequestMaterialPrintProsDetail dt
	END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_SaveSiteActivity]...';


GO

/*
	Procedure Name:[dbo].[RPV2HostedSites_SaveSiteActivity]
	Added By: Noel Dsouza
	Date: 09/20/2015	
	Reason : To add SiteActivity
*/

CREATE PROCEDURE [dbo].[RPV2HostedSites_SaveSiteActivity]
@SiteName nvarchar(100),
@ClientIPAddress varchar(15),
@UserAgentString nvarchar(max),
@HttpMethod varchar(20),
@RequestUriString nvarchar(2083),
@ParsedRequestUriString nvarchar(2083),
@ServerName varchar(15),
@ReferrerUriString nvarchar(2083),
@InitDoc bit,
@RequestBatchId uniqueIdentifier,
@UserId int,
@PageId int,
@Level int,
@DocumentTypeExternalID nvarchar(100),
@TaxonomyExternalId nvarchar(100),
@TaxonomyAssociationGroupId int,
@TaxonomyAssociationId int,
@DocumentTypeId int,
@ClientDocumentGroupId int,
@ClientDocumentId int,
@XBRLDocumentName varchar(100),
@XBRLItemType int,
@BadRequestIssue int,
@BadRequestParameterName nvarchar(200),
@BadRequestParameterValue nvarchar(max)
AS
BEGIN
  DECLARE @UserAgentID int
  
  DECLARE @SiteID int
  
  DECLARE @RequestUri int
  
  DECLARE @ParsedRequestUri int
  
  DECLARE @ReferrerUri int
  
  SELECT @UserAgentID=UserAgentId 
    FROM UserAgent 
   WHERE UserAgentString = @UserAgentString
  
  IF  @UserAgentID IS NULL
  BEGIN
    INSERT INTO UserAgent(UserAgentString)
		VALUES(@UserAgentString)
		
	SELECT @UserAgentID = SCOPE_IDENTITY()
  END
  
  SELECT @SiteID = SiteID 
    FROM [Site] 
  WHERE Name = @SiteName
  
  IF @SiteID IS NULL
  BEGIN
    SELECT @SiteID = DefaultSiteID 
      FROM ClientSettings
  END
  
  SELECT @RequestUri = UriId
    FROM Uri
  WHERE UriString = @RequestUriString
  
  IF @RequestUri is NULL
  BEGIN
    INSERT INTO Uri(UriString)
     VALUES(@RequestUriString)
     SELECT @RequestUri = SCOPE_IDENTITY()     
  END
  
  SELECT @ParsedRequestUri = UriId
    FROM Uri
  WHERE UriString = @ParsedRequestUriString
  
  IF @ParsedRequestUri is NULL
  BEGIN
    INSERT INTO Uri(UriString)
     VALUES(@ParsedRequestUriString)
     SELECT @ParsedRequestUri = SCOPE_IDENTITY()
  END
  
    SELECT @ReferrerUri = UriId
    FROM Uri
  WHERE UriString = @ReferrerUriString
  
  IF @ReferrerUri is NULL
  BEGIN
    INSERT INTO Uri(UriString)
     VALUES(@ReferrerUriString)
     SELECT @ReferrerUri = SCOPE_IDENTITY()
  END
  
  IF @DocumentTypeId is NULL and ISNULL(@DocumentTypeExternalID, '') != ''
  BEGIN
     SELECT @DocumentTypeId=DocumentTypeId
      FROM DocumentTypeExternalId
     WHERE ExternalId = @DocumentTypeExternalID
  END
  
  IF @TaxonomyAssociationId is NULL
  BEGIN
     SELECT @TaxonomyAssociationId= TaxonomyAssociationId
      FROM TaxonomyAssociation TA
     INNER JOIN TaxonomyLevelExternalId  TLE on TA.TaxonomyId = TLE.TaxonomyId 
			AND TLE.[Level] = TA.[Level]
     WHERE TLE.ExternalId = @TaxonomyExternalId AND TLE.[Level]= @Level
  END
  
  INSERT INTO SiteActivity
   (
	 SiteID,
     ClientIPAddress,
     UserAgentId,
     RequestUtcDate,
     HttpMethod,
     RequestUri,
     ParsedRequestUri,
     ServerName,
     ReferrerUri,
     InitDoc,
     RequestBatchId,
     UserId,
     PageId,
     TaxonomyAssociationGroupId,
     TaxonomyAssociationId,
     DocumentTypeId,
     ClientDocumentGroupId,
     ClientDocumentId,
     XBRLDocumentName,
     XBRLItemType
     )
   VALUES
     (
       @SiteID,
       @ClientIPAddress,
       @UserAgentID,
       GETUTCDATE(),
       @HttpMethod,
       @RequestUri,
       @ParsedRequestUri,
       @ServerName,
       @ReferrerUri,
       @InitDoc,
       @RequestBatchId,
       @UserId,
       @PageId,
       @TaxonomyAssociationGroupId,
       @TaxonomyAssociationId,
       @DocumentTypeId,
       @ClientDocumentGroupId,
       @ClientDocumentId,
       @XBRLDocumentName,
       @XBRLItemType
     )  

	 IF(ISNULL(@BadRequestIssue, 0) <> 0)
	 BEGIN

		INSERT INTO BadRequest
           ([SiteActivityId]
           ,[RequestIssue]
           ,[ParameterName]
           ,[ParameterValue])
		VALUES
           (SCOPE_IDENTITY()
           ,@BadRequestIssue
           ,@BadRequestParameterName
           ,@BadRequestParameterValue)
		   		
	 END

END
GO
PRINT N'Creating [dbo].[RPV2HostedSites_UpdateRequestMaterialEmailClickDate]...';


GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_UpdateRequestMaterialEmailClickDate]
@UniqueId UNIQUEIDENTIFIER ,   
@DocumentTypeId INT,
@TaxonomyAssociationId INT output
AS  
BEGIN  
  
   
	DECLARE @FClickDateUtc DATETIME   
	SELECT @FClickDateUtc = FClickDateUtc FROM RequestMaterialEmailHistory WHERE UniqueID = @UniqueId  
   
	IF @FClickDateUtc IS NULL  
	BEGIN  
		UPDATE RequestMaterialEmailHistory  
		SET FClickDateUtc = GetUtcDate()  
		WHERE UniqueID = @UniqueId  
	END  
  
   
	DECLARE @SClickDateUtc datetime   
	SELECT @SClickDateUtc = SClickDateUtc From RequestMaterialEmailHistory  
	INNER JOIN RequestMaterialEmailProsDetail ON RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
	WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId  
   
	IF @SClickDateUtc IS NULL
	BEGIN  
		UPDATE RequestMaterialEmailProsDetail  
		SET SClickDateUtc = GetUtcDate()
		FROM RequestMaterialEmailProsDetail  
		INNER JOIN RequestMaterialEmailHistory on RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
		WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId  
	END
	
	SELECT @TaxonomyAssociationId = RequestMaterialEmailProsDetail.TaxonomyAssociationId
	FROM RequestMaterialEmailProsDetail  
	INNER JOIN RequestMaterialEmailHistory on RequestMaterialEmailProsDetail.RequestMaterialEmailHistoryId = RequestMaterialEmailHistory.RequestMaterialEmailHistoryId  
	WHERE UniqueID = @UniqueId AND DocumentTypeId = @DocumentTypeId 
  
END
GO

PRINT N'Creating [dbo].[RPV2HostedSites_BillingReport]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_BillingReport]
@MarketID NVARCHAR(100) = NULL,
@SearchSiteName NVARCHAR(100) = NULL,
@StartDate DATETIME=NULL,
@EndDate DATETIME=NULL
AS  
BEGIN
	DECLARE @SiteDetails TABLE(RowNum INT, SiteId INT, Name NVARCHAR(200), DefaultPageId INT)	
	DECLARE @URLDetails TABLE(SiteName NVARCHAR(100), MarketID NVARCHAR(100),NameOverride NVARCHAR(200))
	DECLARE @SiteCount INT, @RowCount INT = 1, @DefaultPageID INT, @SiteID INT, @SiteName NVARCHAR(100)
	-- FETCH Site Details based on Search creteria
	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]
	WHERE @SearchSiteName IS NULL OR Name = @SearchSiteName
	--Get the total site counts
	SELECT @SiteCount = COUNT(*) FROM @SiteDetails
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN
		SELECT @SiteID = SiteID, @SiteName = Name, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount
		IF @DefaultPageID = 1 --TAL case
		BEGIN
			--logic for TAL
		INSERT INTO @URLDetails(SiteName, MarketID,NameOverride)
		SELECT @SiteName, MarketID , NameOverride              
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteID
		UNION
   
		SELECT @SiteName, CTA.MarketId,CTA.NameOverride
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
		SELECT TaxonomyAssociationID 
		FROM TaxonomyAssociation WHERE SiteId = @SiteID
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL
		END

		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN
			--Logic for TAD
			INSERT INTO @URLDetails(SiteName, MarketID,NameOverride)
			SELECT @SiteName, MarketID,NameOverride FROM TaxonomyAssociation WHERE SiteID = @SiteID
		END
		SET @RowCount = @RowCount + 1

	END
	
	SELECT * FROM @URLDetails ORDER BY SiteName,MarketID
	
	--Get count of new funds
	SELECT count(*) FROM CUDHistory
	INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationID = CUDHistory.[Key]
	WHERE TableName='TaxonomyAssociation' and CUDType='I'
	AND UtcCUDDate BETWEEN @StartDate AND @EndDate

	--Get count of deleted funds
	SELECT count(*) FROM CUDHistory CH 
	INNER JOIN CUDHistoryData CHD 
	ON CH.CUDHistoryID=CHD.CUDHistoryId 
	WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='D' AND CHD.ColumnName='MarketId'
	AND UtcCUDDate BETWEEN @StartDate AND @EndDate
	AND CHD.OldValue NOT IN (SELECT DISTINCT TaxonomyAssociation.marketId FROM TaxonomyAssociation)
	
	--Get Details of the deleted funds
	SELECT CH.UtcCUDDate,CHD.OldValue FROM CUDHistory CH 
	INNER JOIN CUDHistoryData CHD 
	ON CH.CUDHistoryID=CHD.CUDHistoryId 
	WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='D' AND CHD.ColumnName='MarketId'
	AND CH.UtcCUDDate BETWEEN @StartDate AND @EndDate
	AND CHD.OldValue NOT IN (SELECT DISTINCT TaxonomyAssociation.marketId FROM TaxonomyAssociation)
END
GO

PRINT N'Creating [dbo].[RPV2HostedSites_URLGeneration]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_URLGeneration]
@MarketID NVARCHAR(100) = NULL,
@SearchSiteName NVARCHAR(100) = NULL
AS  
BEGIN

	DECLARE @SiteDetails TABLE(RowNum int, SiteId int, Name nvarchar(200), DefaultPageId int)	
	DECLARE @URLDetails TABLE(SiteName nvarchar(100),FundName nvarchar(200), TaxonomyId int, Level int,TAExternalID NVARCHAR(100),
								DTExternalID NVARCHAR(100), DocumentType NVARCHAR(100), DocumentTypeOrder int, XBRLFeatureMode int)

	DECLARE @SiteCount int, @RowCount int = 1, @DefaultPageID int, @SiteID int, @SiteName NVARCHAR(100), @XBRLFeatureMode int = 0
	
	-- FETCH Site Details based on Search creteria
	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]
	WHERE @SearchSiteName IS NULL OR Name = @SearchSiteName
	
	--Get the total site counts
	SELECT @SiteCount = COUNT(*) from @SiteDetails
	
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN
	
		SELECT @SiteID = SiteID, @SiteName = Name, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount

		SELECT @XBRLFeatureMode = FeatureMode FROm SiteFeature where [Key] = 'XBRL' and SiteId = @SiteID

		IF @DefaultPageID = 1 --TAL case
		BEGIN

		
			--logic for TAL
		INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		SELECT DISTINCT
	    @SiteName  AS SiteName,
		PTA.NameOverride AS FundName,
		PTA.TaxonomyId, PTA.Level,
		TALE.ExternalId,
		
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
	    CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,		
		@XBRLFeatureMode
      	FROM [dbo].[TaxonomyAssociationHierachy] TAH

		INNER JOIN [dbo].[TaxonomyAssociation] PTA ON TAH.ParentTaxonomyAssociationId = PTA.TaxonomyAssociationID 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID 
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = PTA.TaxonomyAssociationID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON PTA.TaxonomyId  = TALE.TaxonomyId AND PTA.[Level] = TALE.[Level]

	    WHERE (PTA.TaxonomyAssociationId IS NOT NULL)
	    And(PTA.MarketId=@MarketID or @MarketID is NULL)
			    
    UNION
        SELECT DISTINCT
	    @SiteName AS SiteName,
	    CTA.NameOverride,
		CTA.TaxonomyId, CTA.Level,
		TALE.ExternalId,
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
		
	  	CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
	    @XBRLFeatureMode
    	FROM [dbo].[TaxonomyAssociationHierachy] TAH

		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID 
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = CTA.TaxonomyAssociationID		
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
    	LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON CTA.TaxonomyId  = TALE.TaxonomyId AND CTA.[Level] = TALE.[Level]

	    WHERE CTA.TaxonomyAssociationId IS NOT NULL
	    And(CTA.MarketId=@MarketID or @MarketID is NULL)	    

    END
		
		
		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN

			--Logic for TAD

		INSERT INTO @URLDetails(SiteName,FundName, TaxonomyId, Level, TAExternalID,DTExternalID,DocumentType, DocumentTypeOrder, XBRLFeatureMode)
		
		SELECT DISTINCT 
		@SiteName  AS SiteName,
		TA.NameOverride as FundName,	
		TA.TaxonomyId, TA.Level,
		TALE.ExternalID,
		
		ISNULL(TADTE.ExternalID,SiteDTE.ExternalID),
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentType,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		@XBRLFeatureMode		
		FROM [dbo].[TaxonomyAssociation] TA 
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
			LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
	
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
		WHERE TA.TaxonomyAssociationId IS NOT NULL AND TA.SiteId = @SiteID
		And(TA.MarketId=@MarketID or @MarketID is NULL)


	End
		  
		SET @RowCount = @RowCount + 1
	End


	SELECT * FROM @URLDetails t
	ORDER BY t.SiteName, t.FundName, t.TAExternalID, t.DocumentTypeOrder

END
Go

PRINT N'Creating [dbo].[RPV2HostedSites_GetDocumentUpdateReportData]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetDocumentUpdateReportData]
@TT_DocumentUpdateVerticalData dbo.TT_DocumentUpdateVerticalData READONLY,
@StartDate DATETIME,
@EndDate DATETIME
AS  
BEGIN

	-----THIS REPORT WILL RUN IN EASTERN TIME ZONE --
	/*
		Actual Start and End date for this report -

		IF we run report at EST - 05/15/2016 12:30 AM, THEN 
		Start Date = 05/14/2016 12:00 AM
		END DATE = 05/14/2016 11:59 PM

		IF we run report at EST - 05/15/2016 12:30 PM, THEN ALSO
		Start Date = 05/14/2016 12:00 AM
		END DATE = 05/14/2016 11:59 PM
	
	*/
	--STEP 1: Convert @EndDate into EST TIME
	DECLARE @EST_END_DATE DATETIME =  DATEADD(HOUR, 1, Convert(datetime, SWITCHOFFSET(convert(DateTimeOffSet, @ENDDATE), DATENAME(tz, SYSDATETIMEOFFSET()))))

	--STEP 2: GET THE EST TIME PART FROM END DATE which will be needed to determine actual start date and end date of this report.
	DECLARE @DateDiff DATETIME = (@EST_END_DATE - Convert(Datetime, Convert(Date, @EST_END_DATE)))
	
	--STEP 3: ACTUAL Start and END Date in UTC
	DECLARE @UTC_Actual_EndDate DATETIME = 	@EndDate - @DateDiff
	DECLARE @UTC_Actual_StartDate DATETIME = @UTC_Actual_EndDate - 1

	DECLARE @CST_Actual_EndDate DATETIME = DATEADD(HOUR, -1, (@EST_END_DATE - @DateDiff))

	--STEP 3: GET NEWLY ADDED CUSIPs  (by considering above actual start and end date logic)
	DECLARE @NewlyAddedTaxonomyIds Table(TaxonomyId INT)

	INSERT INTO @NewlyAddedTaxonomyIds

		SELECT CHD.NewValue 
		FROM CUDHistory CH 
		INNER JOIN CUDHistoryData CHD 
		ON CH.CUDHistoryID=CHD.CUDHistoryId 
		WHERE CH.TableName='TaxonomyAssociation' AND CH.CUDType='I' AND CHD.ColumnName='TaxonomyId'
		AND UtcCUDDate BETWEEN @UTC_Actual_StartDate AND @UTC_Actual_EndDate



	----------------------------------------

	-- Step 1:  Remove all INVALID taxonomy Ids from TaxonomyLevelDocUpdate table which are not present in vertical market

	DELETE TaxonomyLevelDocUpdate
	FROM TaxonomyLevelDocUpdate 
	LEFT JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.MarketId IS NULL AND DocUpDt.DocumentTypeID IS NULL

	-- Step 2: Update  TaxonomyLevelDocUpdate for all changes in DocumentDate

	UPDATE TaxonomyLevelDocUpdate
	SET DocumentDate = DocUpDt.DocumentDate
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.DocumentDate IS NOT NULL AND TaxonomyLevelDocUpdate.DocumentDate <> DocUpDt.DocumentDate

	-- Step 3: Update  TaxonomyLevelDocUpdate for all changes in DocumentUpdateDate

	UPDATE TaxonomyLevelDocUpdate
	SET DocumentUpdatedDate = DocUpDt.DocumentUpdatedDate
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE DocUpDt.DocumentUpdatedDate IS NOT NULL


	-- Step 4: Update  TaxonomyLevelDocUpdate for all changes in TAXONOMY NAME CHANGES

	UPDATE TaxonomyLevelDocUpdate
	SET TaxonomyName = DocUpDt.TaxonomyName
	FROM TaxonomyLevelDocUpdate
	INNER JOIN @TT_DocumentUpdateVerticalData DocUpDt ON DocUpDt.MarketId = TaxonomyLevelDocUpdate.MarketId AND DocUpDt.DocumentTypeID = TaxonomyLevelDocUpdate.DocumentTypeID
	WHERE ISNULL(DocUpDt.TaxonomyName, '') != '' AND TaxonomyLevelDocUpdate.TaxonomyName <> DocUpDt.TaxonomyName

	-- Step 5: INSERT New records in TaxonomyLevelDocUpdate

	INSERT INTO TaxonomyLevelDocUpdate(MarketId, DocumentTypeID, TaxonomyName, DocumentDate, DocumentUpdatedDate)

	SELECT DocUpDt.MarketId, DocUpDt.DocumentTypeID, DocUpDt.TaxonomyName, DocUpDt.DocumentDate, DocUpDt.DocumentUpdatedDate
	FROM @TT_DocumentUpdateVerticalData DocUpDt
	LEFT JOIN TaxonomyLevelDocUpdate ON TaxonomyLevelDocUpdate.MarketId = DocUpDt.MarketId AND TaxonomyLevelDocUpdate.DocumentTypeID = DocUpDt.DocumentTypeID
	WHERE TaxonomyLevelDocUpdate.MarketId IS NULL AND TaxonomyLevelDocUpdate.DocumentTypeID IS NULL


	--Step 6 Select Final data to be sent in Document update


	DECLARE @SiteCount int, @RowCount int = 1, @DefaultPageID int, @SiteID int
	DECLARE @SiteDetails TABLE(RowNum int, SiteId int, Name nvarchar(200), DefaultPageId int)
	DECLARE @DocUPDTDetails TABLE(MarketId nvarchar(100), DocumentTypeMarketId NVARCHAR(100), DocumentDate DateTime, DocumentUpdatedDate DateTime, DocumentUpdated char(1), DocumentTypeOrder int)

	INSERT INTO @SiteDetails(RowNum, SiteId, Name, DefaultPageId)
	SELECT ROW_NUMBER() OVER(ORDER BY SiteID), SiteId, Name, DefaultPageId
	FROM [Site]

	--Get the total site counts
	SELECT @SiteCount = COUNT(*) from @SiteDetails
	
	--Loop through each site and get all required details    
    WHILE @RowCount <= @SiteCount
	BEGIN

		SELECT @SiteID = SiteID, @DefaultPageID = DefaultPageId FROM @SiteDetails WHERE RowNum = @RowCount

		IF @DefaultPageID = 7 --TAG case
		BEGIN

			INSERT INTO @DocUPDTDetails(MarketId, DocumentTypeMarketId, DocumentDate, DocumentUpdatedDate, DocumentUpdated, DocumentTypeOrder)
			
			SELECT DISTINCT TA.MarketId,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
				TaxonomyLevelDocUpdate.DocumentDate,
				DATEADD(HOUR, 1, TaxonomyLevelDocUpdate.DocumentUpdatedDate),				
				CASE WHEN newTA.TaxonomyId IS NULL AND DocumentUpdatedDate BETWEEN @CST_Actual_EndDate - 1 AND @CST_Actual_EndDate THEN 'Y' ELSE 'N' END,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder
			FROM TaxonomyLevelDocUpdate
			INNER JOIN TaxonomyAssociation TA On TA.MarketId = TaxonomyLevelDocUpdate.MarketId
			INNER JOIN TaxonomyAssociationGroupTaxonomyAssociation TAGTA ON TAGTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
			INNER JOIN TaxonomyAssociationGroup TAG ON TAG.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID AND TALevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			LEFT JOIN @NewlyAddedTaxonomyIds newTA ON TA.TaxonomyId = newTA.TaxonomyId
			WHERE TA.TaxonomyAssociationId IS NOT NULL AND TAG.SiteId = @SiteID AND TALE.SendDocumentUpdate = 1 
	
			
			
		END
		ELSE IF @DefaultPageID = 4 --TAD case
		BEGIN

			INSERT INTO @DocUPDTDetails(MarketId, DocumentTypeMarketId, DocumentDate, DocumentUpdatedDate, DocumentUpdated, DocumentTypeOrder)
			
			SELECT DISTINCT TA.MarketId,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
				TaxonomyLevelDocUpdate.DocumentDate,
				DATEADD(HOUR, 1, TaxonomyLevelDocUpdate.DocumentUpdatedDate),
				CASE WHEN newTA.TaxonomyId IS NULL AND DocumentUpdatedDate BETWEEN @CST_Actual_EndDate - 1 AND @CST_Actual_EndDate THEN 'Y' ELSE 'N' END,
				CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder
			FROM TaxonomyLevelDocUpdate
			INNER JOIN TaxonomyAssociation TA On TA.MarketId = TaxonomyLevelDocUpdate.MarketId
			RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID AND SiteLevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID AND TALevelDTA.DocumentTypeId = TaxonomyLevelDocUpdate.DocumentTypeId
			LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
			LEFT JOIN @NewlyAddedTaxonomyIds newTA ON TA.TaxonomyId = newTA.TaxonomyId
			WHERE TA.TaxonomyAssociationId IS NOT NULL AND TA.SiteId = @SiteID AND TALE.SendDocumentUpdate = 1

		END
		--TODO   : Need to add code for TAL and TAHD

		SET @RowCount = @RowCount + 1

	END

	SELECT DISTINCT *
	FROM @DocUPDTDetails
	ORDER BY MarketId, DocumentTypeOrder

END
GO



PRINT N'Creating [dbo].[RPV2HostedSites_GetDocumentUpdateTaxonomyData]...';
GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetDocumentUpdateTaxonomyData]
AS
BEGIN

	SELECT DISTINCT MarketId, CASE WHEN ISNULL(NameOverride, '') != '' THEN CAST (1 AS BIT) ELSE  CAST (0 AS BIT) END 'IsNameOverrideProvided'
	FROM TaxonomyAssociation

END
GO


PRINT N'Creating [dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationClientDocument]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_DeleteTaxonomyAssociationClientDocument]				 
@TaxonomyId int,
@ClientDocumentId int,
@deletedBy int                				 			
AS 
BEGIN 

	DELETE TaxonomyAssociationClientDocument
	WHERE TaxonomyAssociationId IN (
				SELECT TaxonomyAssociationId FROM TaxonomyAssociation WHERE TaxonomyId = @TaxonomyId) 
	AND ClientDocumentId = @ClientDocumentId
		    
   
	UPDATE	CUDHistory				 
	SET	UserId = @deletedBy
	WHERE	TableName = N'TaxonomyAssociationClientDocument'
	AND	[Key] IN (
				SELECT TaxonomyAssociationId FROM TaxonomyAssociation WHERE TaxonomyId = @TaxonomyId)
	AND [SecondKey] = @ClientDocumentId
				
	AND [CUDType] = 'D' 

END
GO

PRINT N'Creating [dbo].[RPV2HostedAdmin_GetAllTaxonomyAssociationClientDocument]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetAllTaxonomyAssociationClientDocument]
AS
BEGIN

	SELECT  distinct	  
		TaxonomyAssociation.[Level] as Level,
		TaxonomyAssociation.TaxonomyId AS TaxonomyId,
		NameOverride,
		TaxonomyAssociationClientDocument.ClientDocumentId AS ClientDocumentId,
	    ClientDocumentType.ClientDocumentTypeId as ClientDocumentTypeId,
		ClientDocumentType.Name as ClientDocumentTypeName,
		ClientDocument.Name as ClientDocumentName,
		ClientDocument.[FileName] as ClientDocumentFileName,
		TaxonomyAssociationClientDocument.UtcModifiedDate as UtcLastModified,
		TaxonomyAssociationClientDocument.ModifiedBy
	FROM TaxonomyAssociationClientDocument
	INNER JOIN TaxonomyAssociation ON TaxonomyAssociation.TaxonomyAssociationId = TaxonomyAssociationClientDocument.TaxonomyAssociationId
	INNER JOIN ClientDocument ON ClientDocument.ClientDocumentId = TaxonomyAssociationClientDocument.ClientDocumentId
	INNER JOIN ClientDocumentType ON ClientDocumentType.ClientDocumentTypeId = ClientDocument.ClientDocumentTypeId

END 
GO

PRINT N'Creating [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociationClientDocument]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveTaxonomyAssociationClientDocument]
@TaxonomyId INT,
@ClientDocumentId INT,
@ModifiedBy INT
AS
BEGIN

	INSERT INTO TaxonomyAssociationClientDocument(
					TaxonomyAssociationId,
					ClientDocumentId,
					UtcModifiedDate,
					ModifiedBy)
		SELECT  TaxonomyAssociationId,
				@ClientDocumentId,
				GETUTCDATE(),
				@ModifiedBy
		FROM TaxonomyAssociation 
		WHERE TaxonomyId = @TaxonomyId
END
GO


PRINT N'Creating [dbo].[RPV2HostedAdmin_TaxonomyAssociationClientDocument_CacheDependencyCheck]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_TaxonomyAssociationClientDocument_CacheDependencyCheck]
AS
BEGIN

	SELECT TaxonomyAssociationId, COUNT_BIG(*) AS Total
	FROM   dbo.TaxonomyAssociation
	GROUP BY TaxonomyAssociationId;

	SELECT ClientDocumentTypeId, COUNT_BIG(*) AS Total
	FROM   dbo.ClientDocumentType
	GROUP BY ClientDocumentTypeId;

	SELECT ClientDocumentId, COUNT_BIG(*) AS Total
	FROM   dbo.ClientDocument
	GROUP BY ClientDocumentId;

	SELECT TaxonomyAssociationId, ClientDocumentId,  COUNT_BIG(*) AS Total
	FROM   dbo.TaxonomyAssociationClientDocument
	GROUP BY TaxonomyAssociationId, ClientDocumentId;

END
GO


PRINT N'Creating [dbo].[RPV2HostedSites_GetClientDocumentByClientDocumentId]...';
GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetClientDocumentByClientDocumentId]
@ClientDocumentId int
AS
BEGIN

	SELECT [FileName], MimeType, Data, ClientDocumentData.UtcModifiedDate 
	FROM ClientDocument
	INNER JOIN ClientDocumentData ON ClientDocument.ClientDocumentId = ClientDocumentData.ClientDocumentId
	WHERE ClientDocument.ClientDocumentId = @ClientDocumentId

END
GO


PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyAssociationClientDocumentFrame]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationClientDocumentFrame]  --- EXEC RPV2HostedSites_GetTaxonomyAssociationClientDocumentFrame NULL, '024936742', 'MMD'
@TAID INT = NULL,
@ExternalId NVARCHAR(100) = NULL,
@ClientDocumentType NVARCHAR(100),
@SiteName nvarchar(100)=null
AS
BEGIN

	DECLARE @TaxonomyAssociationIdsForSite TABLE(TaxonomyAssociationId int)
	DECLARE @SiteID int, @DefaultPageID int
	IF @SiteName is null
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  	BEGIN
  	    SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  	END

	SELECT @DefaultPageID = DefaultPageId FROM Site WHERE SiteID = @SiteId
	IF @DefaultPageID = 1 --TAL case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		
		SELECT DISTINCT TaxonomyAssociationId           
		FROM TaxonomyAssociation TA 
		WHERE SiteId = @SiteID

		UNION
   
		SELECT CTA.TaxonomyAssociationId 
		FROM [dbo].[TaxonomyAssociationHierachy] TAH
		INNER JOIN [dbo].[TaxonomyAssociation] CTA ON TAH.ChildTaxonomyAssociationId = CTA.TaxonomyAssociationID
		INNER JOIN (
			SELECT TaxonomyAssociationID FROM TaxonomyAssociation WHERE SiteId = @SiteID
		) t ON TAH.ParentTaxonomyAssociationId = t.TaxonomyAssociationId     
		WHERE CTA.TaxonomyAssociationId IS NOT NULL		
		
	END
	ELSE IF @DefaultPageID = 4 --TAD case
	BEGIN
		INSERT INTO @TaxonomyAssociationIdsForSite
		SELECT DISTINCT TaxonomyAssociationId FROM TaxonomyAssociation WHERE SiteID = @SiteID
	END

	DECLARE @HostedDocumentsDisplayCount int

	SELECT @HostedDocumentsDisplayCount = HostedDocumentsDisplayCount from ClientDocumentType WHERE Name = @ClientDocumentType
 
	IF @HostedDocumentsDisplayCount <> 0
	BEGIN

		SELECT DISTINCT TOP (@HostedDocumentsDisplayCount) 
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			TA.NameOverride as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.MarketId,
			CD.ClientDocumentId,			
			CD.Name,
			CD.[FileName],
			CD.[Description]
		FROM ClientDocument CD
		INNER JOIN ClientDocumentType CDT ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId AND CDT.Name = @ClientDocumentType
		INNER JOIN TaxonomyAssociationClientDocument TACD ON TACD.ClientDocumentId = CD.ClientDocumentId
		INNER JOIN TaxonomyAssociation TA ON TA.TaxonomyAssociationId = TACD.TaxonomyAssociationId
		INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId 		
		LEFT OUTER JOIN TaxonomyLevelExternalId TLE  ON TA.TaxonomyId = TLE.TaxonomyId AND TA.[Level] = TLE.[Level] 
		WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
							AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)	
		ORDER BY CD.[FileName] DESC	
	END
	ELSE
	BEGIN

		SELECT DISTINCT
			TA.TaxonomyID,
			TA.TaxonomyAssociationID,
			TA.NameOverride as TaxonomyNameOverRide,
			TA.DescriptionOverride as TaxonomyDesciptionOverRide,
			TA.MarketId,
			CD.ClientDocumentId,			
			CD.Name,
			CD.[FileName],
			CD.[Description]
		FROM ClientDocument CD
		INNER JOIN ClientDocumentType CDT ON CD.ClientDocumentTypeId = CDT.ClientDocumentTypeId AND CDT.Name = @ClientDocumentType
		INNER JOIN TaxonomyAssociationClientDocument TACD ON TACD.ClientDocumentId = CD.ClientDocumentId
		INNER JOIN TaxonomyAssociation TA ON TA.TaxonomyAssociationId = TACD.TaxonomyAssociationId
		INNER JOIN @TaxonomyAssociationIdsForSite TAFS ON TAFS.TaxonomyAssociationId = TA.TaxonomyAssociationId		
		LEFT OUTER JOIN TaxonomyLevelExternalId TLE  ON TA.TaxONomyID = TLE.TaxonomyId AND TA.[Level] = TLE.[Level] 
		WHERE (TLE.ExternalID = @ExternalID OR @ExternalId is null)
							AND (TA.TaxonomyAssociationId = @TAID OR @TAID is null)	
		ORDER BY CD.[FileName] DESC	
	
	END

END
GO

PRINT N'Creating [dbo].[RPV2HostedSites_GetBrowserVersion]...';
GO

CREATE PROCEDURE [dbo].[RPV2HostedSites_GetBrowserVersion]
	@BrowserName NVARCHAR(100)=NULL
AS
BEGIN

	SELECT	Id,
			Name,
			[Version],
			DownloadUrl
	FROM BrowserVersion
	WHERE Name = @BrowserName
	
END
GO

PRINT N'Creating [dbo].[RPV2HostedSites_GetTaxonomyAssociationGroups]...';
GO
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetTaxonomyAssociationGroups]   --  exec RPV2HostedSites_GetTaxonomyAssociationGroups 6
	@TAGID INT=NULL,
	@SiteName NVARCHAR(100)=NULL
AS
BEGIN
DECLARE @SiteID INT	
	IF @SiteName IS NULL
		BEGIN
			SELECT @SiteID=DefaultSiteId FROM ClientSettings 
		END
	ELSE
  		BEGIN
  			SELECT @SiteID=SiteId FROM Site WHERE Name = @SiteName
  		END
	SELECT 
		TaxonomyAssociationGroupId,
		Name,
		[Description],
		CssClass,
		ParentTaxonomyAssociationId
	FROM TaxonomyAssociationGroup 
	WHERE SiteId = @SiteID

	--Get All Taxonomies

	SELECT DISTINCT
		TAG.TaxonomyAssociationGroupId,	
		TAG.Name,
		TA.TaxonomyAssociationId,   
		TA.TaxonomyID,
		TA.NameOverride,
		TA.DescriptionOverride,
		TA.NameOverride AS TaxonomyNameOverRide,
		TA.DescriptionOverride AS TaxonomyDesciptionOverRide,
		TA.CssClass AS TaxonomyCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.HeaderText ELSE TALevelDTA.HeaderText END AS DocumentTypeHeaderText,         
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.LinkText ELSE TALevelDTA.LinkText END AS DocumentTypeLinkText,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.DescriptionOverride ELSE TALevelDTA.DescriptionOverride END AS DocumentTypeDescriptionOverride,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.CssClass ELSE TALevelDTA.CssClass END AS DocumentTypeCssClass,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.[Order] ELSE TALevelDTA.[Order] END AS DocumentTypeOrder,
		CASE WHEN TALevelDTA.DocumentTypeAssociationID IS NULL THEN SiteLevelDTA.MarketId ELSE TALevelDTA.MarketId END AS DocumentTypeMarketId,
		CASE WHEN TALevelDTA.DocumentTypeId IS NULL THEN SiteLevelDTA.DocumentTypeId ELSE TALevelDTA.DocumentTypeId END AS DocumentTypeId,         
		--ISNULL(TADTE.ExternalID,SiteDTE.ExternalID) as DocumentTypeExternalID,
		Footnote.[Text] FootnoteText,
		Footnote.[Order] FootnoteOrder		
	FROM [dbo].[TaxonomyAssociationGroup] TAG
	    INNER JOIN [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] TAGTA ON TAG.TaxonomyAssociationGroupId = TAGTA.TaxonomyAssociationGroupId 
																				AND (@TAGID IS NULL OR @TAGID = TAG.TaxonomyAssociationGroupId)
		INNER JOIN [dbo].[TaxonomyAssociation] TA ON TAGTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
		RIGHT OUTER JOIN [dbo].[DocumentTypeAssociation] SiteLevelDTA ON SiteLevelDTA.SiteId = @SiteID
		LEFT OUTER JOIN [dbo].[DocumentTypeAssociation] TALevelDTA ON TALevelDTA.TaxonomyAssociationID = TA.TaxonomyAssociationID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] SiteDTE ON SiteLevelDTA.DocumentTypeID = SiteDTE.DocumentTypeID
		--LEFT OUTER JOIN [dbo].[DocumentTypeExternalID] TADTE ON TALevelDTA.DocumentTypeID = TADTE.DocumentTypeID
		LEFT OUTER JOIN [dbo].[Footnote] Footnote ON TA.TaxonomyAssociationID  = Footnote.TaxonomyAssociationId
		LEFT OUTER JOIN [dbo].[TaxonomyLevelExternalId] TALE ON TA.TaxonomyId  = TALE.TaxonomyId AND TA.[Level] = TALE.[Level]
		
	WHERE TA.TaxonomyAssociationId IS NOT NULL
	
	ORDER BY 1,3,14
END
GO




PRINT N'Creating [dbo].[T_ClientDocument_U]...';


GO
-- Update trigger for ClientDocument
CREATE TRIGGER T_ClientDocument_U
ON dbo.ClientDocument
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.ClientDocumentTypeId) AS ClientDocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
		CONVERT(NVARCHAR(MAX), i.MimeType) as MimeType,
		CONVERT(NVARCHAR(MAX), i.IsPrivate) AS IsPrivate,
		CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
		    CONVERT(NVARCHAR(MAX), d.ClientDocumentTypeId) AS ClientDocumentTypeId,		
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) as MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) AS IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,		
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ClientDocumentTypeId' THEN d.ClientDocumentTypeId
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'MimeType' THEN d.MimeType	
			WHEN 'IsPrivate' THEN d.IsPrivate
			WHEN 'ContentUri' THEN d.ContentUri				
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ClientDocumentTypeId' THEN i.ClientDocumentTypeId
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'MimeType' THEN i.MimeType	
			WHEN 'IsPrivate' THEN i.IsPrivate
			WHEN 'ContentUri' THEN i.ContentUri			
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('ClientDocumentTypeId'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri') , ('Name'),  ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocument_D]...';


GO
-- Delete trigger for ClientDocument
CREATE TRIGGER T_ClientDocument_D
ON dbo.ClientDocument
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
		    CONVERT(NVARCHAR(MAX), d.ClientDocumentTypeId) AS ClientDocumentTypeId,		
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) as MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) AS IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,		
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientDocumentTypeId' THEN d.ClientDocumentTypeId
		WHEN 'FileName' THEN d.[FileName]
		WHEN 'MimeType' THEN d.MimeType	
		WHEN 'IsPrivate' THEN d.IsPrivate
		WHEN 'ContentUri' THEN d.ContentUri				
		WHEN 'Name' THEN d.Name
		WHEN 'Description' THEN d.[Description]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ClientDocumentTypeId'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri') , ('Name'),  ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocument_I]...';


GO
-- Insert trigger for ClientDocument
CREATE TRIGGER T_ClientDocument_I
ON dbo.ClientDocument
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.ClientDocumentTypeId) AS ClientDocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
		CONVERT(NVARCHAR(MAX), i.MimeType) as MimeType,
		CONVERT(NVARCHAR(MAX), i.IsPrivate) AS IsPrivate,
		CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'ClientDocumentTypeId' THEN i.ClientDocumentTypeId
		WHEN 'FileName' THEN i.[FileName]
		WHEN 'MimeType' THEN i.MimeType	
		WHEN 'IsPrivate' THEN i.IsPrivate
		WHEN 'ContentUri' THEN i.ContentUri			
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES ('ClientDocumentTypeId'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri') , ('Name'),  ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentData_D]...';


GO

-- Delete trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_D
ON dbo.ClientDocumentData
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data,
			CONVERT(NVARCHAR(MAX), d.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), d.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), d.DataHash) as DataHash
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Data' THEN d.Data
			WHEN 'HasData' THEN d.HasData
			WHEN 'DataLength' THEN d.[DataLength]
			WHEN 'DataHash' THEN d.DataHash
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentData_U]...';


GO
-- Update trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_U
ON dbo.ClientDocumentData
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data,
			CONVERT(NVARCHAR(MAX), i.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), i.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), i.DataHash) as DataHash
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentId,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data,
			CONVERT(NVARCHAR(MAX), d.HasData) AS HasData,
			CONVERT(NVARCHAR(MAX), d.[DataLength]) AS [DataLength],
			CONVERT(NVARCHAR(MAX), d.DataHash) as DataHash
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Data' THEN d.Data
			WHEN 'HasData' THEN d.HasData
			WHEN 'DataLength' THEN d.[DataLength]
			WHEN 'DataHash' THEN d.DataHash
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Data' THEN i.Data
			WHEN 'HasData' THEN i.HasData
			WHEN 'DataLength' THEN i.[DataLength]
			WHEN 'DataHash' THEN i.DataHash
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentData_I]...';


GO
-- Insert trigger for ClientDocumentData
CREATE TRIGGER T_ClientDocumentData_I
ON dbo.ClientDocumentData
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.Data) AS Data,
		CONVERT(NVARCHAR(MAX), i.HasData) AS HasData,
		CONVERT(NVARCHAR(MAX), i.[DataLength]) AS [DataLength],
		CONVERT(NVARCHAR(MAX), i.DataHash) as DataHash
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Data' THEN i.Data
		WHEN 'HasData' THEN i.HasData
		WHEN 'DataLength' THEN i.[DataLength]
		WHEN 'DataHash' THEN i.DataHash
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Data'), ('HasData'), ('DataLength'), ('DataHash')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroup_D]...';


GO

-- Delete trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_D
ON dbo.ClientDocumentGroup
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentGroupId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.CssClass) AS CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN d.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN d.CssClass
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroup_U]...';


GO
-- Update trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_U
ON dbo.ClientDocumentGroup
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), i.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), i.CssClass) AS CssClass
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
			CONVERT(NVARCHAR(MAX), d.CssClass) AS CssClass
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN d.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN d.CssClass
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
			WHEN 'ParentClientDocumentGroupId' THEN i.ParentClientDocumentGroupId
			WHEN 'CssClass' THEN i.CssClass
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentGroupId = d.ClientDocumentGroupId
			CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentGroupId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroup_I]...';


GO
-- Insert trigger for ClientDocumentGroup
CREATE TRIGGER T_ClientDocumentGroup_I
ON dbo.ClientDocumentGroup
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
		CONVERT(NVARCHAR(MAX), i.ParentClientDocumentGroupId) AS ParentClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.CssClass) AS CssClass
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
		WHEN 'ParentClientDocumentGroupId' THEN i.ParentClientDocumentGroupId
		WHEN 'CssClass' THEN i.CssClass
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'),  ('Description'), ('ParentClientDocumentGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroupClientDocument_U]...';


GO
-- Update trigger for ClientDocumentGroupClientDocument
CREATE TRIGGER T_ClientDocumentGroupClientDocument_U
ON dbo.ClientDocumentGroupClientDocument
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentGroupClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId,i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,d.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentGroupId = d.ClientDocumentGroupId and i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentGroupId = c.[Key] AND i.ClientDocumentId = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroupClientDocument_I]...';


GO
-- Insert trigger for ClientDocumentGroupClientDocument
CREATE TRIGGER T_ClientDocumentGroupClientDocument_I
ON dbo.ClientDocumentGroupClientDocument
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroupClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentGroupId,i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentGroupId,i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentGroupId = c.[Key] AND i.ClientDocumentId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentGroupClientDocument_D]...';


GO
-- Delete trigger for ClientDocumentGroupClientDocument
CREATE TRIGGER T_ClientDocumentGroupClientDocument_D
ON dbo.ClientDocumentGroupClientDocument
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentGroupClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.ClientDocumentGroupId,d.ClientDocumentId, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentGroupId,d.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentGroupId = c.[Key] AND d.ClientDocumentId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentType_U]...';


GO


-- Update trigger for ClientDocumentType
CREATE TRIGGER [dbo].[T_ClientDocumentType_U]
ON [dbo].[ClientDocumentType]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientDocumentType';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentTypeId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentTypeId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientDocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientDocumentTypeId = d.ClientDocumentTypeId
			CROSS JOIN (VALUES('Name'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientDocumentTypeId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentType_D]...';


GO

-- Delete trigger for ClientDocumentType
CREATE TRIGGER T_ClientDocumentType_D
ON dbo.ClientDocumentType
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentType';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientDocumentTypeId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientDocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
		WHEN 'Description' THEN d.[Description]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientDocumentTypeId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientDocumentType_I]...';


GO
-- Insert trigger for ClientDocumentType
CREATE TRIGGER T_ClientDocumentType_I
ON dbo.ClientDocumentType
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientDocumentType';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientDocumentTypeId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientDocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientDocumentTypeId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientSettings_U]...';


GO
-- Update trigger for ClientSettings
CREATE TRIGGER [dbo].[T_ClientSettings_U]
ON [dbo].[ClientSettings]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
		  CONVERT(NVARCHAR(MAX), i.DefaultSiteId) AS DefaultSiteId
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ClientId,
     		CONVERT(NVARCHAR(MAX), d.DefaultSiteId) AS DefaultSiteId		
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DefaultSiteId' THEN d.DefaultSiteId		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DefaultSiteId' THEN i.DefaultSiteId		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ClientId = d.ClientId
			CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ClientId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientSettings_D]...';


GO


-- Delete trigger for ClientSettings
CREATE TRIGGER [dbo].[T_ClientSettings_D]
ON [dbo].[ClientSettings]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ClientId, 'D'
	FROM	deleted d;

	
	
	
	WITH deletedConverted AS
	(
		SELECT	d.ClientId,
		CONVERT(NVARCHAR(MAX), d.DefaultSiteId) AS DefaultSiteId		
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DefaultSiteId' THEN d.DefaultSiteId	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ClientSettings_I]...';


GO
-- Insert trigger for ClientSettings
CREATE TRIGGER T_ClientSettings_I
ON dbo.ClientSettings
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ClientSettings';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ClientId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ClientId,
		CONVERT(NVARCHAR(MAX), i.DefaultSiteId) AS DefaultSiteId
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DefaultSiteId' THEN i.DefaultSiteId
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('DefaultSiteId')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ClientId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeAssociation_D]...';


GO
-- Delete trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_D
ON dbo.DocumentTypeAssociation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.DocumentTypeAssociationId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.DocumentTypeAssociationId,
			CONVERT(NVARCHAR(MAX), d.DocumentTypeId) AS DocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order],
			CONVERT(NVARCHAR(MAX), d.HeaderText) as HeaderText,
			CONVERT(NVARCHAR(MAX), d.LinkText) as LinkText,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass				
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'DocumentTypeId' THEN d.DocumentTypeId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'Order' THEN d.[Order]
			WHEN 'HeaderText' THEN d.HeaderText		
			WHEN 'LinkText' THEN d.LinkText		
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride		
			WHEN 'MarketId' THEN d.MarketId		
			WHEN 'CssClass' THEN d.CssClass								
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.DocumentTypeAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeAssociation_U]...';


GO
-- Update trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_U
ON dbo.DocumentTypeAssociation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.DocumentTypeAssociationId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.DocumentTypeAssociationId,
			CONVERT(NVARCHAR(MAX), i.DocumentTypeId) AS DocumentTypeId,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), i.[Order]) as [Order],
			CONVERT(NVARCHAR(MAX), i.HeaderText) as HeaderText,
			CONVERT(NVARCHAR(MAX), i.LinkText) as LinkText,
			CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass				
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.DocumentTypeAssociationId,
			CONVERT(NVARCHAR(MAX), d.DocumentTypeId) AS DocumentTypeId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order],
			CONVERT(NVARCHAR(MAX), d.HeaderText) as HeaderText,
			CONVERT(NVARCHAR(MAX), d.LinkText) as LinkText,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass				
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DocumentTypeId' THEN d.DocumentTypeId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'Order' THEN d.[Order]
			WHEN 'HeaderText' THEN d.HeaderText		
			WHEN 'LinkText' THEN d.LinkText		
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride	
			WHEN 'MarketId' THEN d.MarketId			
			WHEN 'CssClass' THEN d.CssClass								
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DocumentTypeId' THEN i.DocumentTypeId
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
			WHEN 'Order' THEN i.[Order]
			WHEN 'HeaderText' THEN i.HeaderText		
			WHEN 'LinkText' THEN i.LinkText		
			WHEN 'DescriptionOverride' THEN i.DescriptionOverride	
			WHEN 'MarketId' THEN i.MarketId			
			WHEN 'CssClass' THEN i.CssClass								
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.DocumentTypeAssociationId = d.DocumentTypeAssociationId
					CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.DocumentTypeAssociationId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeAssociation_I]...';


GO
-- Insert trigger for DocumentTypeAssociation
CREATE TRIGGER T_DocumentTypeAssociation_I
ON dbo.DocumentTypeAssociation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.DocumentTypeAssociationId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.DocumentTypeAssociationId,
		CONVERT(NVARCHAR(MAX), i.DocumentTypeId) AS DocumentTypeId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId) AS TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.[Order]) as [Order],
		CONVERT(NVARCHAR(MAX), i.HeaderText) as HeaderText,
		CONVERT(NVARCHAR(MAX), i.LinkText) as LinkText,
		CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
		CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
		CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass				
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DocumentTypeId' THEN i.DocumentTypeId
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
		WHEN 'Order' THEN i.[Order]
		WHEN 'HeaderText' THEN i.HeaderText		
		WHEN 'LinkText' THEN i.LinkText		
		WHEN 'DescriptionOverride' THEN i.DescriptionOverride		
		WHEN 'MarketId' THEN i.MarketId		
		WHEN 'CssClass' THEN i.CssClass								
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('DocumentTypeId'), ('SiteId'), ('TaxonomyAssociationId'), 
					('Order'), ('HeaderText'),('LinkText'),('DescriptionOverride'),('MarketId'),('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.DocumentTypeAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeExternalId_U]...';


GO
-- Update trigger for DocumentTypeExternalId
CREATE TRIGGER T_DocumentTypeExternalId_U
ON dbo.DocumentTypeExternalId
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'DocumentTypeExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName,i.DocumentTypeId, 
				i.ExternalId , 'U', i.ModifiedBy
	FROM	inserted i;
			
    WITH insertedConverted AS
	(
		SELECT	i.DocumentTypeId,i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.DocumentTypeId,d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	),

	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN d.IsPrimary			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN i.IsPrimary			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.DocumentTypeId = d.DocumentTypeId  AND i.ExternalId = d.ExternalId
			CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.DocumentTypeId = c.[Key] AND i.ExternalId = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeExternalId_I]...';


GO
-- Insert trigger for DocumentTypeExternalId
CREATE TRIGGER T_DocumentTypeExternalId_I
ON dbo.DocumentTypeExternalId
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.DocumentTypeId, i.ExternalId , 'I', i.ModifiedBy
	FROM	inserted i;

	WITH insertedConverted AS
	(
		SELECT	
		i.DocumentTypeId ,
		i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN i.IsPrimary
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.DocumentTypeId  = c.[Key]
			AND i.ExternalId = c.[SecondKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_DocumentTypeExternalId_D]...';


GO
-- Delete trigger for DocumentTypeExternalId
CREATE TRIGGER [dbo].T_DocumentTypeExternalId_D
ON [dbo].DocumentTypeExternalId
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'DocumentTypeExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.DocumentTypeId,
				d.ExternalId, 'D'
	FROM	deleted d;

	WITH deletedConverted AS
	(
		SELECT	
		d.DocumentTypeId ,
		d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN d.IsPrimary
	END AS NewValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.DocumentTypeId  = c.[Key]
			AND d.ExternalId = c.[SecondKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Footnote_D]...';


GO
-- Delete trigger for Footnote
CREATE TRIGGER T_Footnote_D
ON dbo.Footnote
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.FootnoteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.FootnoteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId ) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.[Text]) as [Text],
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN d.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'Text' THEN d.[Text]
			WHEN 'Order' THEN d.[Order]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
						 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.FootnoteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Footnote_U]...';


GO
-- Update trigger for Footnote
CREATE TRIGGER T_Footnote_U
ON dbo.Footnote
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.FootnoteId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.FootnoteId,
			CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId ) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), i.[Text]) as [Text],
			CONVERT(NVARCHAR(MAX), i.[Order]) as [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.FootnoteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId ) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.[Text]) as [Text],
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN d.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'Text' THEN d.[Text]
			WHEN 'Order' THEN d.[Order]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN i.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN i.LanguageCulture
			WHEN 'Text' THEN i.[Text]
			WHEN 'Order' THEN i.[Order]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.FootnoteId = d.FootnoteId
					CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
						 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.FootnoteId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Footnote_I]...';


GO
-- Insert trigger for Footnote
CREATE TRIGGER T_Footnote_I
ON dbo.Footnote
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.FootnoteId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.FootnoteId,
		CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId ) AS TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
		CONVERT(NVARCHAR(MAX), i.[Text]) as [Text],
		CONVERT(NVARCHAR(MAX), i.[Order]) as [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
		WHEN 'TaxonomyAssociationGroupId' THEN i.TaxonomyAssociationGroupId
		WHEN 'LanguageCulture' THEN i.LanguageCulture
		WHEN 'Text' THEN i.[Text]
		WHEN 'Order' THEN i.[Order]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
				 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.FootnoteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageFeature_U]...';


GO
-- Update trigger for PageFeature
CREATE TRIGGER T_PageFeature_U
ON dbo.PageFeature
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'PageFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.SiteId,i.PageId,i.[Key], 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,i.PageId,i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteId,d.PageId,d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN d.FeatureMode			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN i.FeatureMode			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteId = d.SiteId and i.PageId = d.PageId AND i.[Key] = d.[Key]
			CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteId = c.[Key] AND i.PageId = c.SecondKey and i.[Key] = c.ThirdKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageFeature_D]...';


GO
-- Delete trigger for PageFeature
CREATE TRIGGER T_PageFeature_D
ON dbo.PageFeature
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey],[ThirdKey], CUDType, UserId)
	SELECT	@TableName, d.SiteId, d.PageId, d.[Key] , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteId,
			d.PageId,
			d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN d.FeatureMode	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteId = c.[Key]
			AND d.PageId = c.[SecondKey]
			AND d.[Key] = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageFeature_I]...';


GO
-- Insert trigger for PageFeature
CREATE TRIGGER T_PageFeature_I
ON dbo.PageFeature
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey],[ThirdKey], CUDType, UserId)
	SELECT	@TableName, i.SiteId, i.PageId, i.[Key] , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
		i.PageId,
		i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN i.FeatureMode
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteId = c.[Key]
			AND i.PageId = c.[SecondKey]
			AND i.[Key] = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageNavigation_D]...';


GO
-- Delete trigger for PageNavigation
CREATE TRIGGER T_PageNavigation_D
ON dbo.PageNavigation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, d.PageNavigationId , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.PageNavigationId ,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
	END AS OldValue			
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.PageNavigationId = c.[Key] 
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageNavigation_U]...';


GO
-- Update trigger for PageNavigation
CREATE TRIGGER T_PageNavigation_U
ON dbo.PageNavigation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'PageNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageNavigationId , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageNavigationId ,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.PageNavigationId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
		END
		AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'PageId' THEN i.PageId
			WHEN 'NavigationKey' THEN i.NavigationKey
			WHEN 'LanguageCulture' THEN i.LanguageCulture
			WHEN 'CurrentVersion' THEN i.CurrentVersion
		END
		AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.PageNavigationId = d.PageNavigationId
			CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.PageNavigationId = c.[Key] 
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageNavigation_I]...';


GO
-- Insert trigger for PageNavigation
CREATE TRIGGER T_PageNavigation_I
ON dbo.PageNavigation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageNavigationId , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageNavigationId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
		CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
		CONVERT(NVARCHAR(MAX), i.[CurrentVersion]) AS [CurrentVersion]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'PageId' THEN i.PageId
		WHEN 'NavigationKey' THEN i.NavigationKey
		WHEN 'LanguageCulture' THEN i.LanguageCulture
		WHEN 'CurrentVersion' THEN i.[CurrentVersion]
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.PageNavigationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageText_I]...';


GO
-- Insert trigger for PageText
CREATE TRIGGER T_PageText_I
ON dbo.PageText
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageTextId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageTextId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
		CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
		CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'PageId' THEN i.PageId
		WHEN 'ResourceKey' THEN i.ResourceKey
		WHEN 'CurrentVersion' THEN i.CurrentVersion
		WHEN 'LanguageCulture' THEN i.LanguageCulture
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'),('PageId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.PageTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageText_U]...';


GO
-- Update trigger for PageText
CREATE TRIGGER T_PageText_U
ON dbo.PageText
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'PageText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.PageTextId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.PageTextId,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.PageTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'ResourceKey' THEN d.ResourceKey
			WHEN 'CurrentVersion' THEN d.CurrentVersion
			WHEN 'LanguageCulture' THEN d.LanguageCulture
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'PageId' THEN i.PageId
			WHEN 'ResourceKey' THEN i.ResourceKey
			WHEN 'CurrentVersion' THEN i.CurrentVersion
			WHEN 'LanguageCulture' THEN i.LanguageCulture
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.PageTextId = d.PageTextId
			CROSS JOIN (VALUES('SiteId'),('PageId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.PageTextId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_PageText_D]...';


GO
-- Delete trigger for PageText
CREATE TRIGGER T_PageText_D
ON dbo.PageText
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'PageText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.PageTextId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.PageTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN d.SiteId
		WHEN 'PageId' THEN d.PageId
		WHEN 'ResourceKey' THEN d.ResourceKey
		WHEN 'CurrentVersion' THEN d.CurrentVersion
		WHEN 'LanguageCulture' THEN d.LanguageCulture
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'), ('PageId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.PageTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ReportContent_U]...';


GO
-- Update trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_U]
ON [dbo].[ReportContent]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ReportContentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ReportContentId,
			CONVERT(NVARCHAR(MAX), i.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), i.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), i.Name) as Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ReportContentId,
			CONVERT(NVARCHAR(MAX), d.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), d.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), d.Name) as Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN d.ReportScheduleId
			WHEN 'ReportRunDate' THEN d.ReportRunDate
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'IsPrivate' THEN d.IsPrivate
			WHEN 'ContentUri' THEN d.ContentUri
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN i.ReportScheduleId
			WHEN 'ReportRunDate' THEN i.ReportRunDate
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'MimeType' THEN i.MimeType
			WHEN 'IsPrivate' THEN i.IsPrivate
			WHEN 'ContentUri' THEN i.ContentUri
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ReportContentId = d.ReportContentId
			CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ReportContentId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ReportContent_I]...';


GO
-- Insert trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_I]
ON [dbo].[ReportContent]
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.ReportContentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ReportContentId,
			CONVERT(NVARCHAR(MAX), i.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), i.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), i.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), i.Name) as Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN i.ReportScheduleId
			WHEN 'ReportRunDate' THEN i.ReportRunDate
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'MimeType' THEN i.MimeType
			WHEN 'IsPrivate' THEN i.IsPrivate
			WHEN 'ContentUri' THEN i.ContentUri
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ReportContentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_ReportContent_D]...';


GO
-- Delete trigger for PageText
CREATE TRIGGER [dbo].[T_ReportContent_D]
ON [dbo].[ReportContent]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'ReportContent';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.ReportContentId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.ReportContentId,
			CONVERT(NVARCHAR(MAX), d.ReportScheduleId) AS ReportScheduleId,
			CONVERT(NVARCHAR(MAX), d.ReportRunDate) AS ReportRunDate,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.IsPrivate) as IsPrivate,
			CONVERT(NVARCHAR(MAX), d.ContentUri) as ContentUri,
			CONVERT(NVARCHAR(MAX), d.Name) as Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'ReportScheduleId' THEN d.ReportScheduleId
			WHEN 'ReportRunDate' THEN d.ReportRunDate
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'IsPrivate' THEN d.IsPrivate
			WHEN 'ContentUri' THEN d.ContentUri
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('ReportScheduleId'),('ReportRunDate'), ('FileName'), ('MimeType'), ('IsPrivate'), ('ContentUri'), ('Name'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ReportContentId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Site_U]...';


GO
-- Update trigger for Site
CREATE TRIGGER T_Site_U
ON dbo.Site
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.TemplateId) AS TemplateId,
			CONVERT(NVARCHAR(MAX), i.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), i.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[TemplateId]) AS [TemplateId],
			CONVERT(NVARCHAR(MAX), d.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), d.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'TemplateId' THEN d.TemplateId
			WHEN 'DefaultPageId' THEN d.DefaultPageId
			WHEN 'ParentSiteId' THEN d.ParentSiteId
			WHEN 'Description' THEN d.[Description]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'TemplateId' THEN i.TemplateId
			WHEN 'DefaultPageId' THEN i.DefaultPageId
			WHEN 'ParentSiteId' THEN i.ParentSiteId
			WHEN 'Description' THEN i.[Description]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteId = d.SiteId
			CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Site_I]...';


GO
-- Insert trigger for Site
CREATE TRIGGER T_Site_I
ON dbo.Site
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
		CONVERT(NVARCHAR(MAX), i.Name) AS Name,
		CONVERT(NVARCHAR(MAX), i.TemplateId) AS TemplateId,
		CONVERT(NVARCHAR(MAX), i.DefaultPageId) AS DefaultPageId,
		CONVERT(NVARCHAR(MAX), i.ParentSiteId) as ParentSiteID,
		CONVERT(NVARCHAR(MAX), i.[Description]) as [Description]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'TemplateId' THEN i.TemplateId
		WHEN 'DefaultPageId' THEN i.DefaultPageId
		WHEN 'ParentSiteId' THEN i.ParentSiteId
		WHEN 'Description' THEN i.[Description]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_Site_D]...';


GO

-- Delete trigger for Site
CREATE TRIGGER T_Site_D
ON dbo.Site
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Site';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.SiteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[TemplateId]) AS [TemplateId],
			CONVERT(NVARCHAR(MAX), d.DefaultPageId) AS DefaultPageId,
			CONVERT(NVARCHAR(MAX), d.ParentSiteId) as ParentSiteID,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN d.Name
		WHEN 'TemplateId' THEN d.TemplateId
		WHEN 'DefaultPageId' THEN d.DefaultPageId
		WHEN 'ParentSiteId' THEN d.ParentSiteId
		WHEN 'Description' THEN d.[Description]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('TemplateId'), ('DefaultPageId'), ('ParentSiteId'), ('Description')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteFeature_D]...';


GO
-- Delete trigger for SiteFeature
CREATE TRIGGER T_SiteFeature_D
ON dbo.SiteFeature
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey], CUDType, UserId)
	SELECT	@TableName, d.SiteId,d.[Key] , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteId,			
			d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN d.FeatureMode	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteId = c.[Key]			
			AND d.[Key] = c.[SecondKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteFeature_U]...';


GO
-- Update trigger for SiteFeature
CREATE TRIGGER T_SiteFeature_U
ON dbo.SiteFeature
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'SiteFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.SiteId,i.[Key], 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteId,d.[Key],
		CONVERT(NVARCHAR(MAX), d.FeatureMode) AS FeatureMode
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN d.FeatureMode			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'FeatureMode' THEN i.FeatureMode			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteId = d.SiteId AND i.[Key] = d.[Key]
			CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteId = c.[Key] AND  i.[Key] = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteFeature_I]...';


GO
-- Insert trigger for SiteFeature
CREATE TRIGGER T_SiteFeature_I
ON dbo.SiteFeature
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteFeature';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],[SecondKey], CUDType, UserId)
	SELECT	@TableName, i.SiteId,  i.[Key] , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteId,
		i.[Key],
		CONVERT(NVARCHAR(MAX), i.FeatureMode) AS FeatureMode
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FeatureMode' THEN i.FeatureMode
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('FeatureMode')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteId = c.[Key]
			AND i.[Key] = c.[SecondKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteNavigation_U]...';


GO
-- Update trigger for SiteNavigation
CREATE TRIGGER T_SiteNavigation_U
ON dbo.SiteNavigation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'SiteNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteNavigationId , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteNavigationId ,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteNavigationId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
		END
		AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'PageId' THEN i.PageId
			WHEN 'NavigationKey' THEN i.NavigationKey
			WHEN 'LanguageCulture' THEN i.LanguageCulture
			WHEN 'CurrentVersion' THEN i.CurrentVersion
		END
		AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteNavigationId = d.SiteNavigationId
			CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteNavigationId = c.[Key] 
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteNavigation_D]...';


GO
-- Delete trigger for SiteNavigation
CREATE TRIGGER T_SiteNavigation_D
ON dbo.SiteNavigation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, d.SiteNavigationId , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteNavigationId ,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.PageId) AS PageId,
			CONVERT(NVARCHAR(MAX), d.NavigationKey) AS NavigationKey,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'PageId' THEN d.PageId
			WHEN 'NavigationKey' THEN d.NavigationKey
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'CurrentVersion' THEN d.CurrentVersion
	END AS OldValue			
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteNavigationId = c.[Key] 
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteNavigation_I]...';


GO
-- Insert trigger for SiteNavigation
CREATE TRIGGER T_SiteNavigation_I
ON dbo.SiteNavigation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteNavigation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteNavigationId , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteNavigationId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.PageId) AS PageId,
		CONVERT(NVARCHAR(MAX), i.NavigationKey) AS NavigationKey,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
		CONVERT(NVARCHAR(MAX), i.[CurrentVersion]) AS [CurrentVersion]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'PageId' THEN i.PageId
		WHEN 'NavigationKey' THEN i.NavigationKey
		WHEN 'LanguageCulture' THEN i.LanguageCulture
		WHEN 'CurrentVersion' THEN i.[CurrentVersion]
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'),('PageId'),('NavigationKey'),('LanguageCulture'),('CurrentVersion')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteNavigationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteText_U]...';


GO
-- Update trigger for SiteText
CREATE TRIGGER T_SiteText_U
ON dbo.SiteText
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'SiteText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteTextId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteTextId,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.SiteTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'ResourceKey' THEN d.ResourceKey
			WHEN 'CurrentVersion' THEN d.CurrentVersion
			WHEN 'LanguageCulture' THEN d.LanguageCulture
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'ResourceKey' THEN i.ResourceKey
			WHEN 'CurrentVersion' THEN i.CurrentVersion
			WHEN 'LanguageCulture' THEN i.LanguageCulture
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.SiteTextId = d.SiteTextId
			CROSS JOIN (VALUES('SiteId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.SiteTextId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteText_D]...';


GO
-- Delete trigger for SiteText
CREATE TRIGGER T_SiteText_D
ON dbo.SiteText
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.SiteTextId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.SiteTextId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ResourceKey) AS ResourceKey,
			CONVERT(NVARCHAR(MAX), d.CurrentVersion) AS CurrentVersion,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) as LanguageCulture
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN d.SiteId
		WHEN 'ResourceKey' THEN d.ResourceKey
		WHEN 'CurrentVersion' THEN d.CurrentVersion
		WHEN 'LanguageCulture' THEN d.LanguageCulture
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('SiteId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.SiteTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_SiteText_I]...';


GO
-- Insert trigger for SiteText
CREATE TRIGGER T_SiteText_I
ON dbo.SiteText
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'SiteText';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.SiteTextId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.SiteTextId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.ResourceKey) AS ResourceKey,
		CONVERT(NVARCHAR(MAX), i.CurrentVersion) AS CurrentVersion,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) as LanguageCulture
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'ResourceKey' THEN i.ResourceKey
		WHEN 'CurrentVersion' THEN i.CurrentVersion
		WHEN 'LanguageCulture' THEN i.LanguageCulture
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('SiteId'), ('ResourceKey'), ('CurrentVersion'), ('LanguageCulture')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.SiteTextId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_StaticResource_D]...';


GO
-- Delete trigger for StaticResource
CREATE TRIGGER T_StaticResource_D
ON dbo.StaticResource
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, d.StaticResourceId , 'D', d.ModifiedBy
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.StaticResourceId ,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.Size) AS Size,
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'Size' THEN d.Size
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'Data' THEN d.Data
	END AS OldValue			
	FROM deletedConverted d
		CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.StaticResourceId = c.[Key] 
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_StaticResource_U]...';


GO
-- Update trigger for StaticResource
CREATE TRIGGER T_StaticResource_U
ON dbo.StaticResource
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.StaticResourceId , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.StaticResourceId ,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.Size) AS Size,
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.StaticResourceId,
			CONVERT(NVARCHAR(MAX), d.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), d.Size) AS Size,
			CONVERT(NVARCHAR(MAX), d.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), d.Data) AS Data
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'FileName' THEN d.[FileName]
			WHEN 'Size' THEN d.Size
			WHEN 'MimeType' THEN d.MimeType
			WHEN 'Data' THEN d.Data
		END
		AS OldValue,
		CASE cn.ColumnName
			WHEN 'FileName' THEN i.[FileName]
			WHEN 'Size' THEN i.Size
			WHEN 'MimeType' THEN i.MimeType
			WHEN 'Data' THEN i.Data
		END
		AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.StaticResourceId = d.StaticResourceId
					CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.StaticResourceId = c.[Key] 
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_StaticResource_I]...';


GO
-- Insert trigger for StaticResource
CREATE TRIGGER T_StaticResource_I
ON dbo.StaticResource
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'StaticResource';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.StaticResourceId , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.StaticResourceId,
			CONVERT(NVARCHAR(MAX), i.[FileName]) AS [FileName],
			CONVERT(NVARCHAR(MAX), i.Size) AS Size,
			CONVERT(NVARCHAR(MAX), i.MimeType) AS MimeType,
			CONVERT(NVARCHAR(MAX), i.Data) AS Data
	    FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'FileName' THEN i.[FileName]
		WHEN 'Size' THEN i.Size
		WHEN 'MimeType' THEN i.MimeType
		WHEN 'Data' THEN i.Data
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('FileName'),('Size'),('MimeType'),('Data')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.StaticResourceId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociation_D]...';


GO
-- Delete trigger for TaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociation_D
ON dbo.TaxonomyAssociation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.TaxonomyAssociationId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Level]) AS [Level],
			CONVERT(NVARCHAR(MAX), d.TaxonomyId) AS TaxonomyId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
			CONVERT(NVARCHAR(MAX), d.NameOverride) as NameOverride,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,			
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Level' THEN d.[Level]
			WHEN 'TaxonomyId' THEN d.TaxonomyId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId
			WHEN 'NameOverride' THEN d.NameOverride
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride	
			WHEN 'MarketId' THEN d.MarketId					
			WHEN 'CssClass' THEN d.CssClass			
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociation_U]...';


GO


-- Update trigger for TaxonomyAssociation
CREATE TRIGGER [dbo].[T_TaxonomyAssociation_U]
ON [dbo].[TaxonomyAssociation]
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), i.[Level]) AS [Level],
			CONVERT(NVARCHAR(MAX), i.TaxonomyId) AS TaxonomyId,
			CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
			CONVERT(NVARCHAR(MAX), i.NameOverride) as NameOverride,
			CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.[Level]) AS [Level],
			CONVERT(NVARCHAR(MAX), d.TaxonomyId) AS TaxonomyId,
			CONVERT(NVARCHAR(MAX), d.SiteId) AS SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
			CONVERT(NVARCHAR(MAX), d.NameOverride) as NameOverride,
			CONVERT(NVARCHAR(MAX), d.DescriptionOverride) as DescriptionOverride,
			CONVERT(NVARCHAR(MAX), d.MarketId) as MarketId,			
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Level' THEN d.[Level]
			WHEN 'TaxonomyId' THEN d.TaxonomyId
			WHEN 'SiteId' THEN d.SiteId
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId
			WHEN 'NameOverride' THEN d.NameOverride
			WHEN 'DescriptionOverride' THEN d.DescriptionOverride	
			WHEN 'MarketId' THEN d.MarketId		
			WHEN 'CssClass' THEN d.CssClass			
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Level' THEN i.[Level]
			WHEN 'TaxonomyId' THEN i.TaxonomyId
			WHEN 'SiteId' THEN i.SiteId
			WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId
			WHEN 'NameOverride' THEN i.NameOverride		
			WHEN 'DescriptionOverride' THEN i.DescriptionOverride		
			WHEN 'MarketId' THEN i.MarketId					
			WHEN 'CssClass' THEN i.CssClass						
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationId = d.TaxonomyAssociationId
			CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociation_I]...';


GO
-- Insert trigger for TaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociation_I
ON dbo.TaxonomyAssociation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.[Level]) AS [Level],
		CONVERT(NVARCHAR(MAX), i.TaxonomyId) AS TaxonomyId,
		CONVERT(NVARCHAR(MAX), i.SiteId) AS SiteId,
		CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationID,
		CONVERT(NVARCHAR(MAX), i.NameOverride) as NameOverride,
		CONVERT(NVARCHAR(MAX), i.DescriptionOverride) as DescriptionOverride,
		CONVERT(NVARCHAR(MAX), i.MarketId) as MarketId,
		CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Level' THEN i.[Level]
		WHEN 'TaxonomyId' THEN i.TaxonomyId
		WHEN 'SiteId' THEN i.SiteId
		WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId
		WHEN 'NameOverride' THEN i.NameOverride		
		WHEN 'DescriptionOverride' THEN i.DescriptionOverride		
		WHEN 'MarketId' THEN i.MarketId		
		WHEN 'CssClass' THEN i.CssClass						
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Level'), ('TaxonomyId'), ('SiteId'), ('ParentTaxonomyAssociationId'), ('NameOverride'), ('DescriptionOverride'), ('MarketId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocument_D]...';


GO
-- Delete trigger for TaxonomyAssociationClientDocument
CREATE TRIGGER T_TaxonomyAssociationClientDocument_D
ON dbo.TaxonomyAssociationClientDocument
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationId,d.ClientDocumentId, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,d.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationId = c.[Key] AND d.ClientDocumentId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocument_U]...';


GO
-- Update trigger for TaxonomyAssociationClientDocument
CREATE TRIGGER T_TaxonomyAssociationClientDocument_U
ON dbo.TaxonomyAssociationClientDocument
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId ,i.ClientDocumentId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId,i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,d.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationId = d.TaxonomyAssociationId and i.ClientDocumentId = d.ClientDocumentId
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationId = c.[Key] AND i.ClientDocumentId = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocument_I]...';


GO
-- Insert trigger for TaxonomyAssociationClientDocument
CREATE TRIGGER T_TaxonomyAssociationClientDocument_I
ON dbo.TaxonomyAssociationClientDocument
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationClientDocument';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId ,i.ClientDocumentId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId ,i.ClientDocumentId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationId = c.[Key] AND i.ClientDocumentId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocumentGroup_D]...';


GO
-- Delete trigger for TaxonomyAssociationClientDocumentGroup
CREATE TRIGGER T_TaxonomyAssociationClientDocumentGroup_D
ON dbo.TaxonomyAssociationClientDocumentGroup
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationId,d.ClientDocumentGroupId, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,d.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationId = c.[Key] AND d.ClientDocumentGroupId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocumentGroup_U]...';


GO
-- Update trigger for TaxonomyAssociationClientDocumentGroup
CREATE TRIGGER T_TaxonomyAssociationClientDocumentGroup_U
ON dbo.TaxonomyAssociationClientDocumentGroup
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId ,i.ClientDocumentGroupId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId,i.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationId,d.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationId = d.TaxonomyAssociationId and i.ClientDocumentGroupId = d.ClientDocumentGroupId
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationId = c.[Key] AND i.ClientDocumentGroupId = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationClientDocumentGroup_I]...';


GO
-- Insert trigger for TaxonomyAssociationClientDocumentGroup
CREATE TRIGGER T_TaxonomyAssociationClientDocumentGroup_I
ON dbo.TaxonomyAssociationClientDocumentGroup
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationClientDocumentGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationId ,i.ClientDocumentGroupId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationId ,i.ClientDocumentGroupId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationId = c.[Key] AND i.ClientDocumentGroupId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroup_D]...';


GO
-- Delete trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_D
ON dbo.TaxonomyAssociationGroup
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.TaxonomyAssociationGroupId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'SiteId' THEN d.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN d.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN d.CssClass		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroup_U]...';


GO
-- Update trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_U
ON dbo.TaxonomyAssociationGroup
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), i.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.Name) AS Name,
			CONVERT(NVARCHAR(MAX), d.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), d.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), d.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), d.CssClass) as CssClass
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Name' THEN d.Name
			WHEN 'Description' THEN d.[Description]		
			WHEN 'SiteId' THEN d.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN d.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN d.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN d.CssClass		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Name' THEN i.Name
			WHEN 'Description' THEN i.[Description]		
			WHEN 'SiteId' THEN i.SiteId		
			WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId								
			WHEN 'ParentTaxonomyAssociationGroupId' THEN i.ParentTaxonomyAssociationGroupId
			WHEN 'CssClass' THEN i.CssClass		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationGroupId = d.TaxonomyAssociationGroupId
			CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationGroupId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroup_I]...';


GO
-- Insert trigger for TaxonomyAssociationGroup
CREATE TRIGGER T_TaxonomyAssociationGroup_I
ON dbo.TaxonomyAssociationGroup
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroup';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), i.Name) AS Name,
			CONVERT(NVARCHAR(MAX), i.[Description]) as [Description],
			CONVERT(NVARCHAR(MAX), i.SiteId) as SiteId,
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationId) as ParentTaxonomyAssociationId,						
			CONVERT(NVARCHAR(MAX), i.ParentTaxonomyAssociationGroupId) as ParentTaxonomyAssociationGroupID,
			CONVERT(NVARCHAR(MAX), i.CssClass) as CssClass
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Name' THEN i.Name
		WHEN 'Description' THEN i.[Description]		
		WHEN 'SiteId' THEN i.SiteId		
		WHEN 'ParentTaxonomyAssociationId' THEN i.ParentTaxonomyAssociationId								
		WHEN 'ParentTaxonomyAssociationGroupId' THEN i.ParentTaxonomyAssociationGroupId
		WHEN 'CssClass' THEN i.CssClass		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Name'), ('Description'), ('SiteId'),('ParentTaxonomyAssociationId'), ('ParentTaxonomyAssociationGroupId'), ('CssClass')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationGroupId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroupTaxonomyAssociation_D]...';


GO
-- Delete trigger for TaxonomyAssociationGroupTaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociationGroupTaxonomyAssociation_D
ON dbo.TaxonomyAssociationGroupTaxonomyAssociation
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroupTaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationGroupId,d.TaxonomyAssociationId, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,d.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationGroupId = c.[Key] AND d.TaxonomyAssociationId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroupTaxonomyAssociation_U]...';


GO
-- Update trigger for TaxonomyAssociationGroupTaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociationGroupTaxonomyAssociation_U
ON dbo.TaxonomyAssociationGroupTaxonomyAssociation
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationGroupTaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId,i.TaxonomyAssociationId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationGroupId,i.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationGroupId,d.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationGroupId = d.TaxonomyAssociationGroupId and i.TaxonomyAssociationId = d.TaxonomyAssociationId
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationGroupId = c.[Key] AND i.TaxonomyAssociationId = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationGroupTaxonomyAssociation_I]...';


GO
-- Insert trigger for TaxonomyAssociationGroupTaxonomyAssociation
CREATE TRIGGER T_TaxonomyAssociationGroupTaxonomyAssociation_I
ON dbo.TaxonomyAssociationGroupTaxonomyAssociation
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationGroupTaxonomyAssociation';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationGroupId,i.TaxonomyAssociationId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationGroupId,i.TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationGroupId = c.[Key] AND i.TaxonomyAssociationId = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationHierachy_D]...';


GO
-- Delete trigger for TaxonomyAssociationHierachy
CREATE TRIGGER [dbo].[T_TaxonomyAssociationHierachy_D]
ON [dbo].[TaxonomyAssociationHierachy]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType)
	SELECT	@TableName, d.ParentTaxonomyAssociationId ,d.ChildTaxonomyAssociationId,
				d.RelationshipType, 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.ParentTaxonomyAssociationId ,d.ChildTaxonomyAssociationId,
				d.RelationshipType,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.ParentTaxonomyAssociationId = c.[Key] 
				AND d.ChildTaxonomyAssociationId = c.SecondKey
				AND d.RelationshipType = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationHierachy_U]...';


GO
-- Update trigger for TaxonomyAssociationHierachy
CREATE TRIGGER T_TaxonomyAssociationHierachy_U
ON dbo.TaxonomyAssociationHierachy
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.ParentTaxonomyAssociationId,i.ChildTaxonomyAssociationId, 
				i.RelationshipType , 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ParentTaxonomyAssociationId,i.ChildTaxonomyAssociationId, 
				i.RelationshipType,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.ParentTaxonomyAssociationId,d.ChildTaxonomyAssociationId, 
				d.RelationshipType,
		CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'Order' THEN d.[Order]			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Order' THEN i.[Order]			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.ParentTaxonomyAssociationId = d.ParentTaxonomyAssociationId
				 AND i.ChildTaxonomyAssociationId = d.ChildTaxonomyAssociationId
				 AND i.RelationshipType = d.RelationshipType
			CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.ParentTaxonomyAssociationId = c.[Key] 
					AND i.ChildTaxonomyAssociationId = c.SecondKey
					AND i.RelationshipType = c.ThirdKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationHierachy_I]...';


GO
-- Insert trigger for TaxonomyAssociationHierachy
CREATE TRIGGER T_TaxonomyAssociationHierachy_I
ON dbo.TaxonomyAssociationHierachy
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationHierachy';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.ParentTaxonomyAssociationId ,i.ChildTaxonomyAssociationId,
				i.RelationshipType , 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.ParentTaxonomyAssociationId ,i.ChildTaxonomyAssociationId,
				i.RelationshipType, 
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'Order' THEN i.[Order]	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.ParentTaxonomyAssociationId = c.[Key] AND i.ChildTaxonomyAssociationId = c.SecondKey
				AND i.RelationshipType = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationMetaData_U]...';


GO
-- Update trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_U
ON dbo.TaxonomyAssociationMetaData
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationID,i.[Key], 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationID,i.[Key],
			CONVERT(NVARCHAR(MAX), i.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), i.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), i.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), i.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), i.StringValue) AS StringValue
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationID,d.[Key],
			CONVERT(NVARCHAR(MAX), d.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), d.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), d.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), d.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), d.StringValue) AS StringValue
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DataType' THEN d.DataType	
			WHEN 'Order' THEN d.[Order]	
			WHEN 'IntegerValue' THEN d.IntegerValue	
			WHEN 'BooleanValue' THEN d.BooleanValue	
			WHEN 'DateTimeValue' THEN d.DateTimeValue	
			WHEN 'StringValue' THEN d.StringValue			
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'DataType' THEN i.DataType	
			WHEN 'Order' THEN i.[Order]	
			WHEN 'IntegerValue' THEN i.IntegerValue	
			WHEN 'BooleanValue' THEN i.BooleanValue	
			WHEN 'DateTimeValue' THEN i.DateTimeValue	
			WHEN 'StringValue' THEN i.StringValue			
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.TaxonomyAssociationID = d.TaxonomyAssociationID and i.[Key] = d.[Key]
			CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.TaxonomyAssociationID = c.[Key] AND i.[Key] = c.SecondKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationMetaData_I]...';


GO
-- Insert trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_I
ON dbo.TaxonomyAssociationMetaData
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType, UserId)
	SELECT	@TableName, i.TaxonomyAssociationID,i.[Key], 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.TaxonomyAssociationID,i.[Key],
		CONVERT(NVARCHAR(MAX), i.DataType) AS DataType,
		CONVERT(NVARCHAR(MAX), i.[Order]) AS [Order],
		CONVERT(NVARCHAR(MAX), i.IntegerValue) AS IntegerValue,
		CONVERT(NVARCHAR(MAX), i.BooleanValue) AS BooleanValue,
		CONVERT(NVARCHAR(MAX), i.DateTimeValue) AS DateTimeValue,
		CONVERT(NVARCHAR(MAX), i.StringValue) AS StringValue
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'DataType' THEN i.DataType	
		WHEN 'Order' THEN i.[Order]	
		WHEN 'IntegerValue' THEN i.IntegerValue	
		WHEN 'BooleanValue' THEN i.BooleanValue	
		WHEN 'DateTimeValue' THEN i.DateTimeValue	
		WHEN 'StringValue' THEN i.StringValue			
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.TaxonomyAssociationID = c.[Key] AND i.[Key] = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyAssociationMetaData_D]...';


GO
-- Delete trigger for TaxonomyAssociationMetaData
CREATE TRIGGER T_TaxonomyAssociationMetaData_D
ON dbo.TaxonomyAssociationMetaData
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyAssociationMetaData';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, CUDType)
	SELECT	@TableName, d.TaxonomyAssociationID,d.[Key], 'D'
	FROM	deleted d;

	
	WITH deletedConverted AS
	(
		SELECT	d.TaxonomyAssociationID,d.[Key],
			CONVERT(NVARCHAR(MAX), d.DataType) AS DataType,
			CONVERT(NVARCHAR(MAX), d.[Order]) AS [Order],
			CONVERT(NVARCHAR(MAX), d.IntegerValue) AS IntegerValue,
			CONVERT(NVARCHAR(MAX), d.BooleanValue) AS BooleanValue,
			CONVERT(NVARCHAR(MAX), d.DateTimeValue) AS DateTimeValue,
			CONVERT(NVARCHAR(MAX), d.StringValue) AS StringValue
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'DataType' THEN d.DataType	
			WHEN 'Order' THEN d.[Order]	
			WHEN 'IntegerValue' THEN d.IntegerValue	
			WHEN 'BooleanValue' THEN d.BooleanValue	
			WHEN 'DateTimeValue' THEN d.DateTimeValue	
			WHEN 'StringValue' THEN d.StringValue			
		END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('DataType'),('Order'),('IntegerValue'),('BooleanValue'),('DateTimeValue'),('StringValue')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.TaxonomyAssociationID = c.[Key] AND d.[Key] = c.SecondKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyLevelExternalId_U]...';


GO
-- Update trigger for TaxonomyLevelExternalId
CREATE TRIGGER T_TaxonomyLevelExternalId_U
ON dbo.TaxonomyLevelExternalId
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'TaxonomyLevelExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	
	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey, ThirdKey, CUDType, UserId)
	SELECT	@TableName, i.[Level],i.TaxonomyId, 
				i.ExternalId , 'U', i.ModifiedBy
	FROM	inserted i;

	WITH insertedConverted AS
	(
		SELECT	i.[Level],i.TaxonomyId, i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.[Level],d.TaxonomyId,d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	),

	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN d.IsPrimary			END AS OldValue,
		CASE cn.ColumnName
			WHEN 'IsPrimary' THEN i.IsPrimary			END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.[Level] = d.[Level] and i.TaxonomyId = d.TaxonomyId AND i.ExternalId = d.ExternalId
			CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.[Level] = c.[Key] AND i.TaxonomyId = c.SecondKey and i.ExternalId = c.ThirdKey
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyLevelExternalId_I]...';


GO
-- Insert trigger for TaxonomyLevelExternalId
CREATE TRIGGER T_TaxonomyLevelExternalId_I
ON dbo.TaxonomyLevelExternalId
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyLevelExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,Thirdkey, CUDType, UserId)
	SELECT	@TableName, i.[Level] ,i.TaxonomyId, i.ExternalId , 'I', i.ModifiedBy
	FROM	inserted i;

	WITH insertedConverted AS
	(
		SELECT	
		i.[Level] ,
		i.TaxonomyId,
		i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN i.IsPrimary
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.[Level]  = c.[Key]
			AND i.TaxonomyId = c.[SecondKey]
			AND i.ExternalId = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_TaxonomyLevelExternalId_D]...';


GO
-- Delete trigger for TaxonomyLevelExternalId
CREATE TRIGGER [dbo].T_TaxonomyLevelExternalId_D
ON [dbo].[TaxonomyLevelExternalId]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyLevelExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType)
	SELECT	@TableName, d.[Level] ,d.TaxonomyId,
				d.ExternalId, 'D'
	FROM	deleted d;

	WITH deletedConverted AS
	(
		SELECT	
		d.[Level] ,
		d.TaxonomyId,
		d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)

	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN d.IsPrimary
	END AS NewValue

	FROM deletedConverted d
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.[Level]  = c.[Key]
			AND d.TaxonomyId = c.SecondKey
			AND d.ExternalId = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;


	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UrlRewrite_U]...';


GO
-- Update trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_U
ON dbo.UrlRewrite
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UrlRewriteId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), i.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), i.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), i.PatternName) as PatternName
		FROM	inserted i
	),
	deletedConverted AS
	(
		SELECT	d.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), d.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), d.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), d.PatternName) as PatternName
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'MatchPattern' THEN d.MatchPattern
			WHEN 'RewriteFormat' THEN d.RewriteFormat
			WHEN 'PatternName' THEN d.PatternName	
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'MatchPattern' THEN i.MatchPattern
			WHEN 'RewriteFormat' THEN i.RewriteFormat
			WHEN 'PatternName' THEN i.PatternName	
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.UrlRewriteId = d.UrlRewriteId
			CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.UrlRewriteId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UrlRewrite_D]...';


GO
-- Delete trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_D
ON dbo.UrlRewrite
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.UrlRewriteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.UrlRewriteId,
			CONVERT(NVARCHAR(MAX), d.MatchPattern) AS MatchPattern,
			CONVERT(NVARCHAR(MAX), d.RewriteFormat) AS RewriteFormat,
			CONVERT(NVARCHAR(MAX), d.PatternName) as PatternName
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MatchPattern' THEN d.MatchPattern
		WHEN 'RewriteFormat' THEN d.RewriteFormat
		WHEN 'PatternName' THEN d.PatternName	
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.UrlRewriteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
PRINT N'Creating [dbo].[T_UrlRewrite_I]...';


GO
-- Insert trigger for UrlRewrite
CREATE TRIGGER T_UrlRewrite_I
ON dbo.UrlRewrite
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'UrlRewrite';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UrlRewriteId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UrlRewriteId,
		CONVERT(NVARCHAR(MAX), i.MatchPattern) AS MatchPattern,
		CONVERT(NVARCHAR(MAX), i.RewriteFormat) AS RewriteFormat,
		CONVERT(NVARCHAR(MAX), i.PatternName) as PatternName
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'MatchPattern' THEN i.MatchPattern
		WHEN 'RewriteFormat' THEN i.RewriteFormat
		WHEN 'PatternName' THEN i.PatternName	
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES ('MatchPattern'), ('RewriteFormat'), ('PatternName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.UrlRewriteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c95b6883-0372-4a72-ae75-b152e53b2b73')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c95b6883-0372-4a72-ae75-b152e53b2b73')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '8a8bbf6f-eb2e-4324-8aa7-45f3f86219b8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('8a8bbf6f-eb2e-4324-8aa7-45f3f86219b8')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[BadRequest] WITH CHECK CHECK CONSTRAINT [FK_BadRequest_SiteActivity];

ALTER TABLE [dbo].[ClientDocument] WITH CHECK CHECK CONSTRAINT [FK_ClientDocument_ClientDocumentType];

ALTER TABLE [dbo].[ClientDocumentGroup] WITH CHECK CHECK CONSTRAINT [FK_ClientDocumentGroup_ClientDocumentGroup];

ALTER TABLE [dbo].[ClientDocumentGroupClientDocument] WITH CHECK CHECK CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocument];

ALTER TABLE [dbo].[ClientDocumentGroupClientDocument] WITH CHECK CHECK CONSTRAINT [FK_ClientDocumentGroupClientDocument_ClientDocumentGroup];

ALTER TABLE [dbo].[ClientSettings] WITH CHECK CHECK CONSTRAINT [FK_ClientSettings_Site];

ALTER TABLE [dbo].[CUDHistoryData] WITH CHECK CHECK CONSTRAINT [FK_CUDHistoryData_CUDHistory];

ALTER TABLE [dbo].[DocumentTypeAssociation] WITH CHECK CHECK CONSTRAINT [FK_DocumentTypeAssociation_Site];

ALTER TABLE [dbo].[DocumentTypeAssociation] WITH CHECK CHECK CONSTRAINT [FK_DocumentTypeAssociation_TaxonomyAssociation];

ALTER TABLE [dbo].[ErrorLog] WITH CHECK CHECK CONSTRAINT [FK_ErrorLog_SiteActivity];

ALTER TABLE [dbo].[Footnote] WITH CHECK CHECK CONSTRAINT [FK_Footnote_TaxonomyAssociation];

ALTER TABLE [dbo].[Footnote] WITH CHECK CHECK CONSTRAINT [FK_Footnote_TaxonomyAssociationGroup];

ALTER TABLE [dbo].[PageFeature] WITH CHECK CHECK CONSTRAINT [FK_PageFeature_Site];

ALTER TABLE [dbo].[PageNavigation] WITH CHECK CHECK CONSTRAINT [FK_PageNavigation_Site];

ALTER TABLE [dbo].[PageNavigationVersion] WITH CHECK CHECK CONSTRAINT [FK_PageNavigationVersion_PageNavigation];

ALTER TABLE [dbo].[PageText] WITH CHECK CHECK CONSTRAINT [FK_PageText_Site];

ALTER TABLE [dbo].[PageTextVersion] WITH CHECK CHECK CONSTRAINT [FK_PageTextVersion_PageText];

ALTER TABLE [dbo].[RequestMaterialEmailProsDetail] WITH CHECK CHECK CONSTRAINT [FK_RequestMaterialEmailProsDetail_RequestMaterialEmailHistory];

ALTER TABLE [dbo].[RequestMaterialPrintProsDetail] WITH CHECK CHECK CONSTRAINT [FK_RequestMaterialPrintProsDetail_RequestMaterialPrintHistory];

ALTER TABLE [dbo].[Site] WITH CHECK CHECK CONSTRAINT [FK_Site_Site];

ALTER TABLE [dbo].[SiteActivity] WITH CHECK CHECK CONSTRAINT [FK_SiteActivity_Site];

ALTER TABLE [dbo].[SiteActivity] WITH CHECK CHECK CONSTRAINT [FK_SiteActivity_Uri];

ALTER TABLE [dbo].[SiteActivity] WITH CHECK CHECK CONSTRAINT [FK_SiteActivity_Uri1];

ALTER TABLE [dbo].[SiteActivity] WITH CHECK CHECK CONSTRAINT [FK_SiteActivity_Uri2];

ALTER TABLE [dbo].[SiteActivity] WITH CHECK CHECK CONSTRAINT [FK_SiteActivity_UserAgent];

ALTER TABLE [dbo].[SiteFeature] WITH CHECK CHECK CONSTRAINT [FK_SiteFeature_Site];

ALTER TABLE [dbo].[SiteNavigation] WITH CHECK CHECK CONSTRAINT [FK_SiteNavigation_Site];

ALTER TABLE [dbo].[SiteNavigationVersion] WITH CHECK CHECK CONSTRAINT [FK_SiteNavigationVersion_SiteNavigation];

ALTER TABLE [dbo].[SiteText] WITH CHECK CHECK CONSTRAINT [FK_SiteText_Site];

ALTER TABLE [dbo].[SiteTextVersion] WITH CHECK CHECK CONSTRAINT [FK_SiteTextVersion_SiteText];

ALTER TABLE [dbo].[SiteXmlImport] WITH CHECK CHECK CONSTRAINT [FK_SiteXmlImport_SiteXmlExport];

ALTER TABLE [dbo].[TaxonomyAssociation] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociation_Site];

ALTER TABLE [dbo].[TaxonomyAssociation] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociation_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationClientDocument] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationClientDocument_ClientDocument];

ALTER TABLE [dbo].[TaxonomyAssociationClientDocument] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationClientDocument_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_ClientDocumentGroup];

ALTER TABLE [dbo].[TaxonomyAssociationClientDocumentGroup] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationClientDocumentGroup_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationGroup_Site];

ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationGroup] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationGroup_TaxonomyAssociationGroup];

ALTER TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationGroupTaxonomyAssociation] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationGroupTaxonomyAssociation_TaxonomyAssociationGroup];

ALTER TABLE [dbo].[TaxonomyAssociationHierachy] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation];

ALTER TABLE [dbo].[TaxonomyAssociationHierachy] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationHierachy_TaxonomyAssociation1];

ALTER TABLE [dbo].[TaxonomyAssociationMetaData] WITH CHECK CHECK CONSTRAINT [FK_TaxonomyAssociationMetaData_TaxonomyAssociation];

ALTER TABLE [dbo].[VerticalXmlImport] WITH CHECK CHECK CONSTRAINT [FK_VerticalXmlImport_VerticalXmlExport];

ALTER TABLE [dbo].[ClientDocumentData] WITH CHECK CHECK CONSTRAINT [FK_ClientDocumentData_ClientDocument];

ALTER TABLE [dbo].[ReportContentData] WITH CHECK CHECK CONSTRAINT [FK_ReportContentData_ReportContent];

ALTER TABLE [dbo].[ReportSchedule] WITH CHECK CHECK CONSTRAINT [CK_ReportSchedule_FrequencyType_FrequencyInterval];


GO
PRINT N'Update complete.';


GO
