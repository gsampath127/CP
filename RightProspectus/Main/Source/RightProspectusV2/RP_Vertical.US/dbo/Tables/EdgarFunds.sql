CREATE TABLE [dbo].[EdgarFunds] (
    [EdgarFundsID]     INT      NOT NULL,
    [EdgarID]          INT      NULL,
    [FundID]           INT      NULL,
    [Processed]        CHAR (1) NULL,
    [DateUpdated]      DATETIME NULL,
    [isURLAssigned]    BIT      NULL,
    [TickerID]         INT      NULL,
    [SLINKRequestID]   INT      NULL,
    [SLINKExtractFrom] INT      NULL,
    [SLINKExtractTo]   INT      NULL,
    [DateProcessed]    DATETIME NULL,
    [IsSECSweeped]     BIT      NULL
);

