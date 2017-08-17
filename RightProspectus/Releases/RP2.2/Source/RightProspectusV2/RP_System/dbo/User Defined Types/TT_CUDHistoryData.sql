-- User Defined Table Type for CUDHistoryData inserts.
CREATE TYPE dbo.TT_CUDHistoryData
AS TABLE
(
	ParentId INT NOT NULL,
	ColumnName NVARCHAR(128),
	PRIMARY KEY CLUSTERED (ParentId, ColumnName),
	SqlDbType INT NOT NULL,
	OldValue NVARCHAR(MAX) NULL,
	NewValue NVARCHAR(MAX) NULL
);