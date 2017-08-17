-- User Defined Table Type for Taxonomy inserts.
CREATE TYPE dbo.TT_Taxonomy
AS TABLE
(	
    TaxonomyId INT NOT NULL,
    [Level] INT NOT NULL,
    IsNameProvided BIT NOT NULL	
);