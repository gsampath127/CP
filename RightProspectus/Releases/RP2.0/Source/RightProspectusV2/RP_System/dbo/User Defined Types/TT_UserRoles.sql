-- User Defined Table Type for UserRoles inserts.
CREATE TYPE dbo.TT_UserRoles
AS TABLE
(	
    UserId INT NOT NULL,
    RoleID INT NOT NULL	
);