CREATE TABLE [dbo].[RequestMaterialSKUDetail]
(
	[RequestMaterialSKUDetailId] INT IDENTITY (1, 1) NOT NULL, 
    [TaxonomyAssociationId] INT NOT NULL, 
    [DocumentTypeId] INT NOT NULL, 
    [SKUName] NVARCHAR(200) NULL	
	CONSTRAINT [PK_RequestMaterialSKUDetail] PRIMARY KEY CLUSTERED 
	(
		[RequestMaterialSKUDetailId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]