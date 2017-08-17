CREATE TABLE [dbo].[HostedCompanies] (
    [CompanyID]   INT            NOT NULL,
    [CompanyName] NVARCHAR (255) NOT NULL,
    [IsHosted]    BIT            NOT NULL,
    [IsXBRL]      BIT            NOT NULL,
    [DateAdded]   DATETIME       NULL
);

