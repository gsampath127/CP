-- User Defined Table Type for ClientUsers inserts.
CREATE TYPE dbo.TT_ClientUsers
AS TABLE
(	
    ClientId INT NOT NULL,
	UserId INT NOT NULL
);