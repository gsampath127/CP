CREATE TABLE [dbo].[ProsDocsHistory] (
    [ProsDocIdHistory]    INT           NOT NULL,
    [ProsDocId]           INT           NOT NULL,
    [ProsId]              INT           NULL,
    [ProsDocTypeId]       VARCHAR (3)   NULL,
    [ProsDocOrder]        INT           NULL,
    [ProsDocURL]          VARCHAR (500) NULL,
    [ProsDocAltURL]       VARCHAR (500) NULL,
    [ProsDocUseAltURL]    BIT           NULL,
    [ProsDocLevel]        INT           NULL,
    [DeletionDate]        DATETIME      NULL,
    [UpdateDate]          DATETIME      NULL,
    [UpdatedField]        VARCHAR (50)  NULL,
    [ProsDocBackUpURL]    VARCHAR (500) NULL,
    [isBackUpURLApproved] TINYINT       NULL
);

