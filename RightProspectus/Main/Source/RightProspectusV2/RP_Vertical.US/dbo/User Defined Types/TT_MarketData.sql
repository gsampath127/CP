CREATE TYPE dbo.[TT_MarketData]
AS TABLE
(	
    [MarketId] [nvarchar](50) NULL,
    [Level] INT NOT NULL,
    [IsNameOverrideProvided] BIT NOT NULL	
);
