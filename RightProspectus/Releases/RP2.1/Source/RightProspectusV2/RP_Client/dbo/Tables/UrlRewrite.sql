CREATE TABLE [dbo].[UrlRewrite] (
    [UrlRewriteId]    INT             IDENTITY (1, 1) NOT NULL,
    [MatchPattern]    NVARCHAR (2083) NOT NULL,
    [RewriteFormat]   NVARCHAR (2083) NOT NULL,
    [UtcModifiedDate] DATETIME        CONSTRAINT [DF_UrlRewrite_UtcModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      INT             NULL,
    [PatternName] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_UrlRewrite] PRIMARY KEY CLUSTERED ([UrlRewriteId] ASC)
);

