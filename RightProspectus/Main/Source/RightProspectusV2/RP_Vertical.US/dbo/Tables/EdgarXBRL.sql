CREATE TABLE [dbo].[EdgarXBRL] (
    [EdgarID]           INT           NULL,
    [Acc#]              VARCHAR (250) NULL,
    [ZIPFileName]       VARCHAR (250) NULL,
    [Path]              VARCHAR (250) NULL,
    [CompanyID]         INT           NULL,
    [EdgarXBRLId]       INT           NOT NULL,
    [CreatedDate]       DATETIME      NULL,
    [FilingDate]        DATETIME      NULL,
    [miscellaneous]     NVARCHAR (50) NULL,
    [FormType]          VARCHAR (50)  NULL,
    [IsClientNotified]  BIT           NULL,
    [Customised]        VARCHAR (255) NULL,
    [IsAmmended]        BIT           NULL,
    [IsXBRLViewerReady] BIT           NULL
);

