﻿CREATE TABLE [dbo].[ProspectusHistory] (
    [ProsHistoryID]   INT            NOT NULL,
    [ProsID]          INT            NOT NULL,
    [ProsName]        NVARCHAR (255) NULL,
    [LevelID]         INT            NULL,
    [Company]         NVARCHAR (255) NULL,
    [URL]             NVARCHAR (255) NULL,
    [ProsDate]        DATETIME       NULL,
    [RevisedProsDate] DATETIME       NULL,
    [SAISURL]         NVARCHAR (255) NULL,
    [SuppURL]         NVARCHAR (255) NULL,
    [SAISURL2]        NVARCHAR (255) NULL,
    [SuppURL2]        NVARCHAR (255) NULL,
    [SAISURL3]        NVARCHAR (255) NULL,
    [SuppURL3]        NVARCHAR (255) NULL,
    [SAISURL4]        NVARCHAR (255) NULL,
    [SuppURL4]        NVARCHAR (255) NULL,
    [SAISURL5]        NVARCHAR (255) NULL,
    [SuppURL5]        NVARCHAR (255) NULL,
    [AltURL]          NVARCHAR (255) NULL,
    [UseAltURL]       CHAR (2)       NULL,
    [CompanyID]       INT            NULL,
    [Online]          CHAR (1)       NULL,
    [ARDate]          DATETIME       NULL,
    [RevisedARDate]   DATETIME       NULL,
    [SARDate]         DATETIME       NULL,
    [RevisedSARDate]  DATETIME       NULL,
    [FEY]             VARCHAR (5)    NULL,
    [PHDate]          DATETIME       NULL,
    [RevisedPHDate]   DATETIME       NULL,
    [DeletionDate]    DATETIME       NULL,
    [DeletionNote]    NVARCHAR (255) NULL,
    [DeletionReason]  NVARCHAR (50)  NULL,
    [UpdateDate]      DATETIME       NULL,
    [UpdatedField]    VARCHAR (50)   NULL,
    [FSDate]          DATETIME       NULL,
    [RevisedFSDate]   DATETIME       NULL
);
