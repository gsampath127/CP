--drop table [DocumentMetaData]
CREATE TABLE [dbo].[DocumentMetaData](
	[DocumentId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL ,	
	[Key] [varchar] (20) not null unique,
	[DataType] [int] NOT NULL,
	[Order] [int],
	[IntegerValue] [int],
	[BooleanValue] [int],
	[DateTimeValue] [datetime] ,
	[StringValue] [nvarchar](max),	
	[UtcModifiedDate] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] [int] NOT NULL,
	CONSTRAINT PK_DocumentMetaData PRIMARY KEY ([DocumentId],[Key])
    --CONSTRAINT [FK_DocumentMetaData_ToTable] FOREIGN KEY (DocumentId) REFERENCES Document(DocumentId) 
)