CREATE TABLE [dbo].[UserAgent] (
    [UserAgentId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserAgentString] NVARCHAR (MAX) NOT NULL,
    [UserAgentHash]   AS             (hashbytes('MD5',[UserAgentString])) PERSISTED,
    [UserAgentLength] AS             (len([UserAgentString])) PERSISTED,
    CONSTRAINT [PK_UserAgent] PRIMARY KEY CLUSTERED ([UserAgentId] ASC)
);

