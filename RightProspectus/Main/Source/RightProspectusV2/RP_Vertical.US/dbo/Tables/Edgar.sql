CREATE TABLE [dbo].[Edgar] (
    [EdgarID]       INT            NOT NULL,
    [Acc#]          VARCHAR (250)  NULL,
    [CIK#]          VARCHAR (10)   NULL,
    [FileName]      VARCHAR (250)  NULL,
    [FormType]      VARCHAR (50)   NULL,
    [DateFiled]     DATETIME       NULL,
    [DocumentType]  VARCHAR (250)  NULL,
    [EffectiveDate] DATETIME       NULL,
    [Notes]         TEXT           NULL,
    [DateUpdated]   DATETIME       NULL,
    [Company]       VARCHAR (75)   NULL,
    [CompanyId]     INT            NULL,
    [DocumentDate]  DATETIME       NULL,
    [UInfo]         VARCHAR (5000) NULL,
    [IsPDF]         TINYINT        NULL,
    [InitialPage]   INT            NULL,
    [IsReplaced]    BIT            NOT NULL
);

