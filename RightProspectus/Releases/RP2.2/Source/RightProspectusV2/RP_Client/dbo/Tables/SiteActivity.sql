﻿CREATE TABLE [dbo].[SiteActivity] (
    [SiteActivityId]             INT          IDENTITY (1, 1) NOT NULL,
    [SiteId]                     INT          NOT NULL,
    [ClientIPAddress]            VARCHAR (15) NOT NULL,
    [UserAgentId]                INT          NOT NULL,
    [RequestUtcDate]             DATETIME     NOT NULL,
    [HttpMethod]                 VARCHAR (20) NOT NULL,
    [RequestUri]                 INT          NOT NULL,
    [ParsedRequestUri]           INT          NOT NULL,
    [ServerName]                 VARCHAR (50) NOT NULL,
    [ReferrerUri]                INT          NULL,
	[InitDoc]					 bit          NOT NULL default(0),
	[RequestBatchId]			 UNIQUEIDENTIFIER     NULL,
    [UserId]                     INT          NULL,
    [PageId]                     INT          NULL,
    [TaxonomyAssociationGroupId] INT          NULL,
    [TaxonomyAssociationId]      INT          NULL,
    [DocumentTypeId]             INT          NULL,
    [ClientDocumentGroupId]      INT          NULL,
    [ClientDocumentId]           INT          NULL,
	[XBRLDocumentName]			 VARCHAR (100) NULL,
	[XBRLItemType]				 INT		  NULL,
    CONSTRAINT [PK_SiteActivity] PRIMARY KEY CLUSTERED ([SiteActivityId] ASC),
    CONSTRAINT [FK_SiteActivity_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([SiteId]),
    CONSTRAINT [FK_SiteActivity_Uri] FOREIGN KEY ([RequestUri]) REFERENCES [dbo].[Uri] ([UriId]),
    CONSTRAINT [FK_SiteActivity_Uri1] FOREIGN KEY ([ParsedRequestUri]) REFERENCES [dbo].[Uri] ([UriId]),
    CONSTRAINT [FK_SiteActivity_Uri2] FOREIGN KEY ([ReferrerUri]) REFERENCES [dbo].[Uri] ([UriId]),
    CONSTRAINT [FK_SiteActivity_UserAgent] FOREIGN KEY ([UserAgentId]) REFERENCES [dbo].[UserAgent] ([UserAgentId])
);
