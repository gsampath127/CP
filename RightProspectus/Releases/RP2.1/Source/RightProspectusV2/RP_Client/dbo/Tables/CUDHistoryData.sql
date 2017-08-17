CREATE TABLE [dbo].[CUDHistoryData] (
    [CUDHistoryId] INT            NOT NULL,
    [ColumnName]   NVARCHAR (128) NOT NULL,
    [SqlDbType]    INT            NOT NULL,
    [OldValue]     NVARCHAR (MAX) NULL,
    [NewValue]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CUDHistoryData] PRIMARY KEY CLUSTERED ([CUDHistoryId] ASC, [ColumnName] ASC),
    CONSTRAINT [FK_CUDHistoryData_CUDHistory] FOREIGN KEY ([CUDHistoryId]) REFERENCES [dbo].[CUDHistory] ([CUDHistoryId])
);

