-- Delete trigger for TaxonomyLevelExternalId
CREATE TRIGGER [dbo].T_TaxonomyLevelExternalId_D
ON [dbo].[TaxonomyLevelExternalId]
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyLevelExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,ThirdKey, CUDType)
	SELECT	@TableName, d.[Level] ,d.TaxonomyId,
				d.ExternalId, 'D'
	FROM	deleted d;

	WITH deletedConverted AS
	(
		SELECT	
		d.[Level] ,
		d.TaxonomyId,
		d.ExternalId,
		CONVERT(NVARCHAR(MAX), d.IsPrimary) AS IsPrimary
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)

	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN d.IsPrimary
	END AS NewValue

	FROM deletedConverted d
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.[Level]  = c.[Key]
			AND d.TaxonomyId = c.SecondKey
			AND d.ExternalId = c.ThirdKey
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;


	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;