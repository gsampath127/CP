CREATE TABLE [dbo].[GenworthSubAccounts] (
    [ProductID]             INT           NULL,
    [ProductCode]           VARCHAR (20)  NULL,
    [CUSIP]                 VARCHAR (50)  NULL,
    [FundName]              VARCHAR (255) NULL,
    [SubAccountID]          INT           NOT NULL,
    [CompanyName]           VARCHAR (255) NULL,
    [FundAndShareClassName] VARCHAR (255) NULL,
    [CompanyID]             INT           NULL
);

