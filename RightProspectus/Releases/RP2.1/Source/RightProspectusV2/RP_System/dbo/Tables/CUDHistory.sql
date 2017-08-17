CREATE TABLE [dbo].[CUDHistory] (
    [CUDHistoryId] INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TableName]    NVARCHAR (128) NOT NULL,
    [Key]          INT            NOT NULL,
    [SecondKey]    NVARCHAR (200) NULL,
    [ThirdKey]     NVARCHAR (200) NULL,
    [CUDType]      CHAR (1)       NULL,
    [UtcCUDDate]   DATETIME       NOT NULL CONSTRAINT [DF_CUDHistory_UtcCUDDate] DEFAULT (GETUTCDATE()),
    [BatchId]      UNIQUEIDENTIFIER            NOT NULL,
    [UserId]       INT            NULL,
    CONSTRAINT [PK_CUDHistory] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC),
    
    
);

