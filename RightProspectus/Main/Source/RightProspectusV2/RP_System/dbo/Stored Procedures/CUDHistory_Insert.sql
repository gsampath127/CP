-- Stored procedure for CUDHistory and Data inserts. Called from Triggers.
CREATE PROCEDURE [dbo].CUDHistory_Insert
(
	@CUDHistory dbo.TT_CUDHistory READONLY,
	@CUDHistoryData dbo.TT_CUDHistoryData READONLY
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdMapTable TABLE
	(
	CUDHistoryId INT NOT NULL PRIMARY KEY CLUSTERED,
	TT_Id INT NULL,
	TableName NVARCHAR(128) NOT NULL,
	[Key] INT NOT NULL,
	SecondKey NVARCHAR(200) NULL,
	ThirdKey NVARCHAR(200) NULL
	);
	
	DECLARE @BatchId UNIQUEIDENTIFIER;
	
	SET @BatchId = NEWID();
	
	INSERT	INTO CUDHistory (TableName, [Key], SecondKey, ThirdKey, CUDType, BatchId, UserId)
	OUTPUT	inserted.CUDHistoryId, inserted.TableName, inserted.[Key], inserted.SecondKey, inserted.ThirdKey
	INTO	@IdMapTable (CUDHistoryId, TableName, [Key], SecondKey, ThirdKey)
	SELECT	c.TableName, c.[Key], c.SecondKey, c.ThirdKey, c.CUDType, @BatchId, c.UserId
	FROM	@CUDHistory c;
	
	UPDATE	t1
	SET	t1.TT_Id = c.Id
	FROM	@IdMapTable t1
	INNER JOIN	@CUDHistory c
	ON	t1.TableName = c.TableName
	AND	t1.[Key] = c.[Key]
	AND (t1.SecondKey = c.SecondKey OR (t1.SecondKey IS NULL AND c.SecondKey IS NULL))
	AND (t1.ThirdKey = c.ThirdKey OR (t1.ThirdKey IS NULL AND c.ThirdKey IS NULL));
	
	INSERT INTO CUDHistoryData (CUDHistoryId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	t1.CUDHistoryId, cd.ColumnName, cd.SqlDbType, cd.OldValue, cd.NewValue
	FROM	@CUDHistoryData cd
	INNER JOIN @IdMapTable t1
	ON	cd.ParentId = t1.TT_Id;
END;