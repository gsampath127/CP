CREATE TABLE [dbo].[XBRLCompanies] (
    [XBRLCompanyID]      INT           NOT NULL,
    [CompanyID]          INT           NOT NULL,
    [Identifier]         VARCHAR (20)  NOT NULL,
    [SiteURL]            VARCHAR (200) NOT NULL,
    [EntryDate]          DATETIME      NOT NULL,
    [EmailBilling]       VARCHAR (50)  NULL,
    [EmailNotifications] VARCHAR (50)  NULL,
    [Price]              MONEY         NULL,
    [Miscellaneous]      NCHAR (50)    NULL
);

