CREATE TABLE [dbo].[ProsTickerHistory] (
    [TickerIDHistory]  INT           NOT NULL,
    [TickerID]         INT           NOT NULL,
    [ProspectusID]     INT           NULL,
    [TickerSymbol]     NVARCHAR (10) NULL,
    [Class]            VARCHAR (100) NULL,
    [FileNumber]       VARCHAR (50)  NULL,
    [CIK]              VARCHAR (50)  NULL,
    [SeriesID]         VARCHAR (50)  NULL,
    [ClassContractID]  VARCHAR (50)  NULL,
    [CUSIP]            VARCHAR (10)  NULL,
    [LIPPER]           VARCHAR (50)  NULL,
    [DeletionDate]     DATETIME      NULL,
    [UpdateDate]       DATETIME      NULL,
    [UpdatedField]     VARCHAR (50)  NULL,
    [DeletionReason]   VARCHAR (50)  NULL,
    [MergerToTickerID] INT           NULL
);

