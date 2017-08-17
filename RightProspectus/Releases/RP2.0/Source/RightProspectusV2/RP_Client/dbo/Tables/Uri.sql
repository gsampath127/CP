CREATE TABLE [dbo].[Uri] (
    [UriId]     INT             IDENTITY (1, 1) NOT NULL,
    [UriString] NVARCHAR (2083) NOT NULL,
    [UriHash]   AS              (hashbytes('MD5',[UriString])) PERSISTED,
    [UriLength] AS              (len([UriString])) PERSISTED,
    CONSTRAINT [PK_Uri] PRIMARY KEY CLUSTERED ([UriId] ASC)
);

