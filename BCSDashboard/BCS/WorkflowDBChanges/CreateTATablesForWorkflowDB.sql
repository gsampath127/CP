


CREATE TABLE [dbo].[tblTransamerica](
	[CUSIP] [varchar](50) NULL,
	[CIK] [varchar](50) NULL,
	[SeriesID] [varchar](50) NULL,
	[ClassContractID] [varchar](50) NULL,
	[TickerSymbol] [varchar](10) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[tblAllianceBernstein](
	[CUSIP] [varchar](50) NULL,
	[CIK] [varchar](50) NULL,
	[SeriesID] [varchar](50) NULL,
	[ClassContractID] [varchar](50) NULL,
	[TickerSymbol] [varchar](10) NULL
) ON [PRIMARY]

GO

Alter Table tbledgar
 add isTransamerica bit default(0) not null

Alter Table tbledgar
 add isAllianceBernstein bit default(0) not null

