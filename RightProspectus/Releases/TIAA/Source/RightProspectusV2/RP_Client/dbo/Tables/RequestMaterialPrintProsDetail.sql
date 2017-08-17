
CREATE TABLE [dbo].[RequestMaterialPrintProsDetail](
	[RequestMaterialPrintProsDetailId]  [int] IDENTITY(1,1) NOT NULL,
	[RequestMaterialPrintHistoryId] [int] NOT NULL,	
	[TaxonomyAssociationId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,	
	[Quantity] [int] CONSTRAINT [DF_RequestMaterialPrintProsDetail_Quantity] DEFAULT (1) NOT NULL,
	CONSTRAINT [FK_RequestMaterialPrintProsDetail_RequestMaterialPrintHistory] FOREIGN KEY ([RequestMaterialPrintHistoryId]) REFERENCES [dbo].[RequestMaterialPrintHistory] ([RequestMaterialPrintHistoryId]),	
 CONSTRAINT [PK_RequestMaterialPrintProsDetail] PRIMARY KEY CLUSTERED 
(
	[RequestMaterialPrintProsDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
