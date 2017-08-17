CREATE TABLE [dbo].[DelawareLifeHistory] (
    [DelawareLifeHistoryID] INT          NOT NULL,
    [ID]                    INT          NULL,
    [CUSIP]                 VARCHAR (50) NULL,
    [CIK]                   VARCHAR (50) NULL,
    [SeriesID]              VARCHAR (50) NULL,
    [ClassContractID]       VARCHAR (50) NULL,
    [TickerSymbol]          VARCHAR (10) NULL,
    [DeletionDate]          DATETIME     NULL,
    [InsertionDate]         DATETIME     NULL,
    [UpdateDate]            DATETIME     NULL,
    [UpdatedField]          VARCHAR (50) NULL
);

