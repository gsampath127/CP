CREATE TABLE [dbo].[BadRequest]
(
    [SiteActivityId]   INT           NOT NULL,
	[RequestIssue]	   INT			 NOT NULL,
    [ParameterName]    nvarchar(200) NULL,
    [ParameterValue]   nvarchar(max) NULL,    
    CONSTRAINT [PK_BadRequest] PRIMARY KEY CLUSTERED (SiteActivityId ASC),
    CONSTRAINT [FK_BadRequest_SiteActivity] FOREIGN KEY ([SiteActivityId]) REFERENCES [dbo].[SiteActivity] ([SiteActivityId])
);