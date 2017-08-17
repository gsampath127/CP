--drop table [ClientUsers]
CREATE TABLE [dbo].[ClientUsers](
	[ClientId] [int]  NOT NULL,	
	[UserId] [int]  NOT NULL,		
	[UtcModifiedDate] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] [int] NOT NULL,
	CONSTRAINT PK_ClientUsers PRIMARY KEY ([ClientId],[UserId]),
	CONSTRAINT fk1_ClientUsers FOREIGN KEY (ClientId) REFERENCES Clients(ClientId),
	CONSTRAINT fk2_ClientUsers FOREIGN KEY (UserId) REFERENCES Users(UserId),
	)ON [PRIMARY]

GO