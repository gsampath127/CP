CREATE TABLE [dbo].[ProsDocsDeleted] (
    [ProsDocID]        INT      NOT NULL,
    [ReasonID]         INT      NOT NULL,
    [UserID]           INT      NULL,
    [DateDeleted]      DATETIME NULL,
    [ReplaceProsDocID] INT      NULL
);

