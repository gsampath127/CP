CREATE TABLE [dbo].[RequestMaterialEmailProsDetail](
	[RequestMaterialEmailProsDetailId] [int] IDENTITY(1,1) NOT NULL,
	[RequestMaterialEmailHistoryId] [int] NOT NULL,	
	[TaxonomyAssociationId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[SClickDateUtc] [datetime] NULL,
	CONSTRAINT [FK_RequestMaterialEmailProsDetail_RequestMaterialEmailHistory] FOREIGN KEY ([RequestMaterialEmailHistoryId]) REFERENCES [dbo].[RequestMaterialEmailHistory] ([RequestMaterialEmailHistoryId]),	
 CONSTRAINT [PK_RequestMaterialEmailProsDetail] PRIMARY KEY CLUSTERED 
(
	[RequestMaterialEmailProsDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]