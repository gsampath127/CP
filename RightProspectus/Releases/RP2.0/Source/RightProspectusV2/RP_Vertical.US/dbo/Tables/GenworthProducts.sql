CREATE TABLE [dbo].[GenworthProducts] (
    [ProductID]      INT           NOT NULL,
    [ProductCode]    VARCHAR (20)  NULL,
    [ProductName]    VARCHAR (150) NULL,
    [GreatWested]    BIT           NULL,
    [ProductType]    CHAR (1)      NULL,
    [ProductStatus]  CHAR (1)      NULL,
    [DisplayOnline]  BIT           NULL,
    [NewYorkProduct] BIT           NULL,
    [ProsID]         INT           NULL
);

